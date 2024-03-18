using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Gestion_Evénement
{
    public partial class GestionEvenement : DbContext
    {
        public GestionEvenement()
            : base("name=GestionEvenement")
        {
        }

        public virtual DbSet<Evenement> Evenements { get; set; }
        public virtual DbSet<Inscription> Inscriptions { get; set; }
        public virtual DbSet<IntervenantsSession> IntervenantsSessions { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<ParticipantsSession> ParticipantsSessions { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evenement>()
               .Property(e => e.IdEvenement);
               

            modelBuilder.Entity<Evenement>()
                .Property(e => e.Libelle)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .Property(e => e.Lieu)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .Property(e => e.Organisateur)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .Property(e => e.SiteWeb)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .HasMany(e => e.Inscriptions)
                .WithRequired(e => e.Evenement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evenement>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Evenement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Participant>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Participant>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Participant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Participant>()
                .Property(e => e.Affiliation)
                .IsUnicode(false);

            modelBuilder.Entity<Participant>()
                .HasMany(e => e.Inscriptions)
                .WithRequired(e => e.Participant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .HasMany(e => e.IntervenantsSessions)
                .WithRequired(e => e.Session)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .HasMany(e => e.ParticipantsSessions)
                .WithRequired(e => e.Session)
                .WillCascadeOnDelete(false);
        }
    }
}
