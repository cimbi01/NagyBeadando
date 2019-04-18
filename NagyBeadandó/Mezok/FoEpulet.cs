using NagyBeadandó.Lakosok;
using NagyBeadandó.Mezok.Alapok;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    public class FoEpulet : Tarolo, ITipusTaroloTermelo<Lakos>
    {
        #region Public Constructors

        public FoEpulet(int id) : base(id, Tipusok.MezoTipusok.Foepulet, new Dictionary<Tipusok.Tarolhatok, int[]>() { [Tipusok.Tarolhatok.Lakos] = new int[2] { 0, 10 } })
        {
            InteraktivMezo = new InteraktivFoEpulet(this);
        }
        public FoEpulet(FoEpulet fep) : base(fep.ID, fep.MezoTipus, fep.Kapacitas)
        { }

        #endregion Public Constructors

        #region Private Fields

        public Dictionary<Tipusok.Tarolhatok, List<Lakos>> Lista { get; private set; } = new Dictionary<Tipusok.Tarolhatok, List<Lakos>>()
        { [Tipusok.Tarolhatok.Lakos] = new List<Lakos>() };
        private readonly int termeles = 10;

        #endregion Private Fields

        #region Public Methods

        public List<Lakos> Kivesz(Lakos tipus, int mennyit)
        {
            List<Lakos> katonak = new List<Lakos>();
            for (int i = 0; i < mennyit; i++)
            {
                katonak.Add(Lista[Tipusok.Tarolhatok.Lakos][0]);
                Lista[Tipusok.Tarolhatok.Lakos].Remove(katonak[i]);
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

        #region Private Classes

        private class InteraktivFoEpulet : FoEpulet, IInteraktivMezo
        {
            #region Public Constructors

            public InteraktivFoEpulet(FoEpulet fep) : base(fep)
            {
                InteraktivMezo = this;
                VanBennePublikusMetodus = Metodusok.Count == 0;
            }

            #endregion Public Constructors

            #region Public Properties

            public Dictionary<string, Action> Metodusok { get; private set; } = new Dictionary<string, Action>();

            public bool VanBennePublikusMetodus { get; private set; }

            #endregion Public Properties
        }

        #endregion Private Classes
    }
}
