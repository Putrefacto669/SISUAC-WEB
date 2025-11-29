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
    public class DocentesController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: Docentes
        public ActionResult Index()
        {
            return View(db.Docentes.ToList());
        }

        // GET: Docentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Docentes docente = db.Docentes.Find(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            
            return View(docente);
        }

        // GET: Docentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Docentes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombres,Apellidos,Direccion,Telefono,Celular,EMail,Cedula,FechaNacimiento,EstadoCivil,FechaIngreso,Foto,SobreDocente,Especialidad,Activo")] Docentes docente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Docentes.Add(docente);
                    db.SaveChanges();
                    
                    TempData["SuccessMessage"] = "Docente creado exitosamente.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el docente: " + ex.Message);
            }

            return View(docente);
        }

        // GET: Docentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Docentes docente = db.Docentes.Find(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            
            return View(docente);
        }

        // POST: Docentes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDocente,Nombres,Apellidos,Direccion,Telefono,Celular,EMail,Cedula,FechaNacimiento,EstadoCivil,FechaIngreso,Foto,SobreDocente,Especialidad,Activo")] Docentes docente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(docente).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    
                    TempData["SuccessMessage"] = "Docente actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el docente: " + ex.Message);
            }

            return View(docente);
        }

        // GET: Docentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Docentes docente = db.Docentes.Find(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            
            return View(docente);
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Docentes docente = db.Docentes.Find(id);
                if (docente == null)
                {
                    return HttpNotFound();
                }

                db.Docentes.Remove(docente);
                db.SaveChanges();
                
                TempData["SuccessMessage"] = "Docente eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar el docente: " + ex.Message;
            }

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
    }
}
