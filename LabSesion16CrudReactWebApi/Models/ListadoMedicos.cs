using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabSesion16CrudReactWebApi.Models
{
    public class ListadoMedicos
    {
        public string codmed { get; set; }
        public string codesp { get; set; }
        public string especialidad { get; set; }
        public string nommed { get; set; }
        public int? anioColegiatura { get; set; }
        public string coddis { get; set; }
        public string distrito { get; set; }
        public int? estado { get; set; }
    }
}
