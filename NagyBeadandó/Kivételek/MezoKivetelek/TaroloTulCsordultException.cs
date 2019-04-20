using System;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    /// <summary>
    /// Kivétel, ami akkor jelentkezik, amikor a Tarolo-ban a maximum kapacitásának megfelelőnél több Tarolhato-t szeretnenk beletenni
    /// </summary>
    public class TaroloTulCsordultException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        #region Public Constructors

        /// <summary>
        /// Inicializálja a kivétel tagjait
        /// </summary>
        /// <param name="tarolhatok">A tárolható típusa, ami a Tárolóba próbált tenni</param>
        /// <param name="mennyiseg">A tárolható mennyisége, amennyit a Tárolóba próbált tenni</param>
        /// <param name="kapacitas">A Tároló max kapacitása az adott tárolhatóból</param>
        public TaroloTulCsordultException() : base("A tárolóban nincs több hely")
        {
        }

        #endregion Public Constructors
    }
}
