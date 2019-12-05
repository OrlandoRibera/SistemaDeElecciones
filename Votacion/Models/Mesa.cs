using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaElecciones.Models
{
    [Table("Mesas")]
    public class Mesa
    {
        [Key]
        public int MesaID { get; set; }
        [Required(ErrorMessage ="El numero de mesa es obligatorio")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "El número no debe contener letras")]
        public long Numero { get; set; }
        public string CodigoQR { get; set; }
        [DisplayName("Deshabilidata")]
        public bool estadoMesa { get; set; }

    }
}
