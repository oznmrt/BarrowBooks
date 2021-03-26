using BarrowBooks.Business.Abstract;
using BarrowBooks.Business.Concrete;
using BarrowBooks.DataAccess.Concrete.Dapper;
using BarrowBooks.Entites.Concrete;
using BarrowBooks.UI.ViewModels;
using BarrowBooks.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarrowBooks.UI.Controllers
{
    public class HomeController : Controller
    {
        private BookManager _bookService;

        private MemberManager _memberService;

        private BookTransactionManager _bookTransactionManager;

        public HomeController()
        {
            _bookService = new BookManager(new BookDAL());
            _memberService = new MemberManager(new MemberDAL());
            _bookTransactionManager = new BookTransactionManager(new BookTransactionDAL());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookSave()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookSave(Book viewModel)
        {
            if (ModelState.IsValid)
            {
                _bookService.Insert(viewModel);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Hata");
            }

            return View(viewModel);
        }

        public ActionResult MemberSave()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberSave(Member viewModel)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _memberService.Insert(viewModel);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Hata");
            }

            return View(viewModel);
        }

        public ActionResult BookSearch()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookSearch(SearchBookViewModel searchBookViewModel)
        {
            var filters = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(searchBookViewModel.Name))
                filters.Add("Name", searchBookViewModel.Name);
            if (searchBookViewModel.ISBN != 0)
                filters.Add("ISBN", searchBookViewModel.ISBN);
            if (!string.IsNullOrEmpty(searchBookViewModel.Author))
                filters.Add("Author", searchBookViewModel.Author);
            var books = _bookService.GetAvailableBooksBySearchFilter(filters);

            return View("ShowBooks", books);
        }

        public ActionResult BookTransaction(string bookISBN)
        {
            CreateBookTransactionViewModel createBookTransactionViewModel = new CreateBookTransactionViewModel();
            createBookTransactionViewModel.TransactionData = new BookTransaction();
            createBookTransactionViewModel.TransactionData.BookISBN = Convert.ToInt32(bookISBN);

            // min return date must be date now
            // max return date must be 30 working days after retrieve.
            var actualReturnDate = DateTime.Now.AddDays(DateHelper.DaysLeft(DateTime.Now, DateTime.Now.AddDays(30), true));
            createBookTransactionViewModel.TransactionData.ReturnDate = actualReturnDate;
            var members = _memberService.GetAll();
            createBookTransactionViewModel.Members = new SelectList(members, "Id", "Name");

            return View(createBookTransactionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookTransaction(CreateBookTransactionViewModel createBookTransactionViewModel)
        {
            createBookTransactionViewModel.TransactionData.Status = 1; // requested
            _bookTransactionManager.Insert(createBookTransactionViewModel.TransactionData);
            return View("Index");
        }

        public ActionResult DailyReport()
        {
            var viewData = new List<DailyReportViewModel>();
            var data = _bookTransactionManager.GetDailyReportData();
            foreach (var item in data)
            {
                var penaltyAmount = decimal.Zero;

                // if the book has penalty for a late due date
                if (DateTime.Now > item.ReturnDate)
                    penaltyAmount = CommonHelper.calculatePenalty(item.ReturnDate);

                viewData.Add(new DailyReportViewModel
                {
                    bookTransaction = item,
                    penaltyAmount = penaltyAmount
                });
            }
            return View(viewData);
        }
    }
}