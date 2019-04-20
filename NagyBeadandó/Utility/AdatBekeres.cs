using System;

namespace NagyBeadandó.Utility
{
    /// <summary>
    /// AdatBekérésért felelős osztály
    /// </summary>
    public static class AdatBekeres
    {
        #region Public Methods

        // csak kiírja a bekeroszoveget és beker egy adatot ellenorzes nélkül
        public static string Bekeres(string bekeroszoveg = "Add meg a bekért adatot!")
        {
            Console.Clear();
            string adat = "";
            Console.WriteLine(bekeroszoveg);
            adat = Console.ReadLine();
            Console.Clear();
            return adat;
        }
        // visszaadja a bekéréstípusának megfelelően vagy ha üres, akkor üresen
        public static T EllenorzottBekeres<T>(string bekeroszoveg = "Add meg a bekért adatot!")
        {
            string adat = Bekeres(bekeroszoveg);
            // a kovnertált adat attól függően milyen típusú adatot várunk vissza
            T adatkonvertált = default(T);
            // ha az adat üres
            if (adat == "")
            {
                Console.WriteLine("Hibás adatbevitel");
                System.Threading.Thread.Sleep(200);
                adatkonvertált = EllenorzottBekeres<T>(bekeroszoveg);
            }
            // ha az adat nem üres
            else if (adat != "")
            {
                try
                {
                    // megpróbálja a kapott adatot T típusúvá tenni
                    adatkonvertált = (T)Convert.ChangeType(adat, typeof(T));
                }
                // ha nem sikerult konvertálni
                catch (FormatException e)
                {
                    Console.WriteLine("Hibás adatbevitel");
                    Console.WriteLine(e.Message);
                    System.Threading.Thread.Sleep(200);
                    // ujra meghívja magát, amíg a konvertálás nem jó
                    adatkonvertált = EllenorzottBekeres<T>(bekeroszoveg);
                }
            }
            Console.Clear();
            return adatkonvertált;
        }

        #endregion Public Methods
    }
}
