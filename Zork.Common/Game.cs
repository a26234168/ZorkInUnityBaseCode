using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Zork.Common;


namespace Zork
{
    public class Game : INotifyPropertyChanged
    {
        private Game command;

        public event PropertyChangedEventHandler PropertyChanged;

        public World World { get; private set; }

        public event EventHandler GameStopped;

        public string StartingLocation { get; set; }

        public string WelcomeMessage { get; set; }

        public string ExitMessage { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }

        public bool IsRunning { get; set; }
        [JsonIgnore]

        public IOutputService Output { get; set; }

        public IInputService Input { get; set; }


        [JsonIgnore]
        public Dictionary<string, Command> Commands { get; private set; }

        public Game(World world, Player player)
        {
            World = world;
            Player = player;

            Commands = new Dictionary<string, Command>()
            {
                { "QUIT", new Command("QUIT", new string[] { "QUIT", "Q", "BYE" }, Quit) },
                { "LOOK", new Command("LOOK", new string[] { "LOOK", "L" }, Look) },
                { "NORTH", new Command("NORTH", new string[] { "NORTH", "N" }, game => Move(game, Directions.North)) },
                { "SOUTH", new Command("SOUTH", new string[] { "SOUTH", "S" }, game => Move(game, Directions.South)) },
                { "EAST", new Command("EAST", new string[] { "EAST", "E"}, game => Move(game, Directions.East)) },
                { "WEST", new Command("WEST", new string[] { "WEST", "W" }, game => Move(game, Directions.West)) },
                { "REWARD", new Command("REWARD", new string[] { "REWARD", "R",  }, AddReward) },
                { "SCORE", new Command("RSCORE", new string[] { "SCORE" }, AskScore) },


            };
        }

        public void Start(IInputService input, IOutputService output)
        {

            Assert.IsNotNull(output);
            Output = output;

            Assert.IsNotNull(input);
            Input = input;
            Input.InputReceived += InputReceivedHandler;

            IsRunning = true;
            Output.WriteLine($"{WelcomeMessage}");



        }


        private void InputReceivedHandler(object sender,string commandString)
        {
            Command foundCommand = null;
            foreach (Command command in Commands.Values)
            {
                if (command.Verbs.Contains(commandString))
                {
                    foundCommand = command;
                    break;
                }
            }

            
            if (foundCommand != null)
            {
                foundCommand.Action(this);
                Player.Moves++;
                
            }
            else
            {
                Output.WriteLine("Unknown command.");

            }
                
        }

        private static void Move(Game game, Directions direction)
        {
            //game.Output.WriteLine($"{game.Player.Location}\n{game.Player.Location.Description}");

            if (game.Player.Move(direction) == false)
            {
                game.Output.WriteLine("The way is shut!");
            }

        }

        public static void Look(Game game) => game.Output.WriteLine($"{game.Player.Location}\n{game.Player.Location.Description}");

        private static void Quit(Game game)
        {
            game.IsRunning = false;
            game.Output.WriteLine($"{game.ExitMessage}");
            game.GameStopped?.Invoke(game, EventArgs.Empty);
            
        }

        public static void AddReward(Game game)
        {
            game.Player.Score++;
            game.Output.WriteLine("One point added!");
        }
        public static void AskScore(Game game)
        {
            game.Output.WriteLine($"Your score would be {game.Player.Score} point(s) in {game.Player.Moves} move(s)");
        }
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) => Player = new Player(World, StartingLocation);
    } 


}