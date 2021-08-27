using System;
using System.Collections.Generic;

#nullable disable

namespace LabSesion16CrudReactWebApi.Models
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            Medicos = new HashSet<Medico>();
        }

        public string Codesp { get; set; }
        public string Nomesp { get; set; }
        public decimal? Costo { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
