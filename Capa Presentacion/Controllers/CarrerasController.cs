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
    public class CarrerasController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Carreras
        public ActionResult Index(string searChBy, string search)
        {
           var carreras = db.Carreras.Include(c => c.Facultad).Include(c => c.TipoPlanEstudio);
           
            if (searChBy == "Nombre")
            {
                return View(db.Carreras.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {

                return View(carreras.ToList());
            }


           
        }

        // GET: Carreras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carreras carreras = db.Carreras.Find(id);
            if (carreras == null)
            {
                return HttpNotFound();
            }
            return View(carreras);
        }

        // GET: Carreras/Create
        public ActionResult Create()
        {
            ViewBag.IdFacultad = new SelectList(db.Facultad, "IdFacultad", "Nombre");
            ViewBag.IdTipoPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre");
            return View();
        }

        // POST: Carreras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCarrera,Codigo,Nombre,IdFacultad,Duracion,Titulo,Observaciones,IdTipoPlanEstudio,AnoCarrera,MaxAsignaturas")] Carreras carreras)
        {
            if (ModelState.IsValid)
            {
                db.Carreras.Add(carreras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFacultad = new SelectList(db.Facultad, "IdFacultad", "Nombre", carreras.IdFacultad);
            ViewBag.IdTipoPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre", carreras.IdTipoPlanEstudio);
            return View(carreras);
        }

        // GET: Carreras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carreras carreras = db.Carreras.Find(id);
            if (carreras == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFacultad = new SelectList(db.Facultad, "IdFacultad", "Nombre", carreras.IdFacultad);
            ViewBag.IdTipoPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre", carreras.IdTipoPlanEstudio);
            return View(carreras);
        }

        // POST: Carreras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCarrera,Codigo,Nombre,IdFacultad,Duracion,Titulo,Observaciones,IdTipoPlanEstudio,AnoCarrera,MaxAsignaturas")] Carreras carreras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carreras).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFacultad = new SelectList(db.Facultad, "IdFacultad", "Nombre", carreras.IdFacultad);
            ViewBag.IdTipoPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre", carreras.IdTipoPlanEstudio);
            return View(carreras);
        }

        // GET: Carreras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carreras carreras = db.Carreras.Find(id);
            if (carreras == null)
            {
                return HttpNotFound();
            }
            return View(carreras);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carreras carreras = db.Carreras.Find(id);
            db.Carreras.Remove(carreras);
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

            var AllCarreras = db.Carreras.ToList();
            return View(AllCarreras);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Carreras.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Carreras.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
