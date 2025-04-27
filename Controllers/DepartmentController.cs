using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Filtrers;
using mvc_project.Models;
using System.Text;

namespace mvc_project.Controllers
{
    [HandelError]
    public class DepartmentController : Controller
	{
		MWContext db = new MWContext();

		[Authorize]
		public IActionResult Index()
		{
			List<Department> dept = db.department.ToList();
			return View(dept);
		}
		public IActionResult Details(int id)
		{
			Department dept = db.department.FirstOrDefault(ww => ww.ID == id);

			return View(dept);
		}

		[HttpGet]
		public IActionResult ADD()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SaveADD(Department dept)
		{
			if (dept.Name != null)
			{
				db.department.Add(dept);
				db.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(dept);

		}


		public IActionResult Edit(int id)
		{
			Department dept = db.department.FirstOrDefault(w => w.ID == id);

			if (dept == null)
			{
				return NotFound();
			}
			return View("Edit", dept);
		}

		[HttpPost]
		[ActionName("Edit")]
		public IActionResult editSave(Department dept)
		{
			Department deptartment = db.department.FirstOrDefault(w => w.ID == dept.ID);

			if (deptartment == null)
			{
				return NotFound();
			}

			deptartment.Name = dept.Name;
			deptartment.Manger = dept.Manger;


			db.SaveChanges();
			return RedirectToAction("index");
		}
		public IActionResult delete(int id)
		{
			Department dept = db.department.FirstOrDefault(w => w.ID == id);

			db.department.Remove(dept);
			db.SaveChanges();
			return RedirectToAction("index");
		}
		public IActionResult ExportToCsv()
		{
			var dept = db.department.ToList();

			var csvBuilder = new StringBuilder();
			csvBuilder.AppendLine("Id,Name,manger");

			foreach (var item in dept)
			{
				csvBuilder.AppendLine($"{item.ID},{item.Name},{item.Manger}");
			}

			var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());


			return File(csvBytes, "text/csv", "department.csv");
		}
	}
}
