using CFlutter_Bridge;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CFlutterBridge
{
    public class Bridge
    {
        [DllExport]
        public static string GetITunesAppVersion()
        {
            string name = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
            
            Dictionary<string,string> ret = new Dictionary<string,string>();
            if (registryKey != null)
            {
                string[] subKeyNames = registryKey.GetSubKeyNames();
                foreach (string name2 in subKeyNames)
                {
                    RegistryKey registryKey2 = registryKey.OpenSubKey(name2);
                    if (registryKey2 == null)
                    {
                        continue;
                    }
                    string text = registryKey2.GetValue("DisplayName", "Nothing").ToString();
                    if (registryKey2.GetValue("Publisher", "Publisher").ToString().Contains("Apple"))
                    {
                        object value = registryKey2.GetValue("DisplayVersion", "Version");
                        if (value != null)
                        {
                            ret[text] = value.ToString();
                        }
                    }
                }
            }
            return JsonConvert.SerializeObject(ret);
        }

        [DllExport]
        public static void OpenITunesExe(string path)
        {
            try
            {
                Task.Run(() =>
                {
                    ProcessStartInfo info = new ProcessStartInfo(path);
                    Process process = new Process();
                    process.StartInfo = info;
                    process.Start();
                    process.WaitForExit();
                }).Start();
            } catch (System.Exception e)
            {
            }
        }

        [DllExport]
        public static void ExceptionTest()
        {
            throw new Exception("crash test");
        }


    }
}
