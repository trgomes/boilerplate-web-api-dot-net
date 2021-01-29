using System;
using System.Security.Cryptography;

namespace Solution.Infra.CrossCuting.Helpers
{
    public static class SecurePasswordHasher
    {
        /// <summary>
        /// Tamanho do salt.
        /// </summary>
        private const int SALT_SIZE = 16;

        /// <summary>
        /// Tamanho do hash.
        /// </summary>
        private const int HASH_SIZE = 20;

        /// <summary>
        /// Cria um hash de uma senha.
        /// </summary>
        /// <param name="password">password.</param>
        /// <param name="iterations">Número de iterações.</param>
        /// <returns>Hash.</returns>
        private static string Hash(string password, int iterations)
        {
            // Cria salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SALT_SIZE]);

            // Cria hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HASH_SIZE);

            // Combina salt e hash
            var hashBytes = new byte[SALT_SIZE + HASH_SIZE];
            Array.Copy(salt, 0, hashBytes, 0, SALT_SIZE);
            Array.Copy(hash, 0, hashBytes, SALT_SIZE, HASH_SIZE);

            // Converte para base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Formata hash com informações extras
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }

        /// <summary>
        /// Cria um hash de uma senha com 10000 iterações
        /// </summary>
        /// <param name="password">password.</param>
        /// <returns>Hash.</returns>
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// Verifica se o hash é suportado.
        /// </summary>
        /// <param name="hashString">hash.</param>
        /// <returns>É suportado?</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }

        /// <summary>
        /// Verifica uma senha em um hash.
        /// </summary>
        /// <param name="password">password.</param>
        /// <param name="hashedPassword">hash.</param>
        /// <returns>Pode ser verificado?</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            // Verifica hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Extrai iteração e string Base64
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Obtem bytes de hash
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Obtem salt
            var salt = new byte[SALT_SIZE];
            Array.Copy(hashBytes, 0, salt, 0, SALT_SIZE);

            // Cria hash com o salt fornecido
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HASH_SIZE);

            // Obtem resultado
            for (var i = 0; i < HASH_SIZE; i++)
            {
                if (hashBytes[i + SALT_SIZE] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
