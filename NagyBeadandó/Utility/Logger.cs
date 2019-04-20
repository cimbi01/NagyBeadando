using System;
using System.IO;

namespace NagyBeadandó.Utility
{
    public static class Logger
    {
        #region Public Constructors

        public static void Close()
        {
            file.Close();
        }
        public static void Log(string szoveg)
        {
            file.WriteLine(szoveg);
            file.Flush();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(szoveg);
            System.Threading.Thread.Sleep(300);
            Console.Clear();
            Controller.Render();
        }

        #endregion Public Constructors

        #region Private Fields

        private static StreamWriter file = new StreamWriter("file.txt");

        #endregion Private Fields
    }
}
