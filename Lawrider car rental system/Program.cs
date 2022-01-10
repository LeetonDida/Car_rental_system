using System;
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

    class Users
    {
        private string firstName;
        private string secondName;
        private int age;
        private string email;
        private string gender;
        private double phoneNumber;
        private string address;
        private string password;
        private bool bookingState;

        public Users(string firstName, string secondName, int age, string email, string gender, double phoneNumber, string addres, string password, bool state = false)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.age = age;
            this.email = email;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
            this.address = addres;
            this.password = password;
            this.bookingState = state;
        }

        public string getFirstName
        {
            get => firstName;
            set => this.firstName = value;
        }

        public string getSecondName
        {
            get => secondName;
            set => this.secondName = value;
        }

        public int getAge
        {
            get => age;
            set => this.age = value;
        }

        public string getEmail
        {
            get => email;
            set => this.email = value;
        }

        public string getGender
        {
            get => gender;
            set => this.gender = value;
        }

        public double getPhoneNumber
        {
            get => phoneNumber;
            set => this.phoneNumber = value;
        }

        public string getAddress
        {
            get => address;
            set => this.address = value;
        }

        public string getPassword
        {
            get => password;
            set => this.password = value;
        }

        public bool getBookingState
        {
            get => bookingState;
            set => this.bookingState = value;
        }
    }

    class GlobalData
    {
        public const string garageFileName = "garage.txt";
        public const string usersFileName = "users.txt";
        public const int MaximumVehicleYears = 2012;
        public const int MinimumVehicleYears = 1983;
        public const decimal MaxFuelCapacity = 39.86m;
        public const decimal MinFuelCapacity = 10.64m;
        public const decimal MaxPrice = 236.252m;
        public const decimal MinPrice = 11.548m;
        public const string garageFilePath = @"files/" + garageFileName;
        public static string currentUser;
    }
    class Program
    {
        static void Main()
        {

            int choice = 0;
            char loginMenuChoice;
            string userKey = "";
            bool loginCheck = false;
            Dictionary<string, Users> users = new Dictionary<string, Users>();
            users = LoadClass.loadUserData(GlobalData.usersFileName);
            do
            {
                loginMenuChoice = DisplayClass.displayLoginMenu();
                loginMenuChoice = char.ToUpper(loginMenuChoice);
                switch (loginMenuChoice)
                {
                    case 'A':
                        userKey = login(users);
                        if (userKey != "")
                        {
                            loginCheck = true;
                        }
                        break;
                    case 'B':
                        SaveClass.register(users);
                        break;
                    case 'C':
                        DataHandling.forgotPassword(users);
                        break;
                    case 'D':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option from the given options above");
                        break;
                }
            } while (loginMenuChoice == 'D' || loginCheck == false);


            //----------------------------------------------------------------------------------------------FROM HERE ITS THE MAIN PROGRAM LOOP--------------------------------------------------------------------------------------------------------------
            if (loginCheck)
            {
                do
                {
                    Dictionary<string, Vehicle> data = new Dictionary<string, Vehicle>();


                    data = LoadClass.loadGarageData(GlobalData.garageFileName);
                    DisplayClass.displayMainMenu();
                    choice = Read.readInt("Choose an option between {0} and {1}", 3, 1, "Please enter a valid choice from the menu above.");

                    switch (choice)
                    {
                        case 1:
                            char bookingMenuOption = DisplayClass.bookAVehicleMenu();

                            if (bookingMenuOption == 'A')
                            {
                                string make = Read.readString("Enter the prefered vehicle make: ", "Please enter a vehicle make.").Trim();
                                string capitalizedMake = char.ToUpper(make[0]) + make.Substring(1);     //capitalization code modified from stackoverflow (Diego 09/11/2010)
                                List<string> keys = DataHandling.GetKeyFromValue(capitalizedMake, data);
                                if (keys.Count == 0)
                                {
                                    Console.WriteLine("Sorry we do not have your desired vehicle make, please search for another one.\n");
                                    continue;
                                }
                                else
                                {
                                    DisplayClass.printGarageDictionary(data, keys);
                                }

                                int vehicleChoice = Read.readInt("Enter your vehicle choice from the choices above: ", keys.Count, 1, "Please enter a valid vehicle choice.") - 1;

                                //if the entered vehicle is not avilable in the keys the loop will restart
                                if (!DataHandling.checkKey(data, keys, vehicleChoice, "Sorry we do not have your desired vehicle make, please search for another one.\n"))
                                {
                                    continue;
                                }

                                int days = Read.numOfDays();
                                decimal totalCost = DataHandling.calculateCost(data, keys, vehicleChoice, days);

                                DataHandling.concludeBooking(data, keys, vehicleChoice, totalCost, days);

                            }
                            else if (bookingMenuOption == 'B')
                            {

                                int minYear;
                                int maxYear;

                                do
                                {       //fix this maximum and minimum issue when you get your arse back from work
                                    maxYear = Read.readInt("Please enter maximum year between " + GlobalData.MaximumVehicleYears + " and " + GlobalData.MinimumVehicleYears + ": ", GlobalData.MaximumVehicleYears, GlobalData.MinimumVehicleYears, "Please enter a year between the given bounds.");
                                    minYear = Read.readInt("Please enter minimum year between " + maxYear + " and " + GlobalData.MinimumVehicleYears + ": ", maxYear, GlobalData.MinimumVehicleYears, "Please enter a year between the given bounds.");

                                    if (maxYear < minYear)
                                    {
                                        Console.WriteLine("Maximum year can not be less than the minimum year. Try reversing your answers.");
                                        continue;
                                    }

                                } while (maxYear < minYear || maxYear > GlobalData.MaximumVehicleYears || minYear < GlobalData.MinimumVehicleYears);

                                List<string> keys = DataHandling.GetKeyFromValue(data, minYear, maxYear);
                                DataHandling.intRangeFilters(data, keys);
                            }
                            else if (bookingMenuOption == 'C')
                            {
                                decimal maxCapacity, minCapacity;
                                string filter = "fuel capacity";

                                do
                                {
                                    maxCapacity = Read.readDecimal("Please enter a maximum fuel capacity range between " + GlobalData.MaxFuelCapacity + " and " + GlobalData.MinFuelCapacity + ": ", GlobalData.MaxFuelCapacity, GlobalData.MinFuelCapacity, "Please enter a fuel capacity range between the given bounds.");
                                    minCapacity = Read.readDecimal("Please enter a minimum fuel capacity range between " + maxCapacity + " and " + GlobalData.MinFuelCapacity + ": ", maxCapacity, GlobalData.MinFuelCapacity, "Please enter a fuel capacity range between the given bounds.");

                                    if (maxCapacity < minCapacity)
                                    {
                                        Console.WriteLine("Maximum year can not be less than the minimum year. Try reversing your answers.");
                                        continue;
                                    }

                                } while (maxCapacity < minCapacity || maxCapacity > GlobalData.MaxFuelCapacity || minCapacity < GlobalData.MinFuelCapacity);

                                List<string> keys = DataHandling.GetKeyFromValue(data, decimal.Round(maxCapacity), decimal.Round(minCapacity), filter);

                                if (keys.Count == 0)
                                {
                                    Console.WriteLine("Sorry we do not have vehicles between the desired range, please try again with a different range.\n");
                                    continue;
                                }
                                else
                                {
                                    DataHandling.intRangeFilters(data, keys);
                                }

                            }
                            else if (bookingMenuOption == 'D')
                            {
                                decimal maxPrice, minPrice;
                                string filter = "price";
                                do
                                {
                                    maxPrice = Read.readDecimal("Please enter a maximum price between " + GlobalData.MaxPrice + " and " + GlobalData.MinPrice + ": ", GlobalData.MaxPrice, GlobalData.MinPrice, "Please enter a price between the given bounds.");
                                    minPrice = Read.readDecimal("Please enter a minimum price between " + maxPrice + " and " + GlobalData.MinPrice + ": ", maxPrice, GlobalData.MinPrice, "Please enter a price between the given bounds.");

                                    if (maxPrice < minPrice)
                                    {
                                        Console.WriteLine("Maximum price can not be less than the minimum price. Try reversing your answers.");
                                        continue;
                                    }

                                } while (maxPrice < minPrice || maxPrice > GlobalData.MaxPrice || minPrice < GlobalData.MinPrice);

                                List<string> keys = DataHandling.GetKeyFromValue(data, decimal.Round(maxPrice), decimal.Round(minPrice), filter);

                                if (keys.Count == 0)
                                {
                                    Console.WriteLine("Sorry we do not have vehicles between the desired range, please try again with a different range.\n");
                                    continue;
                                }
                                else
                                {
                                    DataHandling.intRangeFilters(data, keys);
                                }
                            }
                            else if (bookingMenuOption == 'E')
                            {
                                List<string> keys = new List<string>(data.Keys);
                                List<string> Newkeys = new List<string>();

                                //loop to get keys of avilable cars 
                                int keySize = data.Keys.Count;
                                for (int i = 0; i < keySize; ++i)
                                {
                                    //if the vehicle`s avilability sttate is true, then it is inserted into the Newkeys list
                                    if (data[keys[i]].getSetAvilableState == true)
                                    {
                                        Newkeys.Add(keys[i]);
                                    }
                                }

                                Newkeys.Sort();
                                DisplayClass.printGarageDictionary(data, Newkeys);

                                int vehicleChoice = Read.readInt("Enter your vehicle choice from the choices above: ", Newkeys.Count, 1, "Please enter a valid vehicle choice.") - 1;
                                int days = Read.numOfDays();
                                decimal totalCost = DataHandling.calculateCost(data, Newkeys, vehicleChoice, days);
                                DataHandling.concludeBooking(data, Newkeys, vehicleChoice, totalCost, days);
                            }
                            else if (bookingMenuOption == 'F')
                            {
                                continue;
                            }
                            break;
                        case 2:
                            DataHandling.changeCustomerDetails(users, userKey);
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Please enter a valid option from the given options above");
                            break;
                    }

                } while (choice != 3);
            }

        }

        static string login(Dictionary<string, Users> data)
        {
            string email = Read.readString("Enter your email: ", "Please enter a valid email.");
            string Password = Read.readString("Enter your password: ", "Please enter a valid password");

            try
            {
                if ((data[email].getPassword == Password) && data.ContainsKey(email))
                {

                    setCurrentUser(data, email);
                }
                else
                {
                    Console.WriteLine("Sorry, we do not have an account with the entered details, please try again.");
                    email = "";
                }
            }
            catch
            {
                Console.WriteLine("Sorry, we do not have an account with the entered details, please try again.");
                email = "";
            }

            return email;
        }

        static void setCurrentUser(Dictionary<string, Users> data, string email)
        {
            GlobalData.currentUser = data[email].getFirstName + " " + data[email].getSecondName;
        }

    }
}
