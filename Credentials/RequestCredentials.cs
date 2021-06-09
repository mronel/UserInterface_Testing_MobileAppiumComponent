using Newtonsoft.Json.Linq;
using OAuth;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RaizenTestFuncional.Credentials
{
    public class RequestCredentials
    {
        /// <summary>
        /// Url de consulta ao Cofre de Senhas
        /// </summary>
        public const string urlSafeCredentials = "https://secpwd1.minhati.com.br/iso/coe/senha/";

        /// <summary>
        /// Permite consulta o Cofre de Senhas e obter os dados do(s) usuário(s) informados
        /// </summary>
        /// <returns> Retorna os dados obtidos do Cofre de Senhas </returns>
        public static void GetCredentials()
        {
            for (var i = 0; i <= UserInfo.credentials.Count - 1; i++)
            {
                var request = CreateRequest("GET", urlSafeCredentials + UserInfo.credentials[i].Id, UserInfo.credentials[i].ConsumerKey, UserInfo.credentials[i].ConsumerSecret, UserInfo.credentials[i].TokenKey, UserInfo.credentials[i].TokenSecret);
                var response = GetResponse(request);
                SafeCredentialsData(response);
            }
        }

        /// <summary>
        /// Cria a request de solicitação ao Cofre de Senhas usando OAuth 1.0
        /// </summary>
        /// <param name="httpVerb"></param>
        /// <param name="url"></param>
        /// <param name="ConsumerKey"></param>
        /// <param name="ConsumerSecret"></param>
        /// <param name="TokenKey"></param>
        /// <param name="TokenSecret"></param>
        /// <returns></returns>
        private static OAuthRequest CreateRequest(string httpVerb, string url, string ConsumerKey, string ConsumerSecret, string TokenKey, string TokenSecret)
        {
            OAuthRequest request = OAuthRequest.ForProtectedResource(httpVerb, ConsumerKey, ConsumerSecret, TokenKey, TokenSecret);
            request.RequestUrl = url;
            return request;
        }

        /// <summary>
        /// Obtém o response da request ao Cofre de Senhas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string GetResponse(OAuthRequest request)
        {
            try
            {
                var auth = request.GetAuthorizationHeader();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", auth);
                    var result = client.GetStringAsync(request.RequestUrl).GetAwaiter().GetResult();

                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("[Error] Dados obtidos vazios ou nulos... verificar os dados informados.");
                    }
                };
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("[Error] Erro ao retornar o response com os dados do usuário..." + e.Message + " - " + e.StackTrace);
            }
        }

        /// <summary>
        /// Tratamento do response obtido e inserção na classe UserInfo dos valores para acesso via Template.
        /// </summary>
        /// <param name="json"></param>
        private static void SafeCredentialsData(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                UserData userData = new UserData();

                JObject jsonResponse = JObject.Parse(json);
                JObject jsonObject = (JObject)jsonResponse["senha"];

                userData.username = jsonObject.GetValue("username").ToString();
                userData.senha = jsonObject.GetValue("senha").ToString();

                UserInfo.data.Add(userData);
            }
            else
            {
                throw new Exception("[Erro] Dados do usuário não foram retornados do cofre de senhas... Verificar os dados informados no arquivo specflow.json. Se o problema persistir, informar a equipe de Qualidade.");
            }
        }
    }
}
