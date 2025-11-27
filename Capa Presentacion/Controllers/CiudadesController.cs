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
    public class CiudadesController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Ciudades
        public ActionResult Index(string searChBy, string search)
        {
            var Ciudades = db.Ciudades.Include(c => c.Paises);
            if (searChBy == "Nombre")
            {
                return View(db.Ciudades.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {

                return View(db.Ciudades.ToList());
            }
        }   

        // GET: Ciudades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudades ciudades = db.Ciudades.Find(id);
            if (ciudades == null)
            {
                return HttpNotFound();
            }
            return View(ciudades);
        }

        // GET: Ciudades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ciudades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCiudad,IdPais,Nombre,Codigo")] Ciudades ciudades)
        {
            if (ModelState.IsValid)
            {
                db.Ciudades.Add(ciudades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ciudades);
        }

        // GET: Ciudades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudades ciudades = db.Ciudades.Find(id);
            if (ciudades == null)
            {
                return HttpNotFound();
            }
            return View(ciudades);
        }

        // POST: Ciudades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCiudad,IdPais,Nombre,Codigo")] Ciudades ciudades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciudades).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ciudades);
        }

        // GET: Ciudades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudades ciudades = db.Ciudades.Find(id);
            if (ciudades == null)
            {
                return HttpNotFound();
            }
            return View(ciudades);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ciudades ciudades = db.Ciudades.Find(id);
            db.Ciudades.Remove(ciudades);
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

            /*declaramos variable  la cual contendra conexion 
             a la tabla EstadoCivil*/
            var AllCiudades = db.Ciudades.ToList();
            return View(AllCiudades);

        }

        public ActionResult Print()
        {
            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            /*declaramos variable  la cual contendra conexion 
             a la tabla EstadoCivil*/
            var data = db.Ciudades.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Ciudades.pdf",
                PageSize = Rotativa.Options.Size.Letter

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
