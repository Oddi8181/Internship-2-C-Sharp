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
        static List<Dictionary<string, object>> travels = new List<Dictionary<string, object>>();

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
                var userToEdit = users.Find(u => (long)u["ID"] == id);
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


                    userToEdit["Name"] = newName;
                    userToEdit["Surname"] = newSurname;
                    userToEdit["DateOfBirth"] = birthDate;

                    Console.WriteLine("Korisnik uspješno ažuriran!");

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
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Odabir: {0}", input);
                }
                else
                {
                    Console.WriteLine("Neispravan unos");
                    return;
                }
                switch (input)
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



        
        

        public static void AddTravel()
        {

            var travel = new Dictionary<string, object>();

            Console.WriteLine("UNOS NOVOG PUTOVANJA");

            Console.WriteLine("Datum putovanja: ");
            var dateInput = Console.ReadLine();

            Console.WriteLine("Kilometraža: ");
            var mileageInput = Console.ReadLine();

            Console.WriteLine("Količina potrošenog goriva (u litrama): ");
            var fuelQuantityInput = Console.ReadLine();

            Console.WriteLine("Cijena goriva po litri: ");
            var fuelPriceInput = Console.ReadLine();



            DateTime travelDate;
            if (!DateTime.TryParse(dateInput, out travelDate))
            {
                Console.WriteLine("Neispravan datum!");
                return;
            }

            double mileage;
            if (!double.TryParse(mileageInput, out mileage) || mileage < 0)
            {
                Console.WriteLine("Neispravna kilometraža!");
                return;
            }

            double fuelQuantity;
            if (!double.TryParse(fuelQuantityInput, out fuelQuantity) || fuelQuantity < 0)
            {
                Console.WriteLine("Neispravna količina goriva!");
                return;
            }

            double fuelPrice;
            if (!double.TryParse(fuelPriceInput, out fuelPrice) || fuelPrice < 0)
            {
                Console.WriteLine("Neispravna cijena goriva!");
                return;
            }

            double totalFuelCost = fuelQuantity * fuelPrice;
            Console.WriteLine("Ukupni trošak goriva: " + totalFuelCost);



            long id = travels.Count > 0 ? (long)travels[^1]["ID"] + 1 : 1;

            travel["ID"] = id;
            travel["Date"] = travelDate;
            travel["Mileage"] = mileage;
            travel["FuelQuantity"] = fuelQuantity;
            travel["FuelPrice"] = fuelPrice;
            travel["TotalFuelCost"] = totalFuelCost;

            travels.Add(travel);
            Console.WriteLine("Putovanje uspješno dodano!");


        }
        public static void DeleteTravel()
        {
            Console.WriteLine("a) Brisanje po ID-u");
            Console.WriteLine("b) Brisanje svih putovanja skupljih od unesenog iznosa");
            Console.WriteLine("c) Brisanje svih putovanja jeftinijih od unesenog iznosa");
            var input = Console.ReadLine();

            switch (input)
            {
                case "a:":
                    Console.WriteLine("Unesi id putovanja koje želiš obrisati: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var travelToRemove = travels.Find(t => (long)t["ID"] == id);

                        if (travelToRemove != null)
                        {
                            travels.Remove(travelToRemove);
                            Console.WriteLine("Putovanje uspješno obrisano!");
                        }
                        else
                        {
                            Console.WriteLine("Putovanje s danim id-em nije pronađeno!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neispravan unos id-a!");
                    }
                    break;
                case "b":
                    Console.WriteLine("Unesi iznos: ");
                    if (double.TryParse(Console.ReadLine(), out double highestAmount))
                    {
                        travels.RemoveAll(t => (double)t["TotalFuelCost"] > highestAmount);
                        Console.WriteLine("Putovanja uspješno obrisana!");
                    }
                    else
                    {
                        Console.WriteLine("Neispravan unos iznosa!");
                    }
                    break;


                case "c":
                    Console.WriteLine("Unesi iznos: ");
                    if (double.TryParse(Console.ReadLine(), out double lowestAmount))
                    {
                        travels.RemoveAll(t => (double)t["TotalFuelCOst"] < lowestAmount);
                        Console.WriteLine("Putovanja uspješno obrisana!");
                    }
                    else
                    {
                        Console.WriteLine("Neispravan unos iznosa!");
                    }
                    break;
            }

        }
        public static void EditTravel()
        {
            Console.WriteLine("Unesi ID putovanja koje želiš urediti: ");
            if (long.TryParse(Console.ReadLine(), out long id))
            {
                var travelToEdit = travels.Find(t => (long)t["ID"] == id);
                if (travelToEdit != null)
                {
                    Console.WriteLine("Unesi novi datum putovanja: ");
                    var newDateInput = Console.ReadLine();
                    DateTime newDate;
                    if (string.IsNullOrEmpty(newDateInput))
                    {
                        newDate = (DateTime)travelToEdit["Date"];
                    }
                    else if (!DateTime.TryParse(newDateInput, out newDate))
                    {
                        Console.WriteLine("Neispravan datum!");
                        return;
                    }


                    Console.WriteLine("Unesi novu kilometražu: ");
                    var newMileageInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(newMileageInput))
                    {
                        newMileageInput = travelToEdit["Mileage"].ToString();
                    }

                    Console.WriteLine("Unesi novu količinu potrošenog goriva (u litrama): ");
                    var fuelQuantityInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(fuelQuantityInput))
                    {
                        fuelQuantityInput = travelToEdit["FuelQuantity"].ToString();
                    }
                    Console.WriteLine("Unesi novu cijenu goriva po litri: ");
                    var fuelPriceInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(fuelPriceInput))
                    {
                        fuelPriceInput = travelToEdit["FuelPrice"].ToString();
                    }


                    travelToEdit["Date"] = newDate;
                    travelToEdit["Mileage"] = double.Parse(newMileageInput);
                    travelToEdit["FuelQuantity"] = double.Parse(fuelQuantityInput);
                    travelToEdit["FuelPrice"] = double.Parse(fuelPriceInput);
                    travelToEdit["TotalFuelCost"] = (double)travelToEdit["FuelQuantity"] * (double)travelToEdit["FuelPrice"];
                    Console.WriteLine("Putovanje uspješno uređeno!");


                }
            }
        }
        public static void SortedAscendingByTotalFuelCost()
        {
            var sortedTravels = travels.OrderBy(t => (double)t["TotalFuelCost"]).ToList();
            Console.WriteLine("Putovanja sortirana uzlazno po ukupnom trošku goriva:");
            foreach (var travel in sortedTravels)
            {
                Console.WriteLine("Putovanje #{0} - Ukupni trošak goriva: {1}", travel["ID"], travel["TotalFuelCost"]);
            }
        }
        public static void SortedDescendingByTotalFuelCost()
        {
            var sortedTravels = travels.OrderByDescending(t => (double)t["TotalFuelCost"]).ToList();
            Console.WriteLine("Putovanja sortirana silazno po ukupnom trošku goriva:");
            foreach (var travel in sortedTravels)
            {
                Console.WriteLine("Putovanje #{0} - Ukupni trošak goriva: {1}", travel["ID"], travel["TotalFuelCost"]);
            }
        }
        public static void SortedAscendingByDate()
        {
            var sortedTravels = travels.OrderBy(t => (DateTime)t["Date"]).ToList();
            Console.WriteLine("Putovanja sortirana uzlazno po datumu:");
            foreach (var travel in sortedTravels)
            {
                Console.WriteLine("Putovanje #{0} - Datum: {1}", travel["ID"], travel["Date"]);
            }
        }
        public static void SortedDescendingByDate()
        {
            var sortedTravels = travels.OrderByDescending(t => (DateTime)t["Date"]).ToList();
            Console.WriteLine("Putovanja sortirana silazno po datumu:");
            foreach (var travel in sortedTravels)
            {
                Console.WriteLine("Putovanje #{0} - Datum: {1}", travel["ID"], travel["Date"]);
            }
        }
        public static void SortedAscendingByMileage()
        {
            var sortedTravels = travels.OrderBy(t => (double)t["Mileage"]).ToList();
            Console.WriteLine("Putovanja sortirana uzlazno po kilometraži:");
            foreach (var travel in sortedTravels)
            {
                Console.WriteLine("Putovanje #{0} - Kilometraža: {1}", travel["ID"], travel["Mileage"]);
            }
        }
        public static void SortedDescendingByMileage()
        {
            var sortedTravels = travels.OrderByDescending(t => (double)t["Mileage"]).ToList();
            Console.WriteLine("Putovanja sortirana silazno po kilometraži:");
            foreach (var travel in sortedTravels)
            {
                Console.WriteLine("Putovanje #{0} - Kilometraža: {1}", travel["ID"], travel["Mileage"]);
            }
        }
        public static void ShowAllTravels()
        {
            Console.WriteLine("Popis svih putovanja");
            foreach (var travel in travels)
            {
                Console.WriteLine("Putovanje #{0}" +
                    "Datum: {1}" +
                    "Kilometraža: {2}" +
                    "Količina goriva: {3}" +
                    "Cijena goriva po litri: {4}" +
                    "Ukupna cijena goriva: {5}", travel["ID"], travel["Date"], travel["Milage"], travel["UelQuantity"], travel["FuelPrice"], travel["TotalFuelCost"]);
            }

        }
        public static void ShowTravelsMenu()
        {
            Console.WriteLine("PREGLED SVIH PUTOVANJA");
            Console.WriteLine("Odaberi opciju: ");
            Console.WriteLine("a) Sva putovanja sortirana po unosu");
            Console.WriteLine("b) Sva putovanja sortirana po trošku uzlazno");
            Console.WriteLine("c) Sva putovanja sortirana po trošku silazno");
            Console.WriteLine("d) Sva putovanja sortirana po kilometraži uzlazno");
            Console.WriteLine("e) Sva putovanja sortirana po kilometraži silazno");
            Console.WriteLine("f) Sva putovanja sortirana po datumu uzlazno");
            Console.WriteLine("g) Sva putovanja sortirana po datumu silazno");

            var input = Console.ReadLine();
            switch (input)
            {
                case "a":
                    ShowAllTravels();
                    break;
                case "b":
                    SortedAscendingByTotalFuelCost();
                    break;
                case "c":
                    SortedDescendingByTotalFuelCost();
                    break;
                case "d":
                    SortedAscendingByMileage();
                    break;
                case "e":
                    SortedDescendingByMileage();
                    break;
                case "f":
                    SortedAscendingByDate();
                    break;
                case "g":
                    SortedDescendingByDate();
                    break;
            }
        }

        //ReportsAndAnalysis();




        public static void TravelMenu()
        {
            Console.WriteLine("MENI PUTOVANJA");
            Console.WriteLine("1 - Unos novog putovanja");
            Console.WriteLine("2 - Brisanje putovanja");
            Console.WriteLine("3 - Uređivanje postojećeg putovanja");
            Console.WriteLine("4 - Pregled svih putovanja");
            Console.WriteLine("5 - Izvještaji i analize");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                Console.WriteLine("Odabir");
            }
            switch (input)
            {
                case 1:
                    AddTravel();
                    break;
                case 2:
                    DeleteTravel();
                    break;
                case 3:
                    EditTravel();
                    break;
                case 4:
                    ShowTravelsMenu();
                    break;
                case 5:
                    break;
                case 0:
                    exit = true;
                    break;

            }
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
