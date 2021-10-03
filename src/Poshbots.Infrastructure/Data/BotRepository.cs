using Poshbots.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poshbots.Core.Entities;
using Dapper;

namespace Poshbots.Infrastructure.Data
{
    public class BotRepository : BaseRepository, IBotRepository
    {
        public void Delete(int botId)
        {
            db.Execute(@"DELETE FROM [Bot] WHERE BotID = @BotId", new { botId });
        }

        public Bot Insert(Bot bot)
        {
            var sqlQuery = @"INSERT INTO [Bot] ([UserId], [Name], [Code], [Wins], [Losses], [Draws])
                VALUES (@UserId, @Name, @Code, @Wins, @Losses, @Draws);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var botId = db.Query<int>(sqlQuery, bot).Single();
            bot.BotId = botId;

            return bot;
        }

        public Bot SelectById(int botId)
        {
            return db.Query<Bot>(@"SELECT * FROM [Bot] WHERE [BotId] = @BotId", new { BotId = botId }).SingleOrDefault();
        }

        public List<Bot> SelectByUserId(string userId)
        {
            return db.Query<Bot>(@"SELECT * FROM [Bot] WHERE [UserId] = @UserId", new { userId = userId }).ToList();
        }

        public List<Bot> SelectAll()
        {
            return db.Query<Bot>(@"SELECT * FROM [Bot]").ToList();
        }

        public Bot Update(Bot bot)
        {
            var sqlQuery = @"UPDATE [Bot] SET [Name] = @Name, [Code] = @Code, [Wins] = @Wins, [Losses] = @Losses, [Draws] = @Draws
                WHERE [BotId] = @BotId";
            db.Execute(sqlQuery, bot);

            return bot;
        }
    }
}
