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
    public class GeneroController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Genero
        public ActionResult Index(string searChBy, string search)
        {

            if (searChBy == "Nombre")
            {
                return View(db.Genero.Where(x => x.Nombre.StartsWith(search) || search ==null).ToList());
            }
            else
            {
                //return View(db.Genero.ToList());
                return View(db.Genero.Where(x => x.Abreviatura == search || search == null).ToList());
            }

        }

        // GET: Genero/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGenero,Nombre,Abreviatura")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Genero.Add(genero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genero);
        }

        // GET: Genero/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Genero/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGenero,Nombre,Abreviatura")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genero).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genero);
        }

        // GET: Genero/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Genero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genero genero = db.Genero.Find(id);
            db.Genero.Remove(genero);
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


            var AllEstadoCivil = db.Genero.ToList();
            return View(AllEstadoCivil);

        }

        public ActionResult Print()

        {
            //return new ActionAsPdf("Reporte",data, new {isPDF=true})
            //{ FileName="Test.pdf"};
            var data = db.Genero.ToList();

            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

           
            return new PartialViewAsPdf("Reporte",data)
            {

                FileName = "Genero.pdf",
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo
                PageSize = Rotativa.Options.Size.Letter,
                PageOrientation=Rotativa.Options.Orientation.Portrait,
               // CustomSwitches = "--no-header --no-footer"
            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}

