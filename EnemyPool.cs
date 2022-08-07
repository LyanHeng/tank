using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class EnemyPool
    {
        protected IAmMediator _mapMediator;
        private List<Enemy> _enemyList = new List<Enemy>();
        private int _max;
        private static EnemyPool _firstInstance = null;

        private EnemyPool(int level, IAmMediator mediator) 
        {
            UpdateMax(level);
            _mapMediator = mediator;
        }

        // initialise an enemy pool
        /// <summary>
        /// Initialise an enemy pool
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static EnemyPool GetInstance(int level, IAmMediator mediator)
        {
            if (_firstInstance == null)
            {
                _firstInstance = new EnemyPool(level, mediator);
            }
            return _firstInstance;
        }

        public int Max { get => _max; }
        public IAmMediator Mediator { get => _mapMediator; }

        // generate enemy when below maximum number
        /// <summary>
        /// Cycle a new enemy into the game if below maximum
        /// </summary>
        /// <param name="windowWidth"></param>
        /// <param name="windowHeight"></param>
        public void GenerateEnemy (double windowWidth, double windowHeight)
        {
            double rndX = SplashKit.Rnd(50, (int)windowWidth - 240);
            double rndY = SplashKit.Rnd(50, (int)windowHeight - 50);
            Enemy newEnemy = new Enemy(rndX, rndY, Mediator);
            if (NotEnoughEnemy() && !Mediator.CollideBtw(newEnemy, new string[] { "wall", "tank" }, 0, 0))
            {
                Mediator.AddObject(newEnemy);
            }
        }

        // check if there are enough enemy
        /// <summary>
        /// Check if enemy number is below maximum of each level
        /// </summary>
        /// <returns></returns>
        private bool NotEnoughEnemy()
        {
            return Mediator.CountObjects("enemy") < Max;
        }

        // update maximum number of enemy in each level
        /// <summary>
        /// Update the maximum number of enemy in each level
        /// </summary>
        /// <param name="level"></param>
        public void UpdateMax(int level)
        {
            _max = Convert.ToInt32(Math.Pow(2, level-1)) + 3;
        }
    }
}
