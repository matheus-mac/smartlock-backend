using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartlock_backend.DTOS
{
    public class DTOVerificaUsuarioVinculado
    {
        public string RFIDCard { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int UsuarioId { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }
        public int PerfilId { get; set; }
    }
}