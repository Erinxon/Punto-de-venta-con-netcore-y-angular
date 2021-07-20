namespace WebApiPuntoVenta.Dtos.ComprasDto
{
    public class GetDetalleCompraDto
    {
        public int Id { get; set; }
        public decimal? Costo { get; set; }
        public int? Cantidad { get; set; }
        public long? IdCompra { get; set; }
        public long? IdProducto { get; set; }
    }
}