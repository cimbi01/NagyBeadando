using NagyBeadandó.Lakosok;
using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Utility;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Tevekenysegek
{
    public static class Csata
    {
        #region Private Methods

        /// <summary>
        /// Rendezi a katonákat, úgy, hogy a legnagyobb lesz legelol
        /// </summary>
        /// <param name="katonak">Katona lista amit rendezunk</param>
        /// <param name="tamad">A katonak támadó vagy védőértéke szerint rendezunk-e</param>
        private static void MaximumKivalasztasosRendezes(List<Lakos> katonak, bool tamad)
        {
            for (int i = 0; i > katonak.Count - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < katonak.Count; j++)
                {
                    if (tamad && katonak[j].TamadoErtek > katonak[max].TamadoErtek)
                    {
                        max = j;
                    }
                    else if (katonak[j].VedoErtek > katonak[max].VedoErtek)
                    {
                        max = j;
                    }
                }
                Lakos tmp = katonak[i];
                katonak[i] = katonak[max];
                katonak[max] = tmp;
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
            /// ne kelljen mindig lekérni a Jatek-tól az aktuálisan kívánt játkost
            Jatekos tamado = Jatek.GetJatekosById(tamadas.Jatekos_Id);
            Jatekos vedekezo = Jatek.GetJatekosById(vedekezes.Jatekos_Id);
            if (tamadas.Erő > vedekezes.Erő)
            {
                vedekezo.FoEpuletLeRombol();
                Console.Clear();
                Console.WriteLine("Támadó játékos ID: {0} nyert", tamadas.Jatekos_Id);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Támadó játékos ID: {0} vesztett", tamadas.Jatekos_Id);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                /// Rendezi a katonákat erő szerint
                /// Azért, hogy a ciklusban eloször az erősebbek essenek ki, így közelebb kerulhetunk az ero-hoz
                /// Rendezessel : (pl. ero = 10, katonak erei = 5, 4, 4, 2 ero = iteracionkent : 5, 1, 1, 1)
                /// Rendezes nelkul : (ero ugyanaz katonak ugyanazok sorrend mas = 2, 5, 4, 4 ero iteracionkent = 8, 3, 3, 3)
                MaximumKivalasztasosRendezes(vedekezes.Katonak, false);
                MaximumKivalasztasosRendezes(tamadas.Katonak, true);
                /// eltávolitja összes támadó játékost
                for (int i = 0; i < tamadas.Katonak.Count; i++)
                {
                    /// és eltávolít védő katonákat, az erejének megfeleloen
                    /// (nem mindig addig fut, amig az ero 0, mert lehet hogy az sose jon ki mert a jatekosok erejei random szamok)
                    int ero = tamadas.Katonak[i].TamadoErtek;
                    for (int j = 0; j < vedekezes.Katonak.Count && ero > 0; j++)
                    {
                        if (vedekezes.Katonak[j].VedoErtek < ero)
                        {
                            ero -= vedekezes.Katonak[j].VedoErtek;
                            vedekezo.KatonaMeghal(vedekezes.Katonak[j]);
                            vedekezes.Katonak.Remove(vedekezes.Katonak[j]);
                            j--;
                        }
                    }
                    tamado.KatonaMeghal(tamadas.Katonak[i]);
                    tamadas.Katonak.Remove(tamadas.Katonak[i]);
                    i--;
                }
                // visszatérés
                tamado.KatonakHazaternek(tamadas);
                vedekezo.KatonakHazaternek(vedekezes);
            }
        }
    }

    #endregion Public Methods
}
