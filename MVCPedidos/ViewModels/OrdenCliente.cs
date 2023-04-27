using System.ComponentModel.DataAnnotations;

namespace MVCPedidos.ViewModels
{
    public class OrdenCliente
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        [Required(ErrorMessage = "El cliente es requerido")]
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public ICollection<LineaDetalle>? Detalles { get; set; }
    }
}
