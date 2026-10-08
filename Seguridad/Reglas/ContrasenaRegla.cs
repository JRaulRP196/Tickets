using Abstracciones.Interfaces.Reglas;
using System.Security.Cryptography;

namespace Reglas
{
    // PBKDF2-SHA256 con sal aleatoria por usuario. Formato guardado: "iteraciones.sal.hash" (base64).
    public class ContrasenaRegla : IContrasenaRegla
    {
        private const int TamanoSal = 16;
        private const int TamanoHash = 32;
        private const int Iteraciones = 600_000;

        public string Hashear(string contrasena)
        {
            byte[] sal = RandomNumberGenerator.GetBytes(TamanoSal);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, TamanoHash);
            return $"{Iteraciones}.{Convert.ToBase64String(sal)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verificar(string contrasena, string hashGuardado)
        {
            var partes = hashGuardado.Split('.');
            if (partes.Length != 3 || !int.TryParse(partes[0], out int iteraciones))
                return false;

            byte[] sal = Convert.FromBase64String(partes[1]);
            byte[] esperado = Convert.FromBase64String(partes[2]);
            byte[] calculado = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, iteraciones, HashAlgorithmName.SHA256, esperado.Length);
            return CryptographicOperations.FixedTimeEquals(calculado, esperado);
        }
    }
}
