using PatientZero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientZero.Controllers
{
    public class SectionController : Controller
    {

        public ActionResult Create(string type)
        {
            Section section = EntityManager.Instance.InstantiateSection(type);
            return PartialView("Details", section);
        }

        //
        // GET: /Section/Save/SectionId
        [HttpPost]
        public ActionResult Create(Section section)
        {
            using (EntityContext context = new EntityContext())
            {
                context.Set<Section>().Add(section);
                context.SaveChanges();
            }
            return Json(true);
        }

        public ActionResult Save(Section section)
        {
            using (EntityContext context = new EntityContext())
            {
                Section dbSection = context.Set<Section>().Attach(section);
                context.Entry(section).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
            return Json(true);
        }

        public ActionResult Delete(Section section)
        {
            using (EntityContext context = new EntityContext())
            {
                context.Set<Section>().Remove(section);
                context.SaveChanges();
            }
            return Json(true);
        }

        public ActionResult Details(long id)
        {
            Section section = null;
            using (EntityContext context = new EntityContext())
            {
                section = context.Set<Section>().Find(id);
            }
            return PartialView(section);
        }

    }
}
