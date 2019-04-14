namespace NagyBeadandó.Mezok.Alapok
{
    internal static class Tipusok
    {
        /// <summary>
        /// Elírások elkerüléséért használom Programon belül ha típusokat szeretnénk kifejezni csak
        /// használjuk a megadott típusokat, és nincs elírási hiba
        /// </summary>

        #region Public Enums

        /// <summary>
        /// A Programban használt katona típusok Ha új katona fajtát szeretnénk létrehozni, bővíteni kell
        /// </summary>
        public enum KatonaTipusok { Gyalogos };
        /// <summary>
        /// A Programban használt Mező típusok Ha új mező fajtát szeretnénk létrehozni, bővíteni kell
        /// </summary>
        public enum MezoTipusok { Raktar, Kaszarnya, Foepulet, Buzamezo, Agyagbanya, Faerdo, Ercbanya };
        /// <summary>
        /// A Programban használt tárolhatóegység típusok Ha új tárolhatóegység fajtát szeretnénk létrehozni, bővíteni kell
        /// </summary>
        public enum Tarolhatok { Buza, Agyag, Erc, Fa, Katona };

        #endregion Public Enums
    }
}
