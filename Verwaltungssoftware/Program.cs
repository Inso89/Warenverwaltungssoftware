using System;
using System.Collections.Generic;
using System.Text;

namespace Warenverwaltung
{
    class Program
    {
        // Benutzerliste (Username, Passwort)
        static Dictionary<string, string> benutzerListe = new Dictionary<string, string>
        {
            { "Admin", "admin123" },
            { "Hans", "hans123" },
            { "Harald", "harald123" },
            { "Luise", "luise123" }
        };

        static void Main(string[] args)
        {
           
            Console.Title = "Warenverwaltung - Login";
            bool loginErfolgreich = false;

            Console.WriteLine("Willkommen zur Warenverwaltungssoftware");
            Console.WriteLine("---------------------------------------");

            while (!loginErfolgreich)
            {
                Console.Write("Benutzername: ");
                string benutzername = Console.ReadLine();

                Console.Write("Passwort: ");
                string passwort = LesePasswort();

                if (benutzerListe.ContainsKey(benutzername) && benutzerListe[benutzername] == passwort)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nLogin erfolgreich! Willkommen, {0}.\n", benutzername);
                    Console.ResetColor();
                    loginErfolgreich = true;

                    // Weiter mit Hauptmenü
                    Hauptmenü(benutzername);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLogin fehlgeschlagen. Bitte versuchen Sie es erneut.\n");
                    Console.ResetColor();
                }
            }
        }

        // Passwort verdeckt eingeben
        static string LesePasswort()
        {
            string passwort = string.Empty;
            ConsoleKeyInfo taste;

            do
            {
                taste = Console.ReadKey(true);
                if (taste.Key != ConsoleKey.Backspace && taste.Key != ConsoleKey.Enter)
                {
                    passwort += taste.KeyChar;
                    Console.Write("*");
                }
                else if (taste.Key == ConsoleKey.Backspace && passwort.Length > 0)
                {
                    passwort = passwort[0..^1];
                    Console.Write("\b \b");
                }
            } while (taste.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return passwort;
        }

        // Hauptmenü
        static void Hauptmenü(string benutzername)
        {
            while (true)
            {
                Console.WriteLine("\n--- Hauptmenü ---");
                Console.WriteLine("1. Warenerfassung (CSV)");
                Console.WriteLine("2. Warenbestand anzeigen");
                Console.WriteLine("3. Artikel bestellen (Bestand erhöhen)");
                Console.WriteLine("4. Programm beenden");
                Console.Write("Auswahl: ");
                string eingabe = Console.ReadLine();

                switch (eingabe)
                {
                    case "1":
                        LagerCsv.ArtikelErfassen();
                        break;
                    case "2":
                        LagerCsv.AlleArtikelAnzeigen();
                        break;
                    case "3":
                        LagerCsv.ArtikelBestellen();
                        break;
                    case "4":
                        Console.WriteLine("Programm wird beendet...");
                        return;
                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        break;
                }
            }
        }
    }
}
