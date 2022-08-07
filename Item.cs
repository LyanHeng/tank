using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public abstract class Item : Weapon
    {
        public Item(string[] id, double x, double y, IAmMediator med) : base(id, x, y, med)
        { }

        // check collisions
        public override void Update()
        {
            ItemCollision();
            base.Update();
        }

        // check if item collided with other objects
        /// <summary>
        /// Check for item's collision with other objects in list
        /// </summary>
        public void ItemCollision()
        {
            if (Mediator.CollideBtw(this, new string[] { "player" }, 0, 0))
            {
                this.TakesEffect(Mediator.GetObject("player") as ITakeDamage);
            }
        }
    }
}
