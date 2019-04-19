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
        #region Private Methods

        /// <summary>
        /// Minden Lakost megetet, ha van annyi búza a raktárban, amennyi a fogyasztása a katonának
        /// </summary>
        private bool EtetniProbal()
        {
            bool van_nem_megetetett = false;
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
                            van_nem_megetetett = true;
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
                            van_nem_megetetett = true;
                        }
                    }
                }
            }
            return van_nem_megetetett;
        }
        private void MegNemEtetettMegol()
        {
            foreach (Tipusok.Tarolhatok item in this.kaszarnya.Lista.Keys)
            {
                foreach (Lakosok.Katonasag.Katona item2 in this.kaszarnya.Lista[item])
                {
                    if (!item2.MegVanEtetve)
                    {
                        this.kaszarnya.Eltávolit(item2);
                    }
                }
            }
            foreach (Tipusok.Tarolhatok item in this.foEpulet.Lista.Keys)
            {
                foreach (Lakosok.Lakos item2 in this.foEpulet.Lista[item])
                {
                    if (!item2.MegVanEtetve)
                    {
                        this.foEpulet.Eltávolit(item2);
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
        private bool RaktarBuzaFeltoltes()
        {
            bool benemtakaritott = false;
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
                        benemtakaritott = true;
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
            return benemtakaritott;
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
            while (EtetniProbal() && RaktarBuzaFeltoltes())
            {
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
            if (katona is Katona)
            {
                Katona converted_katona = katona as Katona;
                this.kaszarnya.Lista[Kaszarnya.Tarolhato_katonatipusok[converted_katona.KatonaTipus]].Remove(converted_katona);
            }
            else
            {
                this.foEpulet.Eltávolit(katona);
            }
        }
        /// <summary>
        /// Minden itthon tartozkodo katonat elkuld
        /// </summary>
        public void Tamad()
        {
            List<Katona> katonak = this.kaszarnya.KiveszTipus(
                                Tipusok.Tarolhatok.Gyalogos,
                                this.kaszarnya.ItthonLevok(
                                    Tipusok.Tarolhatok.Gyalogos));
            List<Lakos> lakos_katonak = new List<Lakos>();
            foreach (Katona item in katonak)
            {
                lakos_katonak.Add(item);
            }
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
            return this.tarolo.Kapacitas[tarolhato][0] > mennyiseg;
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
            foreach (Katona item in this.kaszarnya.Lista[Tipusok.Tarolhatok.Gyalogos])
            {
                if (item.ItthonVan)
                {
                    itthon.Add(item);
                }
            }
            katonaiEgyseg = new KatonaiEgyseg(false, itthon, Id);
            return katonaiEgyseg;
        }
        public void KatonaTermel()
        {
            // bekéri mennyi katonat szeretne gyartani
            System.Console.WriteLine("Mennyi katonat szeretnel gyartani");
            int katonak_szama = Convert.ToInt32(Console.ReadLine());
#pragma warning disable S1848 // Objects should not be created to be dropped immediately without being used
            new Fejlesztes(Tipusok.Fejleszthetok.Gyalogos, Id, Termel);
#pragma warning restore S1848 // Objects should not be created to be dropped immediately without being used
            void Termel()
            {
                // kivesz főépuletből annyi lakost, amennnyire szükség van
                try
                {
                    List<Lakos> lakosok = this.foEpulet.KiveszTipus(Tipusok.Tarolhatok.Lakos, katonak_szama);
                    foreach (Lakos item in lakosok)
                    {
                        // lakosokból katonát készít és beteszi a kaszarnyaba
                        this.kaszarnya.BeteszTipus(Tipusok.Tarolhatok.Gyalogos, new Katona(item, Tipusok.KatonaTipusok.Gyalogos));
                    }
                }
                catch (NincsElegTarolhatoException e)
                {
                    throw new NincsElegTarolhatoException(e.Tarolhato);
                }
                catch (TaroloTulCsordultException e)
                {
                    throw new TaroloTulCsordultException(e.Tarolhato, katonak_szama, e.MaxKapacitas);
                }
            }
        }

        #endregion Public Methods

        #region Public Constructors

        public Jatekos(Tarolo _tarolo, List<NyersanyagMezo> _nyersanyagMezok, Kaszarnya _kaszarnya, FoEpulet _foEpulet)
        {
#pragma warning disable S3010 // Static fields should not be updated in constructors
            Id = CurrentId++;
#pragma warning restore S3010 // Static fields should not be updated in constructors
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
            Adatok += "ID : " + Id;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Adatok { get; private set; } = "";
        public int Id { get; private set; }
        public List<IInteraktivMezo> InteraktivMezok { get; private set; } = new List<IInteraktivMezo>();
        public bool Vesztett { get; private set; } = false;

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
