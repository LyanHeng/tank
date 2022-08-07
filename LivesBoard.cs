using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class LivesBoard
    {
        private static LivesBoard _firstInstance = null;
        protected IAmMediator _mediator;

        private LivesBoard(IAmMediator mediator)
        {
            _mediator = mediator;
        }

        // initialise score board
        /// <summary>
        /// Initialise a score board
        /// </summary>
        /// <returns></returns>
        public static LivesBoard GetInstance(IAmMediator med)
        {
            if (_firstInstance == null)
            {
                _firstInstance = new LivesBoard(med);
            }
            return _firstInstance;
        }

        // draw the lives board on window
        /// <summary>
        /// Draw lifeboard on window
        /// </summary>
        /// <param name="windowWidth"></param>
        /// <param name="windowHeight"></param>
        /// <param name="player"></param>
        /// <param name="gameMap"></param>
        public void Draw(double windowWidth, double windowHeight)
        {
            Player player = _mediator.GetObject("player") as Player;
            DrawLife(player, windowWidth);
            DrawHpExp(windowWidth, windowHeight, player);
            DrawLevel();
        }

        // draw lives on lifeboard
        /// <summary>
        /// Draw lives on lifeboard
        /// </summary>
        /// <param name="player"></param>
        /// <param name="windowWidth"></param>
        private void DrawLife(Player player, double windowWidth)
        {
            // lives 
            Bitmap lives = new Bitmap("lives", "lives.png");
            for (int i = 0; i < player.Lives; i++)
            {
                SplashKit.DrawBitmap(lives, windowWidth - (player.Lives - i) * 52, 250);
            }
        }

        // draw Hp bar and Exp bar
        /// <summary>
        /// Draw Hp bar and Experience bar on window
        /// </summary>
        /// <param name="windowWidth"></param>
        /// <param name="windowHeight"></param>
        /// <param name="player"></param>
        /// <param name="gameMap"></param>
        private void DrawHpExp(double windowWidth, double windowHeight, Player player)
        {
            // healthbar and score
            // frame - scale
            SplashKit.FillRectangle(Color.Black, windowWidth - 180, windowHeight - 7 * 60, 30, windowHeight / 3);
            SplashKit.FillRectangle(Color.Black, windowWidth - 90, windowHeight - 7 * 60, 30, windowHeight / 3);
            // value
            double hpRatio = player.Hp / 100.0;
            double hpBarHeight = hpRatio * windowHeight / 3;
            double hpPosY = (windowHeight - 7 * 60) + (windowHeight / 3 - (hpRatio * windowHeight / 3));
            double scoreRatio = player.Score / 2000.0;
            double scoreBarHeight = scoreRatio * windowHeight / 3;
            double scorePosY = (windowHeight - 7 * 60) + (windowHeight / 3 - (scoreRatio * windowHeight / 3));

            SplashKit.FillRectangle(Color.Green, windowWidth - 180, hpPosY, 30, hpBarHeight);
            SplashKit.FillRectangle(Color.Yellow, windowWidth - 90, scorePosY, 30, scoreBarHeight);
        }

        // show level
        /// <summary>
        /// Show level on window
        /// </summary>
        private void DrawLevel()
        {
            SplashKit.DrawText("level : " + Convert.ToString(_mediator.Level), Color.White, "calibri", 100000, 1040, 140);
        }
    }
}
