using NagyBeadandó.Mezok;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Utility
{
    /// <summary>
    /// Feladata:
    /// Inicializálás, Játék élet ciklus kezelése
    /// </summary>
    public static class Jatek
    {
        #region Public Methods

        /// <summary>
        /// Vissszaad egy játékost a játékosokból ID alapján
        /// </summary>
        /// <param name="id">Keresett játékos ID-je</param>
        /// <returns></returns>
        public static Jatekos JatekosById(int id)
        {
            if (jatekosok[0].Id == id)
            {
                return jatekosok[0];
            }
            return jatekosok[1];
        }
        /// <summary>
        /// Játék
        /// </summary>
        public static void Play()
        {
            Init();
            int index = 0;
            do
            {
                if (index % 2 == 0)
                {
                    TevekenysegController.GorgetMind();
                }
                Controller.Jatekos = jatekosok[index % 2];
                jatekosok[index % 2].EtetTermel();
                Controller.Render();
                index++;
            } while (!jatekosok[(index + 1) % 2].Vesztett);
            System.Console.WriteLine("Játék vége");
        }

        /// <summary>
        /// Inicializálja a játékot:
        /// Két Játékost:
        /// </summary>
        private static void Init()
        {
            for (int i = 0; i < 2; i++)
            {
                InitJatekos(i);
            }
        }
        /// <summary>
        /// Inicializalja a jatekosokat:
        /// NyersanyagMezők : Búza, Agyag, Fa, Érc
        /// Raktár, Főépület, Kaszárnya
        /// </summary>
        /// <param name="index">A jatekos helye a jatekosok tombben</param>
        private static void InitJatekos(int index)
        {
            Dictionary<Tipusok.Tarolhatok, int[]> kapacitas_raktar = new Dictionary<Tipusok.Tarolhatok, int[]>()
            {
                [Tipusok.Tarolhatok.Agyag] = new int[2] { 0, 1000 },
                [Tipusok.Tarolhatok.Buza] = new int[2] { 0, 1000 },
                [Tipusok.Tarolhatok.Erc] = new int[2] { 0, 1000 },
                [Tipusok.Tarolhatok.Fa] = new int[2] { 0, 1000 }
            };
            Tarolo raktar = new Tarolo(Tipusok.MezoTipusok.Raktar, kapacitas_raktar);
            FoEpulet fep = new FoEpulet();
            List<NyersanyagMezo> _nyersanyagMezok = new List<NyersanyagMezo>()
            {   new NyersanyagMezo(Tipusok.MezoTipusok.Agyagbanya,
                    new Dictionary<Tipusok.Tarolhatok, int[]> { [Tipusok.Tarolhatok.Agyag] = new int[2] { 0, 1000} }),
                new NyersanyagMezo(Tipusok.MezoTipusok.Buzamezo,
                    new Dictionary<Tipusok.Tarolhatok, int[]> { [Tipusok.Tarolhatok.Buza] = new int[2] { 0, 1000} }),
                new NyersanyagMezo(Tipusok.MezoTipusok.Ercbanya,
                    new Dictionary<Tipusok.Tarolhatok, int[]> { [Tipusok.Tarolhatok.Erc] = new int[2] { 0, 1000} }),
                new NyersanyagMezo(Tipusok.MezoTipusok.Faerdo,
                    new Dictionary<Tipusok.Tarolhatok, int[]> { [Tipusok.Tarolhatok.Fa] = new int[2] { 0, 1000} })};
            jatekosok[index] = new Jatekos(raktar, _nyersanyagMezok, fep);
        }

        #endregion Public Methods

        #region Private Fields

        private static readonly Jatekos[] jatekosok = new Jatekos[2];

        #endregion Private Fields
    }
}
