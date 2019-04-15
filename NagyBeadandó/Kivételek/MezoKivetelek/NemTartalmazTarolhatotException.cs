using NagyBeadandó.Mezok.Alapok;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class NemTartalmazTarolhatotException : TarolhatoException
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        #region Public Constructors

        public NemTartalmazTarolhatotException(Tipusok.Tarolhatok tarolhatok) : base(tarolhatok, "Nem tartalmazza a megadott tárolható típust")
        {
        }

        #endregion Public Constructors
    }
}
