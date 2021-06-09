using System;
using RaizenTestFuncional.Credentials;

namespace RaizenTestFuncional.Setup
{
    public class ValidateConfigurationData
    {   
        /// <summary>
        /// Retorna Enum do paramêtro Project
        /// </summary>
        /// <returns></returns>
        public static EnumProject ProjectParam()
        {
            var verify = Enum.IsDefined(typeof(EnumProject), ConfigurationData.configurationData[0].project);

            if(!verify) 
            {
                throw new System.Exception("[Erro de Configuração - Project] Verificar o parâmetro <Project> em specflow.json - deverá ser 'web' ou 'mobile'.");
            }

            return (EnumProject)Enum.Parse(typeof(EnumProject), ConfigurationData.configurationData[0].project, true);
        }

        /// <summary>
        /// Retorna o Enum do parâmetro Browser
        /// </summary>
        /// <returns></returns>
        public static EnumBrowser BrowserParam()
        {            
            var verify = Enum.IsDefined(typeof(EnumBrowser), ConfigurationData.configurationData[0].browser);

            if (!verify)
            {
                throw new System.Exception("[Erro de Configuração - Browser] Verificar o parâmetro <Browser> em specflow.json - deverá ser firefox, firefoxheadless, chrome, chromeheadless, internetexplorer ou edge.");
            }

            return (EnumBrowser)Enum.Parse(typeof(EnumBrowser), ConfigurationData.configurationData[0].browser, true);
        }

        /// <summary>
        /// Retorna o Enum do parâmetro DevOps
        /// </summary>
        /// <returns></returns>
        public static EnumDevOps DevOpsParam()
        {
            var verify = Enum.IsDefined(typeof(EnumDevOps), ConfigurationData.configurationData[0].devOps);
            
            if(!verify)
            {
                throw new System.Exception("[Erro de Configuração - DevOps] Verificar o parâmetro <DevOps> em specflow.json - deverá ser 'local', 'jenkins' ou 'azure'.");
            }

            return (EnumDevOps)Enum.Parse(typeof(EnumDevOps), ConfigurationData.configurationData[0].devOps, true);
        }

        /// <summary>
        /// Retorna booleano para ativação do report local
        /// </summary>
        /// <returns></returns>
        public static bool ActivedReportTestParam()
        {
            if(!ConfigurationData.configurationData[0].activedReportTest.ToString().ToLower().Equals("true") &&
               !ConfigurationData.configurationData[0].activedReportTest.ToString().ToLower().Equals("false") &&
                ConfigurationData.configurationData[0].activedReportTest.ToString().ToLower().Equals("") ||
                ConfigurationData.configurationData[0].activedReportTest.ToString().ToLower().Equals(null))
            {
                throw new Exception("[Erro de Configuração - ActivedReportTest] Verificar o parâmetro <ActivedReportTest> em specflow.json - Deverá ser 'true' ou 'false'");
            }

            return ConfigurationData.configurationData[0].activedReportTest;
        }

        /// <summary>
        /// Retorna o path da pasta de reports configurada
        /// </summary>
        /// <returns></returns>
        public static string ReportFolderParam()
        {
            if(ConfigurationData.configurationData[0].reportFolder.Equals("") ||
               ConfigurationData.configurationData[0].reportFolder.Equals(null))
            {
                throw new Exception("[Erro de Configuração - ReportFolder] Verificar o parâmetro <ReportFolder>, não poderá ser vazio.");
            }

            return ConfigurationData.configurationData[0].reportFolder;
        }

        /// <summary>
        /// Retorna enum do parâmetro Screenshot
        /// </summary>
        /// <returns></returns>
        public static EnumScreenshot ScreenshotParam()
        {
            var verify = Enum.IsDefined(typeof(EnumScreenshot), ConfigurationData.configurationData[0].screenshots);

            if (!verify)
            {
                throw new Exception("[Erro de Configuração - Screenshot] Verificar o parâmetro <Screenshot> deverá ser 'error' ou 'all'");
            }

            return (EnumScreenshot)Enum.Parse(typeof(EnumScreenshot), ConfigurationData.configurationData[0].screenshots, true);
        }

