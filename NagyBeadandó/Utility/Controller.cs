using System;

namespace NagyBeadandó.Utility
{
    /// <summary>
    /// A játékos Interakciójáért felel
    /// </summary>
    internal static class Controller
    {
        #region Private Methods

        /// <summary>
        /// Kirendereli a játékos adatait és a mezőit
        /// </summary>
        private static void AlapRender()
        {
            render = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Mezok.Alapok.IInteraktivMezo item in Jatekos.InteraktivMezok)
            {
                Console.WriteLine(item.Nev);
            }
            RenderCurrentLine(false);
        }
        /// <summary>
        /// Kezeli az entert
        /// Ha mezőben van, akkor meghívja az kurzor helyén levo szöveg action párját
        /// Ha nem mezőben akkor belép a mezőbe és kiírja az adatait
        /// </summary>
        private static void Enter()
        {
            Logger.Log("Enter leütve");
            if (!mezoben)
            {
                if (Jatekos.InteraktivMezok[kurzorhelye].VanBennePublikusMetodus)
                {
                    InteraktivMezoRender(kurzorhelye);
                }
            }
            /// Végrehajtjat az enter ütés sorában a szöveg-hez tartozó metódust
            else if (Jatekos.InteraktivMezok[mezoindex].Metodusok.Count > 0)
            {
                render = false;
                string[] array = new string[Jatekos.InteraktivMezok[mezoindex].Metodusok.Count];
                Jatekos.InteraktivMezok[mezoindex].Metodusok.Keys.CopyTo(array, 0);
                Jatekos.InteraktivMezok[mezoindex].Metodusok[
                        array[kurzorhelye -
                            Jatekos.InteraktivMezok[mezoindex].Parameterek.
                            Split('\n').Length]].Invoke();
                /// majd kilép a mezőből és alaprender-t végrehajtja
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
            Logger.Log("Escape leütve");
            if (!mezoben)
            {
                render = false;
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
        /// És kiemeli a kurzor sorának szövegét
        /// És visszaállítja a kurzor előző helyén a szöveget
        /// </summary>
        private static void InterAkcio()
        {
            ConsoleKey ck = Console.ReadKey(true).Key;
            kurzorhelye = Console.CursorTop;
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
            render = true;
            Logger.Log("Mezőbe lépve");
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
            Console.SetCursorPosition(0, kurzorhelye);
            if (!eredeti)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (!mezoben)
            {
                Console.Write(Jatekos.InteraktivMezok[kurzorhelye].Nev);
            }
            else
            {
                if (Jatekos.InteraktivMezok[mezoindex].Metodusok.Count > 0)
                {
                    string[] array = new string[Jatekos.InteraktivMezok[mezoindex].Metodusok.Count];
                    Jatekos.InteraktivMezok[mezoindex].Metodusok.Keys.CopyTo(array, 0);
                    Console.Write(array[kurzorhelye - Jatekos.InteraktivMezok[mezoindex].Parameterek.Split('\n').Length]);
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
            Logger.Log("S leütve");
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
            if (kurzorhelye < hatar)
            {
                kurzorhelye++;
            }
        }
        /// <summary>
        /// W megnyomását kezeli:
        /// Egyel feljebb ugrik, ha van feljebb
        /// </summary>
        private static void W()
        {
            Logger.Log("W leütve");
            int hatar = 1;
            if (mezoben)
            {
                hatar = Jatekos.InteraktivMezok[mezoindex].Parameterek.Split('\n').Length + 1;
            }
            if (kurzorhelye >= hatar)
            {
                kurzorhelye--;
            }
        }

        #endregion Private Methods

        #region Public Properties

        /// <summary>
        /// Az aktuálisan aktív játékos, aki a controllert irányítja
        /// Vagy akit a kontroller irányít
        /// </summary>
        public static Jatekos Jatekos { private get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Kezeli a játékos interakciójának életciklusát
        /// </summary>
        public static void Interact()
        {
            if (!Jatek.JatekVege())
            {
                Console.CursorVisible = false;
                AlapRender();
                escaped = false;
                while (!escaped)
                {
                    InterAkcio();
                }
                Logger.Log("Következő játékos");
            }
        }
        public static void Render()
        {
            Console.Clear();
            if (render)
            {
                if (mezoben)
                {
                    InteraktivMezoRender(mezoindex);
                }
                else
                {
                    AlapRender();
                }
            }
        }

        #endregion Public Methods

        #region Private Fields

        private static bool render = false;
        private static int kurzorhelye;
#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
        /// <summary>
        /// Tárolja, hogy kilépett-e a játékos az interakcióból
        /// </summary>
        private static bool escaped = false;
#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables
        /// <summary>
        /// Tárolja, hogy a játékos éppen mezőben van, vagy az alap kiírásoknál
        /// </summary>
        private static bool mezoben = false;
        /// <summary>
        /// Tárolja, hogy melyik mezőben van a játékos
        /// </summary>
        private static int mezoindex;

        #endregion Private Fields
    }
}
