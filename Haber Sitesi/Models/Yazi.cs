namespace Haber_Sitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yazi")]
    public partial class Yazi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yazi()
        {
            Puan = new HashSet<Puan>();
            Yorum = new HashSet<Yorum>();
        }

        public int YaziID { get; set; }

        [StringLength(50)]
        public string Baslik { get; set; }

        public string Metin { get; set; }

        [StringLength(500)]
        public string Foto { get; set; }

        public DateTime? Tarih { get; set; }

        public int? KategoriID { get; set; }

        public int? UyeID { get; set; }

        public virtual Kategori Kategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Puan> Puan { get; set; }

        public virtual Uye Uye { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
