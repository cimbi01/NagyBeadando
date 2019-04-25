using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Utility;
using System.Linq;

namespace NagyBeadandó.Tevekenysegek
{
    public static class Csata
    {
        #region Private Methods

        /// <summary>
        /// A csatában szereplő támadó és védő egységekből vonja ki katonakat
        /// Amíg a támadó erő nagyobb mint a védekezők leggyengébb védője, vagy elfogy a védő
        /// </summary>
        /// <param name="tamadas"></param>
        /// <param name="vedekezes"></param>
        private static void Gyilkol(KatonaiEgyseg tamadas, KatonaiEgyseg vedekezes)
        {
            int ero = tamadas.Erő;
            int min = vedekezes.Katonak.Min(t => t.VedoErtek);
            while (ero >= min && min > 0)
            {
                /// eltávolít védő katonákat támadás erejének függvényében
                /// (nem mindig addig fut, amig az ero 0, mert lehet hogy az sose jon ki mert a jatekosok erejei random szamok)
                for (int j = 0; j < vedekezes.Katonak.Count && ero > 0; j++)
                {
                    if (vedekezes.Katonak[j].VedoErtek <= ero)
                    {
                        ero -= vedekezes.Katonak[j].VedoErtek;
                        vedekezes.Katonak.Remove(vedekezes.Katonak[j]);
                        j--;
                    }
                    /// Mivel rendezve van
                    /// Ezért ha az aktuális indexű katona vedoerteke, nagyobb mint az ero
                    /// Akkor nem lesz kesobb se kisebb
                    else
                    {
                        return;
                    }
                }
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Csata lemeneteléért felel:
        /// Ha a védő erő kisebb mint a támadó, akkor támadó nyert
        /// Ellenkező esetben védő nyert
        /// </summary>
        public static void Csatazas(KatonaiEgyseg tamadas, KatonaiEgyseg vedekezes)
        {
            Logger.Log("Csata elkezdődött");
            /// ne kelljen mindig lekérni a Jatek-tól az aktuálisan kívánt játkost
            Jatekos vedekezo = Jatek.GetJatekosById(vedekezes.Jatekos_Id);
            if (tamadas.Erő > vedekezes.Erő)
            {
                vedekezo.Veszit();
                Logger.Log("Támadó játékos ID: " + tamadas.Jatekos_Id + "nyert");
            }
            else
            {
                Logger.Log("Támadó játékos ID: " + tamadas.Jatekos_Id + "vesztett");
                /// Rendezi a katonákat erő szerint
                /// Hogy Gyilkolásban a minimumkeresés-nél ne kelljen mindig végigfutni a listán
                vedekezes.Katonak.Sort((x, y) => y.VedoErtek.CompareTo(x.VedoErtek));
                Gyilkol(tamadas, vedekezes);
                // visszatérés
                vedekezo.KatonakHazaternek(vedekezes);
            }
        }

        #endregion Public Methods
    }
}
