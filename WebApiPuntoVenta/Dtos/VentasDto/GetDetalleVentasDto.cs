namespace WebApiPuntoVenta.Dtos.VentasDto
{
    public class GetDetalleVentasDto
    {
        public int Id { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public long? IdVenta { get; set; }
        public long? IdProducto { get; set; }
    }
}