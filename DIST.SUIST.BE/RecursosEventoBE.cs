using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
   public class RecursosEventoBE
    {
        #region Variables

        public int value { get; set; }
        public string text { get; set; }
        public string color { get; set; }

        #endregion

        #region Objetos
        
        public List<RecursosEventoBE> lstRecursosEvento { get; set; }

        #endregion
    }
}