       /// <summary>
       /// Retorna a url configurada
       /// </summary>
       /// <returns></returns>
        public static string UrlParam()
        {
            if (ConfigurationData.configurationData[0].url.Equals(""))
            {
                throw new Exception("[Erro de Configuração - Url] Verificar o parâmetro <URL> em specflow.json - Url inválida");
            }

            return ConfigurationData.configurationData[0].url;
        }

        /// <summary>
        /// Retorna o timeout configurado
        /// </summary>
        /// <returns></returns>
        public static TimeSpan TimeOutParam()
        {
            bool verify = int.TryParse(ConfigurationData.configurationData[0].timeOut.Seconds.ToString(), out int v);
            if (!verify)
            {
                throw new Exception("[Erro de Configuração - TimeOut] Verificar o parâmetro <TimeOut> - deverá informar o valor em segundos.");
            }
            
            if (ConfigurationData.configurationData[0].timeOut.Equals("") || 
                ConfigurationData.configurationData[0].timeOut.Equals(null))
            {
                return TimeSpan.FromSeconds(60);
            }

            return ConfigurationData.configurationData[0].timeOut;
        }

        /// <summary>
        /// Retorna o valor do parâmetro JiraRegisterTestCycle
        /// </summary>
        /// <returns></returns>
        public static bool JiraRegisterTestCycleParam()
        {
            if (!ConfigurationData.configurationData[0].jiraRegisterTestCycle.ToString().ToLower().Equals("true") &&
                !ConfigurationData.configurationData[0].jiraRegisterTestCycle.ToString().ToLower().Equals("false") &&
                 ConfigurationData.configurationData[0].jiraRegisterTestCycle.ToString().ToLower().Equals("") ||
                 ConfigurationData.configurationData[0].jiraRegisterTestCycle.ToString().ToLower().Equals(null))
            {
                throw new Exception("[Erro de Configuração - JiraRegisterTestCycle] Verificar o parâmetro <JiraRegisterTestCycle> no specflow.json - Deverá ser 'true' ou 'false'");
            }
            
            return ConfigurationData.configurationData[0].jiraRegisterTestCycle;
        }

        /// <summary>
        /// Retorna o valor do parâmetro JiraProjectKeyParam
        /// </summary>
        /// <returns></returns>
        public static string JiraProjectKeyParam()
        {
            if(ConfigurationData.configurationData[0].jiraProjectKey.Equals("") ||
               ConfigurationData.configurationData[0].jiraProjectKey.Equals(null))
            {
                throw new Exception("[Erro de Configuração - JiraProjectKey] Verificar o parâmetro <JiraProjectKey> em specflow.json - Parâmetro não poderá ser vazio.");
            }

            return ConfigurationData.configurationData[0].jiraProjectKey;
        }

        /// <summary>
        /// Retorna o valor do parâmetro JiraTestRunName
        /// </summary>
        /// <returns></returns>
        public static string JiraTestRunNameParam()
        {
            if(ConfigurationData.configurationData[0].jiraTestRunName.Equals("") ||
               ConfigurationData.configurationData[0].jiraTestRunName.Equals(null))
            {
               throw new Exception("[Erro de Configuração - JiraTestRunName] Verificar o parâmetro <JiraTestRunName> em specflow.json - Parâmetro não poderá ser vazio.");
            }
            return ConfigurationData.configurationData[0].jiraTestRunName;
        }

        /// <summary>
        /// Retorna o valor do parâmetro NodePath
        /// </summary>
        /// <returns></returns>
        public static string NodePathParam()
        {
            if (ConfigurationData.configurationData[0].nodePath.Equals("") ||
                ConfigurationData.configurationData[0].nodePath.Equals(null))
            {
                throw new System.Exception("[Erro de Configuração - NodePath] Verificar o parâmetro <NodePath> em specflow.json, não poderá ser vazio.");
            }

            return ConfigurationData.configurationData[0].nodePath;
        }

        /// <summary>
        /// Retorna o valor do parâmetro AppiumPath
        /// </summary>
        /// <returns></returns>
        public static string AppiumPath()
        {
            if (ConfigurationData.configurationData[0].appiumPath.Equals("") ||
                ConfigurationData.configurationData[0].appiumPath.Equals(null))
            {
                throw new System.Exception("[Erro de Configuração - AppiumPath] Verificar o parâmetro <AppiumPath> em specflow.json, não poderá ser vazio.");
            }

            return ConfigurationData.configurationData[0].appiumPath;
        }

