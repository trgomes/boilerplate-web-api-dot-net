namespace Solution.Api.StartupConfiguration
{
    public class AppSettings
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Chave para leitura e criação do token de autenticação
        /// </summary>
        public string Secret { get; set; }
    }
}
