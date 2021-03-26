using BarrowBooks.DataAccess.Abstract;
using BarrowBooks.Entites.Abstract;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BarrowBooks.DataAccess.Concrete.Dapper
{
    public class EntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly string _tableName;

        protected EntityRepositoryBase(string tableName)
        {
            _tableName = tableName;
        }

        private SqlConnection SqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
        }

        private IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            conn.Open();
            return conn;
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();

        public void DeleteRow(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Execute($"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id });
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<TEntity>($"SELECT * FROM {_tableName}");
            }
        }

        public TEntity Get(int id)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.QuerySingleOrDefault<TEntity>($"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

                return result;
            }
        }

        public IEnumerable<TEntity> Get(Dictionary<string, object> keyValuePairs, string logicalOperator)
        {
            using (var connection = CreateConnection())
            {
                var query = new StringBuilder($"SELECT * FROM {_tableName} ");
                if (keyValuePairs.Count > 0)
                    query.Append(" WHERE 1 = 1 ");
                foreach (var item in keyValuePairs)
                {
                    query.Append($" AND {item.Key} {logicalOperator} '%{item.Value}%'");
                }

                var result = connection.Query<TEntity>(query.ToString());
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with filter could not be found.");

                return result.ToList();
            }
        }

        public IEnumerable<TEntity> Get(string sqlQuery)
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<TEntity>(sqlQuery);
            }
        }

        public IEnumerable<TEntity> Get(string sqlQuery, Dictionary<string, object> parameters)
        {
            using (var connection = CreateConnection())
            {
                var dynamicParameters = new DynamicParameters(parameters);

                return connection.Query<TEntity>(sqlQuery, dynamicParameters);
            }
        }

        public void Insert(TEntity t)
        {
            var insertQuery = GenerateInsertQuery();

            using (var connection = CreateConnection())
            {
                connection.Execute(insertQuery, t);
            }
        }        

        public int SaveRange(IEnumerable<TEntity> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();
            using (var connection = CreateConnection())
            {
                inserted += connection.Execute(query, list);
            }

            return inserted;
        }

        public void Update(TEntity t)
        {
            var updateQuery = GenerateUpdateQuery();

            using (var connection = CreateConnection())
            {
                connection.Execute(updateQuery, t);
            }
        }


        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }

        private static Operator DetermineOperator(Expression binaryExpression)
        {
            switch (binaryExpression.NodeType)
            {
                case ExpressionType.Equal:
                    return Operator.Eq;
                case ExpressionType.GreaterThan:
                    return Operator.Gt;
                case ExpressionType.GreaterThanOrEqual:
                    return Operator.Ge;
                case ExpressionType.LessThan:
                    return Operator.Lt;
                case ExpressionType.LessThanOrEqual:
                    return Operator.Le;
                default:
                    return Operator.Eq;
            }
        }
    }
}
