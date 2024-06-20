using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDatabase
{
    public class CarDatabase
    {
        private List<Car> carList;
        private string filePath;

        public CarDatabase(string filePath)
        {
            this.filePath = filePath;
            carList = new List<Car>();
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var data = line.Split(';');
                    carList.Add(new Car
                    {
                        Owner = data[0].Trim(),
                        Model = data[1].Trim(),
                        Manufacturer = data[2].Trim(),
                        Year = int.Parse(data[3].Trim()),
                        Color = data[4].Trim()
                    });
                }
            }
        }

        public void SaveData()
        {
            var lines = carList.Select(car => car.ToString());
            File.WriteAllLines(filePath, lines);
        }

        public void DisplayDatabase()
        {
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }
        }

        public Car SearchByOwner(string owner)
        {
            return carList.FirstOrDefault(c => c.Owner.Equals(owner, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Car> FilterByColor(string color)
        {
            return carList.Where(c => c.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Car> FilterByManufacturer(string manufacturer)
        {
            return carList.Where(c => c.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Car> FilterByAge(int maxAge)
        {
            var currentYear = DateTime.Now.Year;
            return carList.Where(c => currentYear - c.Year <= maxAge);
        }

        public void AddCar(Car car)
        {
            carList.Add(car);
        }

        public void DeleteCar(string owner)
        {
            var car = carList.FirstOrDefault(c => c.Owner.Equals(owner, StringComparison.OrdinalIgnoreCase));
            if (car != null)
            {
                carList.Remove(car);
            }
        }

        public (string mostPopularModel, double averageAge, string mostPopularColor) CalculateStatistics()
        {
            var mostPopularModel = carList.GroupBy(c => c.Model)
                                          .OrderByDescending(g => g.Count())
                                          .FirstOrDefault()?.Key;
            var averageAge = carList.Average(c => DateTime.Now.Year - c.Year);
            var mostPopularColor = carList.GroupBy(c => c.Color)
                                          .OrderByDescending(g => g.Count())
                                          .FirstOrDefault()?.Key;

            return (mostPopularModel, averageAge, mostPopularColor);
        }
    }
}
