using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Usuario
    {

        public Guid Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string Apellido1 { get; set; }
        [Required(ErrorMessage = "El segundo apellido es requerido")]
        public string Apellido2 { get; set; }
        [Required(ErrorMessage = "La contraseña es requerido")]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string Correo { get; set; }

    }

    public class UsuarioRequest : Usuario
    {
        [Required(ErrorMessage = "El rol es requerido")]
        public int IdRol { get; set; }
    }

    public class UsuarioResponse : UsuarioRequest
    {
        public string Rol { get; set; }
    }

    public class Login
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La contraseña es requerido")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string Correo { get; set; }
    }

}
