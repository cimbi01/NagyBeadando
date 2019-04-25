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
    public static class Tamadas
    {
        #region Public Methods

        /// <summary>
        /// Konstruktor
        /// Iicializálja a támadást
        /// </summary>
        /// <param name="katonasag">Támadó Katonai egysége</param>
        /// <param name="tamadott_jatekos_id">Támadott játékos ID-je</param>
        public static void TamadasInditas(KatonaiEgyseg katonasag, int tamadott_jatekos_id)
        {
            Logger.Log("Támadás menetideje : " + MenetidoSzamitas(katonasag));
            katonasag.Katonak.ForEach(lakos => lakos.ItthonVan = false);
            TevekenysegController.AddTevekenyseg(
                MenetidoSzamitas(katonasag),
                () => Csata.Csatazas(katonasag, Jatek.GetJatekosById(tamadott_jatekos_id).Vedekezik()));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Katonaiegyésg katonái között menetidők maximumát keresi > az a menetidő
        /// v = s/t > t = s/v
        /// </summary>
        private static int MenetidoSzamitas(KatonaiEgyseg katonaiEgyseg)
        {
            Random rnd = new Random();
            int tavolsag = rnd.Next(1, 10);
            int menetido = 0;
            foreach (Lakos item in katonaiEgyseg.Katonak)
            {
                int tmp_menet_ido = (tavolsag / item.MenetSebesseg) + 1;
                if (tmp_menet_ido > menetido)
                {
                    menetido = tmp_menet_ido;
                }
            }
            return menetido;
        }

        #endregion Private Methods
    }
}
