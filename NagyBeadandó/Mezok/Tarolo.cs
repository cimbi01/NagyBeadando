using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    /// <summary>Feladata:
    // Tárolni a különböző tárolható típusokat
    // Lekérni a különböző tárolható típusokból
    /// </summary>
    internal class Tarolo : IMezo
    {
        #region Public Properties

        public int ID { get; private set; }
        public IInteraktivMezo InteraktivMezo { get; private set; }
        public Dictionary<Tipusok.Tarolhatok, int> Kapacitás { get; private set; } = new Dictionary<Tipusok.Tarolhatok, int>();
        public Tipusok.MezoTipusok MezoTipus { get; private set; }
        public string Név { get; private set; }
        public string Parameterek { get; private set; }
        public int Szint { get; private set; }
        public Dictionary<Tipusok.Tarolhatok, int> TároltMennyiseg { get; private set; } = new Dictionary<Tipusok.Tarolhatok, int>();

        #endregion Public Properties

        #region Public Constructors

        public Tarolo()
        { }
        // <summary>
        /// Inicializalja az osszes mezejet
        /// </summary>
        /// <param name="id">Mezo ID-je</param>
        /// <param name="mezotipus">Mezo tipusa</param>
        /// <param name="kapacitas">Mezo tarolhatok szerinti tipusa</param>
        public Tarolo(int id, Tipusok.MezoTipusok mezotipus, Dictionary<Tipusok.Tarolhatok, int> kapacitas)
        {
            ID = id;
            MezoTipus = mezotipus;
            Kapacitás = kapacitas;
        }

        #endregion Public Constructors
    }
}
