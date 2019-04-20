using System;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
    /// <summary>
    /// Kivétel, ami akkor jelentkezik, amikor egy Taroloban nincs annyi Tarolhato, amennyit probalnak belole lekerni
    /// </summary>
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class NincsElegTarolhatoException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        #region Public Constructors

        public NincsElegTarolhatoException() : base("Nincs elég a megadott anyagból")
        {
        }

        #endregion Public Constructors
    }
}
