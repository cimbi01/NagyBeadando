using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    /// <summary>
    /// Feladata:
    /// Nyersanyag Termelése
    /// Nyersanyag Betakaríthatósága
    /// </summary>
    public class NyersanyagMezo : Tarolo
    {
        #region Public Constructors

        /// <summary>
        /// Inicializalja a nyersanyagmezőt
        /// </summary>
        /// <param name="mezotipus">A Mező Típusa</param>
        /// <param name="kapacitas">A mezőkapacitása</param>
        public NyersanyagMezo(Tipusok.MezoTipusok mezotipus, List<Tipusok.Tarolhatok> kapacitas) : base(mezotipus, kapacitas)
        {
        }

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
