using NagyBeadandó.Mezok.Alapok;
using System;

namespace NagyBeadandó.Kivételek.MezoKivetelek
{
    public abstract class TarolhatoException : Exception
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
