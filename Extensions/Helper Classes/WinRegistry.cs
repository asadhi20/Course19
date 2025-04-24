using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses.Helper_Classes
{
    public static class WinRegistry
    {
        public static bool SetValue(string keyPath, string valueName, object valueData, RegistryValueKind rvk = RegistryValueKind.Unknown, Action<string> logError = null)
        {
            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                    {
                        if (key is null) return false;

                        key.SetValue(name: valueName, value: valueData, valueKind: rvk);
                        return true;
                    }
                }
                //Registry.SetValue(keyName: keyPath, valueName: valueName, value: valueData, valueKind: rvk);
                //return true;
            }
            catch (Exception ex)
            {
                logError?.Invoke(ex.Message);
                return false;
            }
        }


        public static ValueOption<int> GetValueAsInteger(string keyPath, string valueName, object defaultValue = null, Action<string> logError = null)
        {
            try
            {
                if (int.TryParse(GetValueAsString(keyPath: keyPath, valueName: valueName, defaultValue: defaultValue), out int result))
                    return ValueOption<int>.Some(result);
                else {
                    logError?.Invoke("Invalid data type");
                    return ValueOption<int>.Some(int.MinValue);
                }
            }
            catch (Exception ex)
            {
                logError?.Invoke(ex.Message);
                return ValueOption<int>.None();
            }
        }
        
        public static ValueOption<bool> GetValueAsBoolean(string keyPath, string valueName, object defaultValue = null, Action<string> logError = null)
        {
            try
            {
                string value = GetValueAsString(keyPath: keyPath, valueName: valueName, defaultValue: defaultValue);

                if (int.TryParse(s: value, out int resultAsInt)) {
                    return ValueOption<bool>.Some(resultAsInt > 0);
                }
                else if (bool.TryParse(value: value, out bool resultAsBoolean))
                {
                    return ValueOption<bool>.Some(resultAsBoolean);
                }
                else {
                    logError?.Invoke("Invalid data type");
                    return ValueOption<bool>.Some(false);
                }
            }
            catch (Exception ex)
            {
                logError?.Invoke(ex.Message);
                return ValueOption<bool>.None();
            }
        }

        public static string GetValueAsString(string keyPath, string valueName, object defaultValue = null
            , RegistryValueOptions rvo = RegistryValueOptions.None, Action<string> logError = null)
            => GetValue(keyPath: keyPath, valueName: valueName, defaultValue: defaultValue, rvo: rvo, logError: logError)?.ToString() ?? null;

        public static object GetValue(string keyPath, string valueName, object defaultValue = null, RegistryValueOptions rvo = RegistryValueOptions.None, Action<string> logError = null)
        {
            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(keyPath))
                    {
                        if (key is null) return null;

                        return key.GetValue(name: valueName, defaultValue: defaultValue, options: rvo);
                    }
                }
                //return Registry.GetValue(keyName: keyPath, valueName: valueName, defaultValue: defaultValue);
            }
            catch (Exception ex)
            {
                logError?.Invoke(ex.Message);
                return null;
            }
        }

    }
}
