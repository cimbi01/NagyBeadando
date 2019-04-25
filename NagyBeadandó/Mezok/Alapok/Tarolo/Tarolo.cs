using NagyBeadandó.Kivételek.MezoKivetelek;
using System;

namespace NagyBeadandó.Mezok.Alapok.Tarolo
{
    internal class Tarolo
    {
        #region Public Methods

        /// <summary>
        /// A tarolt tuple mennyiseg részét növeli mennyit-el
        /// Ha így a mennyiseg tullepi a kapacitast, dob egy TaroloTulCsordultException-t
        /// </summary>
        /// <param name="mennyit">mennyiseg, amennyit bele szeretne tolteni</param>
        public void Betesz(int mennyit)
        {
            int tarolo_maradekhely = this.tarolt.kapacitas - this.tarolt.mennyiseg;
            if (mennyit > tarolo_maradekhely)
            {
                this.tarolt.mennyiseg += tarolo_maradekhely;
                throw new TaroloTulCsordultException();
            }
            this.tarolt.mennyiseg += tarolo_maradekhely;
        }

        /// <summary>
        /// Tarolt tuple mennyiseg-ből elvesz mennyit
        /// Ha a mennyit nagyobb mint a tarolt mennyiseg, akkor NincsElegTarolhatoException-t dob
        /// </summary>
        /// <param name="mennyit">Mennyiseg, amennyit ki szeretne venni</param>
        public void Kivesz(int mennyit)
        {
            if (this.tarolt.mennyiseg < mennyit)
            {
                throw new NincsElegTarolhatoException();
            }
            this.tarolt.mennyiseg -= mennyit;
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

        /// <summary>
        /// Tarolt tuple
        /// tarolt_tipus a tarolhato, amit tarol a tuple
        /// mennyiseg a mennyiseg, amennyit tarol a tuple
        /// kapacitas a kapacitas, amennyi a maximum mennyiseg lehet
        /// </summary>
        private (Tipusok.Tarolhatok tarolt_tipus, int mennyiseg, int kapacitas) tarolt;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Inicializálja a tárolót
        /// </summary>
        /// <param name="tarolhato">Tarolhato tipusa, amiből a tuple-t inicializálja</param>
        public Tarolo(Tipusok.Tarolhatok tarolhato)
        {
            this.tarolt = ValueTuple.Create(tarolhato, 0, 0);
        }

        #endregion Public Constructors
    }
}
