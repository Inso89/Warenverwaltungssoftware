using System;
using System.Collections.Generic;
using System.IO;

namespace Warenverwaltung
{
    public static class LagerCsv
    {
        private static readonly string dateipfad = "lager.csv";

        public static void ArtikelErfassen()
        {
            Console.WriteLine("\n--- Neuer Artikel (CSV) ---");

            Console.Write("Artikelnummer: ");
            string nummer = Console.ReadLine();

            Console.Write("Bezeichnung: ");
            string name = Console.ReadLine();

            Console.Write("Menge: ");
            int menge = int.Parse(Console.ReadLine());

            Console.Write("Preis: ");
            decimal preis = decimal.Parse(Console.ReadLine());

            var artikel = new Artikel
            {
                Artikelnummer = nummer,
                Bezeichnung = name,
                Menge = menge,
                Preis = preis
            };

            File.AppendAllLines(dateipfad, new[] { artikel.ToCsv() });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Artikel gespeichert.\n");
            Console.ResetColor();
        }

        public static void AlleArtikelAnzeigen()
        {
            if (!File.Exists(dateipfad))
            {
                Console.WriteLine("Noch keine Artikel vorhanden.");
                return;
            }

            Console.WriteLine("\n--- Aktueller Warenbestand ---");
            var zeilen = File.ReadAllLines(dateipfad);

            foreach (var zeile in zeilen)
            {
                var artikel = Artikel.FromCsv(zeile);
                Console.WriteLine($"[{artikel.Artikelnummer}] {artikel.Bezeichnung} | Menge: {artikel.Menge} | Preis: {artikel.Preis:C}");
            }
        }
    }
}
