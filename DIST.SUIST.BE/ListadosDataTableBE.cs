namespace DIST.SUIST.BE
{
    public class ListUsuariosBE
    {
        public int col_IdUsuario { get; set; }
        public int col_Estado { get; set; }
        public string col_NombreCompleto { get; set; }
        public string col_Denominacion { get; set; }
        public string col_srtEtado { get; set; }
    }

    public class ListClientesBE
    {
        public int Nro { get; set; }
        public int col_IdCliente { get; set; }
        public string col_Tipo { get; set; }
        public string col_Descripcion { get; set; }
        public string col_DocumentoIdentidad { get; set; }
        public string col_NombreCompleto { get; set; }
        public string col_Direccion { get; set; }
        public string col_Email { get; set; }
        public string col_TelefonoFijo { get; set; }
        public string col_TelefonoCelular { get; set; }
        public string col_SitioWeb { get; set; }
        public string col_Cargo { get; set; }
        public string col_Principal { get; set; }
        public string col_FechaInicioContrato { get; set; }
        public string col_FechaFinContrato { get; set; }
    }

    public class ListContactosBE
    {
        public int col_IdContacto { get; set; }
        public int col_Principal { get; set; }
        public string col_NombreCompleto { get; set; }
    }

    public class ListProyectosBE
    {
        public int Nro { get; set; }
        public int col_IdProyecto { get; set; }
        public string col_Cliente { get; set; }
        public string col_NombreProyecto { get; set; }
        public string col_Precio { get; set; }
    }

    public class ListGastosBE
    {
        public int Nro { get; set; }
        public int col_IdGasto { get; set; }
        public string col_NombreCliente { get; set; }
        public string col_NombreProyecto { get; set; }
        public string col_NombreAbogado { get; set; }
        public string col_Fecha { get; set; }
        public string col_Monto { get; set; }
    }

    public class ListTipoActividadesBE
    {
        public int Nro { get; set; }
        public int col_IdTipoActividad { get; set; }
        public string col_Nombre { get; set; }
        public string col_Precio { get; set; }
    }

    public class ListActividadesBE
    {
        public int Nro { get; set; }
        public string col_NombreUsuario { get; set; }
        public string col_NombreCliente { get; set; }
        public string col_NombreProyecto { get; set; }
        public string col_NombreTipoActividad { get; set; }
        public string col_Fecha { get; set; }
        public double col_Horas { get; set; }
    }
}
