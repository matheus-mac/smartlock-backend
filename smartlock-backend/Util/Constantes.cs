using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartlock_backend.Util
{
    public class Constantes
    {
        public static string EmailUsuarioNaoVinculadoTexto = "O usuario {0}- {1} não vinculado à fechadura {2}- {3} tentou o acesso em {4}";
        public static string EmailUsuarioNaoVinculadoAssunto = "Usuario não vinculado tentou acesso";

        public static string EmailFalhaNoReconhecimentoPorVideoAssunto = "Autenticação por vídeo falhou";
        public static string EmailFalhaNoReconhecimentoPorVideoTexto = "O usuario {0}- {1} tentou realizar" +
            "a autenticação por vídeo na fechadura {2}- {3} em {4} porém não obteve sucesso.";

        public static string EmailRegistraInvasaoAssunto = "(Urgente) Fechadura Invadida!";
        public static string EmailRegistraInvasaoTexto = "A fechadura {0}- {1} foi invadida em {2} " +
            "O vídeo registrado está em: {3}.";

    }
}