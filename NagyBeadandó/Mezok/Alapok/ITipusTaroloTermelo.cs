using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Alapok
{
    public interface ITipusTaroloTermelo<T> : ITaroloTermelo
    {
        #region Public Properties

        Dictionary<Tipusok.Tarolhatok, List<T>> Lista { get; }
        void Eltávolit(T tipus);
        List<T> KiveszTipus(Tipusok.Tarolhatok tipus, int mennyit);

        #endregion Public Properties
    }
}
