using NagyBeadandó.Kivételek.MezoKivetelek;
using NagyBeadandó.Mezok.Alapok;
using NagyBeadandó.Utility;
using System;
using System.Collections.Generic;

namespace NagyBeadandó.Tevekenysegek
{
    internal class Fejlesztes
    {
        #region Public Constructors

        public Fejlesztes(Tipusok.Fejleszthetok fejleszthetok, int id, Action fejlesztes)
        {
            this.fejlesztes_tipusa = fejleszthetok;
            this.jatekos_id = id;
            try
            {
                Fejlesztheto();
#pragma warning disable S1848 // Objects should not be created to be dropped immediately without being used
                new Tevekenyseg(4, fejlesztes);
#pragma warning restore S1848 // Objects should not be created to be dropped immediately without being used
            }
            catch (NincsElegTarolhatoException e)
            {
                throw new NincsElegTarolhatoException(e.Tarolhato);
            }
        }

        #endregion Public Constructors

        #region Private Fields

        private static readonly Dictionary<Tipusok.Fejleszthetok, Dictionary<Tipusok.Tarolhatok, int>> fejleszteshez_szukseges_anyagok =
            new Dictionary<Tipusok.Fejleszthetok, Dictionary<Tipusok.Tarolhatok, int>>()
            {
                [Tipusok.Fejleszthetok.Gyalogos] =
                new Dictionary<Tipusok.Tarolhatok, int>()
                {
                    [Tipusok.Tarolhatok.Agyag] = 50,
                    [Tipusok.Tarolhatok.Buza] = 50,
                    [Tipusok.Tarolhatok.Erc] = 50,
                    [Tipusok.Tarolhatok.Fa] = 50
                }
            };

        #endregion Private Fields

        #region Private Fields

        private readonly Tipusok.Fejleszthetok fejlesztes_tipusa;
        private readonly int jatekos_id;

        #endregion Private Fields

        #region Private Methods

        private void Fejlesztheto()
        {
            foreach (KeyValuePair<Tipusok.Tarolhatok, int> item in fejleszteshez_szukseges_anyagok[this.fejlesztes_tipusa])
            {
                foreach (Tipusok.Tarolhatok item2 in fejleszteshez_szukseges_anyagok[this.fejlesztes_tipusa].Keys)
                {
                    if (!Jatek.JatekosById(this.jatekos_id).VanEleg(item2, fejleszteshez_szukseges_anyagok[this.fejlesztes_tipusa][item2]))
                    {
                        throw new NincsElegTarolhatoException(item2);
                    }
                }
            }
        }

        #endregion Private Methods
    }
}
