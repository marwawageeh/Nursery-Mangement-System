using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using mvc_project.Models;
using System.Text;

namespace mvc_project.Controllers
{
    public class CrsResultController : Controller
    {
        MWContext db = new MWContext();

        public IActionResult Index()
        {
            List<CrsResult> crs = db.crsResult.Include(w => w.course).Include( ww =>ww.trainee ).ToList();
            return View(crs);
        }


        [HttpGet]
        public IActionResult ADD()
        {
            ViewBag.course = db.course.ToList();
            ViewBag.trainee = db.trainee.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult SaveADD(CrsResult cr)
        {
            db.crsResult.Add(cr);
            db.SaveChanges();

            return RedirectToAction("Index");

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







    }
}
