namespace ConsuptionMaterials.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consuption")]
    public partial class Consuption
    {
        public int Id { get; set; }

        public Guid PersonID { get; set; }

        public int MaterialID { get; set; }

        public DateTime DateConsuption { get; set; }

        public int Count { get; set; }

        [StringLength(128)]
        public string Notes { get; set; }

        public int? ManagerID { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual Material Material { get; set; }

        public virtual Person Person { get; set; }
    }
}
