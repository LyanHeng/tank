using System;
using SplashKitSDK;
using System.Collections.Generic;
using TankGame;
using System.IO;
using System.Text;

public class Program
{
    public static void Main()
    {
        int level = 0;
        int counter = 0, shootCoolDown = 0;
        int windowWidth = 1200;
        int windowHeight = 900;
        int speed = 5;

        // new shape and window opened
        new Window("Tank Game", windowWidth, windowHeight);

        // required objects
        Map gameMap = Map.GetInstance(1);
        LivesBoard scoreBoard = LivesBoard.GetInstance(gameMap);
        Fort fort = new Fort(gameMap);
        Player player = new Player(gameMap);
        EnemyPool enemyPool = EnemyPool.GetInstance(1, gameMap);

        do {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();
            gameMap.DrawObjects();
            scoreBoard.Draw(windowWidth, windowHeight);

            // load map - if new map is needed
            if (level != gameMap.Level)
            {
                level = gameMap.LoadNextLevel(level);
                enemyPool.UpdateMax(level);
            }

            // generate new enemies if needed 
            if (SplashKit.Rnd(1, 500) <= 5)
            {
                enemyPool.GenerateEnemy(windowWidth, windowHeight);
            }
            
            // demo - turn up, down, left, right
            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                player.Move(0, speed);
            }
            else if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                player.Move(0, -1 * speed);
            }
            else if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                player.Move(speed, 0);
            }
            else if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                player.Move(-1 * speed, 0);
            }

            // press space - player shoot
            if (SplashKit.KeyTyped(KeyCode.SpaceKey) && shootCoolDown > 50)
            {
                player.Shoot();
                shootCoolDown = 0;
            }

            // generate a random powerup
            if (counter >= 500 && gameMap.CountObjects("item") < 1)
            {
                gameMap.GenerateRndItem(windowWidth, windowHeight);
                counter = 0;
            }

            // delete the powerUp after awhile, also cancels powerups
            if (gameMap.CountObjects("item") > 0 && counter >= 500)
            {
                gameMap.RemoveObject(gameMap.GetObject("item"));
                counter = 0;
            }

            // cancels powerup effects after a while
            if (player.FireRate == 3 && counter >= 400)
            {
                player.FireRate = 1;
                shootCoolDown = 0;
            }

            gameMap.Update();
            shootCoolDown++;
            counter++;
            SplashKit.RefreshScreen(60);
        } while (!SplashKit.WindowCloseRequested("Tank Game"));
    }
}
