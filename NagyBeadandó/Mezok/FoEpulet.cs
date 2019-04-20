using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Lakosok;
using NagyBeadandó.Mezok.Alapok;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    public class FoEpulet : Tarolo
    {
        #region Public Constructors

        public FoEpulet() : base(Tipusok.MezoTipusok.Foepulet, new List<Tipusok.Tarolhatok>() { Tipusok.Tarolhatok.Lakos })
        {
            Random rnd = new Random();
            this.termeles = rnd.Next(0, 1000);
        }
        public FoEpulet(FoEpulet fep) : base(fep)
        {
            Random rnd = new Random();
            this.termeles = rnd.Next(0, 1000);
        }
        public int ItthonLevok(Tipusok.Tarolhatok tarolhato)
        {
            int itthon = 0;
            foreach (Lakos item in Lista[tarolhato])
            {
                if (item.ItthonVan)
                {
                    itthon++;
                }
            }
            return itthon;
        }

        #endregion Public Constructors

        #region Private Fields

        public Dictionary<Tipusok.Tarolhatok, List<Lakos>> Lista { get; private set; } = new Dictionary<Tipusok.Tarolhatok, List<Lakos>>()
        { [Tipusok.Tarolhatok.Lakos] = new List<Lakos>() };
        private readonly int termeles;

        #endregion Private Fields

        #region Public Methods

        public void BeteszTipus(Tipusok.Tarolhatok tipus, Lakos betevendo)
        {
            Lista[tipus].Add(betevendo);
        }
        public void Eltávolit(Lakos tipus)
        {
            Lista[Tipusok.Tarolhatok.Lakos].Remove(tipus);
        }
        public List<Lakos> KiveszTipus(Tipusok.Tarolhatok tipus, int mennyit)
        {
            List<Lakos> katonak = new List<Lakos>();
            if (Lista[tipus].Count < mennyit)
            {
                throw new NincsElegTarolhatoException(tipus);
            }
            for (int i = 0; i < mennyit; i++)
            {
                katonak.Add(Lista[tipus][0]);
                Lista[tipus].Remove(katonak[i]);
            }
            return katonak;
        }
        public override bool MegVanTelve(Tipusok.Tarolhatok tarolhato)
        {
            return Lista[Tipusok.Tarolhatok.Lakos].Count == Kapacitas[Tipusok.Tarolhatok.Lakos][1];
        }
        /// <summary>
        /// termeles-nyit termel pluszban a meglevohoz a tarolhato-bol
        /// </summary>
        /// <param name="tarolhato">A tarolhato tipusa, amibol termel</param>
        public void Termel(Tipusok.Tarolhatok tarolhato)
        {
            for (int i = 0; i < this.termeles && Lista[tarolhato].Count < Kapacitas[tarolhato][1]; i++)
            {
                Lista[tarolhato].Add(new Lakos());
            }
        }

        #endregion Public Methods
    }
}
