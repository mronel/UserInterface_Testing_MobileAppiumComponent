using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using RaizenTestFuncional.Mobile;
using RaizenTestFuncional.Setup;
using System.IO;

namespace RaizenTestFuncional.Driver
{
    public class MobileDriverSetup
    {
        public static AppiumDriver<AppiumWebElement> androidDriver;
        public static AppiumOptions capabilitiesAndroid;
        public static AppiumDriver<AppiumWebElement> iOSDriver;
        public static AppiumOptions capabilitiesiOS;
        public static AppiumLocalService service;

        /// <summary>
        /// Inicializa uma instancia do Appium.
        /// </summary>
        public static void AndroidAppiumInitialize()
        {
            capabilitiesAndroid = new AppiumOptions();
            capabilitiesAndroid.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobileData.mobileAndroidData[0].mobileAndroidPlatformName);
            capabilitiesAndroid.AddAdditionalCapability(MobileCapabilityType.DeviceName, MobileData.mobileAndroidData[0].mobileAndroidDeviceName);
            capabilitiesAndroid.AddAdditionalCapability(MobileCapabilityType.App, Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + "\\App\\" + MobileData.mobileAndroidData[0].mobileAndroidAppPath);
            capabilitiesAndroid.AddAdditionalCapability("activity", MobileData.mobileAndroidData[0].mobileAndroidActivity);
            capabilitiesAndroid.AddAdditionalCapability("appPackage", MobileData.mobileAndroidData[0].mobileAndroidAppPackage);

            service = new AppiumServiceBuilder()
                     .UsingAnyFreePort()
                     .WithAppiumJS(new FileInfo(ValidateConfigurationData.AppiumPath()))
                     .UsingDriverExecutable(new FileInfo(ValidateConfigurationData.NodePathParam()))
                     .Build();
            service.Start();
            androidDriver = new AndroidDriver<AppiumWebElement>(service, capabilitiesAndroid);
        }

        /// <summary>
        /// Configurando os capabilities para o driver mobile iOS
        /// </summary>
        public static void iOSAppiumInitialize()
        {
            foreach (var val in MobileData.mobileiOSData)
            {
                ValidateMobileData.iOSDeviceConfigurationParams();

                capabilitiesiOS = new AppiumOptions();
                capabilitiesiOS.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, MobileData.mobileiOSData[0].mobileiOSPlatformVersion);
                capabilitiesiOS.AddAdditionalCapability(MobileCapabilityType.DeviceName, MobileData.mobileiOSData[0].mobileiOSDeviceName);
                capabilitiesiOS.AddAdditionalCapability(MobileCapabilityType.App, MobileData.mobileiOSData[0].mobileiOSAppPath);

                service = new AppiumServiceBuilder()
                         .UsingAnyFreePort()
                         .WithAppiumJS(new FileInfo(ValidateConfigurationData.AppiumPath()))
                         .UsingDriverExecutable(new FileInfo(ValidateConfigurationData.NodePathParam()))
                         .Build();
                service.Start();
                iOSDriver = new IOSDriver<AppiumWebElement>(service, capabilitiesiOS);
            }
        }

        /// <summary>
        /// Retorna a lista de drivers ativos para Android.
        /// </summary>
        /// <returns>Lista de drivers instanciados para android</returns>
        public static AppiumDriver<AppiumWebElement> GetAndroidDriver()
        {
            return androidDriver;
        }

        /// <summary>
        /// Retorna a lista de drivers ativos para iOS.
        /// </summary>
        /// <returns>Lista de drivers instanciados para iOS</returns>
        public static AppiumDriver<AppiumWebElement> GetIOSDriver()
        {
            return iOSDriver;
        }

        /// <summary>
        /// Finaliza uma instancia do Appium.
        /// </summary>
        /// <param name="service"></param>
        public static void AppiumFinalize()
        {
            service.Dispose();
        }
    }
}
