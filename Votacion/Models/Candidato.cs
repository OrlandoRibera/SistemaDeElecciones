using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaElecciones.Models
{
    [Table("Candidatos")]
    public class Candidato
    {
        [Key]
        public int CandidatoID { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es un campo obligatorio")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage ="El Tipo de voto es obligatorio")]
        [ForeignKey("TipoVotoID")]
        public int TipoVotoID { get; set; }

        [Required(ErrorMessage = "El partido es obligatorio")]
        [Display(Name="Partido")]
        public int PartidoID { get; set; }

        public virtual TipoVoto TipoVoto { get; set; }
        public virtual Partido Partido { get; set; }
    }
}

 