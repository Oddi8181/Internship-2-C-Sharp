using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace FuelUsage
{
    internal class Program
    {
        static bool exit = false;
        static List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();

        public static void AddUser()
        {
            var user = new Dictionary<string, object>();

            Console.WriteLine("Unesite ime korisnika:");
            var name = Console.ReadLine();

            Console.WriteLine("Unesite prezime korisnika");
            var surname = Console.ReadLine();


            Console.WriteLine("Unesite datum rođenja korisnika: ");
            var dateOfBirth = Console.ReadLine();

            DateTime birthDate;
            if (!DateTime.TryParse(dateOfBirth, out birthDate))
            {
                Console.WriteLine("Neispravan datum!");
                return;
            }



            long id = users.Count > 0 ? (long)users[^1]["ID"] + 1 : 1;


            user["ID"] = id;
            user["Name"] = name;
            user["Surname"] = surname;
            user["DateOfBirth"] = birthDate;
            user["trips"] = new List<Dictionary<string, object>>();

            users.Add(user);
            Console.WriteLine("Korisnik uspješno dodan!");


        }
        public static void DeleteUser()
        {
            Console.WriteLine("a) Brisanje po id-u");
            Console.WriteLine("b) Brisanje po imenu i prezimenu");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "a":
                    Console.WriteLine("Unesite ID korisnika kojeg želite obrisati:");
                    if (long.TryParse(Console.ReadLine(), out long id))
                    {
                        var userToRemove = users.Find(u => (long)u["ID"] == id);
                        if (userToRemove != null)
                        {
                            users.Remove(userToRemove);
                            Console.WriteLine("Korisnik uspješno obrisan!");
                        }
                        else
                        {
                            Console.WriteLine("Korisnik s danim ID-em nije pronađen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neispravan unos ID-a.");
                    }
                    break;

                case "b":
                    Console.WriteLine("Unesite ime i prezime korisnika kojeg želite obrisati: ");
                    var userToDelete = Console.ReadLine();
                    var user = users.Find(u => u["Name"].ToString() + " " + u["Surname"].ToString() == userToDelete);
                    if (user != null)
                    {
                        users.Remove(user);
                        Console.WriteLine("Korisnik uspješno obrisan!");
                    }
                    else
                    {
                        Console.WriteLine("Korisnik s danim imenom i prezimenom nije pronađen.");
                    }
                    break;
            }
            

        }
        public static void EditUser()
        {
            Console.WriteLine("Unesite ID korisnika kojeg želite urediti:");
            if (long.TryParse(Console.ReadLine(), out long id))
            {
                var userToEdit = users.Find(u => (long)u["id"] == id);
                if (userToEdit != null)
                {
                    Console.WriteLine("Unesi novo ime: ");
                    var newName = Console.ReadLine();
                    if (string.IsNullOrEmpty(newName))
                    {
                        newName = userToEdit["Name"].ToString();
                    }
                    Console.WriteLine("Unesi novo prezime: ");
                    var newSurname = Console.ReadLine();
                    if (string.IsNullOrEmpty(newSurname))
                    {
                        newSurname = userToEdit["Surname"].ToString();
                    }
                    Console.WriteLine("Unesi novi datum rođenja (dd.MM.yyyy): ");
                    var newDateOfBirth = Console.ReadLine();
                    DateTime birthDate;
                    if (string.IsNullOrEmpty(newDateOfBirth))
                    {
                        birthDate = (DateTime)userToEdit["DateOfBirth"];
                    }
                    else if (!DateTime.TryParse(newDateOfBirth, out birthDate))
                    {
                        Console.WriteLine("Neispravan datum!");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Korisnik s danim ID-em nije pronađen.");
                }
            }
            else
            {
                Console.WriteLine("Neispravan unos ID-a.");
            }
        }



        public static void ShowUsers()
        {
            Console.WriteLine("Popis svih korisnika: ");
            
            foreach (var user in users)
            {
                Console.WriteLine("ID: {0}, Ime: {1}, Prezime: {2}, Datum rođenja: {3}", user["ID"], user["Name"], user["Surname"], ((DateTime)user["DateOfBirth"]).ToString("dd.MM.yyyy"));
            }
        }


        public static void UserMenu()
        {
            do
            {
                Console.WriteLine("MENI KORISNIKA");
                Console.WriteLine("1 - Unos novog korisnika");
                Console.WriteLine("2 - Brisanje korisnika");
                Console.WriteLine("3 - Uređivanje korisnika");
                Console.WriteLine("4 - Pregled svih korisnika");
                Console.WriteLine("0 - Povratak na glavni izbornik");
                if (int.TryParse(Console.ReadLine(), out int n))
                {
                    Console.WriteLine("Odabir: {0}", n);
                }
                else
                {
                    Console.WriteLine("Neispravan unos");
                    return;
                }
                switch (n)
                {
                    case 1:
                         AddUser();
                        break;
                    case 2:
                        DeleteUser();
                        break;
                    case 3:
                        EditUser();
                        break;
                    case 4:
                        ShowUsers();
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            } while (!exit);
        }

        public static void TravelMenu()
        {
        }


        

        static void Main(string[] args)
        {
            
            do
            {
                Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");
                Console.WriteLine("1. Korisnici");
                Console.WriteLine("2. Putovanja");
                Console.WriteLine("0. Izlaz iz aplikacije");

                if (int.TryParse(Console.ReadLine(), out int n))
                {
                    Console.WriteLine("Odabir: {0}", n);
                }
                else
                {
                    Console.WriteLine("Neispravan unos");
                    return;
                }

                switch (n)
                {
                    case 1:
                        UserMenu();
                        break;
                    case 2:
                        TravelMenu();
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
            while (!exit);
        }
    }
            
}
