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
    public class PaisesController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Paises
        public ActionResult Index(string searChBy, string search)
        {

            if (searChBy == "Nombre")
            {
                return View(db.Paises.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {

                return View(db.Paises.Where(x => x.Abreviatura == search || search == null).ToList());
            }
                       
        }

        // GET: Paises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paises paises = db.Paises.Find(id);
            if (paises == null)
            {
                return HttpNotFound();
            }
            return View(paises);
        }

        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPais,Codigo,NombreInternacional,Abreviatura,Nombre")] Paises paises)
        {
            if (ModelState.IsValid)
            {
                db.Paises.Add(paises);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paises);
        }

        // GET: Paises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paises paises = db.Paises.Find(id);
            if (paises == null)
            {
                return HttpNotFound();
            }
            return View(paises);
        }

        // POST: Paises/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPais,Codigo,NombreInternacional,Abreviatura,Nombre")] Paises paises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paises).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paises);
        }

        // GET: Paises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paises paises = db.Paises.Find(id);
            if (paises == null)
            {
                return HttpNotFound();
            }
            return View(paises);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paises paises = db.Paises.Find(id);
            db.Paises.Remove(paises);
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

            var AllPaises = db.Paises.ToList();
            return View(AllPaises);

        }

        public ActionResult Print()
        {

            //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            /*declaramos variable  la cual contendra conexion 
             a la tabla EstadoCivil*/
            var data = db.Paises.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {
                FileName = "Paises.pdf",
                PageSize = Rotativa.Options.Size.Letter

            }
            //var q = new ActionAsPdf("Reporte");
            //return q;
;
        }
    }
}
