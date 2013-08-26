using PatientZero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientZero.Controllers
{
    public class PatientController : Controller
    {

        //
        // GET: /Patient/

        public ActionResult Index()
        {
            IEnumerable<Entity> entities;

            using (EntityContext context = new EntityContext())
            {
                entities = context.Entities("patient").ToList();
            }

            return View(entities);
        }

        //
        // GET: /Patient/Details/5

        public ActionResult Details(long id)
        {
            Entity e;
            using (EntityContext context = new EntityContext())
            {
                e = context.Set<Entity>().Find(id);
                var temp = e.Sections;
            }
            return View(e);
        }

        //
        // GET: /Patient/Create

        public ActionResult Create()
        {
            Entity e = EntityManager.Instance.InstantiateEntity("patient");

            using (EntityContext context = new EntityContext())
            {
                context.Set<Entity>().Add(e);
                context.SaveChanges();
            }
            return RedirectToAction("Details", new { id = e.Id });
        }

        //
        // GET: /Patient/Delete/5

        public ActionResult Delete(long id)
        {
            using (EntityContext context = new EntityContext())
            {
                Entity e = context.Set<Entity>().Find(id);
                context.Set<Entity>().Remove(e);
                context.SaveChanges();
            }
            return View();
        }
    }
}
