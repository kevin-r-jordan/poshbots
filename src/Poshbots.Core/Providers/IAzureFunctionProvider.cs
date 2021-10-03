using Poshbots.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poshbots.Core.Providers
{
    public interface IAzureFunctionProvider
    {
        void UploadBattle(Battle battle, string powershell);

        void StartBattle(Battle battle);
    }
}
