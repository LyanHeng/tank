using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
    public interface IAmMediator
    {
        int Level { get; set; }
        void AddObject(GameObject obj);
        void RemoveObject(GameObject obj);
        int CountObjects(string id);
        GameObject GetObject(string id);
        List<GameObject> GetObjectTypeList(string id);
        bool CollideBtw(GameObject obj, string[] ids, double diffX, double diffY);
        GameObject CollideWith(GameObject obj);
        void EndGame(bool lost);
    }
}
