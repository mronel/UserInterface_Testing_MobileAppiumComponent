using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using RaizenTestFuncional.Credentials;
using RaizenTestFuncional.Setup;
using System;
using System.Collections.Generic;
using System.IO;

namespace RaizenTestFuncional.Driver
{
    public class DriverSetup
    {
        public static readonly string pathDriver = Directory.GetCurrentDirectory();

        public static void SetEnviromentParams(string _seleniumGrid, List<string> _nodes, List<string> _nodesBrowser, List<string> _connectionPortNumber)
        {
            //Setando o timeOut do webdriver
            DriverFactory.timeOut = ValidateConfigurationData.TimeOutParam();

            //Verificando se a execução será realizado: local, jenkins ou azure
            if (ValidateConfigurationData.DevOpsParam().ToString().Equals("local"))
            {
                DriverFactory.url = ValidateConfigurationData.UrlParam();
            } 
            else if (ValidateConfigurationData.DevOpsParam().ToString().Equals("jenkins")) 
            {
                // ApplicationURL: variável de ambiente configurada no Jenkins
                // para definir em qual ambiente será definido a execução dos testes
                DriverFactory.url = Environment.GetEnvironmentVariable("ApplicationURL") != "" ? Environment.GetEnvironmentVariable("ApplicationURL") : throw new Exception ("[Erro Parâmetro ApplicationURL] - Configurar o parâmetro <ApplicationURL> no pipeline.");
            
            } 
            else if (ValidateConfigurationData.DevOpsParam().ToString().Equals("azure"))
            {
                DriverFactory.url = Environment.GetEnvironmentVariable("ApplicationURL", EnvironmentVariableTarget.Process) != "" ? Environment.GetEnvironmentVariable("ApplicationURL", EnvironmentVariableTarget.Process) : throw new Exception("[Erro Parâmetro ApplicationURL] - Configurar o parâmetro <ApplicationURL> no pipeline AzureDevOps.");
            }

            //Verificando se será executado os testes em Selenium Grid
            if (_seleniumGrid == null || bool.Parse(_seleniumGrid) == false)
            {
                DriverFactory.seleniumGrid = false;
                DriverFactory.browser = ValidateConfigurationData.BrowserParam();
            }
            else
            {
                DriverFactory.seleniumGrid = true;
                DriverFactory.nodes = _nodes;
                DriverFactory.nodesBrowser = _nodesBrowser;
                DriverFactory.connectionPortNumber = _connectionPortNumber;
            }


            //Verificando se será obtido dados de acesso do cofre de senhas
            if (ValidateConfigurationData.SafeCredentialsParam())
            {
                ValidateConfigurationData.CredentialsParam();
                RequestCredentials.GetCredentials();
            }

        }

