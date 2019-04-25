using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Lakosok;
using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Mezok;
using NagyBeadandó.Mezok.Alapok;
using NagyBeadandó.Tevekenysegek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NagyBeadandó.Utility
{
    /// <summary>
    /// Játékos osztálya
    /// Ő kezeli a mezőket, és a rajtuk végrehajtott metódusokat
    /// Ő kezeli a nyersanaygok termelési ciklusát
    /// </summary>
    public class Jatekos
    {
        /// <summary>
        /// Rendereléshez kell
        /// Hogy ki tudja írni a metódusokat, és a játékos paramétereit
        /// </summary>
        private class InteraktivJatekos : IInteraktivMezo
        {
            /// <summary>
            /// Iniciaizálja az InteraktívJátékost
            /// </summary>
            /// <param name="_jatekos"></param>
            public InteraktivJatekos(Jatekos _jatekos)
            {
                Metodusok = new Dictionary<string, Action>()
                {
                    ["Tamad"] = _jatekos.Tamad,
                };
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Név : " + Nev);
                Parameterek = stringBuilder.ToString();
                VanBennePublikusMetodus = Metodusok.Count != 0;
            }
            public Dictionary<string, Action> Metodusok { get; set; }

            public bool VanBennePublikusMetodus { get; set; }
            public string Nev { get; set; } = "Jatekos";

            public string Parameterek { get; set; }

            public int Szint { get; set; } = 1;
        }

        #region Private Fields

        /// <summary>
        /// Az akutáliss játékosoknak osztja ki az aktuális CurrentId-t
        /// Amint hozzáadta, megnövel 1-el,  a kvöetkező Játákosnak
        /// </summary>
        private static int CurrentId = 0;
        /// <summary>
        /// Játékos FőÉpület mezője
        /// </summary>
        private readonly FoEpulet foEpulet;
        /// <summary>
        /// Játékos Nyersanyagmezői
        /// </summary>
        private readonly List<NyersanyagMezo> nyersanyagMezok;
        /// <summary>
        /// Játékos TárolóMezője : Raktár
        /// </summary>
        private readonly Tarolo tarolo;

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        /// Minden Lakos megetetve-t átállítja false-ra
        /// </summary>
        private void LakosEtetveReset()
        {
            this.foEpulet.Lista.ForEach(t => t.MegVanEtetve = false);
        }
        /// <summary>
        /// Minden Lakost megetet, ha van annyi búza a raktárban, amennyi a fogyasztása a katonának
        /// </summary>
        private void EtetniProbal()
        {
            this.foEpulet.Lista.ForEach(
                t =>
                {
                    this.tarolo.Kivesz(Tipusok.Tarolhatok.Buza, t.Fogyasztas);
                    t.MegVanEtetve = true;
                }
            );
        }
        /// <summary>
        /// Aki nem lett megetetve, azt kiveszi a főépulet listájából
        /// </summary>
        private void MegNemEtetettMegol()
        {
            for (int i = 0; i < this.foEpulet.Lista.Count; i++)
            {
                Lakosok.Lakos item2 = this.foEpulet.Lista[i];
                if (!item2.MegVanEtetve)
                {
                    this.foEpulet.Eltávolit(item2);
                    i--;
                }
            }
        }
        /// <summary>
        /// Minden Mezőn végrehajtja a termel-t
        /// </summary>
        private void MindenMezonTermel()
        {
            this.foEpulet.Termel();
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
            // TODO: try catch kivesz mindent a mezobol, betesz mindent a mezobe
            IEnumerable<NyersanyagMezo> buzamezok = this.nyersanyagMezok.Where(mezo => mezo.Kapacitas.ContainsKey(Tipusok.Tarolhatok.Buza));
            foreach (NyersanyagMezo nyersanyag_mezo in buzamezok)
            {
                Tipusok.Tarolhatok buzatarolhato = Tipusok.Tarolhatok.Buza;
                int mennyit;
                int tarolo_maradekhely = this.tarolo.Kapacitas[buzatarolhato][1] - this.tarolo.Kapacitas[buzatarolhato][0];
                // ha a mezőben több a teremtl anyag, mint amekkkora a maradék hely, akkor csak maradekhelynyit kér le
                if (nyersanyag_mezo.Kapacitas[buzatarolhato][0] > tarolo_maradekhely)
                {
                    mennyit = tarolo_maradekhely;
                }
                // ellenkező esetben az egészet, amit tárol
                else
                {
                    mennyit = nyersanyag_mezo.Kapacitas[buzatarolhato][0];
                }
                if (mennyit > 0)
                {
                    /// nem csodrulhat túl, mert annyit teszünk bele amennyit még elbír
                    nyersanyag_mezo.Kivesz(buzatarolhato, mennyit);
                    this.tarolo.Betesz(buzatarolhato, mennyit);
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
                    if (item2 != Tipusok.Tarolhatok.Buza)
                    {
                        int mennyit;
                        int tarolo_maradekhely = this.tarolo.Kapacitas[item2][1] - this.tarolo.Kapacitas[item2][0];
                        /// ha a mezőben több a teremtl anyag, mint amekkkora a maradék hely, akkor csak maradekhelynyit kér le
                        if (item.Kapacitas[item2][0] > tarolo_maradekhely)
                        {
                            mennyit = tarolo_maradekhely;
                        }
                        /// ellenkező esetben az egészet, amit tárol
                        else
                        {
                            mennyit = item.Kapacitas[item2][0];
                        }
                        if (mennyit > 0)
                        {
                            /// majd kiveszi és beteszi a tárolóba
                            /// nem csodrulhat túl, mert annyit teszünk bele amennyit még elbír
                            item.Kivesz(item2, mennyit);
                            this.tarolo.Betesz(item2, mennyit);
                        }
                    }
                }
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Termelési ciklusért felel:
        /// Minden mezőn termel
        /// Betakarítja a termelt anyagot
        /// Ciklikusan, amíg mindenki meg nincs etetve és van teljesen be nem takritott búzamező
        /// Megeteti a lakosokat
        /// Betakarítja a Búzát
        /// </summary>
        public void EtetTermel()
        {
            Logger.Log("Termelési ciklus eleje");
            LakosEtetveReset();
            MindenMezonTermel();
            RaktarNyersanyagFeltolt();
            bool van_meg_nem_etetett;
            bool van_be_nem_takaritott;
            /// amíg van megetetlen lakos vagy be nem takaritott búza mező
            do
            {
                /// Búzát tölt, majd etet
                RaktarBuzaFeltoltes();
                try
                {
                    EtetniProbal();
                    van_meg_nem_etetett = false;
                }
                catch (NincsElegTarolhatoException)
                {
                    van_meg_nem_etetett = true;
                }
                IEnumerable<NyersanyagMezo> buzamezok = this.nyersanyagMezok.Where(
                    mezo =>
                    mezo.Kapacitas.ContainsKey(Tipusok.Tarolhatok.Buza));
                van_be_nem_takaritott = buzamezok.Any(mezo2 =>
                    mezo2.Kapacitas[Tipusok.Tarolhatok.Buza][0] > 0);
            } while (van_meg_nem_etetett && van_be_nem_takaritott);
            if (van_meg_nem_etetett)
            {
                MegNemEtetettMegol();
            }
            if (van_be_nem_takaritott)
            {
                RaktarBuzaFeltoltes();
            }
            Logger.Log("Termelési ciklus vége");
        }
        /// <summary>
        /// Vesztett-et igaz-ra állítja
        /// </summary>
        public void Veszit()
        {
            Logger.Log("Játékos vesztett");
            Vesztett = true;
        }
        /// <summary>
        /// Paraméterként kapott Katonaiegység minden lakoságnak ItthonVan-ját true-ra állítja
        /// </summary>
        /// <param name="katonaiEgyseg"></param>
        public void KatonakHazaternek(KatonaiEgyseg katonaiEgyseg)
        {
            Logger.Log("Katonák hazatérnek");
            this.foEpulet.BeteszTipus(katonaiEgyseg.Katonak);
            katonaiEgyseg.Katonak.ForEach(lakos => lakos.ItthonVan = true);
        }
        /// <summary>
        /// Minden itthon tartozkodo katonat elkuld Támadni
        /// </summary>
        public void Tamad()
        {
            /// nem dob kivételt, mert csak az itthonlevok-et kéri le
            List<Lakos> lakos_katonak = this.foEpulet.KiveszTipus(this.foEpulet.ItthonLevok());
            if (lakos_katonak.Count > 0)
            {
                Logger.Log("Támadást kezdeményez");
                Tamadas.TamadasInditas(
                    new KatonaiEgyseg(
                        true,
                        katonak: lakos_katonak,
                        jatekos_id: Id),
                    (Id + 1) % 2);
            }
            else
            {
                Logger.Log("Nincs katonád itthon, amivel támadni tudnál");
            }
        }
        /// <summary>
        /// Visszaad egy katonai egységet az itthon levo lakosokból
        /// </summary>
        /// <returns>A katonai egység az összes itthon található katonát belefűzi egy listába, amiből a katonaiegység van</returns>
        public KatonaiEgyseg Vedekezik()
        {
            Logger.Log("Játékos védekezésre kényszerítve");
            List<Lakos> itthon = this.foEpulet.KiveszTipus(this.foEpulet.ItthonLevok());
            KatonaiEgyseg katonaiEgyseg = new KatonaiEgyseg(false, itthon, Id);
            return katonaiEgyseg;
        }

        #endregion Public Methods

        #region Public Constructors

        /// <summary>
        /// Inicializalja a játékost
        /// </summary>
        /// <param name="_tarolo">Játékos tárolója</param>
        /// <param name="_nyersanyagMezok">Játékos nyersanyagmezői</param>
        /// <param name="_foEpulet">Játékos főépülete</param>
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
            this.nyersanyagMezok.ForEach(mezo => InteraktivMezok.Add(mezo));
            InteraktivMezok.Add(new InteraktivJatekos(this));
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Játékos egyedi ID-je ami alapján le lehet kérni a Játékból
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Játékos InteraktívMezei, amik a Játékos interakcióhoz és Rendereléshez kellenek
        /// </summary>
        public List<IInteraktivMezo> InteraktivMezok { get; private set; } = new List<IInteraktivMezo>();
        /// <summary>
        /// Tárolja, hogy a játékos Vesztett-e
        /// </summary>
        public bool Vesztett { get; private set; } = false;

        #endregion Public Properties
    }
}
