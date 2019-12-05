using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaElecciones.Models
{
    [Table("Partidos")]
    public class Partido
    {
        [Key]
        public int PartidoID { get; set; }
        [Required(ErrorMessage = "Las siglas es un campo obligatorio")]
        public string Sigla { get; set; }
        [Required(ErrorMessage = "El nombre del partido es un campo obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El color del partido es un campo obligatorio")]
        public string Color{ get; set; }

        public List<Candidato> Candidatos { get; set; }
    }
}
