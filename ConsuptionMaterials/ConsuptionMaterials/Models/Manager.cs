namespace ConsuptionMaterials.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manager")]
    public partial class Manager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manager()
        {
            Consuptions = new HashSet<Consuption>();
        }

        public int Id { get; set; }

        public Guid PersonId { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        public short? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consuption> Consuptions { get; set; }

        public virtual Person Person { get; set; }
    }
}
