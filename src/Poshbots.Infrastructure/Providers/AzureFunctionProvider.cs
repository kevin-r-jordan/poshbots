using Poshbots.Core.Providers;
using Poshbots.Core.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Poshbots.Infrastructure.Providers
{
    public class AzureFunctionProvider : IAzureFunctionProvider
    {
        private string ftpAddress = ConfigurationManager.AppSettings["FunctionsFtp"];
        private string ftpUsername = ConfigurationManager.AppSettings["FunctionsFtpAccount"];
        private string ftpPassword = ConfigurationManager.AppSettings["FunctionsFtpPassword"];
        private HttpWebRequest webRequest;

        public void StartBattle(Battle battle)
        {
            webRequest = (HttpWebRequest)WebRequest.Create("https://poshbot-functions.azurewebsites.net/api/" + battle.BattleId);
            webRequest.BeginGetResponse(new AsyncCallback(FinishWebRequest), null);
        }

        private void FinishWebRequest(IAsyncResult result)
        {
            webRequest.EndGetResponse(result);
        }

        public void UploadBattle(Battle battle, string powershell)
        {
            UploadFunctionJson(battle);
            UploadRunPowerShell(battle, powershell);
        }

        private void UploadFunctionJson(Battle battle)
        {
            string json = @"{
  ""bindings"": [
    {
      ""name"": ""req"",
      ""type"": ""httpTrigger"",
      ""direction"": ""in"",
      ""authLevel"": ""anonymous"",
      ""methods"": [
        ""get""
      ]
    },
    {
      ""name"": ""res"",
      ""type"": ""http"",
      ""direction"": ""out""
    }
  ],
  ""disabled"": false
}";
            var ftpCreateRequest = FtpWebRequest.Create(new Uri(new Uri(ftpAddress), "/site/wwwroot/" + battle.BattleId));
            ftpCreateRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            ftpCreateRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            var createResponse = (FtpWebResponse)ftpCreateRequest.GetResponse();

            var ftpUploadRequest = FtpWebRequest.Create(new Uri(new Uri(ftpAddress), "/site/wwwroot/" + battle.BattleId + "/function.json"));
            ftpUploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpUploadRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            var jsonBytes = Encoding.ASCII.GetBytes(json);
            using (Stream stream = ftpUploadRequest.GetRequestStream())
            {
                stream.Write(jsonBytes, 0, jsonBytes.Length);
            }
            var ftpResponse = (FtpWebResponse)ftpUploadRequest.GetResponse();
            ftpResponse.Close();
        }

        private void UploadRunPowerShell(Battle battle, string powershell)
        {
            var ftpUploadRequest = FtpWebRequest.Create(new Uri(new Uri(ftpAddress), "/site/wwwroot/" + battle.BattleId + "/run.ps1"));
            ftpUploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpUploadRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            var psBytes = Encoding.ASCII.GetBytes(powershell);
            using (Stream stream = ftpUploadRequest.GetRequestStream())
            {
                stream.Write(psBytes, 0, psBytes.Length);
            }
            var ftpResponse = (FtpWebResponse)ftpUploadRequest.GetResponse();
            ftpResponse.Close();
        }
    }
}
