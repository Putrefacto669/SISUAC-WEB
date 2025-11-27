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
    public class EspecialidadController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Especialidad
        public ActionResult Index(string searChBy, string search)
        {

            if (searChBy == "Nombre")
            {
                return View(db.Especialidad.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {

                return View(db.Especialidad.ToList());
            }
            
        }

        // GET: Especialidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // GET: Especialidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especialidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEspecialidad,Nombre,Descripcion")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                db.Especialidad.Add(especialidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidad);
        }

        // GET: Especialidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // POST: Especialidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEspecialidad,Nombre,Descripcion")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidad);
        }

        // GET: Especialidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // POST: Especialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Especialidad especialidad = db.Especialidad.Find(id);
            db.Especialidad.Remove(especialidad);
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
            //Indicamos que No estamos generandu un PDF
            ViewBag.IsPDF = false;



            /*declaramos variable  la cual contendra conexion 
             a la tabla ESPECIALIDAD*/

            var AllEspecialidad = db.Especialidad.ToList();
            return View(AllEspecialidad);

        }

        public ActionResult Print()
        {
            //return new ActionAsPdf("Reporte")
            //{ FileName="Test.pdf"};

            //Indicamos que No estamos generandu un PDF
            ViewBag.IsPDF = true;


            var data = db.Especialidad.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Especialidad.pdf",
                PageSize = Rotativa.Options.Size.Letter

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
