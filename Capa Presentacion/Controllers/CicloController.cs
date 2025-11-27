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
    public class CicloController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Ciclo
        public ActionResult Index(string searChBy, string search)
        {
            var ciclo = db.Ciclo.Include(c => c.Concepto1);
            if (searChBy == "Nombre")
            {
                return View(db.Ciclo.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {

                return View(ciclo.ToList());
            }


            //return View(ciclo.ToList());
        }

        // GET: Ciclo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return HttpNotFound();
            }
            return View(ciclo);
        }

        // GET: Ciclo/Create
        public ActionResult Create()
        {
            ViewBag.Concepto = new SelectList(db.Concepto, "IdConcepto", "Nombre");
            return View();
        }

        // POST: Ciclo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCiclo,Nombre,Intervalo,Cantidad,Concepto,Cuotas,Informe")] Ciclo ciclo)
        {
            if (ModelState.IsValid)
            {
                db.Ciclo.Add(ciclo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Concepto = new SelectList(db.Concepto, "IdConcepto", "Nombre", ciclo.Concepto);
            return View(ciclo);
        }

        // GET: Ciclo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Concepto = new SelectList(db.Concepto, "IdConcepto", "Nombre", ciclo.Concepto);
            return View(ciclo);
        }

        // POST: Ciclo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCiclo,Nombre,Intervalo,Cantidad,Concepto,Cuotas,Informe")] Ciclo ciclo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciclo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Concepto = new SelectList(db.Concepto, "IdConcepto", "Nombre", ciclo.Concepto);
            return View(ciclo);
        }

        // GET: Ciclo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return HttpNotFound();
            }
            return View(ciclo);
        }

        // POST: Ciclo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ciclo ciclo = db.Ciclo.Find(id);
            db.Ciclo.Remove(ciclo);
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

            var AllCiclo = db.Ciclo.ToList();
            return View(AllCiclo);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Ciclo.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Ciclo.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
