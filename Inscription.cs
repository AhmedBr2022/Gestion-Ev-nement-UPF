namespace Gestion_Evénement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inscription
    {
        [Key]
        public int IdInscription { get; set; }

        public int IdEvenement { get; set; }

        public int IdParticipant { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateInscription { get; set; }

        public virtual Evenement Evenement { get; set; }

        public virtual Participant Participant { get; set; }
    }
}
