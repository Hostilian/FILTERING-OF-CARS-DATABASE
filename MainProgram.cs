using System;

namespace CarDatabase
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            string filePath = "cars.csv";
            var carDb = new CarDatabase(filePath);

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        carDb.DisplayDatabase();
                        break;
                    case "2":
                        SearchByOwner(carDb);
                        break;
                    case "3":
                        FilterByParameter(carDb);
                        break;
                    case "4":
                        FilterByAge(carDb);
                        break;
                    case "5":
                        ShowStatistics(carDb);
                        break;
                    case "6":
                        AddCar(carDb);
                        break;
                    case "7":
                        DeleteCar(carDb);
                        break;
                    case "8":
                        carDb.SaveData();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("1. Display the whole database");
            Console.WriteLine("2. Search for a person");
            Console.WriteLine("3. Filter cars by parameters");
            Console.WriteLine("4. Filter cars by age");
            Console.WriteLine("5. Calculate statistics");
            Console.WriteLine("6. Add a car");
            Console.WriteLine("7. Delete a car");
            Console.WriteLine("8. Save and exit");
            Console.Write("Enter your choice: ");
        }

        static void SearchByOwner(CarDatabase carDb)
        {
            Console.Write("Enter owner's name: ");
            string owner = Console.ReadLine();
            var car = carDb.SearchByOwner(owner);
            if (car != null)
            {
                Console.WriteLine(car);
            }
            else
            {
                Console.WriteLine("Owner not found.");
            }
        }

        static void FilterByParameter(CarDatabase carDb)
        {
            Console.WriteLine("Filter by: 1. Color 2. Manufacturer");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter color: ");
                    string color = Console.ReadLine();
                    var carsByColor = carDb.FilterByColor(color);
                    foreach (var car in carsByColor)
                    {
                        Console.WriteLine(car);
                    }
                    break;
                case "2":
                    Console.Write("Enter manufacturer: ");
                    string manufacturer = Console.ReadLine();
                    var carsByManufacturer = carDb.FilterByManufacturer(manufacturer);
                    foreach (var car in carsByManufacturer)
                    {
                        Console.WriteLine(car);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static void FilterByAge(CarDatabase carDb)
        {
            Console.Write("Enter maximum car age in years: ");
            if (int.TryParse(Console.ReadLine(), out int maxAge))
            {
                var carsByAge = carDb.FilterByAge(maxAge);
                foreach (var car in carsByAge)
                {
                    Console.WriteLine(car);
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void ShowStatistics(CarDatabase carDb)
        {
            var (mostPopularModel, averageAge, mostPopularColor) = carDb.CalculateStatistics();
            Console.WriteLine($"Most popular model: {mostPopularModel}");
            Console.WriteLine($"Average age of cars: {averageAge:F2} years");
            Console.WriteLine($"Most popular color: {mostPopularColor}");
        }

        static void AddCar(CarDatabase carDb)
        {
            Console.Write("Enter owner's name: ");
            string owner = Console.ReadLine();
            Console.Write("Enter model: ");
            string model = Console.ReadLine();
            Console.Write("Enter manufacturer: ");
            string manufacturer = Console.ReadLine();
            Console.Write("Enter year: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                Console.Write("Enter color: ");
                string color = Console.ReadLine();
                carDb.AddCar(new Car { Owner = owner, Model = model, Manufacturer = manufacturer, Year = year, Color = color });
            }
            else
            {
                Console.WriteLine("Invalid year.");
            }
        }

        static void DeleteCar(CarDatabase carDb)
        {
            Console.Write("Enter owner's name to delete car: ");
            string owner = Console.ReadLine();
            carDb.DeleteCar(owner);
        }
    }
}
