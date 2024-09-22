using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpendSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger,SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()

        {
            var allExpenses= _context.Expenses.ToList();
            var totalExpenses = allExpenses.Sum(expense => expense.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }


        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                var expenseIdInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseIdInDb);
            }
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {

            var expenseIdInDb=_context.Expenses.SingleOrDefault(expense=>expense.Id==id);
            _context.Expenses.Remove(expenseIdInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult CreateEditExpenseForm(Expense expense)

        {
            if (expense.Id != 0)
            {
                //update
                _context.Expenses.Update(expense);
            }
            else
            {
                //create
                _context.Expenses.Add(expense);
            }
           
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
