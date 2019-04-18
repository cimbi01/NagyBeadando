using NagyBeadandó.Mezok.Alapok;
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

        public NyersanyagMezo(Tipusok.MezoTipusok mezotipus, Dictionary<Tipusok.Tarolhatok, int[]> kapacitas) : base(mezotipus, kapacitas)
        {
        }
        public NyersanyagMezo(NyersanyagMezo mezo) : base(mezo)
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

        #endregion Public Methods
    }
}
