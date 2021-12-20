﻿using System;
using System.Collections.Generic;
using System.IO;

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
        public string getMake
        {
            get => make;
            //set => this.make = value;
        }

        public string getModel
        {
            get => model;
            //set => this.model = value;
        }

        public int getYear
        {
            get => year;
            //set => this.year = value;
        }

        public int getSuitCases
        {
            get => suitCases;
            //set => this.suitCases = value;
        }

        public decimal getCost
        {
            get => cost;
            //set => this.cost = value;
        }

        public decimal getFuelCapacity
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
        const string garageFileName = "garage.txt";
        const int MaximumVehicleYears = 2012;
        const int MinimumVehicleYears = 1983;
        static void Main()
        {
            int choice = 0;
            do
            {
                Dictionary<string, Vehicle> data = new Dictionary<string, Vehicle>();
                data = loadGarageData(garageFileName);
                displayMainMenu();
                choice = readInt("Choose an option between {0} and {1}", 2, 1);

                switch (choice)
                {
                    case 1:
                        char bookingMenuOption = bookAVehicleMenu();

                        if (bookingMenuOption == 'A')
                        {
                            string make = readString("Enter the prefered vehicle make: ").Trim();
                            string capitalizedMake = char.ToUpper(make[0]) + make.Substring(1);     //capitalization code modified from stackoverflow (Diego 09/11/2010)
                            List<string> keys = GetKeyFromValue(capitalizedMake, data);
                            //if the entered vehicle is not avilable the loop will restart
                            if (!makeFilter(data, keys))
                            {
                                continue;
                            }

                            int vehicleChoice = readInt("Enter your vehicle choice: ") - 1;
                            int days = numOfDays();
                            decimal totalCost = calculateCost(data, keys, vehicleChoice, days);

                            concludeBooking(data, keys, vehicleChoice, totalCost, days);

                        }
                        else if (bookingMenuOption == 'B')
                        {
                            Console.WriteLine("Enter the range of years you want to view from 2012 - 1983");
                            int minYear, maxYear = 0;
                            do
                            {       //fix this maximum and minimum issue when you get your arse back from work
                                maxYear = readInt("Maximum year: ", MaximumVehicleYears, MinimumVehicleYears);
                                minYear = readInt("Minimum year: ", MaximumVehicleYears, MinimumVehicleYears);
                                if (maxYear < minYear)
                                {
                                    Console.WriteLine("Maximum year can not be less than the minimum year. Try reversing your answers.");
                                }

                            } while (maxYear < minYear);
                            List<string> keys = GetKeyFromValue(data, minYear, maxYear);
                            if (keys.Count == 0)
                            {
                                Console.Write("Sorry we do not have vehicles between the desired range, please try again with a different range.\n");
                                continue;
                            }
                            int vehicleChoice = yearFilter(data, keys) - 1;
                            int days = numOfDays();
                            decimal totalCost = calculateCost(data, keys, vehicleChoice, days);

                            concludeBooking(data, keys, vehicleChoice, totalCost, days);
                        }

                        break;
                }

            } while (choice != 2);

        }

        static void concludeBooking(Dictionary<string, Vehicle> data, List<string> keys, int vehicleChoice, decimal totalCost, int days)
        {
            Console.WriteLine("Your total cost for renting {0} {1} will be {2}. Do you wish to continue. (Y/N)", data[keys[vehicleChoice]].getMake, data[keys[vehicleChoice]].getModel, totalCost);
            char answer = ' ';
            //bool check = false;
            do
            {   //prompting user if they want to continue with the booking or not
                answer = readChar("\nDo you wish to continue. (Y/N)");
                if (answer == 'y' || answer == 'Y')
                {
                    data[keys[vehicleChoice]].getSetAvilableState = false;          //setting the avilability of the vehicle to false (booked)

                    printReciept(data, keys, vehicleChoice);
                    Console.WriteLine("Your total cost: {0}\nNumber of Days {1}", totalCost, days);
                    //insert a saveState() function here
                    Console.WriteLine("Press any key to go back to the main menu.");
                    Console.ReadKey();
                    //check = true;
                    break;
                }
                else if (answer == 'n' || answer == 'N')
                {
                    Console.WriteLine("Press any key to go back to the main menu.");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid answer Y or N");
                }
            } while (answer == ' ' || answer != 'n' || answer != 'N' || answer != 'y' || answer != 'Y');

        }
        static char bookAVehicleMenu()
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

            char choice = readChar("Enter an option: ");
            return choice;
        }

        static int yearFilter(Dictionary<string, Vehicle> data, List<string> keys)
        {
            printGarageDictionary(data, keys);
            int choice = readInt("Enter your choice from the vehicles above: ");
            return choice;
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

        /* static bool dictionaryKeyCheckUp(Dictionary<string, Vehicle> yess, string name)
         {
             bool check = yess.ContainsKey(name);
             if (check == true)
             {
                 return true;
             }
             return false;

         }*/

        static bool makeFilter(Dictionary<string, Vehicle> data, List<string> keys)
        {
            bool check = false;
            if (keys.Count == 0)
            {
                Console.Write("Sorry we do not have your desired vehicle make, please search for another one.\n");
                check = false;
            }
            else
            {
                printGarageDictionary(data, keys);
                check = true;
            }
            return check;
        }

        static List<string> GetKeyFromValue(string valueVar, Dictionary<string, Vehicle> searchDict)
        {
            List<string> listOfKeys = new List<string>();

            //loop to get keys from values into a list
            foreach (string keyVar in searchDict.Keys)
            {
                string valueFomKey = searchDict[keyVar].getMake;
                if (valueFomKey == valueVar)
                {
                    listOfKeys.Add(keyVar);
                }
            }
            return listOfKeys;
        }           //overload for filtering by make

        static List<string> GetKeyFromValue(Dictionary<string, Vehicle> searchDict, int min, int max)           //overload for filtering by year
        {
            List<string> listOfKeys = new List<string>();

            //loop to get keys from values into a list
            foreach (string keyVar in searchDict.Keys)
            {
                int valueFomKey = searchDict[keyVar].getYear;
                if (valueFomKey < max || valueFomKey > min)
                {
                    listOfKeys.Add(keyVar);
                }
            }
            return listOfKeys;
        }

        static decimal calculateCost(Dictionary<string, Vehicle> data, List<string> key, int choice, int days)
        {
            decimal totalCost = data[key[choice]].getCost * (decimal)days;
            return totalCost;
        }

        static int numOfDays()
        {
            int numOfDays = readInt("Enter the number of days you want to rent the car for (Max 30 days): ", 30, 1);
            return numOfDays;
        }

        static string returnChoice(List<string> key, int choice, Dictionary<string, Vehicle> data)            //function to return the selected vehicle for printing reciept
        {
            return key[choice];
        }

        static void printGarageDictionary(Dictionary<string, Vehicle> searchDict, List<string> key)
        {
            Console.WriteLine("Make     | " + " Model     | " + "Year     | " + "Fuel capacity     | " + "Suit cases     | " + "Cost/day |");
            int count = key.Count;

            for (int i = 0; i < count; ++i)
            {
                Console.WriteLine(i + 1 + ") " + searchDict[key[i]].getMake + "   |  " + searchDict[key[i]].getModel + "    | " + searchDict[key[i]].getYear + "     |  " + searchDict[key[i]].getFuelCapacity + "             |  " + searchDict[key[i]].getSuitCases + "            |  " + searchDict[key[i]].getCost);
            }
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

        static Dictionary<string, Vehicle> loadGarageData(string file)            //load the data from the database
        {
            //file = garageFileName;
            string filePath = @"files/" + file;

            if (!File.Exists(filePath))           //checking if file exist and copying from backup if not
            {
                fileExistCheckAndCreate();
            }

            Dictionary<string, Vehicle> loadMe = new Dictionary<string, Vehicle>();
            StreamReader input = new StreamReader(filePath);

            while (!input.EndOfStream)
            {
                string line = input.ReadLine();
                if (line != "")
                {

                    string[] valuesArray = line.Split(',');
                    try             //if there is an exception in the loading of the file the execution will continue
                    {
                        loadMe[valuesArray[1]] = new Vehicle(valuesArray[0], valuesArray[1], int.Parse(valuesArray[2]), decimal.Parse(valuesArray[3]), int.Parse(valuesArray[4]), bool.Parse(valuesArray[5]), decimal.Parse(valuesArray[6]));
                    }
                    catch
                    {
                        continue;
                    }
                    //Console.WriteLine(loadMe[valuesArray[0]].getMake);
                }

            }
            input.Close();
            return loadMe;
        }

        static char readChar(string prompt)         //come back and fix the int issue
        {
            char op = ' ';
            string option = " ";
            bool check = false;
            do
            {
                Console.WriteLine(prompt);
                option = Console.ReadLine().ToUpper().Trim();

                try
                {
                    check = char.TryParse(option, out op);

                }
                catch
                {
                    Console.WriteLine("Please enter one character from the above options.");
                }

            } while (option == " " || check == false || !char.IsLetter(op));
            return op;
        }

        static void printReciept(Dictionary<string, Vehicle> searchDict, List<string> key, int choice)
        {
            Console.WriteLine("--------------------------------------------RECIEPT----------------------------------------------------------\n");
            Console.WriteLine("Make     | " + " Model     | " + "Year     | " + "Fuel capacity     | " + "Suit cases     | " + "Cost/day |");
            Console.WriteLine("1) " + searchDict[key[choice]].getMake + "   |  " + searchDict[key[choice]].getModel + "    | " + searchDict[key[choice]].getYear + "     |  " + searchDict[key[choice]].getFuelCapacity + "             |  " + searchDict[key[choice]].getSuitCases + "            |  " + searchDict[key[choice]].getCost);
            Console.WriteLine("\n-----------------------------------------THANK YOU-----------------------------------------------------------");
        }
    }
}
