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
    
    public partial class SiparisKalem
    {
        public int SiparisKalemId { get; set; }
        public int refSiparisId { get; set; }
        public Nullable<int> refUrunId { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<int> Tutar { get; set; }
    
        public virtual Siparis Siparis { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}