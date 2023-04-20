using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPedidos.Models
{
    public class Producto
    {
        [Key]
        [Display (Name = "Código")]
        public int Id { get; set; }


        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre debe tener máximo 50 carácteres")]
        [Display(Name = "Descripción")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0,double.MaxValue,ErrorMessage = "El precio debe ser un valor mayor a cero")]
        [Column(TypeName="decimal(10, 2)")]
        public decimal Precio { get; set; }

        public ICollection<DetalleOrden>? ProductosOrden { get; set; } 

    }
}
