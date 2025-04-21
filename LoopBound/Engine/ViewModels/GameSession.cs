using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }

        public GameSession()
        {
            CurrentPlayer = new Player();

            CurrentPlayer.Name = "Hero";
            CurrentPlayer.Level = 1;
            CurrentPlayer.HP = 20;
            CurrentPlayer.Gold = 0;
            CurrentPlayer.Attack = 5;
            CurrentPlayer.Defense = 5;
            CurrentPlayer.Equipment = "None";
            CurrentPlayer.Skills = "None";
            CurrentPlayer.Inventory = "None";
        }
    }
}
