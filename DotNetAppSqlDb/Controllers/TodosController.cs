using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetAppSqlDb.Models;
using System.Diagnostics;

using iText;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Geom;
using iText.IO.Image;
using iText.Layout;
using iText.Layout.Element;

namespace DotNetAppSqlDb.Controllers
{
    public class TodosController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Todos
        public ActionResult Index()
        {            
            Trace.WriteLine("GET /Todos/Index");
            return View(db.Todoes.ToList());
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /Todos/Details/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // GET: Todos/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /Todos/Create");
            return View(new Todo { EmploymentStartDate = DateTime.Now });
        }

        // POST: Todos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,FullAddress,MailingAddress,AsAboveAddress,EmailAddress,PhoneNumber,CitizenStatus,EmploymentStartDate,EmploymentType,PositionTitle,EmergencyContactName,EmergencyContactRelationship,EmergencyContactPhoneNumber,EmployeeSignature")] Todo todo)
        {
            Trace.WriteLine("POST /Todos/Create");
            if (ModelState.IsValid)
            {
                db.Todoes.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /Todos/Edit/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id, FirstName, LastName, FullAddress, MailingAddress, AsAboveAddress, EmailAddress, PhoneNumber, CitizenStatus, EmploymentStartDate, EmploymentType, PositionTitle, EmergencyContactName, EmergencyContactRelationship, EmergencyContactPhoneNumber, EmployeeSignature")] Todo todo)
        {
            Trace.WriteLine("POST /Todos/Edit/" + todo.ID);
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Todos/Delete/5
        public ActionResult Delete(int? id)
        {
            Trace.WriteLine("GET /Todos/Delete/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trace.WriteLine("POST /Todos/Delete/" + id);
            Todo todo = db.Todoes.Find(id);
            db.Todoes.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Todos/Delete/5
        public ActionResult GetPDF(int? id)
        {
            Trace.WriteLine("GET /Todos/GetPDF/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }

            string filePath = HttpRuntime.AppDomainAppPath + "/PDF/NewEmployeeDetails.pdf";
            string filePathFilled = HttpRuntime.AppDomainAppPath + "/PDF/"+ id.ToString() +".pdf";

            PdfDocument pdf = new PdfDocument(new PdfReader(filePath), new PdfWriter(filePathFilled));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);

            PdfFont fontHELVETICA = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string AsAboveAddressVaule = todo.AsAboveAddress ? "Yes" : ""; // empty string is false

            form.GetField("First Name").SetValue(todo.FirstName);
            form.GetField("Last Name").SetValue(todo.LastName);
            form.GetField("Full Address").SetValue(todo.FullAddress);
            if (todo.MailingAddress != null && AsAboveAddressVaule.Length <= 0) 
            {
                PdfTextFormField mailingAddress = PdfFormField.CreateText(pdf, new Rectangle(227, 607, 310, 30), "mailingAddress", todo.MailingAddress, fontHELVETICA, 18);
                form.AddField(mailingAddress);
            } else {
                form.GetField("As Above").SetCheckType(PdfFormField.TYPE_CHECK).SetValue(AsAboveAddressVaule);
            }

            form.GetField("Email Address").SetValue(todo.EmailAddress).SetJustification(PdfFormField.ALIGN_LEFT);
            PdfTextFormField phoneNumber = PdfFormField.CreateText(pdf, new Rectangle(145, 538, 392, 30), "phoneNumber", "0" + todo.PhoneNumber.ToString(), fontHELVETICA, 18);
            form.AddField(phoneNumber);
            form.GetField("Citizenship Statas").SetValue(todo.CitizenStatus).SetJustification(PdfFormField.ALIGN_LEFT);
            form.GetField("Employment Start Date").SetValue(todo.EmploymentStartDate.ToString()).SetJustification(PdfFormField.ALIGN_LEFT);
            form.GetField("Employment Type").SetValue(todo.EmploymentType).SetJustification(PdfFormField.ALIGN_LEFT);
            form.GetField("Position Title").SetValue(todo.PositionTitle).SetJustification(PdfFormField.ALIGN_LEFT);
            form.GetField("Name").SetValue(todo.EmergencyContactName);
            form.GetField("Relationship").SetValue(todo.EmergencyContactRelationship);
            PdfTextFormField emergencyContactPhoneNumber = PdfFormField.CreateText(pdf, new Rectangle(145, 275, 392, 30), "emergencyPhoneNumber", "0" + todo.EmergencyContactPhoneNumber.ToString(), fontHELVETICA, 18);
            form.AddField(emergencyContactPhoneNumber);

            if (todo.EmployeeSignature != null) 
            {
                ImageData imageData = ImageDataFactory.CreatePng(todo.EmployeeSignature);
                Image image = new Image(imageData).ScaleAbsolute(200, 50).SetFixedPosition(1, 190, 180);
                Document document = new Document(pdf);
                document.Add(image);
            }

            form.FlattenFields();
            pdf.Close();

            return File(filePathFilled, "application/pdf"); ;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
