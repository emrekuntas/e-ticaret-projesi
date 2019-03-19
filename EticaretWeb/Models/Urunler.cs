//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EticaretWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            this.Sepet = new HashSet<Sepet>();
            this.SiparisKalem = new HashSet<SiparisKalem>();
        }
    
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<int> RefKategoriID { get; set; }
        public string UrunAciklama { get; set; }
        public Nullable<int> UrunFiyat { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisKalem> SiparisKalem { get; set; }
    }
}