namespace Abstracciones.Interfaces.Reglas
{
    public interface IContrasenaRegla
    {
        string Hashear(string contrasena);
        bool Verificar(string contrasena, string hashGuardado);
    }
}
