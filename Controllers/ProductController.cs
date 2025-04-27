using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    public class ProductController : Controller
    {

        public IActionResult GetAll()
        {
            ProductBL prodbl = new ProductBL();
            List<Product> list_prod = prodbl.GetAll();
            return View(list_prod);
        }
        public IActionResult getbyis(int id)
        {
            ProductBL productBL = new ProductBL();
            Product prod = productBL.GetById(id);
            return View(prod);
        }

    }
}
