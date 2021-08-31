using Dominio;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Ferney Leon Rojas", UserName = "ferneylr", Email = "ferney_leonr5@hotmail.es" };
                await  usuarioManager.CreateAsync(usuario, "Password123*");
            }
        }
    }
}
