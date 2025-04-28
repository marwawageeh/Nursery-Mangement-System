using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_project.Filtrers;
using mvc_project.Models;
using mvc_project.ModelView;
using System.Text;

namespace mvc_project.Controllers
{
    //[HandelError]
    public class AttendanceController : Controller
    {
        MWContext DB = new MWContext();

        public IActionResult Index()
        {
            List<Attendance> attn = DB.Attendance.Include(w=>w.Course).Include(ww=>ww.Trainee).ToList();

            return View(attn);
        }
        //public IActionResult Details(int id)
        //{
        //    Instructor instructor = DB.instructor.Include(w => w.department).Include(ww => ww.course).FirstOrDefault(s => s.ID == id);


        //    return View(instructor);
        //}
        public IActionResult Add()
        {
            ViewBag.trainee = DB.trainee.ToList();
            ViewBag.course = DB.course.ToList();


            return View("Add");
        }

        [HttpPost]
        public IActionResult saveadd(Attendance attn)
        {

                DB.Attendance.Add(attn);
                DB.SaveChanges();

                return RedirectToAction("index");
        }


        //public IActionResult Edit(int id)
        //{
        //    var inst = DB.instructor.FirstOrDefault(w => w.ID == id);

        //    if (inst == null)
        //    {
        //        return NotFound();
        //    }
        //    return View("Edit", inst);
        //}

        //[HttpPost]
        //[ActionName("Edit")]
        //public IActionResult editSave(Instructor inst)
        //{
        //    var instructor = DB.instructor.FirstOrDefault(w => w.ID == inst.ID);

        //    if (instructor == null)
        //    {
        //        return NotFound();
        //    }

        //    instructor.Name = inst.Name;
        //    instructor.ImageURL = inst.ImageURL;
        //    instructor.Salary = inst.Salary;
        //    //instructor.dept_id = inst.dept_id;
        //    //instructor.crs_id = inst.crs_id;
        //    instructor.Address = inst.Address;

        //    DB.SaveChanges();
        //    return RedirectToAction("index");
        //}
        //public IActionResult delete(int id)
        //{
        //    var instructor = DB.instructor.FirstOrDefault(w => w.ID == id);

        //    DB.instructor.Remove(instructor);
        //    DB.SaveChanges();
        //    return RedirectToAction("index");
        //}
        //public IActionResult ExportToCsv()
        //{
        //    var attn = DB.Attendance.ToList();

        //    var csvBuilder = new StringBuilder();
        //    csvBuilder.AppendLine("Id,Name,salary,address");

        //    foreach (var item in attn)
        //    {
        //        csvBuilder.AppendLine($"{item.AttendanceId},{item.Date},{item.Status},{item.Course.Name},{item.Course.Name}");
        //    }

        //    var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());


        //    return File(csvBytes, "text/csv", "attendance.csv");
        //}

    }
}
