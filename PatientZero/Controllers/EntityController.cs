using PatientZero.Models;
using PatientZero.Models.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientZero.Controllers
{
    public class EntityController : Controller
    {
        //
        // GET: /Entity/List/Patient

        public ActionResult List(string type) 
        {
            IEnumerable<Entity> e = null;
            using (EntityContext context = new EntityContext()) 
            {
                e = context.Entities(type).ToList();
            }
            return View(e);
        }

        //
        // GET: /Entity/Details/5

        public ActionResult Details(long id)
        {
            Entity e = null;
            using (EntityContext context = new EntityContext()) 
            {
                e = context.Set<Entity>().Find(id);
                e.Sections = e.Sections.ToList();
            }
            return View(e);
        }

        //
        // GET: /Entity/Create/Patient

        public ActionResult Create(string type)
        {
            Entity e;
            using (EntityContext context = new EntityContext()) 
            {
                e = EntityManager.Instance.Instantiate(type);
                context.Set<Entity>().Add(e);
                context.SaveChanges();
            }
            return RedirectToAction("Details", new { id = e.Id });
        }

        //
        // GET: /Entity/Delete/5

        public ActionResult Delete(long id) 
        {
            string type;
            using (EntityContext context = new EntityContext()) 
            {
                Entity e = context.Set<Entity>().Find(id);
                type = e.Type;
                foreach (Section s in e.Sections.ToList()) context.Set<Section>().Remove(s);
                context.Set<Entity>().Remove(e);
                context.SaveChanges();
            }
            return RedirectToAction("List", new { type = type });
        }

    }
}
