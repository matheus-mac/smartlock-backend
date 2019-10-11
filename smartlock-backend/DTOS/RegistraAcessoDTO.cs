using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartlock_backend.DTOS
{
    public class RegistraAcessoDTO
    {
        public bool AcessoRegistrado { get; set; }
        public DateTime HoraAcesso { get; set; }
    }
}