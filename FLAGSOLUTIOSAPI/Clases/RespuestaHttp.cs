namespace sitma.Models.Generic
{
    public class RespuestaHttp
    {
        public bool Exito { get; set; }
        public object Data { get; set; }
        public string Mensaje { get; set; }
        public string MensajeInterno { get; set; }
    }
}
