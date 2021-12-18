using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lawrider_car_rental_system
{

    class Vehicle
    {
        private string make;
        private string model;
        private int year;
        private int suitCases;
        private decimal cost;
        private decimal fuelCapacity;
        private bool avilableState;


        public Vehicle(string make, string model, int year, decimal cost, int suitCases, bool state, decimal fuelCapacity)
        {
            this.make = make;
            this.model = model;
            this.year = year;
            this.cost = cost;
            this.suitCases = suitCases;
            this.avilableState = state;
            this.fuelCapacity = fuelCapacity;

        }

        // the get and set code below has been looked up on the microsoft official documentation site
        public string getSetMake
        {
            get => make;
            //set => this.make = value;
        }

        public string getSetModel
        {
            get => model;
            //set => this.model = value;
        }

        public int getSetYear
        {
            get => year;
            //set => this.year = value;
        }

        public int getSetSuitCases
        {
            get => suitCases;
            //set => this.suitCases = value;
        }

        public decimal getSetCost
        {
            get => cost;
            //set => this.cost = value;
        }

        public decimal getSetFuelCapacity
        {
            get => fuelCapacity;
            //set => this.fuelCapacity = value;
        }

        public bool getSetAvilableState
        {
            get => avilableState;
            set
            {
                //converting to lower then back to bool
                string state = value.ToString().ToLower();
                bool stateBool = Convert.ToBoolean(state);
                this.avilableState = stateBool;
            }
        }
    }

    class Program
    {
        const string garageFileName = "files/garage.txt";
        const string garageFilePath = "files/garage.txt";
        static void Main()
        {
            int choice = 0;
            do
            {
                displayMainMenu();
                loadGarageData(garageFileName);
                choice = readInt("Choose an option between {0} and {1}", 2, 1);

            } while (choice != 2);

        }

        static void displayMainMenu()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("                         * MENU *");
            Console.WriteLine("1) Book a vehicle");
            Console.WriteLine("2) Logout/Exit");
            Console.WriteLine("-----------------------------------------------------------");
        }

        static int readInt(string prompt, int high, int low)
        {
            string input = "";
            int num = 0;

            do
            {
                Console.WriteLine(prompt, low, high);
                input = Console.ReadLine();
                try
                {
                    num = int.Parse(input);
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number.");
                }

            } while (input == "" || num > high || num < low);

            return num;
        }

        static int readInt(string prompt)           //overload for  readInt
        {
            string input = "";
            int num = 0;
            bool check = false;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                try
                {
                    num = int.Parse(input);
                    check = true;
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number.");
                }

            } while (input == "" || check == false);

            return num;
        }

        static string readString(string prompt)
        {
            string input = "";
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine("Please enter some text.");
                }

            } while (input == "");

            return input;
        }

        static void bookAVehicleMenu()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("                  * BOOK A VEHICLE MENU *");
            Console.WriteLine("A) Search by Make");
            Console.WriteLine("B) Filter by year");
            Console.WriteLine("C) Filter by Fuel capacity");
            Console.WriteLine("D) Filter by price");
            Console.WriteLine("E) Show all avilable vehicles");
            Console.WriteLine("F) Go back to main menu");
            Console.WriteLine("-----------------------------------------------------------");
        }


        static Dictionary<string, Vehicle> loadGarageData(string file)            //load the data from the database
        {
            file = garageFileName;

            if (!File.Exists(garageFilePath))           //checking if file exist and copying from backup if not
            {
                fileExistCheckAndCreate();
            }

            Dictionary<string, Vehicle> loadMe = new Dictionary<string, Vehicle>();
            StreamReader input = new StreamReader(file);

            while (!input.EndOfStream)
            {
                string line = input.ReadLine();
                if (line != "")
                {

                    string[] valuesArray = line.Split(',');
                    try             //if there is an exception in the loading of the file the execution will continue
                    {
                        loadMe[valuesArray[0]] = new Vehicle(valuesArray[0], valuesArray[1], int.Parse(valuesArray[2]), decimal.Parse(valuesArray[3]), int.Parse(valuesArray[4]), bool.Parse(valuesArray[5]), decimal.Parse(valuesArray[6]));
                    }
                    catch
                    {
                        continue;
                    }
                    
                    Console.WriteLine(loadMe[valuesArray[0]].getSetMake);
                }
            }
            input.Close();
            return loadMe;
        }

        static void fileExistCheckAndCreate()           //method to copy back up database file if its not avilable
        {
            string sourceDirectory = @"back up/" + garageFileName;
            string destinationDirectory = @"files/" + garageFileName;

            {
                Console.WriteLine("Something went wrong. Press any key to continue report the issue and continue.");
                Console.ReadKey();

                File.Copy(sourceDirectory, destinationDirectory);

            }
        }

    }
}
