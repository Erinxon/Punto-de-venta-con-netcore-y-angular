namespace WebApiPuntoVenta.Dtos.ComprasDto
{
    public class AddDetalleCompraDto
    {
        public decimal? Costo { get; set; }
        public int? Cantidad { get; set; }
        public long? IdProducto { get; set; }
    }
}