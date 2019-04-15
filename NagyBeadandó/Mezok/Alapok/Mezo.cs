using System.Text;

namespace NagyBeadandó.Mezok.Alapok
{
    /// <summary>
    /// Minden mező ős osztálya
    /// IMező azért kell, mert minden  osztálynak csak egy őse lehet.
    /// </summary>
    public abstract class Mezo : IMezo
    {
        #region Public Properties

        public int ID { get; protected set; }

        public IInteraktivMezo InteraktivMezo { get; protected set; }

        public Tipusok.MezoTipusok MezoTipus { get; protected set; }

        public string Név { get; protected set; }

        public string Parameterek { get; protected set; }

        public int Szint { get; protected set; } = 1;

        #endregion Public Properties

        #region Protected Constructors

        protected Mezo(int id, Tipusok.MezoTipusok mezotipus)
        {
            ID = id;
            MezoTipus = mezotipus;
            Név = mezotipus.ToString() + ID.ToString();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Név : " + Név);
            stringBuilder.AppendLine();
            stringBuilder.Append("Szint : " + Szint);
            Parameterek = stringBuilder.ToString();
        }
        protected Mezo()
        {
        }
        // <summary>
        /// Inicializalja az osszes mezejet
        /// </summary>
        /// <param name="id">Mezo ID-je</param>
        /// <param name="mezotipus">Mezo tipusa</param>
        /// <param name="kapacitas">Mezo tarolhatok szerinti tipusa</param>

        #endregion Protected Constructors
    }
}
