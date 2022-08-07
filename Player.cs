using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class Player : Tank
    {
        private int _lives, _score;

        public Player(IAmMediator med) : base(new string[] { "tank", "player" }, 100, 60, 780, med)
        {
            _lives = 3;
            _score = 0;
            med.AddObject(this);
        }

        public int Lives { get => _lives; }
        public int Score { get => _score; }

        // player die
        /// <summary>
        /// Deduct a life from player tank
        /// </summary>
        /// <returns></returns>
        public int LoseLife()
        {
            if (Lives > 0)
            {
                _lives--;
                this.Hp = 100;
                this.X = 60;
                this.Y = 780;
                if (this._score >= 200)
                    this._score -= 200;
            }
            return Lives;
        }
        
        // increase score
        /// <summary>
        /// Increase player's score
        /// </summary>
        /// <param name="score"></param>
        public void UpdateScore(int score)
        {
            _score += score;
        }

        // check if score passes level and check health
        /// <summary>
        /// Check player's score if won and check player's hp if died
        /// </summary>
        public override void Update()
        {
            CheckScore();
            base.Update();
        }

        // check to increase level
        /// <summary>
        /// Check player's score if it has reached level's full score
        /// </summary>
        private void CheckScore()
        {
            if (Score >= 2000.0 && Mediator.Level <= 3)
            {
                Mediator.Level++;
                if (Mediator.Level == 4)
                {
                    Mediator.EndGame(false);
                }
                _score = 0;
                this.X = 60;
                this.Y = 780;
            }
        }
    }
}
