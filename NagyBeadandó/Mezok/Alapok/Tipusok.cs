namespace NagyBeadandó.Mezok.Alapok
{
    /// <summary>
    /// Feladata, hogy tárolja  Programban használt típusokat, hogy ne legyen elírás
    /// Elírások elkerüléséért használom Programon belül ha típusokat szeretnénk kifejezni csak
    /// használjuk a megadott típusokat, és nincs elírási hiba
    /// /// <summary>
    public static class Tipusok
    {
        #region Public Enums

        /// <summary>
        /// A Programban használt Mező típusok Ha új mező fajtát szeretnénk létrehozni, bővíteni kell
        /// </summary>
        public enum MezoTipusok { Raktar, Foepulet, Buzamezo, Agyagbanya, Faerdo, Ercbanya };
        /// <summary>
        /// A Programban használt tárolhatóegység típusok Ha új tárolhatóegység fajtát szeretnénk létrehozni, bővíteni kell
        /// </summary>
        public enum Tarolhatok { Buza, Agyag, Erc, Fa, Lakos };

        #endregion Public Enums
    }
}
