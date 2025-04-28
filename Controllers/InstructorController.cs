using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_project.Filtrers;
using mvc_project.Models;
using mvc_project.ModelView;
using System.Text;

namespace mvc_project.Controllers
{
    [HandelError]
    public class InstructorController : Controller
	{

		MWContext DB = new MWContext();

		public IActionResult Index()
		{
			List<Instructor> instructors = DB.instructor.Include(w=>w.department).Include(ww=>ww.course).ToList();


			return View(instructors);
		}
		public IActionResult Details(int id)
		{
			Instructor instructor = DB.instructor.Include(w=>w.department).Include(ww=>ww.course).FirstOrDefault(s => s.ID == id);


			return View(instructor);
		}
		public IActionResult Add()
		{
			var model = new Inst_With_Cource_MV()
			{
				CoursesList = DB.course.ToList() ?? new List<Course>(),
				Departmentlist = DB.department.ToList() ?? new List<Department>()
		};


			return View("Add",model);
		}

        [HttpPost]
        public IActionResult saveadd(Instructor inst)
        {
            if (inst.Name != null)
            {
                // التحقق من رفع صورة
                if (inst.ImageFile != null)
                {
                    // تحديد مسار الحفظ داخل wwwroot/uploads/
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    // التأكد من وجود المجلد
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    // إنشاء اسم فريد للصورة
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + inst.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // حفظ الصورة على السيرفر
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        inst.ImageFile.CopyTo(fileStream);
                    }

                    // حفظ المسار في قاعدة البيانات
                    inst.ImageURL = "/uploads/" + uniqueFileName;
                }

                DB.instructor.Add(inst);
                DB.SaveChanges();

                return RedirectToAction("index");
            }

            return View("Add", inst);
        }


        public IActionResult Edit(int id)
        {
            var inst = DB.instructor.FirstOrDefault(w => w.ID == id);

            if (inst == null)
            {
                return NotFound();
            }
            return View("Edit", inst);
        }

        [HttpPost]
		[ActionName("Edit")]
		public IActionResult editSave(Instructor inst)
        {
            var instructor = DB.instructor.FirstOrDefault(w => w.ID == inst.ID);

            if (instructor == null)
            {
                return NotFound();
            }

            instructor.Name = inst.Name;
            instructor.ImageURL = inst.ImageURL;
            instructor.Salary = inst.Salary;
            //instructor.dept_id = inst.dept_id;
            //instructor.crs_id = inst.crs_id;
            instructor.Address = inst.Address;

            DB.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult delete(int id)
        {
            var instructor = DB.instructor.FirstOrDefault(w => w.ID == id);

            DB.instructor.Remove(instructor);
            DB.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult ExportToCsv()
        {
            var inst = DB.instructor.ToList();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,Name,salary,address");

            foreach (var item in inst)
            {
                csvBuilder.AppendLine($"{item.ID},{item.Name},{item.Salary},{item.Address}");
            }

            var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());


            return File(csvBytes, "text/csv", "instructor.csv");
        }

    }
}
