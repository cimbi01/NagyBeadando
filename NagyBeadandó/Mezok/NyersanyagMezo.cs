using NagyBeadandó.Mezok.Alapok;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    /// <summary>
    /// Feladata:
    /// Nyersanyag Termelése
    /// Nyersanyag Betakaríthatósága
    /// </summary>
    public class NyersanyagMezo : Tarolo, ITaroloTermelo
    {
        #region Public Constructors

        public NyersanyagMezo(int id, Tipusok.MezoTipusok mezotipus, Dictionary<Tipusok.Tarolhatok, int[]> kapacitas) : base(id, mezotipus, kapacitas)
        {
            InteraktivMezo = new InteraktivNyersanyagMezo(this);
        }
        public NyersanyagMezo(NyersanyagMezo mezo) : base(mezo.ID, mezo.MezoTipus, mezo.Kapacitas)
        { }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Megtolti a kapacitást a maxkapacitásnyival
        /// </summary>
        /// <param name="tarolhato">A tárolható típusa,a mit termel</param>
        public void Termel(Tipusok.Tarolhatok tarolhato)
        {
            Kapacitas[tarolhato][0] = Kapacitas[tarolhato][1];
        }
        private class InteraktivNyersanyagMezo : NyersanyagMezo, IInteraktivMezo
        {
            #region Public Constructors

            public InteraktivNyersanyagMezo(NyersanyagMezo mezo) : base(mezo)
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

        #endregion Public Methods
    }
}
