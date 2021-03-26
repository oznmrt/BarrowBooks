using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.DataAccess.Migrations
{
    public class FirstInitializeWithDapper
    {
        private SqlConnection SqlConnection()
        {
            return new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Integrated security=SSPI;database=master");
        }

        private IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            conn.Open();
            return conn;
        }

        public void StartInitialize()
        {
            using (var connection = CreateConnection())
            {
                var dataBaseQuery = @"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'BarrowBooks')
                              BEGIN
                                CREATE DATABASE BarrowBooks
                              END";

                connection.Execute(dataBaseQuery);

                var bookTableQuery = @"USE [BarrowBooks] CREATE TABLE [dbo].[Book] (
                                            [ISBN]   INT           NOT NULL,
                                            [Name]   NVARCHAR (50) NULL,
                                            [Author] NVARCHAR (50) NULL
                                        );";

                connection.Execute(bookTableQuery);                

                var memberTableQuery = @"USE [BarrowBooks] CREATE TABLE [dbo].[Member] (
                                            [Id]          INT           IDENTITY (1, 1) NOT NULL,
                                            [Name]        NVARCHAR (50) NULL,
                                            [PhoneNumber] NVARCHAR (50) NULL
                                        );";

                connection.Execute(memberTableQuery);


                var bookTransactionTableQuery = @"USE [BarrowBooks] CREATE TABLE [dbo].[Member] (
                                                        [Id]          INT           IDENTITY (1, 1) NOT NULL,
                                                        [Name]        NVARCHAR (50) NULL,
                                                        [PhoneNumber] NVARCHAR (50) NULL
                                                    );";

                connection.Execute(bookTransactionTableQuery);

            }
        }
    }
}
