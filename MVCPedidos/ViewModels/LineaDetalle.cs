namespace MVCPedidos.ViewModels
{
    public class LineaDetalle
    {
        public int IdDetalle { get; set; }
        public int Cantidad { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int ordenId { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get {
                return Cantidad * Precio;
            } }
    }
}
