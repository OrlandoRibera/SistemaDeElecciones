using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaElecciones.Models
{
    [Table("TipoVoto")]
    public class TipoVoto
    {
        [Key]
        public int TipoVotoID { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
