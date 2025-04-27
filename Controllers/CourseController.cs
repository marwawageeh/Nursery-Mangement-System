using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_project.Filtrers;
using mvc_project.Models;
using mvc_project.ModelView;
using System.Text;

namespace mvc_project.Controllers
{
    [HandelError]
    public class CourseController : Controller
	{
		MWContext db = new MWContext();
		public IActionResult Index()
		{
			List<Course> crs = db.course.Include(w=>w.department).ToList();
			return View(crs);
		}
		public IActionResult Details(int id)
		{
			Course crs = db.course.Include(w => w.department).FirstOrDefault(ww => ww.ID == id);
		
			return View(crs);
		}

		[HttpGet]
		public IActionResult ADD()
		{
			ViewBag.department = db.department.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult SaveADD(Course crs)
		{
			if (crs.Name !=null)
			{
				db.course.Add(crs);
				db.SaveChanges();

				return RedirectToAction("Index");
			}

			ViewBag.department = db.department.ToList();
			return View(crs);

		}
		

		public IActionResult Edit(int id)
		{
			Course crs = db.course.FirstOrDefault(w => w.ID == id);

			if (crs == null)
			{
				return NotFound();
			}
			return View("Edit", crs);
		}

		[HttpPost]
		[ActionName("Edit")]
		public IActionResult editSave(Course crs)
		{
			Course course = db.course.FirstOrDefault(w => w.ID == crs.ID);

			if (course == null)
			{
				return NotFound();
			}

			course.Name = crs.Name;
			course.Degree = crs.Degree;
			course.Houres = crs.Houres;
			course.Min_Degree = crs.Min_Degree;

			db.SaveChanges();
			return RedirectToAction("index");
		}
		public IActionResult delete(int id)
		{
			Course course = db.course.FirstOrDefault(w => w.ID == id);

			db.course.Remove(course);
			db.SaveChanges();
			return RedirectToAction("index");
		}
		public IActionResult ExportToCsv()
		{
			var crs = db.course.ToList();

			var csvBuilder = new StringBuilder();
			csvBuilder.AppendLine("Id,Name,DEgree,MinDegree,Hourses");

			foreach (var item in crs)
			{
				csvBuilder.AppendLine($"{item.ID},{item.Name},{item.Degree},{item.Min_Degree},{item.Houres}");
			}

			var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());


			return File(csvBytes, "text/csv", "course.csv");
		}


	}
}
