//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace smartlock_backend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fechadura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fechadura()
        {
            this.Acessos = new HashSet<Acessos>();
            this.Invasoes = new HashSet<Invasoes>();
        }
    
        public string EnderecoMAC { get; set; }
        public Nullable<int> Versao { get; set; }
        public Nullable<System.DateTime> DataDeAtivacao { get; set; }
        public int NumeroSerial { get; set; }
        public string IdentificadorFechadura { get; set; }
        public Nullable<int> UsuarioId { get; set; }
        public Nullable<int> EnderecoId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acessos> Acessos { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invasoes> Invasoes { get; set; }
    }
}
