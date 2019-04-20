using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Utility;
using System;

namespace NagyBeadandó.Tevekenysegek
{
    public static class Csata
    {
        #region Public Methods

        /// <summary>
        /// Csata lemeneteléért felel:
        /// Ha a védő erő kisebb mint a támadó, akkor támadó nyert
        /// Ellenkező esetben védő nyert
        /// </summary>
        public static void Csatazas(Tamadas tamadas, KatonaiEgyseg vedekezes)
        {
            Jatekos tamado = Jatek.JatekosById(tamadas.KatonaiEgyseg.Jatekos_Id);
            Jatekos vedekezo = Jatek.JatekosById(vedekezes.Jatekos_Id);
            if (tamadas.KatonaiEgyseg.Erő > vedekezes.Erő)
            {
                Jatek.JatekosById(tamadas.Tamadott_id).FoEpuletLeRombol();
                Console.Clear();
                Console.WriteLine("Támadó játékos ID: {0} nyert", tamadas.KatonaiEgyseg.Jatekos_Id);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Támadó játékos ID: {0} vesztett", tamadas.KatonaiEgyseg.Jatekos_Id);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                for (int i = 0; i < tamadas.KatonaiEgyseg.Katonak.Count; i++)
                {
                    int ero = tamadas.KatonaiEgyseg.Katonak[i].TamadoErtek;
                    for (int j = 0; j < vedekezes.Katonak.Count && ero > 0; j++)
                    {
                        if (vedekezes.Katonak[j].VedoErtek < ero)
                        {
                            ero -= vedekezes.Katonak[j].VedoErtek;
                            vedekezo.KatonaMeghal(vedekezes.Katonak[j]);
                        }
                    }
                    tamado.KatonaMeghal(tamadas.KatonaiEgyseg.Katonak[i]);
                }
                // visszatérés
                Jatek.JatekosById(tamadas.KatonaiEgyseg.Jatekos_Id).KatonakHazaternek(tamadas.KatonaiEgyseg);
                Jatek.JatekosById(vedekezes.Jatekos_Id).KatonakHazaternek(vedekezes);
            }
        }

        #endregion Public Methods
    }
}
