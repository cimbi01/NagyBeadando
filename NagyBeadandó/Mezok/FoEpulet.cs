using NagyBeadandó.Mezok.Alapok;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    public class FoEpulet : Tarolo, ITaroloTermelo
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

        private readonly int termeles = 10;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// termeles-nyit termel pluszban a meglevohoz a tarolhato-bol
        /// </summary>
        /// <param name="tarolhato">A tarolhato tipusa, amibol termel</param>
        public void Termel(Tipusok.Tarolhatok tarolhato)
        {
            // növeli a tarolt mennyiseget
            Kapacitas[tarolhato][0] += this.termeles;
            // ha a taroltmennyiseg > mint a maxkapactitás, akkor egyenlővé teszi vele
            if (Kapacitas[tarolhato][0] > Kapacitas[tarolhato][1])
            {
                Kapacitas[tarolhato][0] = Kapacitas[tarolhato][1];
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
