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
    public class UniversidadesController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Universidades
        public ActionResult Index(string searChBy, string search)
        {
            if (searChBy == "Nombre")
            {
                return View(db.Universidades.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Universidades.ToList());
                //return View(db.Genero.Where(x => x.Abreviatura == search || search == null).ToList());
            }
           
        }

        // GET: Universidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universidades universidades = db.Universidades.Find(id);
            if (universidades == null)
            {
                return HttpNotFound();
            }
            return View(universidades);
        }

        // GET: Universidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Universidades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUniversidad,Nombre,Observacion,Anulada")] Universidades universidades)
        {
            if (ModelState.IsValid)
            {
                db.Universidades.Add(universidades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(universidades);
        }

        // GET: Universidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universidades universidades = db.Universidades.Find(id);
            if (universidades == null)
            {
                return HttpNotFound();
            }
            return View(universidades);
        }

        // POST: Universidades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUniversidad,Nombre,Observacion,Anulada")] Universidades universidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universidades).State =System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(universidades);
        }

        // GET: Universidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universidades universidades = db.Universidades.Find(id);
            if (universidades == null)
            {
                return HttpNotFound();
            }
            return View(universidades);
        }

        // POST: Universidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Universidades universidades = db.Universidades.Find(id);
            db.Universidades.Remove(universidades);
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

            var AllUniversidades = db.Universidades.ToList();
            return View(AllUniversidades);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Universidades.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Universidades.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
