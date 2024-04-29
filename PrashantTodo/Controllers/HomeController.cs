using Microsoft.AspNetCore.Mvc;
using PrashantTodo.Data;
using PrashantTodo.Models;
using PrashantTodo.Models.ViewModels;
using System.Diagnostics;

namespace PrashantTodo.Controllers
{
	public class HomeController : Controller
	{
		private TodoDbContext _todoDbContext;

		public HomeController(TodoDbContext todoDbContext)
		{
			_todoDbContext = todoDbContext;
		}

		[HttpGet]
		[ActionName("Index")]
		public IActionResult Index()
		{
			List<TodoItem> todoList = _todoDbContext.TodoItems.ToList();

			return View(todoList);
		}

		[HttpGet]
		[ActionName("AddTodo")]
		public IActionResult AddTodo() 
		{
			return View();
		}

        [HttpPost]
        [ActionName("SaveTodo")]
        public IActionResult SaveTodo(TodoViewModel todoItem)
        {
			var todo = new TodoItem
			{
				Name = todoItem.Name,
				Id = todoItem.Id,
				dateTime = todoItem.dateTime,
				status = todoItem.status,
				description = todoItem.description,
			};

			try
			{
				_todoDbContext.TodoItems.Add(todo);
				_todoDbContext.SaveChanges();
				TempData["message"] = "Save Success";
				return RedirectToAction("Index");
			}
			catch(Exception ex)
			{
				TempData["error"] = "Save Failure: " + ex.Message;
				return RedirectToAction("SaveTodo");
			}
        }
    }
}