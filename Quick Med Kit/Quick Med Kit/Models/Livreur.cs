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
    using System.Collections.Generic;
    
    public partial class Livreur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Livreur()
        {
            this.Commande = new HashSet<Commande>();
        }
    
        public int ID_Livreur { get; set; }
        public string Nom_Livreur { get; set; }
        public string Prenom_Livreur { get; set; }
        public string CIN { get; set; }
        public string Email_Livreur { get; set; }
        public Nullable<int> telephone_Livreur { get; set; }
        public Nullable<int> age { get; set; }
        public string Pswrd_telephone_Livreur { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public Nullable<bool> Isenligne { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commande { get; set; }
    }
}
