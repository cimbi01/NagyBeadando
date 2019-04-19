using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Lakosok.Katonasag;
using NagyBeadandó.Mezok.Alapok;
using System.Collections.Generic;

namespace NagyBeadandó.Mezok
{
    public class Kaszarnya : Tarolo, ITipusTaroloTermelo<Katona>
    {
        #region Private Fields

        public Kaszarnya() : base(Tipusok.MezoTipusok.Kaszarnya, new Dictionary<Tipusok.Tarolhatok, int[]>()
        { [Tipusok.Tarolhatok.Gyalogos] = new int[2] { 0, 1000 } }
        )
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
        public int ItthonLevok(Tipusok.Tarolhatok tipus)
        {
            int itthon = 0;
            foreach (Katona item in Lista[tipus])
            {
                if (item.ItthonVan)
                {
                    itthon++;
                }
            }
            return itthon;
        }
        public List<Katona> KiveszTipus(Tipusok.Tarolhatok tipus, int mennyit)
        {
            List<Katona> katonak = new List<Katona>();
            // itthon levok
            int itthon = ItthonLevok(tipus);
            if (itthon < mennyit)
            {
                throw new NincsElegTarolhatoException(tipus);
            }
            for (int i = 0; i < Lista[tipus].Count && mennyit > 0; i++)
            {
                if (Lista[tipus][i].ItthonVan)
                {
                    katonak.Add(Lista[tipus][i]);
                    mennyit--;
                }
            }
            return katonak;
        }
        public void Termel(Tipusok.Tarolhatok tarolhato)
        {
        }

        #endregion Public Methods
    }
}
