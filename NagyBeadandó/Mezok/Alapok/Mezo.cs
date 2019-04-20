using System;
using System.Collections.Generic;
using System.Text;

namespace NagyBeadandó.Mezok.Alapok
{
    /// <summary>
    /// Minden mező ős osztálya
    /// IMező azért kell, mert minden  osztálynak csak egy őse lehet.
    /// </summary>
    public abstract class Mezo : IInteraktivMezo
    {
        #region Public Properties

        /// <summary>
        /// Tárolja az osztályban található metódusokat és egy szöveget hozzá A szöveget írja ki a console-ra
        /// </summary>
        public Dictionary<string, Action> Metodusok { get; protected set; } = new Dictionary<string, Action>();
        public Tipusok.MezoTipusok MezoTipus { get; protected set; }
        public string Nev { get; protected set; }
        public virtual string Parameterek { get; protected set; }
        /// <summary>
        /// Segít "Renderelésnél", hogy van-e az osztálynak publikus metódusa, azaz hogy a metódus tartalmaz-e párt
        /// </summary>
        public bool VanBennePublikusMetodus { get; protected set; }

        #endregion Public Properties

        #region Protected Constructors

        protected Mezo(Tipusok.MezoTipusok mezotipus)
        {
            MezoTipus = mezotipus;
            Nev = mezotipus.ToString();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Név : " + Nev);
            stringBuilder.AppendLine();
            Parameterek = stringBuilder.ToString();
        }

        #endregion Protected Constructors
    }
}
