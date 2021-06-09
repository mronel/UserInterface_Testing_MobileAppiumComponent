using System;

namespace RaizenTestFuncional.Mobile
{
    public class ValidateMobileData
    {
        /// <summary>
        /// Retorna os valores de configuração dos devices Android
        /// </summary>
        public static void AndroidDeviceConfigurationParams()
        {
            foreach (var val in MobileData.mobileAndroidData)
            {
                if (val.mobileAndroidPlatformName.Equals("") || val.mobileAndroidPlatformName.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileAndroidPlatformName] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.mobileAndroidDeviceName.Equals("") || val.mobileAndroidDeviceName.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileAndroidDeviceName] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.mobileAndroidAppPath.Equals("") || val.mobileAndroidAppPath.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileAndroidAppPath] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.mobileAndroidAppPackage.Equals("") || val.mobileAndroidAppPackage.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileAndroidAppPackage] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.mobileAndroidActivity.Equals("") || val.mobileAndroidActivity.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileAndroidActivity] Verificar o parâmetro, não pode ser vazio.");
                }
            }
        }

        /// <summary>
        /// Retorna os valores de configuração dos devices iOS
        /// </summary>
        public static void iOSDeviceConfigurationParams()
        {
            foreach (var val in MobileData.mobileiOSData)
            {
                if (val.mobileiOSPlatformVersion.Equals("") || val.mobileiOSPlatformVersion.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileiOSPlatformVersion] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.mobileiOSDeviceName.Equals("") || val.mobileiOSDeviceName.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileiOSDeviceName] Verificar o parâmetro, não pode ser vazio.");
                }

                if (val.mobileiOSAppPath.Equals("") || val.mobileiOSAppPath.Equals(null))
                {
                    throw new Exception("[Erro de Configuração - MobileiOSAppPath] Verificar o parâmetro, não pode ser vazio.");
                }
            }
        }
    }
}
