using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class Fort : GameObject, ITakeDamage
    {
        public Fort(IAmMediator med) : base(new string[] { "fort", "fort" }, 10, 480, 780, med)
        {
            med.AddObject(this);
        }

        // consider enemy bumping into fort
        /// <summary>
        /// Check if enemy tanks hit or hp lost
        /// </summary>
        public override void Update()
        {
            if (Mediator.CollideBtw(this, new string[] { "enemy" }, 0.5, 0.5) || IsDestroyed())
                Mediator.EndGame(true);
            base.Update();
        }
    }
}
