using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueDealer.Core;
using RogueDealer.Systems;
using RogueDealer.Interfaces;

namespace RogueDealer.Monsters
{
    
    public class Kobold : Monster
    {
         
        public static Kobold Create(int level)
        {
            Random r = new Random();
            int health = r.Next(2,5);
            return new Kobold
            {
                Attack = r.Next(1, 3) + level / 3,
                AttackChance = r.Next(2, 5),
                Awareness = 10,
                Color = Colors.KoboldColor,
                Defense = r.Next(1, 3) + level / 3,
                DefenseChance = r.Next(4, 10),
                Gold = r.Next(5, 100),
                Health = health,
                MaxHealth = health,
                Name = "Kobold",
                Speed = 14,
                Symbol = 'X'
            };
        }
    }
}
