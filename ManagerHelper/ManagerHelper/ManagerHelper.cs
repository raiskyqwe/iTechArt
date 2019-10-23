﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ManagerHelper
{
    public class Managerhelper
    {
        public static void GetAvailableCars(List<Car> cars)
        {
            string path = @"..\..\..\Resources\CarsAvailable.txt";

            using (StreamReader _textStreamReader = new StreamReader(path))
            {
                string line;

                while ((line = _textStreamReader.ReadLine()) != null)
                {
                    string[] words = line.Split(new char[] { ' ' });//divide each line into words
                    double engineSize = Convert.ToDouble(words[1]);

                    if (words[0] == typeof(LandCruiser).Name)
                    {
                        switch (words[3])
                        {
                            case "Manual":
                                cars.Add(new LandCruiser(engineSize, words[2], 1));
                                break;
                            case "Automatic":
                                cars.Add(new LandCruiser(engineSize, words[2], 2));
                                break;
                            case "CVT":
                                cars.Add(new LandCruiser(engineSize, words[2], 3));
                                break;
                        }
                    }
                    if (words[0] == typeof(Camry).Name)
                    {
                        switch (words[3])
                        {
                            case "Manual":
                                cars.Add(new Camry(engineSize, words[2], 1));
                                break;
                            case "Automatic":
                                cars.Add(new Camry(engineSize, words[2], 2));
                                break;
                            case "CVT":
                                cars.Add(new Camry(engineSize, words[2], 3));
                                break;
                        }
                    }
                    if (words[0] == typeof(Corolla).Name)
                    {
                        switch (words[3])
                        {
                            case "Manual":
                                cars.Add(new Corolla(engineSize, words[2], 1));
                                break;
                            case "Automatic":
                                cars.Add(new Corolla(engineSize, words[2], 2));
                                break;
                            case "CVT":
                                cars.Add(new Corolla(engineSize, words[2], 3));
                                break;
                        }
                    }
                }
            }
        }
        public static void CreateSelectedModel()
        {
            Console.WriteLine("Enter model: 1 - LandCruiser, 2 - Camry, 3 - Corolla");
            string selectedModel = Console.ReadLine();

            if(selectedModel == "1" || selectedModel == "2" || selectedModel == "3")
            {
                Console.WriteLine("Input engine size: 1 - 1.8, 2 - 2.0, 3 - 3.0");
                string selectedEngineSize = Console.ReadLine();

                if (IsEngineSizeValid(selectedEngineSize)) 
                {
                    Console.WriteLine("Input color: 1-Green, 2-Black, 3-Red, 4-Blue");
                    string selectedColor = Console.ReadLine();

                    if (IsColorValid(selectedColor))
                    {
                        Console.WriteLine("Input transmission: 1-Manual, 2-Automatic, 3-CVT");
                        string selectedTransmission = Console.ReadLine();
                        if (IsTransmissionValid(selectedTransmission))
                        {
                            Car createdSelectedModel = new Car();

                            switch (selectedModel)
                            {
                                case "1":
                                    createdSelectedModel = new LandCruiser();

                                    SetConfigurations(createdSelectedModel, selectedEngineSize, selectedColor, selectedTransmission);
                                    break;
                                case "2":
                                    createdSelectedModel = new Camry();

                                    SetConfigurations(createdSelectedModel, selectedEngineSize, selectedColor, selectedTransmission);
                                    break;
                                case "3":
                                    createdSelectedModel = new Corolla();

                                    SetConfigurations(createdSelectedModel, selectedEngineSize, selectedColor, selectedTransmission);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Selected transmission is incorrect, try to input transmission: 1-Manual, 2-Automatic, 3-CVT");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Selected color is incorrect, try to input color: 1-Green, 2-Black, 3-Red, 4-Blue");
                    }
                }
                else
                {
                    Console.WriteLine("Selected engine size is incorrect, try to input engine size: 1 - 1.8, 2 - 2.0, 3 - 3.0");
                }
            }
            else
            {
                Console.WriteLine("The entered value is incorrect, try to input model: 1 - LandCruiser, 2 - Camry, 3 - Corolla");
            }
            
        }
        public static bool IsEngineSizeValid(string selectedEngineSize)
        {
            if (selectedEngineSize == "1" || selectedEngineSize == "2" || selectedEngineSize == "3")
                return true;
            return false;
        }
        public static bool IsColorValid(string selectedColor)
        {
            if (selectedColor == "1" || selectedColor == "2" || selectedColor == "3" || selectedColor == "4")
                return true;
            return false;
        }
        public static bool IsTransmissionValid(string selectedTransmission)
        {
            if (selectedTransmission == "1" || selectedTransmission == "2" || selectedTransmission == "3")
                return true;
            return false;
        }
        public static void SetConfigurations(Car car, string selectedEngineSize, string selectedColor, string selectedTransmission)
        {
            SetEngineSize(car, selectedEngineSize);

            SetSelectedColor(car, selectedColor);

            SetSelectedTransmission(car, selectedTransmission);

            DisplaySelectedConfiguration(car);
        }

        public static Car SetEngineSize(Car car, string selectedEngineSize)
        {
            switch (selectedEngineSize)
            {
                case "1":
                    car.EngineSize = 1.8;
                    return car;
                case "2":
                    car.EngineSize = 2.0;
                    return car;
                case "3":
                    car.EngineSize = 3.0;
                    return car;
            }
            return null;
        }

        public static Car SetSelectedColor(Car car, string selectedColor)
        {
            switch (selectedColor)
            {
                case "1":
                    car.Color = "Green";
                    return car;
                case "2":
                    car.Color = "Black";
                    return car;
                case "3":
                    car.Color = "Red";
                    return car;
                case "4":
                    car.Color = "Blue";
                    return car;
            }
            return null;
        }

        public static Car SetSelectedTransmission(Car car, string selectedTransmission)
        {

            switch (selectedTransmission)
            {
                case "1":
                    car.SelectedTransmission = 1;
                    return car;
                case "2":
                    car.SelectedTransmission = 2;
                    return car;
                case "3":
                    car.SelectedTransmission = 3;
                    return car;
            }
            return null;
        }

        public static void DisplaySelectedConfiguration(Car car)
        {
            Console.WriteLine(car.CarInformation());
        }

        public static void SortCarCost(List<Car> cars)
        {//a - first car, b - next car
            cars.Sort((a, b) => a.Cost.CompareTo(b.Cost));
            DisplayCarsInformation(cars);
        }

        public static void GetCarsInPriceRange(List<Car> cars)
        {// написать тесты для интов, только инты могут передаваться; а < b иначе не выведет; больше 0
            List<Car> listCarsInPriceRange = new List<Car>();

            Console.WriteLine("Enter range of the price for the car: \n");
            Console.WriteLine("Starting price at ");

            string startingPrice = Console.ReadLine();

            Console.WriteLine("to ");

            string endingPrice = Console.ReadLine();

            foreach (Car car in cars)
            {
                if (car.Cost >= Convert.ToInt32(startingPrice) && car.Cost <= Convert.ToInt32(endingPrice))
                {
                    listCarsInPriceRange.Add(car);
                }
            }

            if(listCarsInPriceRange.Count == 0)
            {
                Console.WriteLine("In this range there are no available cars");
            }

            SortCarCost(listCarsInPriceRange);
        }
        public static void DisplayCarsInformation(List<Car> cars)
        {
            foreach (Car c in cars)
            {
                Console.WriteLine(c.CarInformation());
            }
        }

        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();
            bool isQuit = false;

            GetAvailableCars(cars);

            do
            {
                Console.Clear();

                Console.WriteLine("1 - to view information about cars\n" +
                "2 - to calculate the cost of the car depending on the selected configuration\n" +
                "3 - to sort cars by price\n" +
                "4 - to find a complete set that corresponds to a given price range\n" +
                "5 - quit\n");
                string selection = Console.ReadLine();

                switch (selection)
                {
                    #region case "1"
                    case "1":
                        Console.Clear();

                        DisplayCarsInformation(cars);

                        Console.WriteLine("\n\n\nFor return to menu press any key");
                        Console.ReadKey();
                        break;
                    #endregion
                    #region case "2"
                    case "2":
                        Console.Clear();

                        CreateSelectedModel();

                        Console.WriteLine("\n\n\nFor return to menu press any key");
                        Console.ReadKey();
                        break;
                    #endregion
                    #region case "3"
                    case "3":
                        Console.Clear();

                        SortCarCost(cars);

                        Console.WriteLine("\n\n\nFor return to menu press any key");
                        Console.ReadKey();
                        break;
                    #endregion
                    #region case "4"
                    case "4":
                        Console.Clear();

                        try
                        {
                            GetCarsInPriceRange(cars);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("The entered values are incorrect");
                        }

                        Console.WriteLine("\n\n\nFor return to menu press any key");
                        Console.ReadKey();
                        break;
                    #endregion
                    case "5":
                        isQuit = true;
                        break;
                }
            } while (!isQuit);
        }
    }
}


