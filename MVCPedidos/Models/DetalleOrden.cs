using System.ComponentModel.DataAnnotations;

namespace MVCPedidos.Models
{
    public class DetalleOrden
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1,int.MaxValue,ErrorMessage = "La cantidad debe ser mayor a cero")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El producto es requerido")]
        [Display(Name = "Código de Producto")]
        public int ProductoId { get; set; }

        public Producto? producto { get; set; }

        [Required(ErrorMessage = "La orden es requerida")]
        [Display(Name = "Código de Orden")]
        public int OrdenId { get; set; }
        public Orden? orden { get; set; }

    }
}
