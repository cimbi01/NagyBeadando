using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Alapok
{
    public interface ITipusTaroloTermelo<T> : ITaroloTermelo
    {
        #region Public Properties

        Dictionary<Tipusok.Tarolhatok, List<T>> Lista { get; }
        List<T> Kivesz(T tipus, int mennyit);

        #endregion Public Properties
    }
}
