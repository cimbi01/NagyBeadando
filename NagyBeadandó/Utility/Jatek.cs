using NagyBeadandó.Mezok;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;
using System.Linq;

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
        public static Jatekos GetJatekosById(int id)
        {
            if (jatekosok[0].Id == id)
            {
                return jatekosok[0];
            }
            return jatekosok[1];
        }
        /// <summary>
        /// Visszaadja, hogy van-e játékos, aki vesztett
        /// </summary>
        /// <returns></returns>
        public static bool JatekVege()
        {
            return jatekosok.Any(jatekos => jatekos.Vesztett);
        }
        /// <summary>
        /// Játék élet ciklusáért felel
        /// Kezeli a tevékenységek élet ciklusát
        /// Kezeli a játékosok interakció élet ciklusát
        /// Amíg nincs játékos, aki nem vesztett
        /// </summary>
        public static void Play()
        {
            Init();
            int index = 0;
            do
            {
                if (index % 2 == 0)
                {
                    Logger.Log("Tevékenységek görget leütve");
                    TevekenysegController.GorgetMind();
                }
                if (!Jatek.JatekVege())
                {
                    Logger.Log("Játákos csere");
                    Controller.Jatekos = jatekosok[index % 2];
                    jatekosok[index % 2].EtetTermel();
                    Controller.Interact();
                    index++;
                }
            } while (!JatekVege());
            Logger.Close();
            System.Console.WriteLine("Játék vége");
        }
        /// <summary>
        /// Inicializálja a játékot:
        /// Két Játékost:
        /// </summary>
        private static void Init()
        {
            Logger.Log("Játék inicializálása");
            for (int i = 0; i < 2; i++)
            {
                InitJatekos(i);
            }
        }
        /// <summary>
        /// Inicializalja a jatekosokat:
        /// NyersanyagMezők : Búza, Agyag, Fa, Érc
        /// Raktár, Főépület
        /// </summary>
        /// <param name="index">A jatekos helye a jatekosok tombben</param>
        private static void InitJatekos(int index)
        {
            Logger.Log("Játékos inicializálása");
            List<Tipusok.Tarolhatok> kapacitas_raktar = new List<Tipusok.Tarolhatok>()
            {
                Tipusok.Tarolhatok.Agyag,
                Tipusok.Tarolhatok.Buza,
                Tipusok.Tarolhatok.Erc,
                Tipusok.Tarolhatok.Fa
            };
            Tarolo raktar = new Tarolo(Tipusok.MezoTipusok.Raktar, kapacitas_raktar);
            FoEpulet fep = new FoEpulet();
            List<NyersanyagMezo> _nyersanyagMezok = new List<NyersanyagMezo>()
            {   new NyersanyagMezo(Tipusok.MezoTipusok.Agyagbanya,
                    new List<Tipusok.Tarolhatok> { Tipusok.Tarolhatok.Agyag}),
                new NyersanyagMezo(Tipusok.MezoTipusok.Buzamezo,
                    new List<Tipusok.Tarolhatok> { Tipusok.Tarolhatok.Buza }),
                new NyersanyagMezo(Tipusok.MezoTipusok.Ercbanya,
                    new List<Tipusok.Tarolhatok> { Tipusok.Tarolhatok.Erc }),
                new NyersanyagMezo(Tipusok.MezoTipusok.Faerdo,
                    new List<Tipusok.Tarolhatok> { Tipusok.Tarolhatok.Fa })};
            jatekosok[index] = new Jatekos(raktar, _nyersanyagMezok, fep);
        }

        #endregion Public Methods

        #region Private Fields

        /// <summary>
        /// Játék játékosai, akik játszanak
        /// </summary>
        private static readonly Jatekos[] jatekosok = new Jatekos[2];

        #endregion Private Fields
    }
}
