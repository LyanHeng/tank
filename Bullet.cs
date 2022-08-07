using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class Bullet : Weapon
    {
        private char _orientation; 
        private double _speed = 7;
        public Bullet(double x, double y, char orientation, IAmMediator med) : base(new string[] { "weapon", "bullet" }, x, y, med)
        {
            Damage = 10;
            _orientation = orientation;
        }
        public double Speed { get => _speed; }
        public char Orientation { get => _orientation; set => _orientation = value; }

        // move in a pre-determined direction
        /// <summary>
        /// Move bullet
        /// </summary>
        public void Move()
        {
            switch (Orientation)
            {
                case 'u':
                    Y -= Speed;
                    break;
                case 'd':
                    Y += Speed;
                    break;
                case 'l':
                    X -= Speed;
                    break;
                case 'r':
                    X += Speed;
                    break;
            }
        }

        // draw bullet
        /// <summary>
        /// Draw Bullet as Bitmap on Window
        /// </summary>
        public override void Draw()
        {
            string bmName = "bullet" + GetSpriteFile(Orientation);
            string bmFile = bmName + ".png";
            ObjBM = new Bitmap(bmName, bmFile);
            if (ObjBM != null)
            {
                SplashKit.DrawBitmap(ObjBM, X, Y);
            }
        }

        // apply damages to the thing collided
        /// <summary>
        /// Bullet damages an object
        /// </summary>
        /// <param name="damaged"></param>
        public override void TakesEffect(ITakeDamage damaged)
        {
            // take away all its health - essentially deleting it
            this.TakeDamage(Hp);
            if (damaged is null)
                return;
            damaged.TakeDamage(Damage);
        }

        // update bullet
        /// <summary>
        /// Update Bullet's position & check for collision
        /// </summary>
        public override void Update()
        {
            Move();
            BulletCollision();
            base.Update();
        }

        // bullet collide with tank or walls
        /// <summary>
        /// Check bullet's collision with tanks and walls
        /// </summary>
        private void BulletCollision()
        {
            if (Mediator.CollideBtw(this, new string[] { "tank", "wall" }, 0.5, 0.5))
            {
                TakesEffect(Mediator.CollideWith(this) as ITakeDamage);
            }
        }
    }
}
