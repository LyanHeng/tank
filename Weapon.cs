using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
    public abstract class Weapon : GameObject
    {
        private int _damage;

        public Weapon(string[] id, double x, double y, IAmMediator med) : base(id, 10, x, y, med)
        { }

        public int Damage { get => _damage; set => _damage = value; }

        // causes damages on tanks or walls
        /// <summary>
        /// Applies weapon's effect with a certain damage
        /// </summary>
        /// <param name="damaged"></param>
        public abstract void TakesEffect(ITakeDamage damaged);
    }
}
