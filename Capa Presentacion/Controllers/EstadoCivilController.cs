using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using CapaPresentación;
using CrystalDecisions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Rotativa;
using System.Data.SqlClient;



namespace CapaPresentación.Controllers
{
    public class EstadoCivilController : Controller
    {
        private AcademicoEntities db = new AcademicoEntities();

        // GET: EstadoCivil
        public ActionResult Index(string searChBy, string search)
        {
            if (searChBy == "Nombre")
            {
                 return View(db.EstadoCivil.Where(x => x.Nombre.StartsWith(search) || search == null).ToList());
            }
            else
            {

                return View(db.EstadoCivil.ToList());
            }
        }

        // GET: EstadoCivil/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            if (estadoCivil == null)
            {
                return HttpNotFound();
            }
            return View(estadoCivil);
        }

        // GET: EstadoCivil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoCivil/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEstadoCivil,Nombre,Descripcion")] EstadoCivil estadoCivil)
        {
            if (ModelState.IsValid)
            {
                db.EstadoCivil.Add(estadoCivil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoCivil);
        }

        // GET: EstadoCivil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            if (estadoCivil == null)
            {
                return HttpNotFound();
            }
            return View(estadoCivil);
        }

        // POST: EstadoCivil/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEstadoCivil,Nombre,Descripcion")] EstadoCivil estadoCivil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoCivil).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoCivil);
        }

        // GET: EstadoCivil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            if (estadoCivil == null)
            {
                return HttpNotFound();
            }
            return View(estadoCivil);
        }

        // POST: EstadoCivil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoCivil estadoCivil = db.EstadoCivil.Find(id);
            db.EstadoCivil.Remove(estadoCivil);
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
            var AllEstadoCivil = db.EstadoCivil.ToList();
            
            return View(AllEstadoCivil);

        }

        public ActionResult Print()
        {
             //indicamos que estamos generando un pdf
            ViewBag.IsPDF = true;

            var data = db.EstadoCivil.ToList();

            return new PartialViewAsPdf("Reporte", data)
            {

                FileName = "EstadoCivil.pdf",
                PageSize = Rotativa.Options.Size.Letter,
                PageMargins = new Rotativa.Options.Margins(10, 10, 10, 10), // Márgenes: Superior, Derecho, Inferior, Izquierdo

            }

;        }
        //Utilizando cryustal report
        public ActionResult Reportecrystal()
            {
            ReportDocument rd = new ReportDocument();
            //cargando el archivo .rpt
            rd.Load(Server.MapPath("~/Reportes/EstadoCivil.rpt"));

            // Configurar la conexión a la base de datos si es necesario
            rd.SetDatabaseLogon("sa", "123", ".", "Academicado");
            // Conectar a SQL Server y obtener datos
            DataTable dt = new DataTable();
            string connStr = "tu cadena de conexión a SQL Server";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM TuTabla", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            // Establecer la fuente de datos para el informe
            rd.SetDataSource(dt);

            // Pasar el informe a la vista
            ViewBag.Report = rd;


            return View(); 
        }
    }
}