        public static void SeleniumGridConfig()
        {
            for (var i = 0; i <= DriverFactory.nodes.Count - 1; i++)
            {
                //o browser configurado para o node terá que estar na mesma posição do array
                DriverFactory.browser = (EnumBrowser)Enum.Parse(typeof(EnumBrowser), DriverFactory.nodesBrowser[i], true);

                switch (DriverFactory.browser)
                {
                    case EnumBrowser.firefox:
                        FirefoxOptions optionsFirefox = new FirefoxOptions();
                        optionsFirefox.AddArguments("--lang-pt", "--no-sandbox");
                        DriverFactory.driver = new RemoteWebDriver(new Uri("http://" + DriverFactory.nodes[i] + ":" + DriverFactory.connectionPortNumber[i] + "/wd/hub"), optionsFirefox);
                        break;

                    case EnumBrowser.firefoxheadless:
                        optionsFirefox = new FirefoxOptions();
                        optionsFirefox.AddArguments("--lang-pt", "--no-sandbox", "--headless");
                        DriverFactory.driver = new RemoteWebDriver(new Uri("http://" + DriverFactory.nodes[i] + ":" + DriverFactory.connectionPortNumber[i] + "/wd/hub"), optionsFirefox);
                        break;

                    case EnumBrowser.chrome:
                        ChromeOptions optionsChrome = new ChromeOptions();
                        optionsChrome.AddArguments("--lang-pt", "--no-sandbox", "--disable-notifications", "--proxy-server = http: //127.0.0.1:8090");
                        DriverFactory.driver = new RemoteWebDriver(new Uri("http://" + DriverFactory.nodes[i] + ":" + DriverFactory.connectionPortNumber[i] + "/wd/hub"), optionsChrome);
                        break;

                    case EnumBrowser.chromeheadless:
                        optionsChrome = new ChromeOptions();
                        optionsChrome.AddArguments("--lang-pt", "--no-sandbox", "--headless", "--disable-notifications");
                        DriverFactory.driver = new RemoteWebDriver(new Uri("http://" + DriverFactory.nodes[i] + ":" + DriverFactory.connectionPortNumber[i] + "/wd/hub"), optionsChrome);
                        break;

                    case EnumBrowser.internetExplorer:
                        InternetExplorerOptions optionsIE = new InternetExplorerOptions();
                        DriverFactory.driver = new RemoteWebDriver(new Uri("http://" + DriverFactory.nodes[i] + ":" + DriverFactory.connectionPortNumber[i] + "/wd/hub"), optionsIE);
                        break;

                    case EnumBrowser.edge:
                        EdgeOptions optionsEdge = new EdgeOptions();
                        optionsEdge.UseInPrivateBrowsing = false;
                        DriverFactory.driver = new RemoteWebDriver(new Uri("http://" + DriverFactory.nodes[i] + ":" + DriverFactory.connectionPortNumber[i] + "/wd/hub"), optionsEdge);
                        break;

                    default:
                        throw new DriveNotFoundException("Não foi possível configurar o WebDriver. Por favor verifique as configurações em specflow.json - parâmetro [Browser] ou  verifique se o WebDriver se encontra na pasta Debug.");
                }
            }
        }

        public static void WebDriverConfig()
        {
            switch (DriverFactory.GetBrowser())
            {
                case EnumBrowser.firefox:

                    FirefoxOptions optionsFirefox = new FirefoxOptions();
                    optionsFirefox.AddArguments("--lang-pt", "--no-sandbox");
                    DriverFactory.driver = new FirefoxDriver(pathDriver, optionsFirefox);
                    break;

                case EnumBrowser.firefoxheadless:
                    FirefoxOptions optionsFirefoxHeadless = new FirefoxOptions();
                    optionsFirefoxHeadless.AddArguments("--lang-pt", "--no-sandbox", "--headless");
                    DriverFactory.driver = new FirefoxDriver(pathDriver, optionsFirefoxHeadless);
                    break;

                case EnumBrowser.chrome:
                    ChromeOptions optionsChrome = new ChromeOptions();
                    optionsChrome.AddArguments("--lang-pt", "--no-sandbox", "--disable-notifications");
                    DriverFactory.driver = new ChromeDriver(pathDriver, optionsChrome);
                    break;

                case EnumBrowser.chromeheadless:
                    ChromeOptions optionsChromeHeadless = new ChromeOptions();
                    optionsChromeHeadless.AddArguments("--lang-pt", "--no-sandbox", "--headless", "--disable-notifications");
                    DriverFactory.driver = new ChromeDriver(pathDriver, optionsChromeHeadless);
                    break;

                case EnumBrowser.internetExplorer:
                    DriverFactory.driver = new InternetExplorerDriver(pathDriver);
                    break;

                case EnumBrowser.edge:
                    EdgeOptions optionsEdge = new EdgeOptions();
                    optionsEdge.UseInPrivateBrowsing = false;
                    DriverFactory.driver = new EdgeDriver(pathDriver, optionsEdge);
                    break;

                default:
                    throw new DriveNotFoundException("Não foi possível configurar o WebDriver. Por favor verifique as configurações em specflow.json - parâmetro [Browser] ou  verifique se o WebDriver se encontra na pasta Debug.");
            }
        }

        /// <summary>
        /// Configuração do Webdriver
        /// </summary>
        public static void ConfigWebDriver(IWebDriver driver)
        {
            if (!driver.Equals(EnumBrowser.chromeheadless) && !driver.Equals(EnumBrowser.firefoxheadless))
            {
                driver.Manage().Window.Maximize();
            }
            driver.Manage().Timeouts().ImplicitWait = DriverFactory.GetTimeOut();
            driver.Manage().Timeouts().PageLoad = DriverFactory.GetTimeOut();
            driver.Manage().Timeouts().AsynchronousJavaScript = DriverFactory.GetTimeOut();
        }

    }
}
