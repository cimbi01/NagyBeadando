using NagyBeadandó.Mezok.Alapok;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class NincsElegTarolhatoException : TarolhatoException
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        #region Public Constructors

        public NincsElegTarolhatoException(Tipusok.Tarolhatok tarolhato) : base(tarolhato, "Nincs elég a megadott anyagból : ")
        {
        }

        #endregion Public Constructors
    }
}
