using System.Web.Mvc;
using webMVC.Repositories;
using webMVC.Models;
using System.Linq;

namespace webMVC.Controllers
{
    public class TasksController : Controller
    {
        private TaskRepository taskRepo = new TaskRepository();

        public ActionResult Index()
        {
            var tasks = taskRepo.GetAllTasks();
            return View(tasks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                taskRepo.AddTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public ActionResult Edit(int id)
        {
            var task = taskRepo.GetAllTasks().FirstOrDefault(t => t.Id == id);
            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                taskRepo.UpdateTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public ActionResult Delete(int id)
        {
            var task = taskRepo.GetAllTasks().FirstOrDefault(t => t.Id == id);
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            taskRepo.DeleteTask(id);
            //taskRepo.ResetAutoIncrement();
            return RedirectToAction("Index");
        }
    }
}
