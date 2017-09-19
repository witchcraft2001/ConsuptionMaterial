namespace ConsuptionMaterials.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ConsuptionContext : DbContext
    {
        public ConsuptionContext()
            : base("name=ConsuptionContext")
        {
        }

        public virtual DbSet<Consuption> Consuptions { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consuption>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.Consuptions)
                .WithRequired(e => e.Material)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Consuptions)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Managers)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
