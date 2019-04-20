﻿using System;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok.Alapok
{
    /// <summary>
    /// Feladata, hogy a Renderlésnél felhasználja a mezők tulajdonságait
    /// </summary>
    public interface IInteraktivMezo
    {
        #region Public Properties

        /// <summary>
        /// Tárolja az osztályban található metódusokat és egy szöveget hozzá A szöveget írja ki a console-ra
        /// </summary>
        Dictionary<string, Action> Metodusok { get; }
        string Nev { get; }
        string Parameterek { get; }
        /// <summary>
        /// Segít "Renderelésnél", hogy van-e az osztálynak publikus metódusa, azaz hogy a metódus tartalmaz-e párt
        /// </summary>
        bool VanBennePublikusMetodus { get; }

        #endregion Public Properties
    }
}
