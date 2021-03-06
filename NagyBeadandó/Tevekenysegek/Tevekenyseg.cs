﻿using System;

namespace NagyBeadandó.Tevekenysegek
{
    /// <summary>
    /// Feladata:
    ///     Időben lefolyó tevékenységek végrehajtása
    ///     Miután az idő lejárt
    /// </summary>
    public class Tevekenyseg
    {
        #region Public Properties

        /// <summary>
        /// Az idő, amit minden körben egyel csökkentünk
        /// </summary>
        public int Ido { get; set; }

        #endregion Public Properties

        #region Private Fields

        /// <summary>
        /// Delegált, amit végrehajt, amikor az idő lejárt
        /// </summary>
        public Action VegreHajt { get; private set; }

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Inicializálja a Tevekenyseget
        /// </summary>
        /// <param name="_ido">a tevékenység aktiváláshoz szükséges idő</param>
        /// <param name="_vegrehajt">A metódus, ami végrehajtódik, idő-nyi iterációs egység után</param>
        public Tevekenyseg(int _ido, Action _vegrehajt)
        {
            Ido = _ido;
            VegreHajt = _vegrehajt;
        }

        #endregion Public Constructors
    }
}
