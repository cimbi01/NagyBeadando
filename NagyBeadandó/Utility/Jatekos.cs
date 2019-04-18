using NagyBeadandó.Mezok;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Utility
{
    public class Jatekos
    {
        #region Private Methods

        /// <summary>
        /// Minden Lakost megetet, ha van annyi búza a raktárban, amennyi a fogyasztása a katonának
        /// </summary>
        private void EtetniProbal()
        {
            foreach (Tipusok.Tarolhatok item in this.kaszarnya.Lista.Keys)
            {
                foreach (Lakosok.Katonasag.Katona item2 in this.kaszarnya.Lista[item])
                {
                    if (!item2.MegVanEtetve)
                    {
                        try
                        {
                            this.tarolo.Kivesz(Tipusok.Tarolhatok.Buza, item2.Fogyasztas);
                            item2.MegVanEtetve = true;
                        }
                        catch (Kivételek.MezoKivetelek.NincsElegTarolhatoException)
                        {
                            // nem sikerult megetetni, mert nem volt elég búza a raktárban
                        }
                    }
                }
            }
            foreach (Tipusok.Tarolhatok item in this.foEpulet.Lista.Keys)
            {
                foreach (Lakosok.Lakos item2 in this.foEpulet.Lista[item])
                {
                    if (!item2.MegVanEtetve)
                    {
                        try
                        {
                            this.tarolo.Kivesz(Tipusok.Tarolhatok.Buza, item2.Fogyasztas);
                            item2.MegVanEtetve = true;
                        }
                        catch (Kivételek.MezoKivetelek.NincsElegTarolhatoException)
                        {
                            // nem sikerult megetetni, mert nem volt elég búza a raktárban
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Minden Mezőn végrehajtja a termel-t
        /// </summary>
        private void MindenMezonTermel()
        {
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                foreach (Tipusok.Tarolhatok item2 in item.Kapacitas.Keys)
                {
                    item.Termel(item2);
                }
            }
        }
        /// <summary>
        /// Feltolti a raktár Búza részét
        /// </summary>
        private void RaktarBuzaFeltoltes()
        {
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                if (item.Kapacitas.ContainsKey(Tipusok.Tarolhatok.Buza))
                {
                    Tipusok.Tarolhatok buzatarolhato = Tipusok.Tarolhatok.Buza;
                    int mennyit;
                    int tarolo_maradekhely = this.tarolo.Kapacitas[buzatarolhato][1] - this.tarolo.Kapacitas[buzatarolhato][0];
                    // ha a mezőben több a teremtl anyag, mint amekkkora a maradék hely, akkor csak maradekhelynyit kér le
                    if (item.Kapacitas[buzatarolhato][0] > tarolo_maradekhely)
                    {
                        mennyit = tarolo_maradekhely;
                    }
                    // ellenkező esetben az egészet, amit tárol
                    else
                    {
                        mennyit = item.Kapacitas[buzatarolhato][0];
                    }
                    // majd kiveszi és beteszi a tárolóba
                    int kivett = item.Kivesz(buzatarolhato, mennyit);
                    this.tarolo.Betesz(buzatarolhato, kivett);
                }
            }
        }
        /// <summary>
        /// Búzán kívül minden nyersanyagot betakarít
        /// </summary>
        private void RaktarNyersanyagFeltolt()
        {
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                foreach (Tipusok.Tarolhatok item2 in item.Kapacitas.Keys)
                {
                    if (item2 != Tipusok.Tarolhatok.Buza)
                    {
                        int mennyit;
                        int tarolo_maradekhely = this.tarolo.Kapacitas[item2][1] - this.tarolo.Kapacitas[item2][0];
                        // ha a mezőben több a teremtl anyag, mint amekkkora a maradék hely, akkor csak maradekhelynyit kér le
                        if (item.Kapacitas[item2][0] > tarolo_maradekhely)
                        {
                            mennyit = tarolo_maradekhely;
                        }
                        // ellenkező esetben az egészet, amit tárol
                        else
                        {
                            mennyit = item.Kapacitas[item2][0];
                        }
                        // majd kiveszi és beteszi a tárolóba
                        int kivett = item.Kivesz(item2, mennyit);
                        this.tarolo.Betesz(item2, kivett);
                    }
                }
            }
        }

        #endregion Private Methods

        #region Public Constructors

        public Jatekos(Tarolo _tarolo, List<NyersanyagMezo> _nyersanyagMezok, Kaszarnya _kaszarnya, FoEpulet _foEpulet)
        {
            Id = CurrentId++;
            this.tarolo = _tarolo;
            this.nyersanyagMezok = _nyersanyagMezok;
            this.kaszarnya = _kaszarnya;
            this.foEpulet = _foEpulet;
            InteraktivMezok.Add(this.kaszarnya.InteraktivMezo);
            InteraktivMezok.Add(this.foEpulet.InteraktivMezo);
            InteraktivMezok.Add(this.tarolo.InteraktivMezo);
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                InteraktivMezok.Add(item.InteraktivMezo);
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public int Id { get; private set; }
        public List<IInteraktivMezo> InteraktivMezok { get; private set; }

        #endregion Public Properties

        #region Private Fields

        private static int CurrentId = 0;

        private readonly FoEpulet foEpulet;
        private readonly Kaszarnya kaszarnya;
        private readonly List<NyersanyagMezo> nyersanyagMezok;
        /// <summary>
        /// Tárolók, amik csak tárolnak (pl. Raktár)
        /// </summary>
        private readonly ITarolo tarolo;

        #endregion Private Fields
    }
}
