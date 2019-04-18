using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    public class Kaszarnya : Tarolo, ITipusTaroloTermelo<Katona>
    {
        #region Public Constructors

        public Kaszarnya()
        {
        }

        #endregion Public Constructors

        #region Private Fields

        private static readonly Dictionary<Tipusok.KatonaTipusok, Tipusok.Tarolhatok> tarolhato_katonatipusok =
            new Dictionary<Tipusok.KatonaTipusok, Tipusok.Tarolhatok>()
            { [Tipusok.KatonaTipusok.Gyalogos] = Tipusok.Tarolhatok.Gyalogos };
        #endregion Private Fields

        #region Public Properties

        public Dictionary<Tipusok.Tarolhatok, List<Katona>> Lista { get; private set; } =
            new Dictionary<Tipusok.Tarolhatok, List<Katona>>()
            { [Tipusok.Tarolhatok.Gyalogos] = new List<Katona>() };

        #endregion Public Properties

        #region Public Methods

        List<Katona> ITipusTaroloTermelo<Katona>.Kivesz(Katona tipus, int mennyit)
        {
            List<Katona> katonak = new List<Katona>();
            for (int i = 0; i < mennyit; i++)
            {
                katonak.Add(Lista[tarolhato_katonatipusok[tipus.KatonaTipus]][0]);
                Lista[tarolhato_katonatipusok[tipus.KatonaTipus]].Remove(katonak[i]);
            }
            return katonak;
        }
        public void Termel(Tipusok.Tarolhatok tarolhato)
        {
        }

        #endregion Public Methods
    }
}