        /// <summary>
        /// Retorna o valor do parâmetro MobileExecution
        /// </summary>
        /// <returns></returns>
        public static string MobileExecutionParam()
        {

            if (!ConfigurationData.configurationData[0].mobileExecution.ToLower().Equals("emulator") &&
                !ConfigurationData.configurationData[0].mobileExecution.ToLower().Equals("device") &&
                ConfigurationData.configurationData[0].mobileExecution.ToLower().Equals("") ||
                ConfigurationData.configurationData[0].mobileExecution.ToLower().Equals(null))
            {
                throw new Exception("[Erro de Configuração - MobileExecution] Verificar o parâmetro <MobileExecution> em specflow.json, deverá ser 'emulator' ou 'device'.");
            }
            return ConfigurationData.configurationData[0].mobileExecution.ToLower();
        }

        /// <summary>
        /// Retorna o valor do parâmetro MobileAndroid
        /// </summary>
        /// <returns></returns>
        public static bool MobileAndroidParam()
        {
            if (!ConfigurationData.configurationData[0].mobileAndroid.ToString().ToLower().Equals("true") &&
                !ConfigurationData.configurationData[0].mobileAndroid.ToString().ToLower().Equals("false") &&
                 ConfigurationData.configurationData[0].mobileAndroid.ToString().ToLower().Equals("") ||
                 ConfigurationData.configurationData[0].mobileAndroid.ToString().ToLower().Equals(null))
            {
                throw new Exception("[Erro de Configuração - MobileAndroid] Verificar o parâmetro <MobileAndroid> em specflow.json - Deverá ser 'true' ou 'false'");
            }

            return ConfigurationData.configurationData[0].mobileAndroid;
        }

        /// <summary>
        /// Retorna o valor do parâmetro MobileiOSParam
        /// </summary>
        /// <returns></returns>
        public static bool MobileiOSParam()
        {
            if (!ConfigurationData.configurationData[0].mobileiOS.ToString().ToLower().Equals("true") &&
                !ConfigurationData.configurationData[0].mobileiOS.ToString().ToLower().Equals("false") &&
                 ConfigurationData.configurationData[0].mobileiOS.ToString().ToLower().Equals("") ||
                 ConfigurationData.configurationData[0].mobileiOS.ToString().ToLower().Equals(null))
            {
                throw new Exception("[Erro de Configuração - MobileiOS] Verificar o parâmetro <MobileiOS> em specflow.json - Deverá ser 'true' ou 'false'");
            }
            
            return ConfigurationData.configurationData[0].mobileiOS;
        }

        /// <summary>
        /// Retorna o valor do parâmetro SafeCredentials
        /// </summary>
        /// <returns></returns>
        public static bool SafeCredentialsParam()
        {
            if (!ConfigurationData.configurationData[0].safeCredentials.ToString().ToLower().Equals("true") &&
                !ConfigurationData.configurationData[0].safeCredentials.ToString().ToLower().Equals("false") &&
                 ConfigurationData.configurationData[0].safeCredentials.ToString().ToLower().Equals("") ||
                 ConfigurationData.configurationData[0].safeCredentials.ToString().ToLower().Equals(null))
            {
                throw new Exception("[Erro de Configuração - SafeCrendentials] Verificar o parâmetro <SafeCrendentials> em specflow.json - Deverá ser 'true' ou 'false'");
            }

            return ConfigurationData.configurationData[0].safeCredentials;
        }

        /// <summary>
        /// Validação dos campos das Credenciais
        /// </summary>
       public static void CredentialsParam()
        {
            foreach(var val in UserInfo.credentials)
            {
                if (val.Name.Equals("") || val.Name.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - Credentials > Name] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.Id.Equals("") || val.Id.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - Credentials > Id] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.TokenKey.Equals("") || val.TokenKey.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - Credentials > TokenKey] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.ConsumerKey.Equals("") || val.ConsumerKey.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - Credentials > ConsumerKey] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.ConsumerSecret.Equals("") || val.ConsumerSecret.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - Credentials > ConsumerSecret] Verificar o parâmetro, não pode ser vazio.");
                }
            }
            
        }
    }
}
