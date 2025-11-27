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
    public class ColegiosController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Colegios
        public ActionResult Index(string searChBy, string search)
        {
            if (searChBy == "Nombre")
            {
                return View(db.Colegios.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Colegios.ToList());
                //return View(db.Genero.Where(x => x.Abreviatura == search || search == null).ToList());
            }
                       
        }

        // GET: Colegios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colegios colegios = db.Colegios.Find(id);
            if (colegios == null)
            {
                return HttpNotFound();
            }
            return View(colegios);
        }

        // GET: Colegios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colegios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdColegio,Nombre,Observacion")] Colegios colegios)
        {
            if (ModelState.IsValid)
            {
                db.Colegios.Add(colegios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(colegios);
        }

        // GET: Colegios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colegios colegios = db.Colegios.Find(id);
            if (colegios == null)
            {
                return HttpNotFound();
            }
            return View(colegios);
        }

        // POST: Colegios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdColegio,Nombre,Observacion")] Colegios colegios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colegios).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colegios);
        }

        // GET: Colegios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colegios colegios = db.Colegios.Find(id);
            if (colegios == null)
            {
                return HttpNotFound();
            }
            return View(colegios);
        }

        // POST: Colegios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colegios colegios = db.Colegios.Find(id);
            db.Colegios.Remove(colegios);
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

            var AllColegios = db.Colegios.ToList();
            return View(AllColegios);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Colegios.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Colegios.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
