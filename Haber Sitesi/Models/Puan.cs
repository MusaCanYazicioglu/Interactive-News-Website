namespace Haber_Sitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Puan")]
    public partial class Puan
    {
        public int PuanID { get; set; }

        [Column("Puan")]
        public int? Puan1 { get; set; }

        public int? UyeID { get; set; }

        public int? YaziID { get; set; }

        public virtual Uye Uye { get; set; }

        public virtual Yazi Yazi { get; set; }
    }
}
