namespace Warenverwaltung
{
    public class Artikel
    {
        public string Artikelnummer { get; set; }
        public string Bezeichnung { get; set; }
        public int Menge { get; set; }
        public decimal Preis { get; set; }

        public string ToCsv()
        {
            return $"{Artikelnummer};{Bezeichnung};{Menge};{Preis}";
        }

        public static Artikel FromCsv(string zeile)
        {
            var teile = zeile.Split(';');
            return new Artikel
            {
                Artikelnummer = teile[0],
                Bezeichnung = teile[1],
                Menge = int.Parse(teile[2]),
                Preis = decimal.Parse(teile[3])
            };
        }
    }
}
