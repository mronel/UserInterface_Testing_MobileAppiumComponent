using OpenQA.Selenium;
using RaizenTestFuncional.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using RaizenTestFuncional.Driver;
using OpenQA.Selenium.Appium;

namespace RaizenTestFuncional
{
    /// <summary>
    /// Gerenciamento do webdriver
    /// </summary>
    public abstract class DriverFactory
    {
        #region Variáveis        

        public static IWebDriver driver;
        public static EnumBrowser browser;

        public static TimeSpan timeOut;
        public static string url;
        public static bool seleniumGrid;

        public static List<string> nodes;
        public static List<string> nodesBrowser;
        public static List<string> connectionPortNumber;

        #endregion

        #region Métodos

        /// <summary>
        /// Configurações das informações necessárias para execução
        /// </summary>
        public static void Enviroment(string _seleniumGrid = null, List<string> _nodes = null, List<string> _nodesBrowser = null, List<string> _connectionPortNumber = null)
        {
            //Definindo e configurando o ambiente Web
            if(ValidateConfigurationData.ProjectParam().ToString().Equals("web"))
            {
                DriverSetup.SetEnviromentParams(_seleniumGrid, _nodes, _nodesBrowser, _connectionPortNumber);
            }

            //Definindo e configurando o ambiente Mobile
            if (ValidateConfigurationData.ProjectParam().ToString().Equals("mobile"))
            {
                CreateMobileDriver();
            }
        }

        /// <summary>
        /// CreateWebDriver: cria uma instância do Webdriver para execução dos testes.
        /// Recupera todas as informações do arquivo "specflow.json" e configura o webdriver para execução.
        /// </summary>
        public static void CreateWebDriver()
        {
            if (ValidateConfigurationData.ProjectParam().ToString().Equals("web"))
            {
                if (seleniumGrid)
                {
                   DriverSetup.SeleniumGridConfig();
                }
                else
                {
                    DriverSetup.WebDriverConfig();
                }

                DriverSetup.ConfigWebDriver(driver);
                AccessUrl();
            }
            
        }

        /// <summary>
        /// Cria um driver mobile Android e/ou iOS
        /// </summary>
        private static void CreateMobileDriver()
        {
            if (ValidateConfigurationData.ProjectParam().ToString().Equals("web"))
            {
                throw new Exception("[Erro Projeto] Verificar o parâmetro <Project> no specflow.json - Para execução de projetos mobile, o parâmetro deverá ser setado com a opção 'mobile'");
            }

            if (ValidateConfigurationData.MobileAndroidParam())
            {
               MobileDriverSetup.AndroidAppiumInitialize();
            }

            if (ValidateConfigurationData.MobileiOSParam())
            {
                MobileDriverSetup.iOSAppiumInitialize();
            }
        }   

        /// <summary>
        /// Preenche o alert javascript com os dados de usuário da rede e realiza a autenticação via proxy.
        /// </summary>
        /// <param name="username">Usuário de rede</param>
        /// <param name="password">Senha do usuário de rede</param>
        public static void ProxyAuthentication(string username, string password)
        {
            driver.SwitchTo().Alert().SetAuthenticationCredentials(username, password);
        }

        /// <summary>
        /// AccessUrl: Acessa a Url definida em specflow.json - parâmetro [Url]
        /// OBS: não utilizar este método para acessar URLs. Ele é executado somente no início dos testes e automaticamente.
        /// Para acessar uma URL, utilizar o método IWebElementExtensions.GoToPage('url');
        /// </summary>
        private static void AccessUrl()
        {
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (WebDriverException)
            {
                throw new WebDriverException("Não foi possível realizar o acesso a URL informada. Por favor verifique as configurações em specflow.json, paramêtro [URL]");
            }
        }

        /// <summary>
        /// Recupera o driver instanciado.
        /// Permite acessar as configurações e ações do webdriver.
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetDriver()
        {
            return driver;
        }

