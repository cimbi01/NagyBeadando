using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Alapok
{
    public interface ITarolo : IMezo
    {
        #region Public Properties

        Dictionary<Tipusok.Tarolhatok, int[]> Kapacitas { get; }

        #endregion Public Properties

        #region Public Methods

        void Betesz(Tipusok.Tarolhatok tarolhato, int mennyit);
        int Kivesz(Tipusok.Tarolhatok tarolhato, int mennyit);
        bool MegVanTelve(Tipusok.Tarolhatok tarolhato);

        #endregion Public Methods
    }
}
