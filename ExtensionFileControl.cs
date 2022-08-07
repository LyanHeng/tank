using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TankGame
{
    public static class ExtensionFileControl
    {
        // read integer

        /// <summary>
        /// Using streamreader, reads an integer
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static int ReadInteger(this StreamReader reader)
        {
            return Convert.ToInt32(reader.ReadLine());
        }
    }
}
