//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quick_Med_Kit.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuickMedkitEntities : DbContext
    {
        public QuickMedkitEntities()
            : base("name=QuickMedkitEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }
        public virtual DbSet<Medicament> Medicament { get; set; }
        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<Livreur> Livreur { get; set; }
    }
}
