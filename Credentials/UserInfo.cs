using System.Collections.Generic;

namespace RaizenTestFuncional.Credentials
{
    /// <summary>
    /// Permite acesso aos dados informados no Specflow Json e nos dados recebidos via Cofre de Senhas
    /// </summary>
    public static class UserInfo
    {
        public static List<UserCredentials> credentials = new List<UserCredentials>();
        public static List<UserData> data = new List<UserData>();
    }
}
