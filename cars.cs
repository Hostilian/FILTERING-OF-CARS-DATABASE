using System;

namespace CarDatabase
{
    public class Car
    {
        public string Owner { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"{Owner}; {Model}; {Manufacturer}; {Year}; {Color}";
        }
    }
}
