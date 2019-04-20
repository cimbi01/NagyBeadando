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
        #region Public Properties

        public KatonaiEgyseg KatonaiEgyseg { get; private set; }
        public int Tamadott_id { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public Tamadas(KatonaiEgyseg katonasag, int tamadott_jatekos_id)
        {
            KatonaiEgyseg = katonasag;
            foreach (Lakos item in KatonaiEgyseg.Katonak)
            {
                item.ItthonVan = false;
            }
            Tamadott_id = tamadott_jatekos_id;
#pragma warning disable S1848 // Objects should not be created to be dropped immediately without being used
            new Tevekenyseg(MenetidoSzamitas(), TamadasInditas);
#pragma warning restore S1848 // Objects should not be created to be dropped immediately without being used
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
            foreach (Lakos item in KatonaiEgyseg.Katonak)
            {
                int tmp_menet_ido = (tavolsag / item.MenetSebesseg) + 1;
                if (tmp_menet_ido > menetido)
                {
                    menetido = tmp_menet_ido;
                }
            }
            return menetido;
        }
        private void TamadasInditas()
        {
#pragma warning disable S1848 // Objects should not be created to be dropped immediately without being used
            Csata.Csatazas(
                this,
                Jatek.JatekosById(Tamadott_id).Vedekezik());
#pragma warning restore S1848 // Objects should not be created to be dropped immediately without being used
        }

        #endregion Private Methods
    }
}
