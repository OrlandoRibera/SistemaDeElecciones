using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaElecciones.Models
{
    [Table("Votos")]
    public class Voto
    {
        [Key]
        public int VotoID { get; set; }

        [Required(ErrorMessage = "El candidato es un campo obligatorio")]
        [Display(Name = "Candidato")]
        public int CandidatoID { get; set; }

        [Required(ErrorMessage = "El usuario es un campo obligatorio")]
        [Display(Name = "Usuario")]
        public int UsuarioID { get; set; }
        public virtual Candidato Candidato { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
