using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Lakosok;
using NagyBeadandó.Mezok.Alapok;
using NagyBeadandó.Utility;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    /// <summary>
    /// Tarolo, ami tárolja a Lakosokat
    /// </summary>
    public class FoEpulet : Tarolo
    {
        #region Private Methods

        /// <summary>
        /// Fogyasztás szerint rendezi a lakosok listáját
        /// A legkisebb fogyasztású a legkisebb indexen van
        /// </summary>
        private void MinimumKivalasztasosRendezes()
        {
            for (int i = 0; i < Lista.Count - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Lista.Count; j++)
                {
                    if (Lista[j].Fogyasztas < Lista[min].Fogyasztas)
                    {
                        min = j;
                    }
                }
                Lakos tmp = Lista[i];
                Lista[i] = Lista[min];
                Lista[min] = tmp;
            }
        }

        #endregion Private Methods

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
            int itthon = 0;
            foreach (Lakos item in Lista)
            {
                if (item.ItthonVan)
                {
                    itthon++;
                }
            }
            return itthon;
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
            /// az otthon levo katonákat hozzáadja a katonákhoz
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
            Logger.Log("Kivettek " + mennyit + " darab lakost a Főépuletbol");
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
            MinimumKivalasztasosRendezes();
            Logger.Log("Főepulet termelt");
        }
        /// <summary>
        /// Visszadja, hogy van-e a meg nem etett lakos
        /// </summary>
        /// <returns></returns>
        public bool VanMegNemEtetett()
        {
            foreach (Lakosok.Lakos item2 in Lista)
            {
                if (!item2.MegVanEtetve)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion Public Methods
    }
}
