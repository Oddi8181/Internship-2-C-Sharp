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
            else if (birthDate > DateTime.Now)
            {
                Console.WriteLine("Datum je neispravan!");
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
                    else if (birthDate > DateTime.Now)
                    {
                        Console.WriteLine("Datum je neispravan!");
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
                        break;
                }
            } while (!exit);
        }
        public static void IntiUsers()
        {
            users.Add(new Dictionary<string, object>
            {
                {"ID", 1L },
                {"Name", "Ivan" },
                {"Surname", "Ivić" },
                {"DateOfBirth", new DateTime(1995, 5, 15) },
                {"trips", new List<Dictionary<string, object>>() }
            });
            users.Add(new Dictionary<string, object>
            {
                {"ID", 2L },
                {"Name", "Ana" },
                {"Surname", "Anić" },
                {"DateOfBirth", new DateTime(1992, 2, 22) },
                {"trips", new List<Dictionary<string, object>>() }
            });
            users.Add(new Dictionary<string, object>
            {
                {"ID", 3L },
                {"Name", "Marko" },
                {"Surname", "Markić" },
                {"DateOfBirth", new DateTime(1988, 8, 8) },
                {"trips", new List<Dictionary<string, object>>() }
            });
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
            else if (travelDate > DateTime.Now)
            {
                Console.WriteLine("Datum je neispravan!");
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
                    else if(newDate > DateTime.Now)
                    {
                        Console.WriteLine("Datum je neispravan!");
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
                case "0":
                    break;
            }
        }
        public static void TotalFuelConsumption()
        {
            double totalConsumption = travels.Sum(t => (double)t["FuelQuantity"]);
            Console.WriteLine("Ukupna potrošnja goriva: {0} litara", totalConsumption);
        }
        public static void TotalFuelCost()
        {
            double totalCost = travels.Sum(t => (double)t["TotalFuelCost"]);
            Console.WriteLine("Ukupni trošak goriva: {0}", totalCost);
        }
        public static void AverageFuelConsumption()
        {
            double totalMileage = travels.Sum(t => (double)t["Mileage"]);
            double totalFuel = travels.Sum(t => (double)t["FuelQuantity"]);
            double averageConsumption = (totalFuel / totalMileage) * 100;
            Console.WriteLine("Prosječna potrošnja goriva: {0} L/100km", averageConsumption);
        }
        public static void TravelWithHighestFuelConsumption()
        {
            var travel = travels.OrderByDescending(t => (double)t["FuelQuantity"] / (double)t["Mileage"]).FirstOrDefault();
            if (travel != null)
            {
                double consumption = ((double)travel["FuelQuantity"] / (double)travel["Mileage"]) * 100;
                Console.WriteLine("Putovanje s najvećom potrošnjom goriva je putovanje #{0} s potrošnjom od {1} L/100km", travel["ID"], consumption);
            }
            else
            {
                Console.WriteLine("Nema unesenih putovanja.");
            }
        }
        public static void TravelsByDate()
        {
            Console.WriteLine("Unesite datum za pregled putovanja: ");
            var dateInput = Console.ReadLine();
            DateTime date;
            if (!DateTime.TryParse(dateInput, out date))
            {
                Console.WriteLine("Neispravan datum!");
                return;
            }
            var travelsOnDate = travels.Where(t => ((DateTime)t["Date"]).Date == date.Date).ToList();
            if (travelsOnDate.Count > 0)
            {
                Console.WriteLine("Putovanja na datum {0}:", date.ToString("dd.MM.yyyy"));
                foreach (var travel in travelsOnDate)
                {
                    Console.WriteLine("Putovanje #{0} - Kilometraža: {1}, Količina goriva: {2}, Cijena goriva po litri: {3}, Ukupna cijena goriva: {4}", travel["ID"], travel["Mileage"], travel["FuelQuantity"], travel["FuelPrice"], travel["TotalFuelCost"]);
                }
            }
            else
            {
                Console.WriteLine("Nema putovanja na uneseni datum.");
            }
        }
        public static void ReportsAndAnalysis()
        {
            Console.WriteLine("IZVJEŠTAJI I ANALIZE");
            Console.WriteLine("a) Ukupna potrošnja goriva(zbroj svih litara)");
            Console.WriteLine("b) Ukupni trošak goriva(zbroj svih goriva*cijena)");
            Console.WriteLine("c) Prosječna potrošnja goriva u L/100km");
            Console.WriteLine("d) Putovanje s njavećom potrošnjom goriva");
            Console.WriteLine("e) Pregled putovanja po određenom datumu");

            var input = Console.ReadLine();
            switch (input)
            {
                case "a":
                    TotalFuelConsumption();
                    break;
                case "b":
                    TotalFuelCost();
                    break;
                case "c":
                    AverageFuelConsumption();
                    break;
                case "d":
                    TravelWithHighestFuelConsumption();
                    break;
                case "e":
                    TravelsByDate();
                    break;
                case "0":
                    break;
            }
        }
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
                    ReportsAndAnalysis();
                    break;
                case 0:
                    break;

            }
        }
        public static void InitTravels()
        {
            travels.Add(new Dictionary<string, object>
            {
                {"ID", 1 },
                {"Date", new DateTime(2023, 1, 15) },
                {"Mileage", 150.0 },
                {"FuelQuantity", 10.0 },
                {"FuelPrice", 1.5 },
                {"TotalFuelCost", 15.0 }
            });
            travels.Add(new Dictionary<string, object>
            {
                {"ID", 2 },
                {"Date", new DateTime(2023, 2, 20) },
                {"Mileage", 200.0 },
                {"FuelQuantity", 12.0 },
                {"FuelPrice", 1.6 },
                {"TotalFuelCost", 19.2 }
            });
            travels.Add(new Dictionary<string, object>
            {
                {"ID", 3 },
                {"Date", new DateTime(2023, 3, 10) },
                {"Mileage", 300.0 },
                {"FuelQuantity", 20.0 },
                {"FuelPrice", 1.4 },
                {"TotalFuelCost", 28.0 }
            });
            travels.Add(new Dictionary<string, object>
            {
                {"ID", 4 },
                {"Date", new DateTime(2023, 4, 5) },
                {"Mileage", 250.0 },
                {"FuelQuantity", 15.0 },
                {"FuelPrice", 1.55 },
                {"TotalFuelCost", 23.25 }
            });
            travels.Add(new Dictionary<string, object>
            {
                {"ID", 5 },
                {"Date", new DateTime(2023, 5, 18) },
                {"Mileage", 180.0 },
                {"FuelQuantity", 11.0 },
                {"FuelPrice", 1.45 },
                {"TotalFuelCost", 15.95 }
            });
        }

        static void Main(string[] args)
        {
            InitTravels();
            IntiUsers();

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
