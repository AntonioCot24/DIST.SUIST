using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class MensajeWrapperBE
    {
        public string Resultado { get; set; }
        public string Mensaje { get; set; }
        public string ObjEmbedded { get; set; }
        public string Listado { get; set; }
    }

    public class MensajeRespuestaBE<T> : MensajeWrapperBE
    {
        public IEnumerable<T> Rows { get; set; }
    }
}
