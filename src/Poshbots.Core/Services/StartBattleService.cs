using Poshbots.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poshbots.Core.Services
{
    public class StartBattleService
    {
        private IAzureFunctionProvider _azureFunctionProvider;
        private IFileIOProvider _fileIOProvider;
        public StartBattleService(IAzureFunctionProvider azureFunctionProvider, IFileIOProvider fileIOProvider)
        {
            _azureFunctionProvider = azureFunctionProvider;
            _fileIOProvider = fileIOProvider;
        }

        public void Setup(Battle battle)
        {
            // Modify run.ps1 for each players code
            var fileContents = _fileIOProvider.ReadFile("PowerShell\\run.ps1");
            fileContents = fileContents.Replace("$GameName = $null", "$GameName = \"" + battle.BattleId + "\"");

            fileContents = fileContents.Replace("$Player1Name = $null", "$Player1Name = \"" + battle.Players[0].Bot.Name + "\"");
            fileContents = fileContents.Replace("$Player2Name = $null", "$Player2Name = \"" + battle.Players[1].Bot.Name + "\"");

            fileContents = fileContents.Replace("function Move-Player1 { }", "function Move-Player1 { " + SubstituteCode(battle.Players[0].Bot.Code, "$Player1Name") + " } ");
            fileContents = fileContents.Replace("function Move-Player2 { }", "function Move-Player2 { " + SubstituteCode(battle.Players[1].Bot.Code, "$Player2Name") + " } ");

            // Upload code to Azure functions
            _azureFunctionProvider.UploadBattle(battle, fileContents);
        }

        public void Begin(Battle battle)
        {
            _azureFunctionProvider.StartBattle(battle);
        }

        private string SubstituteCode(string code, string playerNumber)
        {
            code = code.Replace("Move-Up", "Move-Up " + playerNumber);
            code = code.Replace("Move-Down", "Move-Down " + playerNumber);
            code = code.Replace("Move-Left", "Move-Left " + playerNumber);
            code = code.Replace("Move-Right", "Move-Right " + playerNumber);
            code = code.Replace("Get-Surroundings", "Get-Surroundings " + playerNumber);

            return code;
        }
    }
}
