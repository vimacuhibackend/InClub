using Microsoft.AspNetCore.Http;
using System;

namespace Inclub.Api.Application.Commands
{
    public class AuditoriaCommand
    {
        public Guid UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string IpRegistro { get; set; }

        public AuditoriaCommand()
        {
            this.UsuarioRegistro = Guid.NewGuid();
            this.FechaRegistro = DateTime.Now;
            this.IpRegistro = "::1";
        }
    }
}
