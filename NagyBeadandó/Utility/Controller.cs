using System;

namespace NagyBeadandó.Utility
{
    internal static class Controller
    {
        #region Private Methods

        /// <summary>
        /// Kirendereli a játékos adatait és a mezőit
        /// </summary>
        private static void AlapRender()
        {
            Console.Clear();
            foreach (Mezok.Alapok.IInteraktivMezo item in Jatekos.InteraktivMezok)
            {
                Console.WriteLine(item.Nev);
            }
        }
        private static void Enter()
        {
            if (!mezoben)
            {
                InteraktivMezoRender(Console.CursorTop);
            }
            else
            {
                string[] array = new string[Jatekos.InteraktivMezok[mezoindex].Metodusok.Count];
                Jatekos.InteraktivMezok[mezoindex].Metodusok.Keys.CopyTo(array, 0);
                Jatekos.InteraktivMezok[mezoindex].Metodusok[array[Console.CursorTop]].Invoke();
            }
        }
        private static void Escape()
        {
            if (!mezoben)
            {
                escaped = true;
            }
            else
            {
                AlapRender();
            }
        }
        /// <summary>
        /// Kezeli a játékos gomb lenyomását
        /// </summary>
        private static void InterAkcio()
        {
            ConsoleKey ck = Console.ReadKey(true).Key;
            RenderCurrentLine(true);
            switch (ck)
            {
                // W-nél egy-el feljebb megy a cursor és azt írja ki más színnel ami ott van
                case ConsoleKey.W:
                    W();
                    break;
                case ConsoleKey.S:
                    S();
                    break;
                case ConsoleKey.Enter:
                    Enter();
                    break;
                case ConsoleKey.Escape:
                    Escape();
                    break;
            }
        }
        private static void InteraktivMezoRender(int index)
        {
            Console.Clear();
            mezoindex = index;
            mezoben = true;
            foreach (string item in Jatekos.InteraktivMezok[index].Metodusok.Keys)
            {
                Console.WriteLine(item);
            }
        }
        private static void RenderCurrentLine(bool eredeti)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            if (!eredeti)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.Write(Jatekos.InteraktivMezok[Console.CursorTop].Nev);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        private static void S()
        {
            if (Console.CursorTop < Jatekos.InteraktivMezok.Count)
            {
                Console.CursorTop++;
                Console.Write(Jatekos.InteraktivMezok[Console.CursorTop].Nev);
            }
        }
        private static void W()
        {
            if (Console.CursorTop > 0)
            {
                Console.CursorTop--;
                RenderCurrentLine(false);
            }
        }

        #endregion Private Methods

        #region Public Properties

        public static Jatekos Jatekos { private get; set; }

        #endregion Public Properties

        #region Public Methods

        public static void Render()
        {
            AlapRender();
            while (!escaped)
            {
                InterAkcio();
            }
        }

        #endregion Public Methods

        #region Private Fields

        private static bool escaped = false;
        private static bool mezoben = false;
        private static int mezoindex;

        #endregion Private Fields
    }
}
