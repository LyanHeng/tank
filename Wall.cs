using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
    public class Wall : GameObject, ITakeDamage
    {
        public Wall(string[] id, int x, int y, IAmMediator med) : base(id, 0, x, y, med)
        {
            Hp = WallTypeHp(id);
        }

        // retrieve Hp for wall type
        /// <summary>
        /// retrieve Hp for provided wall type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int WallTypeHp(string[] id)
        {
            switch(id[1])
            {
                case "stone":
                    return 500;
                case "water":
                    return 100000;
                case "brick":
                    return 60;
                default:
                    return 0;
            }
        }
    }
}
