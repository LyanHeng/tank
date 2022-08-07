using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace TankGame
{
    public class ExplodeAll : Item
    {
        public ExplodeAll(double x, double y, IAmMediator med) : base(new string[] { "item", "explodeAll" }, x, y, med)
        {
            Hp = 10;
        }

        // kill all enemies
        /// <summary>
        /// Kill all enemy tanks
        /// </summary>
        /// <param name="damaged"></param>
        public override void TakesEffect(ITakeDamage damaged)
        {
            this.TakeDamage(Hp);
            List<GameObject> enemy = Mediator.GetObjectTypeList("enemy");
            Player player = Mediator.GetObject("player") as Player;
            player.UpdateScore(25 * enemy.Count);
            foreach (Enemy en in enemy)
                Mediator.RemoveObject(en);
        }
    }
}
