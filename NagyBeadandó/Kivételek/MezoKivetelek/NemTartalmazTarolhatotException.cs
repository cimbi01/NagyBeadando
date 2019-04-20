using System;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
    /// <summary>
    /// Kivétel, ami a Tarolok-ban jelentkezik, amikor a tároló nem tartalmazza a paraméterként kapott tarolhatot
    /// </summary>
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class NemTartalmazTarolhatotException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        #region Public Constructors

        public NemTartalmazTarolhatotException() : base("Nem tartalmazza a megadott tárolható típust")
        {
        }

        #endregion Public Constructors
    }
}
