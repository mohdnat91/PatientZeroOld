using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PatientZero.Models
{
    public class EntityContext : DbContext
    {
        public IQueryable<Entity> Entities(string type) 
        {
            return Set<Entity>().Where(e => e.Type == type);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Entity>();
            modelBuilder.Entity<Section>();
            modelBuilder.Entity<PatientInfoSection>();
        }
    }

    public class Entity
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }

    public abstract class Section
    {
        public long Id { get; set; }

        public virtual Entity Entity { get; set; }
    }

    public class PatientInfoSection : Section
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string PhoneNum { get; set; }
    }
}