﻿using System;

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
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Mezok.Alapok.IInteraktivMezo item in Jatekos.InteraktivMezok)
            {
                Console.WriteLine(item.Nev);
            }
            Console.CursorTop--;
            RenderCurrentLine(false);
        }
        /// <summary>
        /// Kezeli az entert
        /// Ha mezőben van, akkor meghívja az kurzor helyén levo szöveg action párját
        /// Ha nem mezőben akkor belép a mezőbe és kiírja az adatait
        /// </summary>
        private static void Enter()
        {
            if (!mezoben)
            {
                if (Jatekos.InteraktivMezok[Console.CursorTop].VanBennePublikusMetodus)
                {
                    InteraktivMezoRender(Console.CursorTop);
                }
            }
            else if (Jatekos.InteraktivMezok[mezoindex].Metodusok.Count > 0)
            {
                string[] array = new string[Jatekos.InteraktivMezok[mezoindex].Metodusok.Count];
                Jatekos.InteraktivMezok[mezoindex].Metodusok.Keys.CopyTo(array, 0);
                Jatekos.InteraktivMezok[mezoindex].Metodusok[
                        array[Console.CursorTop -
                            Jatekos.InteraktivMezok[mezoindex].Parameterek.
                            Split('\n').Length]].Invoke();
                mezoben = false;
                AlapRender();
            }
        }
        /// <summary>
        /// Kezeli az Escape billentyűt
        /// Ha mezoben van, akkor kilép a játékos-hoz és kiírja a játékos mezőit
        /// Ha nem mezőben van, akkor az escaped true
        /// </summary>
        private static void Escape()
        {
            if (!mezoben)
            {
                escaped = true;
            }
            else
            {
                mezoben = false;
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
            RenderCurrentLine(false);
        }
        /// <summary>
        /// Kiírja az interaktív mező metódusainak nevét
        /// </summary>
        /// <param name="index"></param>
        private static void InteraktivMezoRender(int index)
        {
            Console.Clear();
            mezoindex = index;
            mezoben = true;
            Console.WriteLine(Jatekos.InteraktivMezok[index].Parameterek);
            foreach (string item in Jatekos.InteraktivMezok[index].Metodusok.Keys)
            {
                Console.WriteLine(item);
            }
            Console.CursorTop--;
        }
        /// <summary>
        /// Kirendereli a jelenlegi sort
        /// </summary>
        /// <param name="eredeti">Ha igaz, akkor a háttérszínt nem vátloztatja meg, ha hamis, akkor a háttérszínt fehérre változtatja</param>
        private static void RenderCurrentLine(bool eredeti)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            if (!eredeti)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (!mezoben)
            {
                Console.Write(Jatekos.InteraktivMezok[Console.CursorTop].Nev);
            }
            else
            {
                if (Jatekos.InteraktivMezok[mezoindex].Metodusok.Count > 0)
                {
                    string[] array = new string[Jatekos.InteraktivMezok[mezoindex].Metodusok.Count];
                    Jatekos.InteraktivMezok[mezoindex].Metodusok.Keys.CopyTo(array, 0);
                    Console.Write(array[Console.CursorTop - Jatekos.InteraktivMezok[mezoindex].Parameterek.Split('\n').Length]);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// S megnyomását kezeli
        /// Egyel lejebb ugrik a "kurzor", ha van lejebb
        /// </summary>
        private static void S()
        {
            int hatar = 0;
            if (!mezoben)
            {
                hatar = Jatekos.InteraktivMezok.Count - 1;
            }
            else
            {
                hatar = Jatekos.InteraktivMezok[mezoindex].Parameterek.Split('\n').Length
                    + Jatekos.InteraktivMezok[mezoindex].Metodusok.Count - 1;
            }
            if (Console.CursorTop < hatar)
            {
                Console.CursorTop++;
            }
        }

        /// <summary>
        /// W megnyomását kezeli:
        /// Egyel feljebb ugrik, ha van feljebb
        /// </summary>
        private static void W()
        {
            int hatar = 1;
            if (mezoben)
            {
                hatar = Jatekos.InteraktivMezok[mezoindex].Parameterek.Split('\n').Length + 1;
            }
            if (Console.CursorTop >= hatar)
            {
                Console.CursorTop--;
            }
        }

        #endregion Private Methods

        #region Public Properties

        public static Jatekos Jatekos { private get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Kezeli a játékos interakciójának életciklusát
        /// </summary>
        public static void Render()
        {
            if (!Jatekos.Vesztett)
            {
                Console.CursorVisible = false;
                AlapRender();
                escaped = false;
                while (!escaped)
                {
                    InterAkcio();
                }
                Console.Clear();
                Console.WriteLine("Következő játékos");
                System.Threading.Thread.Sleep(500);
            }
        }

        #endregion Public Methods

        #region Private Fields

#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
        private static bool escaped = false;
#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables
        private static bool mezoben = false;
        private static int mezoindex;

        #endregion Private Fields
    }
}
