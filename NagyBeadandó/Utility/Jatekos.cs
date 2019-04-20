using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Lakosok;
using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Mezok;
using NagyBeadandó.Mezok.Alapok;
using NagyBeadandó.Tevekenysegek;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Utility
{
    public class Jatekos
    {
        private class InteraktivJatekos : IInteraktivMezo
        {
            public InteraktivJatekos(Jatekos _jatekos)
            {
                Metodusok = new Dictionary<string, Action>()
                {
                    ["Tamad"] = _jatekos.Tamad,
                };
                VanBennePublikusMetodus = Metodusok.Count != 0;
            }
            public Dictionary<string, Action> Metodusok { get; set; }

            public bool VanBennePublikusMetodus { get; set; }

            public int ID { get; set; }

            public IInteraktivMezo InteraktivMezo { get; set; }

            public Tipusok.MezoTipusok MezoTipus { get; set; }

            public string Nev { get; set; } = "Jatekos";

            public string Parameterek { get; set; } = "Jatekos";

            public int Szint { get; set; } = 1;
        }

        #region Private Methods

        private bool VanMegNemEtetett()
        {
            foreach (Tipusok.Tarolhatok item in this.foEpulet.Lista.Keys)
            {
                foreach (Lakosok.Lakos item2 in this.foEpulet.Lista[item])
                {
                    if (!item2.MegVanEtetve)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Minden Lakost megetet, ha van annyi búza a raktárban, amennyi a fogyasztása a katonának
        /// </summary>
        private void EtetniProbal()
        {
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
                            return;
                        }
                    }
                }
            }
        }
        private void MegNemEtetettMegol()
        {
            foreach (Tipusok.Tarolhatok item in this.foEpulet.Lista.Keys)
            {
                for (int i = 0; i < this.foEpulet.Lista[item].Count; i++)
                {
                    Lakosok.Lakos item2 = this.foEpulet.Lista[item][i];
                    if (!item2.MegVanEtetve)
                    {
                        this.foEpulet.Eltávolit(item2);
                        i--;
                    }
                }
            }
        }
        /// <summary>
        /// Minden Mezőn végrehajtja a termel-t
        /// </summary>
        private void MindenMezonTermel()
        {
            this.foEpulet.Termel(Tipusok.Tarolhatok.Lakos);
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                foreach (Tipusok.Tarolhatok item2 in item.Kapacitas.Keys)
                {
                    item.Termel(item2);
                }
            }
        }
        private bool VanBeNemTakaritottBuzaMezo()
        {
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                if (item.Kapacitas.ContainsKey(Tipusok.Tarolhatok.Buza))
                {
                    Tipusok.Tarolhatok buzatarolhato = Tipusok.Tarolhatok.Buza;
                    if (item.Kapacitas[buzatarolhato][0] > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
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
                    try
                    {
                        this.tarolo.Betesz(buzatarolhato, kivett);
                    }
                    catch (TaroloTulCsordultException)
                    {
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// Minden nyersanyagot betakarít
        /// </summary>
        private void RaktarNyersanyagFeltolt()
        {
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                foreach (Tipusok.Tarolhatok item2 in item.Kapacitas.Keys)
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

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Termelési ciklusért felel:
        /// Minden mezőn termel
        /// Betakarítja a termelt anyagot
        /// Ciklikusan, amíg mindenki meg nincs etetve:
        /// Megeteti a lakosokat
        /// Betakarítja a Búzát
        /// </summary>
        public void EtetTermel()
        {
            MindenMezonTermel();
            RaktarNyersanyagFeltolt();
            while (VanMegNemEtetett() && VanBeNemTakaritottBuzaMezo())
            {
                RaktarBuzaFeltoltes();
                EtetniProbal();
                // etet és búzát tölt
            }
            MegNemEtetettMegol();
            RaktarBuzaFeltoltes();
        }
        public void FoEpuletLeRombol()
        {
            Vesztett = true;
        }
        public void KatonakHazaternek(KatonaiEgyseg katonaiEgyseg)
        {
            foreach (Lakos item in katonaiEgyseg.Katonak)
            {
                item.ItthonVan = true;
            }
        }
        public void KatonaMeghal(Lakos katona)
        {
            this.foEpulet.Eltávolit(katona);
        }
        /// <summary>
        /// Minden itthon tartozkodo katonat elkuld
        /// </summary>
        public void Tamad()
        {
            List<Lakos> lakos_katonak = this.foEpulet.KiveszTipus(Tipusok.Tarolhatok.Lakos, this.foEpulet.ItthonLevok(Tipusok.Tarolhatok.Lakos));

#pragma warning disable S1848 // Objects should not be created to be dropped immediately without being used
            new Tamadas(
                new KatonaiEgyseg(
                    true,
                    katonak: lakos_katonak,
                    jatekos_id: Id),
                (Id + 1) % 2);
#pragma warning restore S1848 // Objects should not be created to be dropped immediately without being used
        }
        /// <summary>
        /// Visszaadja, hogy egy tárolhatóból van-e a táróbólban mennyiseg
        /// </summary>
        /// <param name="tarolhato">A tárolható típusa</param>
        /// <param name="mennyiseg">A mennyiseg</param>
        /// <returns></returns>
        public bool VanEleg(Tipusok.Tarolhatok tarolhato, int mennyiseg)
        {
            if (tarolhato != Tipusok.Tarolhatok.Lakos)
            {
                return this.tarolo.Kapacitas[tarolhato][0] > mennyiseg;
            }
            else
            {
                return this.foEpulet.Lista[tarolhato].Count > mennyiseg;
            }
        }
        /// <summary>
        /// Visszaad egy katonai egységet
        /// </summary>
        /// <returns>A katonai egység az összes itthon található katonát belefűzi egy listába, amiből a katonaiegység van</returns>
        public KatonaiEgyseg Vedekezik()
        {
            KatonaiEgyseg katonaiEgyseg = null;
            List<Lakos> itthon = new List<Lakos>();
            foreach (Lakos item in this.foEpulet.Lista[Tipusok.Tarolhatok.Lakos])
            {
                if (item.ItthonVan)
                {
                    itthon.Add(item);
                }
            }
            katonaiEgyseg = new KatonaiEgyseg(false, itthon, Id);
            return katonaiEgyseg;
        }

        #endregion Public Methods

        #region Public Constructors

        public Jatekos(Tarolo _tarolo, List<NyersanyagMezo> _nyersanyagMezok, FoEpulet _foEpulet)
        {
#pragma warning disable S3010 // Static fields should not be updated in constructors
            Id = CurrentId++;
#pragma warning restore S3010 // Static fields should not be updated in constructors
            this.tarolo = _tarolo;
            this.nyersanyagMezok = _nyersanyagMezok;
            this.foEpulet = _foEpulet;
            InteraktivMezok.Add(this.foEpulet);
            InteraktivMezok.Add(this.tarolo);
            foreach (NyersanyagMezo item in this.nyersanyagMezok)
            {
                InteraktivMezok.Add(item);
            }
            InteraktivMezok.Add(new InteraktivJatekos(this));
        }

        #endregion Public Constructors

        #region Public Properties

        public int Id { get; private set; }
        public List<IInteraktivMezo> InteraktivMezok { get; private set; } = new List<IInteraktivMezo>();
        public bool Vesztett { get; private set; } = false;

        #endregion Public Properties

        #region Private Fields

        private static int CurrentId = 0;
        private readonly FoEpulet foEpulet;
        private readonly List<NyersanyagMezo> nyersanyagMezok;
        /// <summary>
        /// Tárolók, amik csak tárolnak (pl. Raktár)
        /// </summary>
        private readonly Tarolo tarolo;

        #endregion Private Fields
    }
}
