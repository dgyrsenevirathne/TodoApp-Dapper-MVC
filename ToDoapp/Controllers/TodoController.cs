using Microsoft.AspNetCore.Mvc;
using ToDoapp.Repositories;
using ToDoapp.Models;  

namespace ToDoapp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoRepository _repository;

        public TodoController()
        {
            _repository = new TodoRepository(Configuration);
        }

        public IConfiguration Configuration =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        public ActionResult Index()
        {
            var todos = _repository.GetAllTodos();
            return View(todos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(todo);
                return RedirectToAction("Index");
            }
            return View(todo);

        }

        public ActionResult Edit(int id)
        {
            var todo = _repository.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(todo);
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToggleComplete(int id)
        {
            var todo = _repository.GetById(id);
            if (todo != null)
            {
                todo.IsCompleted = !todo.IsCompleted;
                _repository.Update(todo);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
