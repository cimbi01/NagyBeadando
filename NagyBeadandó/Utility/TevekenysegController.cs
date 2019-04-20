using NagyBeadandó.Tevekenysegek;
using System.Collections.Generic;

namespace NagyBeadandó.Utility
{
    /// <summary>
    /// Feladata:
    /// Tevékenységek végrehajtása, görgetése (időtelik)
    /// </summary>
    public static class TevekenysegController
    {
        #region Public Methods

        public static void AddTevekenyseg(Tevekenyseg tevekenyseg)
        {
            tevekenysegek.Add(tevekenyseg);
        }
        /// <summary>
        /// Az összes tevekenyseg-nek az idejét 1-el csökkenti
        /// És ha az egyel csökkentett idő = 0
        /// Akkor lefuttatja a Vegrehajt Action-t
        /// Majd törli a listából
        /// </summary>
        public static void GorgetMind()
        {
            for (int i = 0; i < tevekenysegek.Count; i++)
            {
                Tevekenyseg item = tevekenysegek[i];
                if (--item.Ido == 0)
                {
                    item.VegreHajt();
                    tevekenysegek.Remove(item);
                    i--;
                }
                else if (item.Ido < 0)
                {
                    tevekenysegek.Remove(item);
                    i--;
                }
            }
        }

        #endregion Public Methods

        #region Private Fields

        public static readonly List<Tevekenyseg> tevekenysegek = new List<Tevekenyseg>();

        #endregion Private Fields
    }
}
