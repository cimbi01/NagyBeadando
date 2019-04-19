using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Utility;

namespace NagyBeadandó.Tevekenysegek
{
    internal class Csata
    {
        #region Private Methods

        /// <summary>
        /// Csata lemeneteléért felel:
        /// Ha a védő erő kisebb mint a támadó, akkor támadó nyert
        /// Ellenkező esetben védő nyert
        /// </summary>
        private void Csatazas()
        {
            if (this.tamadas.KatonaiEgyseg.Erő > this.vedekezes.Erő)
            {
                Jatek.JatekosById(this.tamadas.Tamadott_id).FoEpuletLeRombol();
            }
            else
            {
                for (int i = 0; i < this.tamadas.KatonaiEgyseg.Katonak.Count; i++)
                {
                    Jatek.JatekosById(this.tamadas.KatonaiEgyseg.Jatekos_Id).KatonaMeghal(this.tamadas.KatonaiEgyseg.Katonak[i]);
                    Jatek.JatekosById(this.vedekezes.Jatekos_Id).KatonaMeghal(this.vedekezes.Katonak[i]);
                }
            }

            // visszatérés
            Jatek.JatekosById(this.tamadas.KatonaiEgyseg.Jatekos_Id).KatonakHazaternek(this.tamadas.KatonaiEgyseg);
            Jatek.JatekosById(this.vedekezes.Jatekos_Id).KatonakHazaternek(this.vedekezes);
        }

        #endregion Private Methods

        #region Public Constructors

        public Csata(Tamadas _tamadas, KatonaiEgyseg _vedekezes)
        {
            this.tamadas = _tamadas;
            this.vedekezes = _vedekezes;
            Csatazas();
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly Tamadas tamadas;
        private readonly KatonaiEgyseg vedekezes;

        #endregion Private Fields
    }
}
