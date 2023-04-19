using System.ComponentModel.DataAnnotations;

namespace MVCPedidos.Models
{
    public class Cliente
    {
        [Key]
        [Display(Name = "Identificación")]
        public int Id { get; set; }

        [Required (ErrorMessage = "El nombre es requerido")]
        [MaxLength(30, ErrorMessage = "El nombre debe tener máximo 30 carácteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(30, ErrorMessage = "El apellido debe tener máximo 30 carácteres")]
        public string Apellido { get; set; }

        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ingresar un email válido")]
        public string? Email { get; set; }

        public ICollection<Orden>? ordenes { get; set; }
    }
}
