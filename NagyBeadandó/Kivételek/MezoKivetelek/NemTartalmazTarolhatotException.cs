using NagyBeadandó.Mezok.Alapok;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
    public class NemTartalmazTarolhatotException : TarolhatoException
    {
        #region Public Constructors

        public NemTartalmazTarolhatotException(Tipusok.Tarolhatok tarolhatok) : base(tarolhatok, "Nem tartalmazza a megadott tárolható típust")
        {
        }

        #endregion Public Constructors
    }
}
