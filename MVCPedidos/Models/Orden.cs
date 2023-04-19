using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPedidos.Models
{
    public class Orden
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaPedido { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public ICollection<DetalleOrden>? ProductosOrden { get; set; }

    }
}
