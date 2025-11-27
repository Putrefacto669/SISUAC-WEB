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
    public class Clase_SecundariaController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Clase_Secundaria
        public ActionResult Index(string searChBy, string search)
        {
            if (searChBy == "Nombre")
            {
                return View(db.Genero.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Clase_Secundaria.ToList());
                //return View(db.Genero.Where(x => x.Abreviatura == search || search == null).ToList());
            }
             
        }

        // GET: Clase_Secundaria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase_Secundaria clase_Secundaria = db.Clase_Secundaria.Find(id);
            if (clase_Secundaria == null)
            {
                return HttpNotFound();
            }
            return View(clase_Secundaria);
        }

        // GET: Clase_Secundaria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clase_Secundaria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdClase,Nombre,Descripcion")] Clase_Secundaria clase_Secundaria)
        {
            if (ModelState.IsValid)
            {
                db.Clase_Secundaria.Add(clase_Secundaria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clase_Secundaria);
        }

        // GET: Clase_Secundaria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase_Secundaria clase_Secundaria = db.Clase_Secundaria.Find(id);
            if (clase_Secundaria == null)
            {
                return HttpNotFound();
            }
            return View(clase_Secundaria);
        }

        // POST: Clase_Secundaria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdClase,Nombre,Descripcion")] Clase_Secundaria clase_Secundaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clase_Secundaria).State =System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clase_Secundaria);
        }

        // GET: Clase_Secundaria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clase_Secundaria clase_Secundaria = db.Clase_Secundaria.Find(id);
            if (clase_Secundaria == null)
            {
                return HttpNotFound();
            }
            return View(clase_Secundaria);
        }

        // POST: Clase_Secundaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clase_Secundaria clase_Secundaria = db.Clase_Secundaria.Find(id);
            db.Clase_Secundaria.Remove(clase_Secundaria);
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

            var AllClase_Secundaria = db.Clase_Secundaria.ToList();
            return View(AllClase_Secundaria);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.Clase_Secundaria.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Clase Secundaria.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
