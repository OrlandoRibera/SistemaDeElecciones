using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaElecciones.Models
{
    [Table("Jurados")]
    public class Jurado
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "El CI es un campo obligatorio")]
        public string CI { get; set; }

        [Required(ErrorMessage = "El Nombre es un campo obligatorio")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El Apellido es un campo obligatorio")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El Usuario es un campo obligatorio")]
        public string  Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es un campo obligatorio")]
        [Display(Name ="Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es un campo obligatorio")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La mesa es un campo obligatorio")]
        [Display(Name ="Mesa")]
        public int MesaID { get; set; }

        public virtual Mesa Mesa { get; set; }
    }
}
