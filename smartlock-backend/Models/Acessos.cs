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
    
    public partial class Acessos
    {
        public string DataHora { get; set; }
        public string AcessosId { get; set; }
        public Nullable<int> NumeroSerial { get; set; }
        public Nullable<int> UsuarioId { get; set; }
    
        public virtual Fechadura Fechadura { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
