using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            if (Request.Cookies["username"] == null)
            { return RedirectToAction("EnternameFirst"); }
            else
            {
                var items = context.TodoItems.Include(e => e.User).Where(e => e.User.Name.ToLower() ==
              Request.Cookies["username"].ToLower()).ToList();
                return View(items);
            }
  
  
        }
        public IActionResult EnternameFirst()
        {

            return View();
        }

        public IActionResult AddTask()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddTask(TodoItem todoItem,IFormFile FileDescription)
        {
            var userid=context.Users.Where(e => e.Name.ToLower() ==
              Request.Cookies["username"].ToLower()).AsNoTracking().ToList();

            if (FileDescription != null)
            {
                var filename =Guid.NewGuid().ToString()+FileDescription.FileName;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", filename);
                using(var stream=System.IO.File.Create(filepath))
                {
                    FileDescription.CopyTo(stream);
                }
                todoItem.FileDescription = filename;
            }
            todoItem.UserId = userid[0].Id;
            todoItem.DeadLine = DateTime.Parse(todoItem.DeadLine.ToString());
            context.TodoItems.Add(todoItem);
            context.SaveChanges();
            TempData["success"] = "item added successfully!!";
            return RedirectToAction("Index");
        }


        public IActionResult UpdateTask(int idtask)
        {
            var task = context.TodoItems.ToList().Find(e => e.Id == idtask);
            return View(task);
        }
        [HttpPost]
        public IActionResult UpdateTask(TodoItem todoItem,IFormFile FileDescription)
        {
            var oldtaskid = context.TodoItems.Where(e => e.Id == todoItem.Id).AsNoTracking().ToList(); 
            if(FileDescription != null)
            {
                var oldname = oldtaskid[0].FileDescription;
                var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", oldname);
                if(System.IO.File.Exists(oldpath))
                { System.IO.File.Delete(oldpath); }

                var filename = Guid.NewGuid().ToString() + FileDescription.FileName;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", filename);
                using (var stream = System.IO.File.Create(filepath))
                {
                    FileDescription.CopyTo(stream);
                }
                todoItem.FileDescription = filename;

            }
            // at case user don't change photo
            else
            {
                todoItem.FileDescription = oldtaskid[0].FileDescription;
            }
            context.Update(todoItem);
            context.SaveChanges();
            TempData["update"] = "item updated successfully!!";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int idtask)
        {
            var task = context.TodoItems.ToList().Find(e => e.Id == idtask);
            var filename = task.FileDescription;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", filename);
            if(System.IO.File.Exists(filepath))
            {  System.IO.File.Delete(filepath);}
            context.Remove(task);
            context.SaveChanges();
            TempData["delet"] = "item deleted successfully!!";
            return RedirectToAction("Index");
        }

        public IActionResult DownloadTask(string filename)
        {
            var filepath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", filename);
            // Return the file for download
            var fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/pdf", filename);
        }
    }
}
