namespace NagyBeadandó.Mezok.Alapok
{
    /// <summary>
    /// Feladata, hogy a Termelésért felelős osztályoknak adjon metódust, amivel a külövilág tud kommunikálni
    /// </summary>
    public interface ITermelo
    {
        #region Public Methods

        /// <summary>
        /// Termelést indít a termelő osztályon
        /// </summary>
        void Termel(Tipusok.Tarolhatok tarolhato);

        #endregion Public Methods
    }
}