        /// <summary>
        /// Recupera o driver Android instanciado.
        /// Permite acessar as configurações e ações do webdriver.
        /// </summary>
        /// <returns></returns>
        public static AppiumDriver<AppiumWebElement> GetAndroidDriver()
        {
            return MobileDriverSetup.GetAndroidDriver();
        }

        /// <summary>
        /// Recupera o driver iOS instanciado.
        /// Permite acessar as configurações e ações do webdriver.
        /// </summary>
        /// <returns></returns>
        public static AppiumDriver<AppiumWebElement> GetiOSDriver()
        {
            return MobileDriverSetup.GetIOSDriver();
        }

        /// <summary>
        /// Recupera a URL configurada no specflow.json pelo parâmetro "appSettings:Url".
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return url;
        }

        /// <summary>
        /// Recupera o valor de timeout configurado no specflow.json pelo parâmetro "appSettings:TimeOut".
        /// </summary>
        /// <returns></returns>
        public static TimeSpan GetTimeOut()
        {
            return timeOut;
        }

        /// <summary>
        /// Recupera o browser configurado no specflow.json para execução dos testes pelo parâmetro "appSettings:Browser".
        /// </summary>
        /// <returns></returns>
        public static EnumBrowser GetBrowser()
        {
            return browser;
        }

        ///<summary>
        /// Recupera os nodes informados
        /// </summary>
        public static List<string> GetNodes()
        {
            foreach(string node in nodes)
            {
                if(node.Equals("") || node.Equals(null))
                {
                    throw new DriveNotFoundException("Por favor verifique as configurações em specflow.json, paramêtro [Nodes] - parâmetro vazio ou nulo.");
                }
            }
            return nodes;
        }

        ///<summary>
        ///Recupera os nodesBrowsers informados
        /// </summary>
        public static List<string> GetNodesBrowser()
        {
            foreach(string nodeBrowser in nodesBrowser)
            {
                if (nodeBrowser.Equals("") || nodeBrowser.Equals(null))
                {
                    throw new DriveNotFoundException("Por favor verifique as configurações em specflow.json, paramêtro [NodesBrowser] - parâmetro vazio ou nulo.");
                }
            }
            return nodesBrowser;
        }

        /// <summary>
        /// Fecha a janela de execução dos teste atual.
        /// </summary>
        public static void CloseTest()
        {
            if (Enum.IsDefined(typeof(EnumProject), "web"))
            {
                if (driver != null)
                {
                    driver.Close();
                }
                else
                {
                    throw new DriveNotFoundException("[Close] Webdriver não encontrado.");
                }
            }
        }

        /// <summary>
        /// Finaliza o processo do Webdriver.
        /// </summary>
        public static void QuitDriver()
        {
            if (ConfigurationData.configurationData[0].project.Equals("web"))
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver = null;
                }
                else
                {
                    throw new DriveNotFoundException("[Quit] Webdriver não encontrado.");
                }
            }
        }

        /// <summary>
        /// Finaliza o processo do Android Driver.
        /// </summary>
        public static void QuitAndroidDriver()
        {
            if (ConfigurationData.configurationData[0].project.Equals("mobile") &&
                ValidateConfigurationData.MobileAndroidParam() &&
                !GetAndroidDriver().Equals(null))
            {
                GetAndroidDriver().Quit();
                MobileDriverSetup.AppiumFinalize();
            }
        }

        /// <summary>
        /// Finaliza o processo do iOS Driver.
        /// </summary>
        public static void QuitiOSDriver()
        {
            if (ConfigurationData.configurationData[0].project.Equals("mobile") && 
                ValidateConfigurationData.MobileiOSParam() &&
                !GetiOSDriver().Equals(null))
            {
                GetiOSDriver().Quit();
                MobileDriverSetup.AppiumFinalize();
            }
        }

        #endregion
    }
}
