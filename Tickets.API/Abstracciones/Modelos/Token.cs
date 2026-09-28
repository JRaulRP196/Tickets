using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Token
    {

        public string AccessToken { get; set; }
        public bool ValidacionExitosa { get; set; }

    }

    public class TokenConfiguracion
    {
        [Required]
        [StringLength(100, MinimumLength = 32, ErrorMessage = "La clave debe tener entre 32 y 100 caracteres.")]
        public string Key { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }
        [Required]
        public double Expire { get; set; }

    }
}
