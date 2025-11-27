using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapaPresentación;

namespace CapaPresentación.Controllers
{
    public class UniversidadController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Universidad
        public ActionResult Index()
        {
            return View(db.Universidad.ToList());
        }

        // GET: Universidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universidad universidad = db.Universidad.Find(id);
            if (universidad == null)
            {
                return HttpNotFound();
            }
            return View(universidad);
        }

        // GET: Universidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Universidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUniversidad,Nombre,Direccion,Web,Telefonos,Fecha,Resena,Mision,Vision,Objetivos,Logo,Lema")] Universidad universidad)
        {
            if (ModelState.IsValid)
            {
                db.Universidad.Add(universidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(universidad);
        }

        // GET: Universidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universidad universidad = db.Universidad.Find(id);
            if (universidad == null)
            {
                return HttpNotFound();
            }
            return View(universidad);
        }

        // POST: Universidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUniversidad,Nombre,Direccion,Web,Telefonos,Fecha,Resena,Mision,Vision,Objetivos,Logo,Lema")] Universidad universidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universidad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(universidad);
        }

        // GET: Universidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universidad universidad = db.Universidad.Find(id);
            if (universidad == null)
            {
                return HttpNotFound();
            }
            return View(universidad);
        }

        // POST: Universidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Universidad universidad = db.Universidad.Find(id);
            db.Universidad.Remove(universidad);
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
       
        //// Si la imagen viene de una base de datos como arreglo de bytes
        //public ActionResult MostrarImagenDesdeBD()
        //{
        //    var data = db.Carreras.ToList();
        //    byte[] imagenBytes = Convert.ToByte(data); // Tu lógica para obtener los bytes
        //    var imageBase64 = Convert.ToBase64String(imagenBytes);
        //    ViewBag.ImageBase64 = String.Format("data:image/png;base64,{0}", imageBase64);
        //    return View();
        //}
    }
}
