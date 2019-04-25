using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Lakosok;
using NagyBeadandó.Mezok.Alapok;
using NagyBeadandó.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NagyBeadandó.Mezok
{
    /// <summary>
    /// Tarolo, ami tárolja a Lakosokat
    /// </summary>
    public class FoEpulet : Tarolo
    {
        #region Public Properties

        /// <summary>
        /// Tarolja a Lakosokategyesevel adataikkal egyutt
        /// </summary>
        public List<Lakos> Lista { get; private set; } = new List<Lakos>();

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Inicializálja a Főépulet Kapcitását, Listáját, Termelését
        /// </summary>
        public FoEpulet() : base(Tipusok.MezoTipusok.Foepulet, new List<Tipusok.Tarolhatok>() { Tipusok.Tarolhatok.Lakos })
        {
            Random rnd = new Random();
            this.termeles = rnd.Next(0, 1000);
        }

        #endregion Public Constructors

        #region Private Fields

        /// <summary>
        /// Tarolja, hogy egy iterációs egység alatt mennyi Lakos-t termel
        /// </summary>
        private readonly int termeles;

        #endregion Private Fields

        #region Public Methods

        public void BeteszTipus(List<Lakos> lakosok)
        {
            for (int i = 0; i < lakosok.Count && Lista.Count < Kapacitas[Tipusok.Tarolhatok.Lakos][1]; i++)
            {
                Lista.Add(lakosok[i]);
            }
        }
        /// <summary>
        /// Eltávolít egy lakost a Listából
        /// </summary>
        /// <param name="lakos">Lakos, amit elvátolítunk a listából</param>
        public void Eltávolit(Lakos lakos)
        {
            Lista.Remove(lakos);
        }
        /// <summary>
        ///Megszámolja, hány lakos van itthon
        ///Azaz hány lakosnak ItthonVan = true
        /// </summary>
        /// <returns>Visszaadja, hány lakos van itthon</returns>
        public int ItthonLevok()
        {
            return Lista.Count(lakos => lakos.ItthonVan);
        }
        /// <summary>
        /// Visszaad egy Listát, benne mennyit-nyi Lakos
        /// Ha nincs annyi lakos, vagy nincs annyi itthon levo lakos
        /// Akkor NincsElegTarolhatoException hibat dob
        /// </summary>
        /// <param name="mennyit">A lakosok mennyisege</param>
        /// <param name="itthon">A itthon legyenek a lakosok?</param>
        /// <returns></returns>
        public List<Lakos> KiveszTipus(int mennyit, bool itthon = true)
        {
            List<Lakos> katonak = new List<Lakos>();
            if ((itthon && ItthonLevok() < mennyit)
                || Lista.Count < mennyit)
            {
                throw new NincsElegTarolhatoException();
            }
            Logger.Log("Kivettek " + mennyit + " darab lakost a Főépuletbol");
            /// az otthon levo katonákat hozzáadja a katonákhoz és elveszi a listából
            for (int i = 0; i < Lista.Count && mennyit > 0; i++)
            {
                if (Lista[i].ItthonVan)
                {
                    katonak.Add(Lista[0]);
                    Lista.Remove(Lista[0]);
                    i--;
                    mennyit--;
                }
            }
            return katonak;
        }
        /// <summary>
        /// Termeles-nyit termel pluszban a meglevohoz a tarolhato-bol, legfelejebb a kapacitás-ig
        /// </summary>
        public void Termel()
        {
            for (int i = 0; i < this.termeles && Lista.Count < Kapacitas[Tipusok.Tarolhatok.Lakos][1]; i++)
            {
                Lista.Add(new Lakos());
            }
            Lista.Sort((lak1, lak2) => lak1.Fogyasztas.CompareTo(lak2.Fogyasztas));
            Logger.Log("Főepulet termelt");
        }

        #endregion Public Methods
    }
}
