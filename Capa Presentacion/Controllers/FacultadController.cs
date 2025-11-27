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
    public class FacultadController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Facultad
        public ActionResult Index(string searChBy, string search)
        {
            var Facultad = db.Facultad.Include(c => c.Docentes);
            if (searChBy == "Nombre")
            {
                return View(db.Facultad.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Facultad.ToList());
               // return View(db.Facultad.Where(x => x.Director == search || search == null).ToList());
            }
        }

        // GET: Facultad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facultad facultad = db.Facultad.Find(id);
            if (facultad == null)
            {
                return HttpNotFound();
            }
            return View(facultad);
        }

        // GET: Facultad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facultad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFacultad,Nombre,Director,Secretario")] Facultad facultad)
        {
            if (ModelState.IsValid)
            {
                db.Facultad.Add(facultad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facultad);
        }

        // GET: Facultad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facultad facultad = db.Facultad.Find(id);
            if (facultad == null)
            {
                return HttpNotFound();
            }
            return View(facultad);
        }

        // POST: Facultad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFacultad,Nombre,Director,Secretario")] Facultad facultad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facultad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facultad);
        }

        // GET: Facultad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facultad facultad = db.Facultad.Find(id);
            if (facultad == null)
            {
                return HttpNotFound();
            }
            return View(facultad);
        }

        // POST: Facultad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facultad facultad = db.Facultad.Find(id);
            db.Facultad.Remove(facultad);
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
        /*bloque de código usand rotativa para la creación de pdf*/

        public ActionResult Reporte()
        {
            // Indicar que estamos No estamos generando un  pdf
            ViewBag.IsPDF = false;

            var AllFacultad = db.Facultad.ToList();
            return View(AllFacultad);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Facultad.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Facultad.pdf",
                PageSize = Rotativa.Options.Size.Letter

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
