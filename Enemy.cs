using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class Enemy : Tank
    {
        static private double _speed = 3;
        private List<double[]> _movableDirection;

        // used for movable direction - allowed in OO?
        private double[] _upDir = { 0, Speed };
        private double[] _downDir = { 0, Speed * -1 };
        private double[] _rightDir = { Speed, 0 };
        private double[] _leftDir = { Speed * -1, 0 };
        private int _rndPath = 0;

        public Enemy(double x, double y, IAmMediator med) : base(new string[] { "tank", "enemy" }, 50, x, y, med)
        {
            // move up by default
            _movableDirection = new List<double[]>();
            _movableDirection.Add(new double[] { 0, _speed});
        }
        static public double Speed { get => _speed; set => _speed = value; }

        // overload - for enemy objects to generate random movement
        /// <summary>
        /// Move instantiation of enemy
        /// </summary>
        public void Move()
        {
            // try to move in the same direction
            double beforeMoveX = X, beforeMoveY = Y;

            // making sure there's always one direction to move
            if (_movableDirection.Count == 0)
            {
                _movableDirection.Add(_upDir);
                _rndPath = 0;
            }

            try
            {
                base.Move(_movableDirection[_rndPath][0], _movableDirection[_rndPath][1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("bot's movable direction non-existence");
            }

            // keep checking direction that it can take - when it stops moving
            // up, down, right, left - in order
            if (beforeMoveX == X && beforeMoveY == Y)
            {
                _movableDirection.Clear();
                if (!Mediator.CollideBtw(this, new string[] { "wall" }, 0, Speed * 1.1) && !_movableDirection.Exists(dir => dir == _upDir))
                    _movableDirection.Add(_upDir);
                if (!Mediator.CollideBtw(this, new string[] { "wall" }, 0, Speed * -1.1) && !_movableDirection.Exists(dir => dir == _downDir))
                    _movableDirection.Add(_downDir);
                if (!Mediator.CollideBtw(this, new string[] { "wall" }, Speed * 1.1, 0) && !_movableDirection.Exists(dir => dir == _rightDir))
                    _movableDirection.Add(_rightDir);
                if (!Mediator.CollideBtw(this, new string[] { "wall" }, Speed * -1.1, 0) && !_movableDirection.Exists(dir => dir == _leftDir))
                    _movableDirection.Add(_leftDir);
                // randomize path and choose one

                if (_movableDirection.Count > 1)
                {
                    _rndPath = SplashKit.Rnd(1, 100);
                    _rndPath = _rndPath % _movableDirection.Count;
                }
            }
        }
        
        // shoot based on the percentage given
        /// <summary>
        /// Create a bullet using enemy's position
        /// </summary>
        public override void Shoot()
        {
            if (SplashKit.Rnd(1, 500) <= FireRate && !Mediator.CollideBtw(this, new string[] { "any" }, 1, 1))
                base.Shoot();
        }

        // enemy - move and shoot
        /// <summary>
        /// Move enemy and shoot based on fireRate
        /// </summary>
        public override void Update()
        {
            base.Update();
            Move();
            Shoot();
        }
    }
}
