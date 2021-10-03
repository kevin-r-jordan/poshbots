using Poshbots.Core.Entities;
using Poshbots.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poshbots.Core.Services
{
    public class BotService
    {
        private IBotRepository _botRepository;
        public BotService(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public Bot Create(Bot bot)
        {
            return _botRepository.Insert(bot);
        }

        public Bot GetById(int botId)
        {
            return _botRepository.SelectById(botId);
        }

        public Bot Update(Bot bot)
        {
            return _botRepository.Update(bot);
        }

        public void Delete(int botId)
        {
            _botRepository.Delete(botId);
        }

        public List<Bot> GetBotsByUserId(string userId)
        {
            return _botRepository.SelectByUserId(userId);
        }

        public List<Bot> GetAllBots()
        {
            return _botRepository.SelectAll();
        }
    }
}
