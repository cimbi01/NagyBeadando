using NagyBeadandó.Lakosok.Katonasag;

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
        #region Public Constructors

        public Tamadas(KatonaiEgyseg katonasag, string tamadott_jatekos)
        {
            this.katonaiEgyseg = katonasag;
            MenetidoSzamitas();
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly KatonaiEgyseg katonaiEgyseg;
        private readonly Tevekenyseg tevekenyseg;
        private int menetido = 0;

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        /// Katonaiegyésg katonái között menetidők maximumát keresi > az a menetidő
        /// </summary>
        private void MenetidoSzamitas()
        {
            foreach (Katona item in this.katonaiEgyseg.Katonak)
            {
                if (item.MenetSebesseg > 0)
                {
                    this.menetido = item.MenetSebesseg;
                }
            }
        }

        #endregion Private Methods
    }
}
