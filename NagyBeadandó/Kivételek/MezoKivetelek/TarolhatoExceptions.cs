using NagyBeadandó.Mezok.Alapok;
using System;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public abstract class TarolhatoException : Exception
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        #region Public Constructors

        protected TarolhatoException(Tipusok.Tarolhatok tarolhato, string message) : base(message)
        {
            Tarolhato = tarolhato;
        }

        #endregion Public Constructors

        #region Public Properties

        public Tipusok.Tarolhatok Tarolhato { get; private set; }

        #endregion Public Properties
    }
}
