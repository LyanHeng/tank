using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public abstract class Tank : GameObject, ITakeDamage
    {
        private char _orientation = 'u';
        private short _fireRate = 1;

        public Tank(string[] id, int hp, double x, double y, IAmMediator med) : base(id, hp, x, y, med)
        { }

        public char Orientation { get => _orientation; set => _orientation = value; }
        public short FireRate { get => _fireRate; set => _fireRate = value; }

        // draw tank based on Orientation
        public override void Draw()
        {
            string bmName = SpecificObj + GetSpriteFile(Orientation);
            string bmFile = bmName + ".png";
            // *** look at implementing exception since it involves files ***
            ObjBM = new Bitmap(bmName, bmFile);
            if (ObjBM != null)
                SplashKit.DrawBitmap(ObjBM, X, Y);
        }

        // move tank - check that it won't collide before moving
        /// <summary>
        /// Move tank with provided velocity
        /// </summary>
        /// <param name="veloX"></param>
        /// <param name="veloY"></param>
        public void Move(double veloX, double veloY)
        {
            if (!Mediator.CollideBtw(this, new string[] { "wall" }, veloX * 1.1, veloY * 1.1))
            {
                if (veloY > 0)
                    Orientation = 'u';
                else if (veloY < 0)
                    Orientation = 'd';
                else if (veloX > 0)
                    Orientation = 'l';
                else
                    Orientation = 'r';

                X -= veloX;
                Y -= veloY;
            }
        }

        // shoot bullets
        /// <summary>
        /// Tank creates a bullet based on tank's orientation
        /// </summary>
        public virtual void Shoot() 
        {
            double displaceX = 0, displaceY = 0;
            switch (Orientation)
            {
                case 'u':
                    displaceY = 50;
                    break;
                case 'd':
                    displaceY = -50;
                    break;
                case 'l':
                    displaceX = 50;
                    break;
                case 'r':
                    displaceX = -50;
                    break;
            }
            for (int i = 1; i <= FireRate; i++)
            {
                Bullet fireBullet = new Bullet(X - (displaceX * i), Y - (displaceY * i), Orientation, Mediator);
                Mediator.AddObject(fireBullet);
            }
        }
    }
}
