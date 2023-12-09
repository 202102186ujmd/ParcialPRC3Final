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
        public DateTime FechadeCreacion { get; set; }
        [Required]
        public int IdEstado { get; set; }
    }
}
