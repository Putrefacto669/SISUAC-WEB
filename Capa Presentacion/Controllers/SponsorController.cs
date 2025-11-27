using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapaPresentación;
using Rotativa;

namespace CapaPresentación.Controllers
{
    public class SponsorController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Sponsor
        public ActionResult Index(string searChBy, string search)
        {
            if (searChBy == "Nombre")
            {
                return View(db.Carreras.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {

                return View(db.Sponsor.ToList());
            }
            
        }

        // GET: Sponsor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = db.Sponsor.Find(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // GET: Sponsor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sponsor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSponsor,Nombre,Descripcion")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                db.Sponsor.Add(sponsor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sponsor);
        }

        // GET: Sponsor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = db.Sponsor.Find(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSponsor,Nombre,Descripcion")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sponsor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sponsor);
        }

        // GET: Sponsor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = db.Sponsor.Find(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sponsor sponsor = db.Sponsor.Find(id);
            db.Sponsor.Remove(sponsor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Reporte()
        {
            // Indicar que estamos No estamos generando un  pdf
            ViewBag.IsPDF = false;

            var AllSponsor = db.Sponsor.ToList();
            return View(AllSponsor);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Sponsor.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Sponsor.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }

    }
}
