using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;
using System.IO;

namespace TankGame
{
    public class Map : IAmMediator
    {
        private static Map _firstInstance = null;
        private List<GameObject> _objects = new List<GameObject>();
        private int _level;

        // private constructor
        private Map(int level)
        {
            _level = level;
        }

        // create only one instance of Map
        /// <summary>
        /// Instantiate a map
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static Map GetInstance(int level)
        {
            if (_firstInstance == null)
            {
                _firstInstance = new Map(level);
            }
            return _firstInstance;
        }

        public int Level { get => _level; set => _level = value; }
        public List<GameObject> Objects { get => _objects; }

        // load the walls of the map based on level
        /// <summary>
        /// Load the next level on the map, returns the new level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int LoadNextLevel(int level)
        {
            // map file
            StreamReader reader = new StreamReader("GameMap.txt");
            bool started = false;

            // find level in file
            while (!started)
            {
                if (reader.ReadLine() == Level.ToString())
                {
                    started = true;
                    List<GameObject> toDelete = GetObjectTypeList("wall");
                    Objects.ForEach(obj => obj.AreYou("enemy"));
                    toDelete.ForEach(td => RemoveObject(td));
                }
            }

            // generate map from file
            while (!reader.EndOfStream && level + 1 == Level)
            {
                try
                {
                    // read map
                    string obj = reader.ReadLine();
                    if (obj == "stop")
                    {
                        break;
                    }
                    int x = reader.ReadInteger();
                    int y = reader.ReadInteger();
                    Wall newWall = new Wall(new string[] { "wall", obj }, x, y, this);
                    AddObject(newWall);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Map error");
                }

            }
            reader.Close();
            return Level;
        }

        // draw the map and objects
        /// <summary>
        /// Draw all objects in the map
        /// </summary>
        public void DrawObjects()
        {
            SplashKit.ClearScreen(Color.Black);
            foreach (GameObject obj in Objects)
            {
                obj.Draw();
            }
        }

        // update objects
        /// <summary>
        /// Update all objects in map - Check for conditions, move, etc.
        /// </summary>
        public void Update()
        {
            foreach (GameObject obj in Objects.ToArray())
            {
                if (obj.AreYou("enemy"))
                    (obj as Enemy).Update();
                else if (obj.AreYou("item"))
                    (obj as Item).Update();
                else if (obj.AreYou("bullet"))
                    (obj as Bullet).Update();
                else
                {
                    obj.Update();
                }
               
            }
        }

        // add new object to list
        /// <summary>
        /// Add an object to the Object List
        /// </summary>
        /// <param name="obj"></param>
        public void AddObject(GameObject obj)
        {
            Objects.Add(obj);
        }

        // return number of object with the same type
        /// <summary>
        /// Return the number of objects with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CountObjects(string id)
        {
            return Objects.FindAll(obj => obj.AreYou(id)).Count;
        }

        // remove object from list
        /// <summary>
        /// Delete an object from the list
        /// </summary>
        /// <param name="obj"></param>
        public void RemoveObject(GameObject obj)
        {
            if (obj.AreYou("player"))
            {
                if ((obj as Player).LoseLife() <= 0)
                {
                    EndGame(true);
                }
            }
            else if (obj.AreYou("enemy"))
            {
                (GetObject("player") as Player).UpdateScore(100);
                Objects.Remove(obj);
            }
            else
            {
                Objects.Remove(obj);
            }
        }

        // get one object
        /// <summary>
        /// Retrieve an object from the list with provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameObject GetObject(string id)
        {
            return Objects.Find(obj => obj.AreYou(id));
        }

        // get a list of object types
        /// <summary>
        /// Retrieve a group of objects from the list with provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GameObject> GetObjectTypeList(string id)
        {
            return Objects.FindAll(obj => obj.AreYou(id));
        }

        // check if an object collided with specified types of objects
        /// <summary>
        /// Check if an object collides with anything in the list
        /// </summary>
        /// <param name="o"></param>
        /// <param name="ids"></param>
        /// <param name="diffX"></param>
        /// <param name="diffY"></param>
        /// <returns></returns>
        public bool CollideBtw(GameObject obj, string[] ids, double diffX, double diffY)
        {
            obj.Draw();
            List<GameObject> collidableObj = new List<GameObject>();
            if (ids.Length == 1 && ids[0] == "any")
            {
                collidableObj.AddRange(Objects.FindAll(objt => objt.ObjType != obj.ObjType));
            }
            else
            {
                foreach (string id in ids)
                    collidableObj.AddRange(GetObjectTypeList(id));
            }
            return collidableObj.Exists(objt => objt.Collided(obj, diffX, diffY));
        }

        // returns object collided with 
        /// <summary>
        /// Returns the object collided with provided object
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public GameObject CollideWith(GameObject obj)
        {
            return Objects.Find(objt => objt.Collided(obj, 0.5, 0.5));
        }

        // gameOver, win or lose
        /// <summary>
        /// Show end game window including win & lose
        /// </summary>
        /// <param name="lost"></param>
        public void EndGame(bool lost)
        {
            string window, filename;

            if (lost)
            {
                window = "Sorry, You Lost";
                filename = "gameOver";
            }
            else
            {
                window = "Lucky, You Won";
                filename = "winGame";
            }

            SplashKit.CloseCurrentWindow();
            new Window(window, 600, 300);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                Bitmap gameOver = new Bitmap(filename, filename + ".png");
                SplashKit.DrawBitmap(gameOver, 0, 0);
                SplashKit.RefreshScreen(60);
            } while (!SplashKit.WindowCloseRequested(window));
        }

        // generate random items in the map
        /// <summary>
        /// Generate random items in the window
        /// </summary>
        /// <param name="windowWidth"></param>
        /// <param name="windowHeight"></param>
        public void GenerateRndItem(int windowWidth, int windowHeight)
        {
            double rndX = SplashKit.Rnd(50, windowWidth - 240);
            double rndY = SplashKit.Rnd(50, windowHeight - 100);
            Item powerUp = new ItemFactory().MakeItem(SplashKit.Rnd(0, 4), rndX, rndY, this);
            AddObject(powerUp);
        }
    }
}
