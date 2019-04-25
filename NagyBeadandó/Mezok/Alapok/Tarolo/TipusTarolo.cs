using NagyBeadandó.Kivételek.MezoKivetelek;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NagyBeadandó.Mezok.Alapok.Tarolo
{
    internal class TipusTarolo<T>
    {
        #region Public Methods

        /// <summary>
        /// Hozzáaadja a paraméterként kapott listát a tarolt tuple tarolt_tarolhatok-jához
        /// Ha a tarolt menniseg és a paraméter össze hossza nagyobb mint a kapacitas
        /// Akkor feltölti majd dob egy TaroloTulCsordultException-t
        /// </summary>
        /// <param name="tarolhatok">Lista, amit hozzaadunk</param>
        public void Betesz(List<T> tarolhatok)
        {
            for (int i = 0; i < tarolhatok.Count && this.tarolt.tarolt_tarolhatok.Count <= this.tarolt.kapacitas; i++)
            {
                this.tarolt.tarolt_tarolhatok.Add(tarolhatok[i]);
                tarolhatok.Remove(tarolhatok[i]);
                i--;
            }
            if (tarolhatok.Count > 0)
            {
                throw new TaroloTulCsordultException();
            }
        }
        /// <summary>
        /// A tarolt tuple mennyiseg részét növeli mennyit-el
        /// Ha így a mennyiseg tullepi a kapacitast, dob egy TaroloTulCsordultException-t
        /// </summary>
        /// <param name="mennyit">mennyiseg, amennyit bele szeretne tolteni</param>
        public void Betesz(T tarolhato)
        {
            int tarolo_maradekhely = this.tarolt.kapacitas - this.tarolt.tarolt_tarolhatok.Count;
            if (1 > tarolo_maradekhely)
            {
                throw new TaroloTulCsordultException();
            }
            this.tarolt.tarolt_tarolhatok.Add(tarolhato);
        }
        /// <summary>
        /// Tarolt tuple mennyiseg-ből elvesz mennyit
        /// Ha a mennyit nagyobb mint a tarolt mennyiseg, akkor NincsElegTarolhatoException-t dob
        /// </summary>
        /// <param name="mennyit">Mennyiseg, amennyit ki szeretne venni</param>
        /// <param name="predicate">A lamdbda kifejezes, amivel szuri a listából az elemeket</param>
        public List<T> Kivesz(Predicate<T> predicate)
        {
            Func<T, bool> kivalasztas = new Func<T, bool>(predicate);
            IEnumerable<T> predicate_lista = this.tarolt.tarolt_tarolhatok.Where(kivalasztas);
            this.tarolt.tarolt_tarolhatok.RemoveAll(predicate);
            return predicate_lista.ToList();
        }
        /// </summary>
        /// <param name="tarolhato">Tarolhato tipusa</param>
        /// <returns>Visszaadja, hogy a tarolhato egyenlő-e a tarolt tuple tarolt_tipus-al</returns>
        public bool VanBenne(Tipusok.Tarolhatok tarolhato)
        {
            return this.tarolt.tarolt_tipus == tarolhato;
        }

        #endregion Public Methods

        #region Private Fields

        private (Tipusok.Tarolhatok tarolt_tipus, List<T> tarolt_tarolhatok, int kapacitas) tarolt;

        #endregion Private Fields

        #region Public Constructors

        public TipusTarolo(Tipusok.Tarolhatok tarolhato)
        {
            this.tarolt = ValueTuple.Create(tarolhato, new List<T>(), 0);
        }

        #endregion Public Constructors
    }
}
