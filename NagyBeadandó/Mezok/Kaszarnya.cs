using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    public class Kaszarnya : Tarolo, ITipusTaroloTermelo<Katona>
    {
        #region Private Fields

        public Kaszarnya(Tipusok.MezoTipusok mezotipus, Dictionary<Tipusok.Tarolhatok, int[]> kapacitas) : base(mezotipus, kapacitas)
        { }
        public static Dictionary<Tipusok.KatonaTipusok, Tipusok.Tarolhatok> Tarolhato_katonatipusok { get; private set; } =
            new Dictionary<Tipusok.KatonaTipusok, Tipusok.Tarolhatok>()
            { [Tipusok.KatonaTipusok.Gyalogos] = Tipusok.Tarolhatok.Gyalogos };
        #endregion Private Fields

        #region Public Properties

        public Dictionary<Tipusok.Tarolhatok, List<Katona>> Lista { get; private set; } =
            new Dictionary<Tipusok.Tarolhatok, List<Katona>>()
            { [Tipusok.Tarolhatok.Gyalogos] = new List<Katona>() };

        #endregion Public Properties

        #region Public Methods

        public void Eltávolit(Katona tipus)
        {
            Lista[Tarolhato_katonatipusok[tipus.KatonaTipus]].Remove(tipus);
        }
        public List<Katona> KiveszTipus(Tipusok.Tarolhatok tipus, int mennyit)
        {
            List<Katona> katonak = new List<Katona>();
            if (Lista[tipus].Count < mennyit)
            {
                throw new NincsElegTarolhatoException(tipus);
            }
            for (int i = 0; i < mennyit; i++)
            {
                katonak.Add(Lista[tipus][0]);
                Lista[tipus].Remove(katonak[i]);
            }
            return katonak;
        }
        public void Termel(Tipusok.Tarolhatok tarolhato)
        {
        }

        #endregion Public Methods
    }
}
