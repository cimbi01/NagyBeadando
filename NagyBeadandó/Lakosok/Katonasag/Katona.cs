using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Lakosok.Katonasag
{
    public class Katona : Lakos
    {
        #region Private Fields

        private static readonly Dictionary<Tipusok.KatonaTipusok, int[]> katona_ertekek = new Dictionary<Tipusok.KatonaTipusok, int[]>()
        { [Tipusok.KatonaTipusok.Gyalogos] = new int[4] { 5, 10, 10, 10 } };

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// KatonaTípus alapján beállít mindent
        /// </summary>
        /// <param name="katona_tipus">Katona Típusa</param>
        public Katona(Lakos lakos, Tipusok.KatonaTipusok katona_tipus) : base(lakos.ID)
        {
            ID = lakos.ID;
            KatonaTipus = katona_tipus;
            int[] ertekek = katona_ertekek[KatonaTipus];
            MenetSebesseg = ertekek[0];
            RomboloErtek = ertekek[1];
            TamadoErtek = ertekek[2];
            VedoErtek = ertekek[3];
        }

        #endregion Public Constructors

        #region Public Properties

        public bool ItthonVan { get; set; } = true;
        public Tipusok.KatonaTipusok KatonaTipus { get; private set; }
        public int MenetSebesseg { get; private set; }
        public int RomboloErtek { get; private set; }
        public int TamadoErtek { get; private set; }

        #endregion Public Properties
    }
}
