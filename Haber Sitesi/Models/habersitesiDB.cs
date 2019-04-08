namespace Haber_Sitesi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class habersitesiDB : DbContext
    {
        public habersitesiDB()
            : base("name=habersitesiDB")
        {
        }

        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Puan> Puan { get; set; }
        public virtual DbSet<Uye> Uye { get; set; }
        public virtual DbSet<Yazi> Yazi { get; set; }
        public virtual DbSet<Yorum> Yorum { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
