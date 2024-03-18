using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

public class MyDbContext : DbContext
{
    public MyDbContext() : base("Data Source=.;Initial Catalog=GestionEvenements;Integrated Security=True") // Replace with your actual connection string
    {
    }

    public DbSet<Evenement> Evenements { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Inscription> Inscriptions { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<IntervenantSession> IntervenantsSessions { get; set; }
    public DbSet<ParticipantSession> ParticipantsSessions { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inscription>()
            .HasRequired(i => i.Evenement)
            .WithMany(e => e.Inscriptions)
            .HasForeignKey(i => i.IdEvenement);

        modelBuilder.Entity<Inscription>()
            .HasRequired(i => i.Participant)
            .WithMany(p => p.Inscriptions)
            .HasForeignKey(i => i.IdParticipant);

        modelBuilder.Entity<Session>()
            .HasRequired(s => s.Evenement)
            .WithMany(e => e.Sessions)
            .HasForeignKey(s => s.IdEvenement);

        modelBuilder.Entity<IntervenantSession>()
            .HasRequired(i => i.Session)
            .WithMany(s => s.IntervenantsSessions)
            .HasForeignKey(i => i.IdSession);

        modelBuilder.Entity<ParticipantSession>()
            .HasRequired(p => p.Session)
            .WithMany(s => s.ParticipantsSessions)
            .HasForeignKey(p => p.IdSession);
    }
}

public class Evenement
{
    public int IdEvenement { get; set; }
    public string Libelle { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public string Lieu { get; set; }
    public string Organisateur { get; set; }
    public string SiteWeb { get; set; }

    public virtual ICollection<Inscription> Inscriptions { get; set; }
    public virtual ICollection<Session> Sessions { get; set; }
}

public class Participant
{
    public int IdParticipant { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string Affiliation { get; set; }

    public virtual ICollection<Inscription> Inscriptions { get; set; }
}

public class Inscription
{
    public int IdInscription { get; set; }
    public int IdEvenement { get; set; }
    public int IdParticipant { get; set; }
    public DateTime DateInscription { get; set; }

    public virtual Evenement Evenement { get; set; }
    public virtual Participant Participant { get; set; }
}

public class Session
{
    public int IdSession { get; set; }
    public string Libelle { get; set; }
    public int IdEvenement { get; set; }

    public virtual Evenement Evenement { get; set; }
    public virtual ICollection<IntervenantSession> IntervenantsSessions { get; set; }
    public virtual ICollection<ParticipantSession> ParticipantsSessions { get; set; }
}

public class IntervenantSession
{
    public int IdIntervenantSession { get; set; }
    public int IdSession { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }

    public virtual Session Session { get; set; }
}

public class ParticipantSession
{
    public int IdParticipantSession { get; set; }
    public int IdSession { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }

    public virtual Session Session { get; set; }
}
