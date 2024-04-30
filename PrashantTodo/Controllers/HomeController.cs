using Microsoft.AspNetCore.Mvc;
using PrashantTodo.Data;
using PrashantTodo.Models;
using PrashantTodo.Models.ViewModels;
using System;
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
		public IActionResult Index()
		{
			List<TodoItem> todoList = _todoDbContext.TodoItems.ToList();

			return View(todoList);
		}

        [HttpGet("/AddDescription/{id}")]
        public IActionResult AddDescription(int id)
        {
            var todoItem = _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);

				var editTodoRequest = new EditTodoRequest
				{
					Id = todoItem.Id,
					Name = todoItem.Name
				};

            return View(editTodoRequest);
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
				description = todoItem.description
			};

			try
			{
				_todoDbContext.TodoItems.Add(todo);
				_todoDbContext.SaveChanges();
                TempData["newItem"] = $"Successfully Added '{todo.Name}'";
                return RedirectToAction("Index");
			}
			catch(Exception ex)
			{
				TempData["error"] = "Save Failure: " + ex.Message;
				return RedirectToAction("SaveTodo");
			}
        }

        [HttpGet("/EditTodo/{id}")]
        public IActionResult EditTodo(int id)
        {
			var todoItem = _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);

			if(todoItem != null)
			{
				var editTodoRequest = new EditTodoRequest
				{
					Id = todoItem.Id,
					Name = todoItem.Name,
					dateTime = todoItem.dateTime,
					status = todoItem.status,
					description = todoItem.description
				};

                return View(editTodoRequest);
            }

			return View(null);
        }

        [HttpPost]
        public IActionResult SaveEditTodo(EditTodoRequest todoItem)
		{ 
			var existingTodo = _todoDbContext.TodoItems.Find(todoItem.Id);

            if (existingTodo != null)
			{
                string oldName = existingTodo.Name;
                existingTodo.Name = todoItem.Name;
				existingTodo.dateTime = todoItem.dateTime;
				existingTodo.status = todoItem.status;
				existingTodo.description = todoItem.description;

                _todoDbContext.SaveChanges();

                TempData["edit"] = $"Successfully Updated '{oldName}' to '{existingTodo.Name}'";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveDescriptionTodo(int id, string description)
        {
            var existingTodo = _todoDbContext.TodoItems.Find(id);

            if(existingTodo != null)
            {
                string name = existingTodo.Name;
                existingTodo.description = description;

                _todoDbContext.SaveChanges();

                TempData["description"] = $"Successfully Added Description for '{name}'";
            }

            return RedirectToAction("Index");
        }

		[HttpPost("/MarkComplete")]
        public void MarkComplete(int id)
		{

            var existingTodo = _todoDbContext.TodoItems.Find(id);
            
            existingTodo.status = Status.Completed;

            _todoDbContext.SaveChanges();
        }

        [HttpGet]
		public IActionResult Delete(int id) 
		{
			var todo = _todoDbContext.TodoItems.Find(id);
			
			_todoDbContext.TodoItems.Remove(todo);
			_todoDbContext.SaveChanges();

            TempData["delete"] = $"Successfully Deleted '{todo.Name}'";
            return RedirectToAction("Index");
		}
    }
}