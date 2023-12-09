using System.ComponentModel.DataAnnotations;

namespace Becas.Shared
{
    public class Solicitud
    {
        [Key]
        public int IdSolicitud { get; set; }
        [Required]
        public int IdSolicitante { get; set; }
        [Required]
        public DateTime FechadeCreacion { get; set; } = DateTime.Now; // Establecer por defecto la fecha actual

        [Required]
        public int IdEstado { get; set; } = 1; // Establecer por defecto el estado a 1


    }
}
