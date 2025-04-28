using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using mvc_project.Filtrers;
using mvc_project.Models;
using System.Text;

namespace mvc_project.Controllers
{
    [HandelError]
    public class TraineeController : Controller
	{
		MWContext db = new MWContext();
		public IActionResult Index()
		{
			List<Trainee> trs = db.trainee.Include(w=>w.department).ToList();
			return View(trs);
		}
		public IActionResult Details(int id)
		{
			Trainee tr = db.trainee.Include(w => w.department).Include(t => t.crsResults).Include(tt=>tt.Payments) // تحميل قائمة الدرجات
                          .FirstOrDefault(ww => ww.ID == id);


            if (tr == null)
            {
                return NotFound();
            }
            return View(tr);
		}


		[HttpGet]
		public IActionResult ADD()
		{
			ViewBag.department = db.department.ToList();
			return View();
		}

        [HttpPost]
        public IActionResult SaveADD(Trainee tr)
        {
            if (tr.Name != null)
            {
                // التحقق من رفع صورة
                if (tr.ImageFile != null)
                {
                    // تحديد مسار الحفظ داخل wwwroot/uploads/
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    // التأكد من وجود المجلد
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    // إنشاء اسم فريد للصورة
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + tr.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // حفظ الصورة على السيرفر
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        tr.ImageFile.CopyTo(fileStream);
                    }

                    // حفظ المسار في قاعدة البيانات
                    tr.ImageURL = "/uploads/" + uniqueFileName;
                }

                db.trainee.Add(tr);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.department = db.department.ToList();
            return View(tr);
        }



        public IActionResult Edit(int id)
		{
			Trainee tr = db.trainee.FirstOrDefault(w => w.ID == id);

			if (tr == null)
			{
				return NotFound();
			}
			return View("Edit", tr);
		}

		[HttpPost]
		[ActionName("Edit")]
		public IActionResult editSave(Trainee tr)
		{
			Trainee trainee = db.trainee.FirstOrDefault(w => w.ID == tr.ID);

			if (trainee == null)
			{
				return NotFound();
			}

			trainee.Name = tr.Name;
			trainee.ImageURL = tr.ImageURL;
			trainee.Grade = tr.Grade;
			trainee.Address = tr.Address;

			db.SaveChanges();
			return RedirectToAction("index");
		}
		public IActionResult delete(int id)
		{
			Trainee tr = db.trainee.FirstOrDefault(w => w.ID == id);

			db.trainee.Remove(tr);
			db.SaveChanges();
			return RedirectToAction("index");
		}
		public IActionResult ExportToCsv()
		{
			var tr = db.trainee.ToList();

			var csvBuilder = new StringBuilder();
			csvBuilder.AppendLine("Id,Name,Grade,Address");

			foreach (var item in tr)
			{
				csvBuilder.AppendLine($"{item.ID},{item.Name},{item.Grade},{item.Address}");
			}

			var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());


			return File(csvBytes, "text/csv", "trainee.csv");
		}

        [HttpGet]
        public IActionResult SearchTrainee()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SearchTrainee(int nationalId)
        {
            if (nationalId == 0)
            {
                ViewBag.ErrorMessage = "Please enter a valid National ID.";
                return View();
            }

            var trainee = db.trainee.FirstOrDefault(t => t.ID == nationalId);

            if (trainee == null)
            {
                ViewBag.ErrorMessage = "Trainee not found!";
                return View();
            }

            return RedirectToAction("Details", new { ID = trainee.ID });
        }





    }
}
