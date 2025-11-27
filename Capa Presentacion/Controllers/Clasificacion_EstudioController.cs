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
    public class Clasificacion_EstudioController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Clasificacion_Estudio
        public ActionResult Index(string searChBy, string search)
        {
            if(searChBy =="Nombre")
            {
                return View(db.Clasificacion_Estudio.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {
               return View(db.Clasificacion_Estudio.ToList());
            }
           
        }

        // GET: Clasificacion_Estudio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clasificacion_Estudio clasificacion_Estudio = db.Clasificacion_Estudio.Find(id);
            if (clasificacion_Estudio == null)
            {
                return HttpNotFound();
            }
            return View(clasificacion_Estudio);
        }

        // GET: Clasificacion_Estudio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clasificacion_Estudio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdClasificacion,Nombre,Descripcion")] Clasificacion_Estudio clasificacion_Estudio)
        {
            if (ModelState.IsValid)
            {
                db.Clasificacion_Estudio.Add(clasificacion_Estudio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clasificacion_Estudio);
        }

        // GET: Clasificacion_Estudio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clasificacion_Estudio clasificacion_Estudio = db.Clasificacion_Estudio.Find(id);
            if (clasificacion_Estudio == null)
            {
                return HttpNotFound();
            }
            return View(clasificacion_Estudio);
        }

        // POST: Clasificacion_Estudio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdClasificacion,Nombre,Descripcion")] Clasificacion_Estudio clasificacion_Estudio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clasificacion_Estudio).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clasificacion_Estudio);
        }

        // GET: Clasificacion_Estudio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clasificacion_Estudio clasificacion_Estudio = db.Clasificacion_Estudio.Find(id);
            if (clasificacion_Estudio == null)
            {
                return HttpNotFound();
            }
            return View(clasificacion_Estudio);
        }

        // POST: Clasificacion_Estudio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clasificacion_Estudio clasificacion_Estudio = db.Clasificacion_Estudio.Find(id);
            db.Clasificacion_Estudio.Remove(clasificacion_Estudio);
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

            var AllClasificacion_Estudio= db.Clasificacion_Estudio.ToList();
            return View(AllClasificacion_Estudio);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Clasificacion_Estudio.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Clase Estudio.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}

