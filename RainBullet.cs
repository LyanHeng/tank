using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class RainBullet : Item
    {
        public RainBullet(double x, double y, IAmMediator med) : base(new string[] { "item", "rainBullet" }, x, y, med)
        {
            Hp = 10;
        }

        // tell player to shoot three shots
        /// <summary>
        /// Makes player fire three bullets at once
        /// </summary>
        /// <param name="damaged"></param>
        public override void TakesEffect(ITakeDamage damaged)
        {
            this.TakeDamage(Hp);
            GameObject player = Mediator.GetObject("player");
            (player as Player).FireRate = 3;
        }
    }
}
