using NagyBeadandó.Lakosok;
using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Utility;
using System;

namespace NagyBeadandó.Tevekenysegek
{
    /// <summary>
    /// Feladata:
    ///     Katonák tárolása
    ///     Hadászati értékek számítása
    ///     Menetelés indítása > Tevékenység indítása, Csata indítása
    /// </summary>
    public class Tamadas
    {
        #region Public Methods

        /// <summary>
        /// Hozzáad a Tevekenységkontrollerhez
        /// </summary>
        public void Tamad()
        {
            TevekenysegController.AddTevekenyseg(MenetidoSzamitas(), TamadasInditas);
        }

        #endregion Public Methods

        #region Private Fields

        /// <summary>
        /// Támadó katonaiEgysége
        /// </summary>
        private readonly KatonaiEgyseg KatonaiEgyseg;
        /// <summary>
        /// A támadott játékos ID-je
        /// </summary>
        private readonly int Tamadott_id;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Konstruktor
        /// Iicializálja a támadást
        /// </summary>
        /// <param name="katonasag">Támadó Katonai egysége</param>
        /// <param name="tamadott_jatekos_id">Támadott játékos ID-je</param>
        public Tamadas(KatonaiEgyseg katonasag, int tamadott_jatekos_id)
        {
            this.KatonaiEgyseg = katonasag;
            foreach (Lakos item in this.KatonaiEgyseg.Katonak)
            {
                item.ItthonVan = false;
            }
            this.Tamadott_id = tamadott_jatekos_id;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Katonaiegyésg katonái között menetidők maximumát keresi > az a menetidő
        /// v = s/t > t = s/v
        /// s = 5
        /// v = menetsebesseg
        /// t = v/s
        /// </summary>
        private int MenetidoSzamitas()
        {
            Random rnd = new Random();
            int tavolsag = rnd.Next(1, 10);
            int menetido = 0;
            foreach (Lakos item in this.KatonaiEgyseg.Katonak)
            {
                int tmp_menet_ido = (tavolsag / item.MenetSebesseg) + 1;
                if (tmp_menet_ido > menetido)
                {
                    menetido = tmp_menet_ido;
                }
            }
            return menetido;
        }
        /// <summary>
        /// Elindítja a támadást
        /// Arra kell, hogy legyen egy paraméter és visszatérés nélküli metódus
        /// Amit át lehet adni a tevekenyseg konstrruktornak
        /// </summary>
        private void TamadasInditas()
        {
            Csata.Csatazas(
                this.KatonaiEgyseg,
                Jatek.GetJatekosById(this.Tamadott_id).Vedekezik());
        }

        #endregion Private Methods
    }
}
