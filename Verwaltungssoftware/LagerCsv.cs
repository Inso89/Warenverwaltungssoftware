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
        public static void ArtikelBestellen()
        {
            if (!File.Exists(dateipfad))
            {
                Console.WriteLine("Noch keine Artikel vorhanden.");
                return;
            }

            Console.Write("\nArtikelnummer für Bestellung: ");
            string artikelnummer = Console.ReadLine();

            Console.Write("Bestellmenge: ");
            if (!int.TryParse(Console.ReadLine(), out int bestellmenge) || bestellmenge <= 0)
            {
                Console.WriteLine("Ungültige Menge.");
                return;
            }

            var zeilen = File.ReadAllLines(dateipfad);
            var artikelListe = new List<Artikel>();
            bool gefunden = false;

            foreach (var zeile in zeilen)
            {
                var artikel = Artikel.FromCsv(zeile);

                if (artikel.Artikelnummer.Equals(artikelnummer, StringComparison.OrdinalIgnoreCase))
                {
                    artikel.Menge += bestellmenge;
                    gefunden = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Artikel {artikel.Bezeichnung} wurde um {bestellmenge} Stück erhöht.");
                    Console.ResetColor();
                }

                artikelListe.Add(artikel);
            }

            if (!gefunden)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Artikelnummer nicht gefunden.");
                Console.ResetColor();
                return;
            }

            // Datei mit aktualisierten Daten überschreiben
            File.WriteAllLines(dateipfad, artikelListe.Select(a => a.ToCsv()));
        }

    }
} 
