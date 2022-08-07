using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
    public interface ITakeDamage
    {
        string ObjType { get; }
        string SpecificObj { get; }
        int Hp { get; }
        void TakeDamage(int dmg);
    }
}
