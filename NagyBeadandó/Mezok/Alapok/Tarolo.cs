using NagyBeadandó.Kivételek.MezoKivetelek;
using System;

namespace NagyBeadandó.Mezok.Alapok
{
    internal class Tarolo
    {
        #region Public Methods

        /// <summary>
        /// A Kapacitás tároló részéhez [0] hozzáad mennyit-nyit
        /// Ha így nagyobb akkor teletölti, majd dob egy TaroloTulCsordultException-t
        /// </summary>
        /// <param name="tarolhato">A tárolható típusa, amelyik rekeszbe szeretnék tenni a mennyit</param>
        /// <param name="mennyit">A mennyiség, amennyit be szeretnénk tenni a rekeszbe</param>
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
        /// Visszaadja mmenyit-nyit a Tárolhato-ból
        /// </summary>
        /// <param name="tarolhato">A típus, amiből lekérjük a mennyiséget</param>
        /// <param name="mennyit">A mennyiség, hogy mennyit szeretne kivenni a tárolóból</param>
        /// <returns>Visszaad mennyit-nyit, ha van belőle mennyit-nyi a kapacitásban, ellenkező esetben dob egy NincsElégTarolhatoException-t</returns>
        public void Kivesz(int mennyit)
        {
            if (this.tarolt.mennyiseg < mennyit)
            {
                throw new NincsElegTarolhatoException();
            }
            this.tarolt.mennyiseg -= mennyit;
        }

        public bool VanBenne(Tipusok.Tarolhatok tarolhato)
        {
            return this.tarolt.tarolt_tipus == tarolhato;
        }

        #endregion Public Methods

        #region Private Fields

        private (Tipusok.Tarolhatok tarolt_tipus, int mennyiseg, int kapacitas) tarolt;

        #endregion Private Fields

        #region Public Constructors

        public Tarolo(Tipusok.Tarolhatok tarolhato)
        {
            this.tarolt = ValueTuple.Create(tarolhato, 0, 0);
        }

        #endregion Public Constructors
    }
}
