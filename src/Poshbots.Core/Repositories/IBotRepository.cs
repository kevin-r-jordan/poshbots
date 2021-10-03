using Poshbots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poshbots.Core.Repositories
{
    public interface IBotRepository
    {
        Bot Insert(Bot bot);
        Bot SelectById(int botId);
        Bot Update(Bot bot);
        void Delete(int botId);

        List<Bot> SelectByUserId(string userId);
        List<Bot> SelectAll();
    }
}
