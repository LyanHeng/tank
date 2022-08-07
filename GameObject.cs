using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public abstract class GameObject
    {
        protected IAmMediator _mapMediator;
        private string[] _id;
        private int _hp;
        private double _x;
        private double _y;
        private Bitmap _objBM;

        public GameObject(string[] id, int hp, double x, double y, IAmMediator mediator)
        {
            _id = id;
            _x = x;
            _y = y;
            _hp = hp;
            if (!(AreYou("tank") || AreYou("bullet")))
                _objBM = new Bitmap(SpecificObj, SpecificObj + ".png");
            _mapMediator = mediator;
        }

        // id = { type of object , specific object }
        public string ObjType { get => _id[0]; }
        public string SpecificObj { get => _id[1]; }
        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }
        public int Hp { get => _hp; set => _hp = value; }
        public Bitmap ObjBM { get => _objBM; set => _objBM = value; }
        public IAmMediator Mediator { get => _mapMediator; }


        // object losing health
        /// <summary>
        /// Deducts object's Hp
        /// </summary>
        /// <param name="dmg"></param>
        public void TakeDamage(int dmg)
        {
            _hp -= dmg;
        }

        // check that the object is itself
        /// <summary>
        /// Check if object has the identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AreYou(string id)
        {
            return id == SpecificObj || id == ObjType;
        }

        // check if the object and another object collided
        /// <summary>
        /// Check if the object collided with a given object with offsets
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="moveX"></param>
        /// <param name="moveY"></param>
        /// <returns></returns>
        public bool Collided(GameObject obj, double offsetX, double offsetY)
        {
            return SplashKit.BitmapCollision(this.ObjBM,this.X,this.Y,obj.ObjBM,obj.X-offsetX, obj.Y-offsetY);
        }

        // get sprite name for draw
        /// <summary>
        /// Retrive files based on orientation
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public string GetSpriteFile(char orientation)
        {
            string bmName;
            switch (orientation)
            {
                case 'd':
                    bmName = "Down";
                    break;
                case 'l':
                    bmName = "Left";
                    break;
                case 'r':
                    bmName = "Right";
                    break;
                default:
                    bmName = "Up";
                    break;
            }
            return bmName;
        }

        // instructions on drawing object
        /// <summary>
        /// Draw object's bitmap on Window
        /// </summary>
        public virtual void Draw()
        {
            if (ObjBM != null)
            {
                SplashKit.DrawBitmap(ObjBM, X, Y);
            }
        }

        // check if the object is dead
        /// <summary>
        /// Check if Hp is 0
        /// </summary>
        /// <returns></returns>
        public bool IsDestroyed()
        {
            return Hp <= 0;
        }

        // update the object for each loop
        /// <summary>
        /// Update actions or check conditions of objects
        /// </summary>
        public virtual void Update() 
        {
            // remove any dead object
            if (IsDestroyed())
            {
                Mediator.RemoveObject(this);
            }
        }
    }
}
