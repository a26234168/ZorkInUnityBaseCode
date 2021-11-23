using System.IO;
using Newtonsoft.Json;
using Zork.Common;

namespace Zork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename);

            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));


            ConsoleOutputService output = new ConsoleOutputService();
            ConsoleIntputService input = new ConsoleIntputService();


            game.Player.LocationChanged += Player_LocationChanged;
            game.Start(input, output);

            Room previousRoom = null;

            while (game.IsRunning)
            {

                {
                    if (previousRoom != game.Player.Location)
                    {
                        Game.Look(game);
                        previousRoom = game.Player.Location;
                    }

                    output.Write("\n> ");
                    input.ProcessInput();

                }



            }
            output.WriteLine(string.IsNullOrWhiteSpace(game.ExitMessage) ? "Thank you for playing!" : game.ExitMessage);

        }


        private static void Player_LocationChanged (object sender, Room e)
        {
            System.Console.WriteLine($"You moved to {e.Name}");
        }
        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}