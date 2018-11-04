using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class DiceGame
    {
        private const int Goal = 13;
        private const int MaxTries = 3;

        private readonly Dice _dice = new Dice();

        private bool _active = false;
        private bool _gameOver = false;
        private bool _playerStatsShowing = false;
        private int _tries = 0;
        private int _score = 0;

        public DiceGame()
        {
            Console.Title = "Dice Game";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        public void Run()
        {
            this.DisplaySplashScreen();

            while(true)
            {
                this.ListenForUserInput();
                this.ListenForGameOver();
            }
        }

        private void ListenForUserInput()
        {
            bool whileSplashScreen = !this._active;
            bool whilePlayerStatsScreen = this._playerStatsShowing;
            bool whilePlayingScreen = this._active && !this._gameOver;
            bool whileGameOverScreen = this._active && this._gameOver;
            ConsoleKey key = Console.ReadKey(true).Key;

            if (whilePlayingScreen)
            {
                switch (key)
                {
                    case ConsoleKey.Escape:
                        this.Quit();
                        break;
                    case ConsoleKey.Spacebar:
                        this.Play();
                        break;
                }

                return;
            }
            if (whileGameOverScreen)
            {
                switch (key)
                {
                    case ConsoleKey.Escape:
                        this.Quit();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        this.Start();
                        break;
                }

                return;
            }
            if (whilePlayerStatsScreen)
            {
                if (!key.Equals(ConsoleKey.Escape)) return;
                this._playerStatsShowing = false;
                this.DisplaySplashScreen();
                return;
            }

            if (!whileSplashScreen) return;
            switch (key)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    this.Start();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.S:
                    this.ShowPlayerStats();
                    break;
            }

            return;
        }

        private void ListenForGameOver()
        {
            if (this._score >= Goal)
            {
                this.Win();
            }
            if (this._tries == MaxTries)
            {
                this.Lose();
            }
        }

        private void Start()
        {
            DisplayControls();

            this._active = true;
            this._gameOver = false;
        }

        private void Quit()
        {
            this.DisplaySplashScreen();
            this._active = false;
            this._gameOver = false;
            this.Reset();
        }

        private void Play()
        {
            int rolledScore = _dice.Roll();

            _dice.DisplayRollMessage(rolledScore);

            this._score += rolledScore;
            this._tries++;
        }

        private void ShowPlayerStats()
        {
            this._playerStatsShowing = true;
            this.DisplayPlayerStats();
        }

        private void Lose()
        {
            this._gameOver = true;
            PlayerStatsSingleton.GetInstance().Losses++;
            this.DisplayLosingMessage();
            this.Reset();
        }

        private void Win()
        {
            this._gameOver = true;
            PlayerStatsSingleton.GetInstance().Wins++;
            this.DisplayWinningMessage();
            this.Reset();
        }

        private void Reset()
        {
            this._tries = 0;
            this._score = 0;
        }
         
        private void DisplaySplashScreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("████████▄   ▄█   ▄████████    ▄████████         ▄██████▄     ▄████████   ▄▄▄▄███▄▄▄▄      ▄████████");
            Console.WriteLine("███   ▀███ ███  ███    ███   ███    ███        ███    ███   ███    ███ ▄██▀▀▀███▀▀▀██▄   ███    ███");
            Console.WriteLine("███    ███ ███▌ ███    █▀    ███    █▀         ███    █▀    ███    ███ ███   ███   ███   ███    █▀");
            Console.WriteLine("███    ███ ███▌ ███         ▄███▄▄▄           ▄███          ███    ███ ███   ███   ███  ▄███▄▄▄");
            Console.WriteLine("███    ███ ███▌ ███        ▀▀███▀▀▀          ▀▀███ ████▄  ▀███████████ ███   ███   ███ ▀▀███▀▀▀");
            Console.WriteLine("███    ███ ███  ███    █▄    ███    █▄         ███    ███   ███    ███ ███   ███   ███   ███    █▄");
            Console.WriteLine("███   ▄███ ███  ███    ███   ███    ███        ███    ███   ███    ███ ███   ███   ███   ███    ███");
            Console.WriteLine("████████▀  █▀   ████████▀    ██████████        ████████▀    ███    █▀   ▀█   ███   █▀    ██████████");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("Objective:");
            Console.WriteLine("Reach a score of 13 by rolling the dice 3 times");
            Console.WriteLine("");
            Console.WriteLine("Controls:");
            Console.WriteLine("Press the spacebar to roll the dice");
            Console.WriteLine("Press escape to quit the game");
            Console.WriteLine("");
            Console.WriteLine("Press enter to start");
            Console.WriteLine("Press S to show player statistics");
        }

        private static void DisplayControls()
        {
            Console.WriteLine("Spacebar: roll dice | Escape: back to home");
            Console.WriteLine("");
        }

        private void DisplayLosingMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Oh no! You lose!");

            this.DisplayGameOverMessage();
        }

        private void DisplayWinningMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Congratulations! You win!");
            
            this.DisplayGameOverMessage();
        }

        private void DisplayGameOverMessage()
        {
            this.DisplayWinsAndLosses();
            Console.WriteLine("Press enter to start a new game");
        }

        private void DisplayPlayerStats()
        {
            Console.Clear();
            Console.WriteLine("Player statistics");
            this.DisplayWinsAndLosses();
            Console.WriteLine("Press escape to go back to home screen");
        }

        private void DisplayWinsAndLosses()
        {
            Console.WriteLine("");
            Console.WriteLine("Total games won: " + PlayerStatsSingleton.GetInstance().Wins);
            Console.WriteLine("Total games lost: " + PlayerStatsSingleton.GetInstance().Losses);
            Console.WriteLine("");
        }
    }
}
