using NagyBeadandó.Mezok.Alapok;
using System;
using System.Collections.Generic;
using System.Text;

namespace NagyBeadandó.Mezok
{
    /// <summary>Feladata:
    // Tárolni a különböző tárolható típusokat
    // Lekérni a különböző tárolható típusokból
    /// </summary>
    internal class Tarolo : Mezo
    {
        #region Public Constructors

        public Tarolo()
        { }
        // <summary>
        /// Inicializalja az osszes mezejet
        /// </summary>
        /// <param name="id">Mezo ID-je</param>
        /// <param name="mezotipus">Mezo tipusa</param>
        /// <param name="kapacitas">Mezo tarolhatok szerinti tipusa</param>
        public Tarolo(int id, Tipusok.MezoTipusok mezotipus, Dictionary<Tipusok.Tarolhatok, int[]> kapacitas) : base(id, mezotipus)
        {
            // paraméterek StringBuilder-e (kevesebbet erőforrást igényel a hasznáalta, mint ha mindig hozzáadogatnék a string-hez)
            StringBuilder stringBuilder = new StringBuilder();
            Kapacitas = kapacitas;
            stringBuilder.Append("Tárolt típusok: ");
            foreach (Tipusok.Tarolhatok item in kapacitas.Keys)
            {
                stringBuilder.AppendLine();
                stringBuilder.Append("Típus: ");
                stringBuilder.Append(item.ToString());
                stringBuilder.Append(" Mennyiség: ");
                kapacitas.TryGetValue(item, out int[] value);
                stringBuilder.Append(value[0].ToString());
                stringBuilder.Append(", Kapacitás: ");
                stringBuilder.Append(value[1].ToString());
                stringBuilder.Append(";");
            }
            Parameterek += stringBuilder.ToString();
            InteraktivMezo = new InteraktívTarolo(this);
        }
        public Tarolo(Tarolo tarolo) : base(tarolo.ID, tarolo.MezoTipus)
        {
            Kapacitas = tarolo.Kapacitas;
            Parameterek = tarolo.Parameterek;
            InteraktivMezo = tarolo.InteraktivMezo;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Minden típushoz tárolja, hogy mennyit tárol és hogy mennyit tárolhat
        /// A tömb első eleme a mennyiség
        /// Második eleme a maximum kapacitás
        /// </summary>
        public Dictionary<Tipusok.Tarolhatok, int[]> Kapacitas { get; private set; } = new Dictionary<Tipusok.Tarolhatok, int[]>();

        #endregion Public Properties

        #region Private Classes

        private class InteraktívTarolo : Tarolo, IInteraktivMezo
        {
            #region Public Constructors

            public InteraktívTarolo(Tarolo tarolo) : base(tarolo)
            {
                VanBennePublikusMetodus = false;
                InteraktivMezo = this;
            }

            #endregion Public Constructors

            #region Public Properties

            public Dictionary<string, Action> Metódusok { get; private set; } = new Dictionary<string, Action>();

            public bool VanBennePublikusMetodus { get; private set; }

            #endregion Public Properties
        }

        #endregion Private Classes
    }
}
