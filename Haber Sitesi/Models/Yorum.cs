namespace Haber_Sitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        public int YorumID { get; set; }

        [StringLength(500)]
        public string Metin { get; set; }

        public DateTime? Tarih { get; set; }

        public int? UyeID { get; set; }

        public int? YaziID { get; set; }

        public virtual Uye Uye { get; set; }

        public virtual Yazi Yazi { get; set; }
    }
}
