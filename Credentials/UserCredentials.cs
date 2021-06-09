namespace RaizenTestFuncional.Credentials
{
    /// <summary>
    /// Informações dos usuários para consulta no Cofre de Senhas
    /// </summary>
    public class UserCredentials
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string TokenKey { get; set; }
        public string TokenSecret { get; set; }
    }
}
