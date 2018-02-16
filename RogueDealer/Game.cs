using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueDealer.Core;
using RogueDealer.Systems;
using RogueDealer.Interfaces;

namespace RogueDealer
{

    public class Game
    {

        private static readonly int _screenWidth = 140;
        private static readonly int _screenHeight = 100;

        private static RLRootConsole _rootConsole;

        private static readonly int _mapWidth = 120;
        private static readonly int _mapHeight = 80;
        private static RLConsole _mapConsole;

        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 100;
        private static RLConsole _statConsole;

        private static readonly int _inventoryWidth = 140;
        private static readonly int _inventoryHeight = 10;
        private static RLConsole _inventoryConsole;

        public static Player Player { get; set; }
        public static DungeonMap DungeonMap { get; private set; }

        private static bool _renderRequired = true;
        public static CommandSystem CommandSystem { get; private set; }
        public static Random Random { get; private set; }
        public static void Main()
        {

            Random = new Random();

            string consoleTitle = $"RogueLike game VCS Level1";
            CommandSystem = new CommandSystem();
            Player = new Player();
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 25, 15, 7);

            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();

            string fontFileName = "terminal8x8.png";
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
              8, 8, 1f, consoleTitle);

            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);

            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {

            bool didPlayerAct = false;
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();


            if (keyPress != null)
            {
                if (keyPress.Key == RLKey.Up)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                }
                else if (keyPress.Key == RLKey.Down)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                }
                else if (keyPress.Key == RLKey.Left)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                }
                else if (keyPress.Key == RLKey.Right)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                }
                else if (keyPress.Key == RLKey.Escape)
                {
                    _rootConsole.Close();
                }
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {

            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _rootConsole, 0, _inventoryHeight);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _rootConsole, _mapWidth, 0);
            RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, _rootConsole, 0, 0);
            Player.Draw(_mapConsole, DungeonMap);

            _rootConsole.Draw();

            if (_renderRequired)
            {
                DungeonMap.Draw(_mapConsole, _statConsole);
                Player.Draw(_mapConsole, DungeonMap);
                Player.DrawStats(_statConsole);
            }
        }
    }
}
