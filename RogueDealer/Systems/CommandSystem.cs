using System; 
 using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using RogueDealer.Core; 
 using RogueSharp.DiceNotation; 
 using RogueDealer.Interfaces; 
 using RogueSharp; 
 using RLNET;


namespace RogueDealer.Systems
{
    public class CommandSystem
    {
        public Random r = new Random();

        public bool MovePlayer(Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;
            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);



            switch (direction)
            {
                case Direction.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Direction.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Direction.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Direction.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }

                    
                default:
                    {
                        return false;
                    }
            }

            if (monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }



            return false;
        }

        public void Attack(Actor attacker, Actor defender)
        {
            ResolveDamage(defender, attacker.Attack);
        }

        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;
                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
        }

        private static void ResolveDeath(Actor defender)
        {

            if (defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster)defender);
            }
        }
    }
}
