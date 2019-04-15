using NagyBeadandó.Mezok.Alapok;
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
            Kapacitás = kapacitas;
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
            // PARAMÉTEREK BEÁLLÍTÁSA
            // IINTERAKTÍVMEZŐ BEÁLLÍTÁSA
            // MEZŐ ABSTRACT OSZTÁLY ALAPNAK
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Minden típushoz tárolja, hogy mennyit tárol és hogy mennyit tárolhat
        /// A tömb első eleme a mennyiség
        /// Második eleme a maximum kapacitás
        /// </summary>
        public Dictionary<Tipusok.Tarolhatok, int[]> Kapacitás { get; private set; } = new Dictionary<Tipusok.Tarolhatok, int[]>();

        #endregion Public Properties
    }
}
