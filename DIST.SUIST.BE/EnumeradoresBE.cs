using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class EnumeradoresBE
    {
        #region Enumeradores
        
        public enum enumPerfiles
        {
            Administrador = 1,
            Jefe = 2,
            Secretaria = 3,
            Abogado = 4,
            Practicante = 5
        }

        public enum enumCatalogos
        {
            Autorizadores = 1,
            Estado_Tasacion = 2,
            Estado_File = 3,
            Tipo_File_Condicion = 4,
            Ubicacion_Existencia = 5,
            Estado_Existencia = 23,
            Colores = 30,
            EstadosCiviles = 37
        }             

        public enum enumTipoCombo
        {
            Seleccionar = 1,
            Todos = 2,
            Normal = 3
        }

        public enum enumTipoFacturacion
        {
            Plana = 1,
            MontoxHora = 2,
            Mixto = 3
        }

        #endregion
    }
}
