using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_project.Models;
using System.Text;

/*مهمين جدا لمكتبه الiTextSharp الى نزلتها عشان اقدر انزل ال */
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;



namespace mvc_project.Controllers
{
    public class PaymentController : Controller
    {

        private readonly MWContext _context;

        public PaymentController(MWContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments.Include(p => p.Trainee).ToListAsync();
            return View(payments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.trainee = _context.trainee.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (payment.Amount !=0)
            {
                //// تحقق من وجود دفعة سابقة لنفس الطالب ولنفس الشهر
                //var isDuplicate = await _context.Payments
                //    .AnyAsync(p => p.TraineeId == payment.TraineeId && p.Month == payment.Month);

                //if (isDuplicate)
                //{
                //    ModelState.AddModelError("", "⚠️ هذا الشهر تم دفعه بالفعل لهذا الطالب.");
                //    return View("Create", payment);
                //}

                payment.PaymentDate = DateTime.Now;
                // تأكد من أن المدفوعات يتم حفظها في قاعدة البيانات
                _context.Add(payment);
                await _context.SaveChangesAsync(); // تنفيذ العملية في قاعدة البيانات

                // رسالة تأكيد للمستخدم بعد الدفع
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("DownloadReceipt", new { paymentId = payment.Id });// أو يمكنك إعادة توجيه المستخدم إلى صفحة أخرى
            }
            ViewBag.trainee = _context.trainee.ToList();
            return View(payment);

        }
        public async Task<IActionResult> PaymentStatistics()
        {
            var totalAmount = await _context.Payments.SumAsync(p => p.Amount);
            ViewBag.TotalAmount = totalAmount;
            return View();
        }

        public async Task<IActionResult> PaymentHistory(int traineeId)
        {
            if (traineeId == 0)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .Where(p => p.TraineeId == traineeId)
                .Include(p => p.Trainee)
                .ToListAsync();

            if (payments == null || payments.Count == 0)
            {
                TempData["ErrorMessage"] = "لا يوجد تاريخ مدفوعات لهذا الطالب.";
                return RedirectToAction("Index");
            }

            return View(payments);
        }

        public IActionResult DownloadReceipt(int paymentId)
        {
            var payment = _context.Payments
                .Include(p => p.Trainee)
                .FirstOrDefault(p => p.Id == paymentId);

            if (payment == null)
            {
                return NotFound();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // إنشاء PdfWriter مع الذاكرة
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                Document document = new Document(pdfDocument);

                // إضافة محتوى PDF
                document.Add(new Paragraph("إيصال الدفع"));
                document.Add(new Paragraph("اسم الطالب: " + payment.Trainee.Name));
                document.Add(new Paragraph("الشهر المدفوع: " + payment.Month));
                document.Add(new Paragraph("طريقة الدفع: " + payment.PaymentMethod));
                document.Add(new Paragraph("المبلغ: " + payment.Amount.ToString()));
                document.Add(new Paragraph("تاريخ الدفع: " + payment.PaymentDate.ToString("dd/MM/yyyy")));

                document.Close();

                // إرسال الـ PDF مباشرة للمتصفح
                return File(memoryStream.ToArray(), "application/pdf", "Receipt_" + payment.Id + ".pdf");
            }
        }

        public IActionResult ExportToCsv()
        {
            var pay = _context.Payments.ToList();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("studentName,date,month,amount");

            foreach (var item in pay)
            {
                csvBuilder.AppendLine($"{item.Trainee?.Name},{item.PaymentDate},{item.Month},{item.Amount}");
            }

            var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());


            return File(csvBytes, "text/csv", "payment.csv");
        }


    }
}
