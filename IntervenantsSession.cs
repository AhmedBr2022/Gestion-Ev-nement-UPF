namespace Gestion_Evénement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IntervenantsSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdIntervenantSession { get; set; }

        public int IdSession { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public virtual Session Session { get; set; }
    }
}
