using Poshbots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Poshbots.Core.Services
{
    public class Battle
    {
        private MapCell[,] _map;
        private int _winScore;
        private Random _random;

        public string BattleId { get; private set; }
        public MapCell[,] Map { get { return _map; } }

        public List<Player> Players { get; set; }

        public bool Started { get; set; }
        public string Winner { get; private set; }


        public Battle(List<Player> players, int xSize, int ySize, int winScore)
        {
            _random = new Random();

            BattleId = GenerateBattleId();
            Started = false;

            _winScore = winScore;
            _map = new MapCell[xSize, ySize];            
            
            foreach (var player in players)
            {
                player.Position = new MapCoordinate().Random(xSize, ySize, _random);
                player.Score = 0;
            }
            Players = players;

            this.GenerateMap();
        }
        
        public MapCell[,] GetSurroundings(Player player)
        {
            var miniMap = new MapCell[5, 5];
            
            for (int y = -2; y <= 2; y++)
            {
                for (int x = -2; x <= 2; x++)
                {
                    try
                    {
                        miniMap[x + 2, y + 2] = _map[player.Position.X + x, player.Position.Y + y];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        miniMap[x + 2, y + 2] = null;
                    }
                }
            }
            return miniMap;
        }

        public void MoveUp(Player player)
        {
            SetPlayerPosition(player, player.Position.X, player.Position.Y - 1);
        }

        public void MoveDown(Player player)
        {
            SetPlayerPosition(player, player.Position.X, player.Position.Y + 1);
        }

        public void MoveLeft(Player player)
        {
            SetPlayerPosition(player, player.Position.X - 1, player.Position.Y);
        }

        public void MoveRight(Player player)
        {
            SetPlayerPosition(player, player.Position.X + 1, player.Position.Y);
        }

        private void SetPlayerPosition(Player player, int x, int y)
        {
            // Ensure that the player isn't moving faster than other players
            foreach (var otherPlayer in Players)
            {
                if ((player.ActionCount - otherPlayer.ActionCount) > 1)
                    return;
            }

            player.ActionCount++;

            // Can't move off the map
            if (x < 0 || y < 0 || x >= _map.GetLength(0) || y >= _map.GetLength(1))
            {
                UpdateScores(player);
                return;
            }

            // Can't move more than one space
            if (Math.Abs(player.Position.X - x) > 1 || Math.Abs(player.Position.Y - y) > 1)
            {
                UpdateScores(player);
                return;
            }

            // Can't double occupy a space
            if (!String.IsNullOrEmpty(_map[x, y].Occupied))
            {
                UpdateScores(player);
                return;
            }

            // Old cube no longer occupied
            _map[player.Position.X, player.Position.Y].Occupied = null;

            player.Position = new MapCoordinate(x, y);
            
            _map[player.Position.X, player.Position.Y].Owner = player.Bot.Name;
            _map[player.Position.X, player.Position.Y].Occupied = player.Bot.Name;

            UpdateScores(player);
        }


        private void UpdateScores(Player player)
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    if (!String.IsNullOrEmpty(_map[x, y].Owner) && _map[x, y].Owner == player.Bot.Name)
                    {
                        player.Score += _map[x, y].Points;
                    }
                }
            }

            if (player.Score > _winScore) Winner = player.Bot.Name;
        }

        private void GenerateMap()
        {
            for (int y = 0; y < _map.GetLength(1); y++)
            {
                for (int x = 0; x < _map.GetLength(0); x++)
                {
                    var weightedRandom = _random.Next(1, 101);
                    var points = 1;
                    if (weightedRandom > 98) points = 9;
                    else if (weightedRandom > 95) points = 6;
                    else if (weightedRandom > 90) points = 5;
                    else if (weightedRandom > 85) points = 4;
                    else if (weightedRandom > 75) points = 3;
                    else if (weightedRandom > 50) points = 2;

                    _map[x, y] = new MapCell() { Points = points };
                }
            }
        }

        private string GenerateBattleId()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, 20).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }

    

    
}
