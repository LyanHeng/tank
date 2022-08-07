using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
    public class ItemFactory
    {
        // "create" an item
        /// <summary>
        /// Create a new item from the item factory
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        public Item MakeItem(int selector, double x, double y, IAmMediator med)
        {
            if (selector.Equals(null))
            {
                return null;
            }
            if (selector == 0)
            {
                return new ExplodeAll(x, y, med);
            }
            else if (selector >= 1)
            {
                return new RainBullet(x, y, med);
            }
            return null;
        }
    }
}
