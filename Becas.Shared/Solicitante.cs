using System.ComponentModel.DataAnnotations;

namespace Becas.Shared
{
    #region Validacion Fecha Futura
    public class FechaFuturaValidaAttribute : ValidationAttribute
    {
        //Validacion de la fecha de nacimiento no sea mayor a la fecha actual
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? fechaNacimiento = (DateTime?)value;

            if (fechaNacimiento.HasValue && fechaNacimiento > DateTime.Now)
            {
                return new ValidationResult("La fecha no puede estar en el futuro.");
            }

            return ValidationResult.Success;
        }
    }
    #endregion

    #region Validacion Estado Civil
    public class EstadoCivilValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Validacion de estado civil
            var estadoCivil = value as string;
            //Validacion de estado civil
            if (estadoCivil != null)
            {
                var estadosValidos = new string[] { "Soltero", "Casado", "Divorciado", "Viudo" }; // Puedes agregar más estados según sea necesario
                if (!Array.Exists(estadosValidos, estado => estado.Equals(estadoCivil, StringComparison.OrdinalIgnoreCase)))
                {
                    return new ValidationResult("El estado civil no es válido.");
                }
            }
            return ValidationResult.Success;
        }
    }
    #endregion

    #region Tabla Solicitante
    public class Solicitante
    {
        [Key]
        public int IdSolicitante { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El nombre no cumple los requerimientos")]
       
        public string? Nombre { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El Apellido no cumple los requerimientos")]

        public string? Apellido { get; set; }

        [Required]
        [FechaFuturaValida(ErrorMessage = "La fecha de nacimiento no puede estar en el futuro.")]
        public DateTime FechaNacimiento { get; set; }
        [EstadoCivilValido(ErrorMessage = "El estado civil no es válido.")]
       
        public string? EstadoCivil { get; set; }
        [RegularExpression("^[0-9]{8}-[0-9]{1}$", ErrorMessage = "Formato de DUI no válido. Ejemplo: 12345678-9")]

        public string? Dui { get; set; }

        [StringLength(12, MinimumLength = 9, ErrorMessage = "El pasaporte no cumple los requerimientos. Ejemplo: ABC123456")]
        [RegularExpression("^[A-Z0-9]{9}$", ErrorMessage = "Formato de pasaporte no válido. Ejemplo: ABC123456")]

        public string? Pasaporte { get; set; }

        [Required(ErrorMessage = "La fecha de expedición del pasaporte es requerida.")]
        [FechaFuturaValida(ErrorMessage = "La fecha de expedición del pasaporte no puede estar en el futuro.")]
        public DateTime FechaExpedicionPasaporte { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "El lugar de expedición del pasaporte no cumple los requerimientos.")]
        public string? LugardeExpedicion { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "La dirección no cumple los requerimientos.")]
        public string? Direccion { get; set; }


        [EmailAddress(ErrorMessage = "El email no es válido.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El email no cumple los requerimientos.")]
        public string? Email { get; set; }

        [RegularExpression("^[0-9]{4}-[0-9]{4}$", ErrorMessage = "Formato de teléfono no válido. Ejemplo: 1234-5678")]
        public string? Telefono { get; set; }
    }
    #endregion
}
