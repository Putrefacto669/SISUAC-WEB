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
    public class Documentos_IngresoController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Documentos_Ingreso
        public ActionResult Index(string searChBy, string search)
        {
            var documentos_Ingreso = db.Documentos_Ingreso.Include(d => d.TipoPlanEstudio);

            if (searChBy == "Nombre")
            {
                return View(db.Documentos_Ingreso.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Documentos_Ingreso.ToList());
                //return View(db.Genero.Where(x => x.Abreviatura == search || search == null).ToList());
            }


            
        }

        // GET: Documentos_Ingreso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentos_Ingreso documentos_Ingreso = db.Documentos_Ingreso.Find(id);
            if (documentos_Ingreso == null)
            {
                return HttpNotFound();
            }
            return View(documentos_Ingreso);
        }

        // GET: Documentos_Ingreso/Create
        public ActionResult Create()
        {
            ViewBag.IdPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre");
            return View();
        }

        // POST: Documentos_Ingreso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDocumento,Nombre,Requerido,Activo,Observaciones,IdPlanEstudio")] Documentos_Ingreso documentos_Ingreso)
        {
            if (ModelState.IsValid)
            {
                db.Documentos_Ingreso.Add(documentos_Ingreso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre", documentos_Ingreso.IdPlanEstudio);
            return View(documentos_Ingreso);
        }

        // GET: Documentos_Ingreso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentos_Ingreso documentos_Ingreso = db.Documentos_Ingreso.Find(id);
            if (documentos_Ingreso == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre", documentos_Ingreso.IdPlanEstudio);
            return View(documentos_Ingreso);
        }

        // POST: Documentos_Ingreso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDocumento,Nombre,Requerido,Activo,Observaciones,IdPlanEstudio")] Documentos_Ingreso documentos_Ingreso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentos_Ingreso).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPlanEstudio = new SelectList(db.TipoPlanEstudio, "IdTipoPlan", "Nombre", documentos_Ingreso.IdPlanEstudio);
            return View(documentos_Ingreso);
        }

        // GET: Documentos_Ingreso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentos_Ingreso documentos_Ingreso = db.Documentos_Ingreso.Find(id);
            if (documentos_Ingreso == null)
            {
                return HttpNotFound();
            }
            return View(documentos_Ingreso);
        }

        // POST: Documentos_Ingreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documentos_Ingreso documentos_Ingreso = db.Documentos_Ingreso.Find(id);
            db.Documentos_Ingreso.Remove(documentos_Ingreso);
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

            var AllDocumentos_Ingreso = db.Documentos_Ingreso.ToList();
            return View(AllDocumentos_Ingreso);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Documentos_Ingreso.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Documentos Ingreso.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
