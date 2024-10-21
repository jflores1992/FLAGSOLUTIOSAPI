namespace FLAGSOLUTIOSAPI.Clases
{
    public class Conexion
    {
        private readonly IConfiguration _configuration;

        public Conexion(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public string MantenimientoDbConection()
        {
            try
            {
                string CN = _configuration.GetConnectionString("conexiondatabasemantenimiento");
                return CN;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
