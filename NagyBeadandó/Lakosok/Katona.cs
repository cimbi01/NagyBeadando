using NagyBeadandó.Mezok.Alapok;

namespace NagyBeadandó.Lakosok
{
    internal class Katona : Lakos
    {
        #region Public Constructors

        /// <summary>
        /// KatonaTípus alapján beállít mindent
        /// </summary>
        /// <param name="katona_tipus">Katona Típusa</param>
        public Katona(int id, Tipusok.KatonaTipusok katona_tipus)
        {
            ID = id;
            KatonaTipus = katona_tipus;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool ItthonVan { get; private set; } = true;
        public Tipusok.KatonaTipusok KatonaTipus { get; private set; }
        public int MenetSebesseg { get; private set; }
        public int RomboloErtek { get; private set; }
        public int TamadoErtek { get; private set; }

        #endregion Public Properties
    }
}
