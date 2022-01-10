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

    class Program
    {
        const string garageFileName = "garage.txt";
        const string usersFileName = "users.txt";
        const int MaximumVehicleYears = 2012;
        const int MinimumVehicleYears = 1983;
        const decimal MaxFuelCapacity = 39.86m;
        const decimal MinFuelCapacity = 10.64m;
        const decimal MaxPrice = 236.252m;
        const decimal MinPrice = 11.548m;
        const string garageFilePath = @"files/" + garageFileName;
        public static string currentUser;

        static void Main()
        {

            int choice = 0;
            char loginMenuChoice;
            string userKey = "";
            bool loginCheck = false;
            Dictionary<string, Users> users = new Dictionary<string, Users>();
            users = loadUserData(usersFileName);
            do
            {
                loginMenuChoice = displayLoginMenu();
                loginMenuChoice = char.ToUpper(loginMenuChoice);
                switch (loginMenuChoice)
                {
                    case 'A':
                        userKey = login(users);
                        if(userKey != "")
                        {
                            loginCheck = true;
                        }
                        break;
                    case 'B':
                        register(users);
                        break;
                    case 'C':
                        forgotPassword(users);
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


                    data = loadGarageData(garageFileName);
                    displayMainMenu();
                    choice = readInt("Choose an option between {0} and {1}", 3, 1, "Please enter a valid choice from the menu above.");

                    switch (choice)
                    {
                        case 1:
                            char bookingMenuOption = bookAVehicleMenu();

                            if (bookingMenuOption == 'A')
                            {
                                string make = readString("Enter the prefered vehicle make: ", "Please enter a vehicle make.").Trim();
                                string capitalizedMake = char.ToUpper(make[0]) + make.Substring(1);     //capitalization code modified from stackoverflow (Diego 09/11/2010)
                                List<string> keys = GetKeyFromValue(capitalizedMake, data);
                                if (keys.Count == 0)
                                {
                                    Console.WriteLine("Sorry we do not have your desired vehicle make, please search for another one.\n");
                                    continue;
                                }
                                else
                                {
                                    printGarageDictionary(data, keys);
                                }

                                int vehicleChoice = readInt("Enter your vehicle choice from the choices above: ", keys.Count, 1, "Please enter a valid vehicle choice.") - 1;

                                //if the entered vehicle is not avilable in the keys the loop will restart
                                if (!checkKey(data, keys, vehicleChoice, "Sorry we do not have your desired vehicle make, please search for another one.\n"))
                                {
                                    continue;
                                }

                                int days = numOfDays();
                                decimal totalCost = calculateCost(data, keys, vehicleChoice, days);

                                concludeBooking(data, keys, vehicleChoice, totalCost, days);

                            }
                            else if (bookingMenuOption == 'B')
                            {

                                int minYear;
                                int maxYear;

                                do
                                {       //fix this maximum and minimum issue when you get your arse back from work
                                    maxYear = readInt("Please enter maximum year between " + MaximumVehicleYears + " and " + MinimumVehicleYears + ": ", MaximumVehicleYears, MinimumVehicleYears, "Please enter a year between the given bounds.");
                                    minYear = readInt("Please enter minimum year between " + maxYear + " and " + MinimumVehicleYears + ": ", maxYear, MinimumVehicleYears, "Please enter a year between the given bounds.");

                                    if (maxYear < minYear)
                                    {
                                        Console.WriteLine("Maximum year can not be less than the minimum year. Try reversing your answers.");
                                        continue;
                                    }

                                } while (maxYear < minYear || maxYear > MaximumVehicleYears || minYear < MinimumVehicleYears);

                                List<string> keys = GetKeyFromValue(data, minYear, maxYear);
                                intRangeFilters(data, keys);
                            }
                            else if (bookingMenuOption == 'C')
                            {
                                decimal maxCapacity, minCapacity;
                                string filter = "fuel capacity";

                                do
                                {
                                    maxCapacity = readDecimal("Please enter a maximum fuel capacity range between " + MaxFuelCapacity + " and " + MinFuelCapacity + ": ", MaxFuelCapacity, MinFuelCapacity, "Please enter a fuel capacity range between the given bounds.");
                                    minCapacity = readDecimal("Please enter a minimum fuel capacity range between " + maxCapacity + " and " + MinFuelCapacity + ": ", maxCapacity, MinFuelCapacity, "Please enter a fuel capacity range between the given bounds.");

                                    if (maxCapacity < minCapacity)
                                    {
                                        Console.WriteLine("Maximum year can not be less than the minimum year. Try reversing your answers.");
                                        continue;
                                    }

                                } while (maxCapacity < minCapacity || maxCapacity > MaxFuelCapacity || minCapacity < MinFuelCapacity);

                                List<string> keys = GetKeyFromValue(data, decimal.Round(maxCapacity), decimal.Round(minCapacity), filter);

                                if (keys.Count == 0)
                                {
                                    Console.WriteLine("Sorry we do not have vehicles between the desired range, please try again with a different range.\n");
                                    continue;
                                }
                                else
                                {
                                    intRangeFilters(data, keys);
                                }

                            }
                            else if (bookingMenuOption == 'D')
                            {
                                decimal maxPrice, minPrice;
                                string filter = "price";
                                do
                                {
                                    maxPrice = readDecimal("Please enter a maximum price between " + MaxPrice + " and " + MinPrice + ": ", MaxPrice, MinPrice, "Please enter a price between the given bounds.");
                                    minPrice = readDecimal("Please enter a minimum price between " + maxPrice + " and " + MinPrice + ": ", maxPrice, MinPrice, "Please enter a price between the given bounds.");

                                    if (maxPrice < minPrice)
                                    {
                                        Console.WriteLine("Maximum price can not be less than the minimum price. Try reversing your answers.");
                                        continue;
                                    }

                                } while (maxPrice < minPrice || maxPrice > MaxPrice || minPrice < MinPrice);

                                List<string> keys = GetKeyFromValue(data, decimal.Round(maxPrice), decimal.Round(minPrice), filter);

                                if (keys.Count == 0)
                                {
                                    Console.WriteLine("Sorry we do not have vehicles between the desired range, please try again with a different range.\n");
                                    continue;
                                }
                                else
                                {
                                    intRangeFilters(data, keys);
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
                                printGarageDictionary(data, Newkeys);

                                int vehicleChoice = readInt("Enter your vehicle choice from the choices above: ", Newkeys.Count, 1, "Please enter a valid vehicle choice.") - 1;
                                int days = numOfDays();
                                decimal totalCost = calculateCost(data, Newkeys, vehicleChoice, days);
                                concludeBooking(data, Newkeys, vehicleChoice, totalCost, days);
                            }
                            else if (bookingMenuOption == 'F')
                            {
                                continue;
                            }
                            break;
                        case 2:
                            changeCustomerDetails(users, userKey);
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


        static Dictionary<string, Users> loadUserData(string file)
        {
            //file = garageFileName;
            string filePath = @"files/" + file;

            if (!File.Exists(filePath))           //checking if file exist and copying from backup if not
            {
                fileExistCheckAndCreate(file);
            }

            Dictionary<string, Users> loadMe = new Dictionary<string, Users>();

            //the code below calls a function which loads the file
            StreamReader input = new StreamReader(filePath);

            while (!input.EndOfStream)
            {
                string line = input.ReadLine();
                if (line != "")
                {

                    string[] valuesArray = line.Split(',');

                    try
                    {
                        loadMe[valuesArray[3]] = new Users(valuesArray[0], valuesArray[1], int.Parse(valuesArray[2]), valuesArray[3], valuesArray[4], double.Parse(valuesArray[5].Trim()), valuesArray[6], valuesArray[7], bool.Parse(valuesArray[8]));
                    }
                    catch
                    {
                        continue;
                    }
                }

            }
            input.Close();

            return loadMe;
        }

        static char displayLoginMenu()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("                  * LOGIN MENU *");
            Console.WriteLine("A) Login");
            Console.WriteLine("B) Register");
            Console.WriteLine("C) Forgot password");
            Console.WriteLine("D) Exit");
            Console.WriteLine("-----------------------------------------------------------");

            char choice = readChar("Enter an option: ", "Please enter one character from the above options.");
            return char.ToUpper(choice);
        }

        static string login(Dictionary<string, Users> data)
        {
            string email = readString("Enter your email: ", "Please enter a valid email.");
            string Password = readString("Enter your password: ", "Please enter a valid password");

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
            currentUser = data[email].getFirstName + " " + data[email].getSecondName;
        }

        static double phoneNumberValidation()
        {
            string phone; int count;
            do
            {
                count = 0;
                phone = readString("Enter your phone number: ", "Please enter a valid phone number").Trim();

                foreach (char ch in phone)
                {
                    count++;
                }

                if (count > 10 || count < 10)
                {
                    Console.WriteLine("Please enter a valid phone number.");
                }
            } while (count != 10);
            return double.Parse(phone);
        }           // //phone number validation

        static string genderValidation()
        {
            char choice;
            string gender = "";
            do
            {
                choice = readChar("Enter your gender (M/F): ", "Please enter between the options above");
                char.ToUpper(choice);
                if (choice == 'M')
                {
                    gender = "Male";
                }
                else if (choice == 'F')
                {
                    gender = "Female";
                }
                else
                {
                    Console.WriteLine("Please enter between the options above");
                }
            } while (gender == "");
            return gender;
        }           //gender validation

        static string emailValidation()
        {
            string email;
            bool check = false;
            string domainName;
            do
            {
                email = readString("Enter your email: ", "Please enter a valid email address").Trim();
                try
                {
                    domainName = email.Substring(email.Length - 4);         //code adapted from stack overflow (thestar 20/2017)
                    foreach (char ch in email)
                    {
                        if (ch == '@' && (domainName == ".com" || domainName == ".net" || domainName == ".gov" || domainName == ".org"))
                        {
                            check = true;
                        }
                    }
                }
                catch
                {
                    if (check == false)
                    {
                        Console.WriteLine("Please enter a valid Email address.");
                    }
                }
            } while (check == false);
            return email;
        }           ////email validation

        static void register(Dictionary<string, Users> data)
        {
            string joinedString = "";
            string email = emailValidation();
            if (!data.ContainsKey(email))
            {
                string firstName = readString("Enter your first name: ", "Please enter a valid first name");
                string secondName = readString("Enter your second name: ", "Please enter a valid second name");
                int age = readInt("Enter your age: ", 85, 15, "Please enter a valid age (Minimum 16 yrs)");
                string gender = genderValidation();
                double phoneNumber = phoneNumberValidation();
                string address = readString("Enter your address post code: ", "Please enter a valid postcode");
                string password = readString("Please enter a your password: ", "Please enter a valid password");

                //adding to dictionary for use in runtime
                data[email] = new Users(firstName, secondName, age, email, gender, phoneNumber, address, password);

                //adding into our database text file for later use
                joinedString = firstName + ',' + secondName + ',' + age + ',' + email + ',' + gender + ',' + phoneNumber + ',' + address + ',' + password + ',' + data[email].getBookingState;
                StreamWriter writter = new StreamWriter(@"files/" + usersFileName, append: true);
                writter.WriteLine("\n" + joinedString);
                writter.Close();

                Console.WriteLine("Account Created successfully!!!");
            }
            else
            {
                Console.WriteLine("You already have an account please login, or choose forgot password if you have forgotten your password.");
            }
        }

        static void saveUserDetails(Dictionary<string, Users> data, string path = @"files/" + usersFileName)
        {
            string dataToSave = "";
            foreach (string keyVar in data.Keys)
            {
                //add dictionary values into an array
                dataToSave += data[keyVar].getFirstName + "," + data[keyVar].getSecondName + "," + data[keyVar].getAge + "," + data[keyVar].getEmail + "," + data[keyVar].getGender + "," + data[keyVar].getPhoneNumber + "," + data[keyVar].getAddress + "," + data[keyVar].getPassword + "," + data[keyVar].getBookingState + "\n";
            }
            //write the text into the file
            File.WriteAllText(path, dataToSave);
        }
        static void forgotPassword(Dictionary<string, Users> data)
        {
            string email = readString("Enter your email: ", "Please enter a valid email");

            try
            {

                double phoneNumber = phoneNumberValidation();

                if ((data[email].getPhoneNumber == phoneNumber && data.ContainsKey(email)))
                {
                    string password = readString("Enter a new password: ", "Please enter a password");
                    //changing the password if email and phone number match
                    data[email].getPassword = password;
                    saveUserDetails(data);
                }
                else
                {
                    Console.WriteLine("Sorry we do not have the associated account with the details you`ve provided");
                }
            }
            catch
            {
                Console.WriteLine("Sorry, we do not have an account with the entered details, please try again.");
            }


        }

        static void changeCustomerDetails(Dictionary<string, Users> data, string currentUser)
        {
            char choice;

            do
            {
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("                  * CHANGE MY DETAILS MENU *");
                Console.WriteLine("A) Name");
                Console.WriteLine("B) Email");
                Console.WriteLine("C) Phone number");
                Console.WriteLine("D) Address");
                Console.WriteLine("E) Password");
                Console.WriteLine("F) Back");
                Console.WriteLine("-----------------------------------------------------------");

                choice = readChar("Enter your choice: ", "Please enter a valid option from the ones above.");
                switch (choice)
                {

                    case 'A':
                        string newName = readString("Enter your new name: ", "Please enter a valid name.");
                        data[currentUser].getFirstName = newName;
                        saveUserDetails(data);
                        Console.WriteLine("Name Changed successfully!!!");
                        break;
                    case 'B':
                        string newEmail = emailValidation();
                        data[currentUser].getEmail = newEmail;
                        saveUserDetails(data);
                        Console.WriteLine("Email Changed successfully!!!");
                        break;
                    case 'C':
                        double newPhoneNumber = phoneNumberValidation();
                        data[currentUser].getPhoneNumber = newPhoneNumber;
                        saveUserDetails(data);
                        Console.WriteLine("Phone number Changed successfully!!!");
                        break;
                    case 'D':
                        string newAddress = readString("Enter your new address: ", "Please enter a valid address");
                        data[currentUser].getAddress = newAddress;
                        saveUserDetails(data);
                        Console.WriteLine("Address Changed successfully!!!");
                        break;
                    case 'E':
                        forgotPassword(data);
                        Console.WriteLine("Password Changed successfully!!!");
                        break;
                    default:
                        Console.WriteLine("Please enter an option from the options above.");
                        break;
                }
            } while (choice != 'F');

        }




        //----------------------------------------------------------------------------------------- DO NOT TOUCH ANYTHING BELOW THIS POINT -----------------------------------------------------------------------------------------------------------------------------
        static void saveGarageData(Dictionary<string, Vehicle> data, string filePath)
        {
            string dataToSave = "";
            foreach (string keyVar in data.Keys)
            {
                //add dictionary values into an array
                dataToSave += data[keyVar].getMake + "," + data[keyVar].getModel + "," + data[keyVar].getYear + "," + data[keyVar].getCost + "," + data[keyVar].getSuitCases + "," + data[keyVar].getSetAvilableState + "," + data[keyVar].getFuelCapacity + "\n";
            }
            //write the array into the file
            File.WriteAllText(filePath, dataToSave);
        }


        static Dictionary<string, Vehicle> loadGarageData(string file)            //load the data from the database
        {
            //file = garageFileName;
            string filePath = @"files/" + file;

            if (!File.Exists(filePath))           //checking if file exist and copying from backup if not
            {
                fileExistCheckAndCreate(file);
            }

            Dictionary<string, Vehicle> loadMe = new Dictionary<string, Vehicle>();

            //the code below calls a function which loads the file
            StreamReader input = new StreamReader(filePath);

            while (!input.EndOfStream)
            {
                string line = input.ReadLine();
                if (line != "")
                {

                    string[] valuesArray = line.Split(',');
                    try             //if there is an exception in the loading of the file the execution will continue (eg if there are not enough values in a line)
                    {
                        loadMe[valuesArray[1]] = new Vehicle(valuesArray[0], valuesArray[1], int.Parse(valuesArray[2]), decimal.Parse(valuesArray[3]), int.Parse(valuesArray[4]), bool.Parse(valuesArray[5]), decimal.Parse(valuesArray[6]));
                    }
                    catch
                    {
                        continue;
                    }
                }

            }
            input.Close();

            return loadMe;
        }
        static void intRangeFilters(Dictionary<string, Vehicle> data, List<string> keys)
        {

            printGarageDictionary(data, keys);
            int vehicleChoice = readInt("Enter your choice from the vehicles above: ", keys.Count, 1, "Please enter a valid vehicle choice.") - 1;
            if (checkKey(data, keys, vehicleChoice, "Sorry we do not have your desired vehicle make, please search for another one.\n"))
            {
                //checking the key in the dictionary 
                if (checkKey(data, keys, vehicleChoice, "Sorry we do not have vehicles between the desired range, please try again with a different range.\n"))
                {
                    int days = numOfDays();
                    decimal totalCost = calculateCost(data, keys, vehicleChoice, days);
                    concludeBooking(data, keys, vehicleChoice, totalCost, days);
                }
            }

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

            char choice = readChar("Enter an option: ", "Please enter one character from the above options.");
            return choice;
        }

        static bool checkKey(Dictionary<string, Vehicle> data, List<string> keys, int vehicleChoice, string error)
        {
            if (keys.Count == 0 || !data.ContainsKey(keys[vehicleChoice]) || vehicleChoice > keys.Count || vehicleChoice <= -1)
            {
                Console.Write(error);
                return false;
            }
            return true;
        }
        static void concludeBooking(Dictionary<string, Vehicle> data, List<string> keys, int vehicleChoice, decimal totalCost, int days)
        {
            Console.WriteLine("Your total cost for renting {0} {1} will be {2}. Do you wish to continue. (Y/N)", data[keys[vehicleChoice]].getMake, data[keys[vehicleChoice]].getModel, totalCost);
            char answer = ' ';
            do
            {   //prompting user if they want to continue with the booking or not
                answer = readChar("\nDo you wish to continue. (Y/N)", "Please enter one character from the above options.");
                if (answer == 'y' || answer == 'Y')
                {
                    data[keys[vehicleChoice]].getSetAvilableState = false;          //setting the avilability of the vehicle to false (booked)

                    printReciept(data, keys, vehicleChoice);
                    Console.WriteLine("Your total cost: {0}\nNumber of Days {1}", totalCost, days);

                    //saving the current transaction
                    saveGarageData(data, garageFilePath);
                    Console.WriteLine("Press any key to go back to the main menu.");
                    Console.ReadKey();
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

        static List<string> GetKeyFromValue(string valueVar, Dictionary<string, Vehicle> searchDict)
        {
            List<string> listOfKeys = new List<string>();

            //loop to get keys from values into a list
            foreach (string keyVar in searchDict.Keys)
            {
                //if the vehicle`s avilability sttate is true, then it is inserted into the keys
                if (searchDict[keyVar].getSetAvilableState == true)
                {
                    string valueFomKey = searchDict[keyVar].getMake;
                    if (valueFomKey == valueVar)
                    {
                        listOfKeys.Add(keyVar);
                    }
                }
            }
            return listOfKeys;
        }           //overload for filtering by value

        static List<string> GetKeyFromValue(Dictionary<string, Vehicle> searchDict, int min, int max)           //overload for filtering by int range
        {
            List<string> listOfKeys = new List<string>();

            //loop to get keys from values into a list
            foreach (string keyVar in searchDict.Keys)
            {
                //if the vehicle`s avilability sttate is true, then it is inserted into the keys
                if (searchDict[keyVar].getSetAvilableState == true)
                {
                    int valueFomKey = searchDict[keyVar].getYear;
                    if (valueFomKey <= (int)max && valueFomKey >= (int)min)
                    {
                        listOfKeys.Add(keyVar);
                    }
                }
            }
            return listOfKeys;
        }

        static List<string> GetKeyFromValue(Dictionary<string, Vehicle> searchDict, decimal max, decimal min, string filterName)           //overload for filtering by decimal range
        {
            List<string> listOfKeys = new List<string>();
            decimal valueFomKey = -1;

            //loop to get keys from values into a list by setting the value to be used to filter the keys
            foreach (string keyVar in searchDict.Keys)
            {
                if (filterName == "fuel capacity")
                {
                    valueFomKey = decimal.Round(searchDict[keyVar].getFuelCapacity);
                }
                else if (filterName == "price")
                {
                    valueFomKey = decimal.Round(searchDict[keyVar].getCost);
                }

                //if the vehicle`s avilability sttate is true, then it is inserted into the keys
                if (searchDict[keyVar].getSetAvilableState == true)
                {
                    if (valueFomKey <= max && valueFomKey >= min)
                    {
                        listOfKeys.Add(keyVar);
                    }
                }
            }
            return listOfKeys;
        }

        static decimal calculateCost(Dictionary<string, Vehicle> data, List<string> key, int choice, int days)
        {
            decimal totalCost = data[key[choice]].getCost * (decimal)days;
            return totalCost;
        }

        static void fileExistCheckAndCreate(string file)           //method to copy back up database file if its not avilable
        {
            string sourceDirectory = @"back up/" + file;
            string destinationDirectory = @"files/" + file;

            {
                Console.WriteLine("Something went wrong. Press any key to continue report the issue and continue.");
                Console.ReadKey();

                File.Copy(sourceDirectory, destinationDirectory);

            }
        }

        static int numOfDays()
        {
            int numOfDays = (int)readInt("Enter the number of days you want to rent the car for (Max 30 days): ", 30, 1, "Please enter a value between 1 and 30.");
            return numOfDays;
        }

        static void printGarageDictionary(Dictionary<string, Vehicle> searchDict, List<string> key)
        {
            Console.WriteLine("Make     | " + " Model     | " + "Year     | " + "Fuel capacity     | " + "Suit cases     | " + "Cost/day |");
            int count = key.Count;
            key.Sort();

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
            Console.WriteLine("2) Change my details");
            Console.WriteLine("3) Logout/Exit");
            Console.WriteLine("-----------------------------------------------------------");
        }

        static decimal readDecimal(string prompt, decimal high, decimal low, string error)
        {
            string input = "";
            decimal num = 0;

            do
            {
                Console.WriteLine(prompt, low, high);
                input = Console.ReadLine();
                try
                {
                    num = decimal.Parse(input);
                    if (num > high || num < low)
                    {
                        Console.WriteLine(error);
                    }
                }
                catch
                {
                    Console.WriteLine(error);
                }

            } while (input == "" || num > high || num < low);

            return num;
        }

        static int readInt(string prompt, int high, int low, string error)
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
                    if (num > high || num < low)
                    {
                        Console.WriteLine(error);
                    }
                }
                catch
                {
                    Console.WriteLine(error);
                }

            } while (input == "" || num > high || num < low);

            return num;
        }

        static int readInt(string prompt, string error)           //overload for  readInt
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
                    Console.WriteLine(error);
                }

            } while (input == "" || check == false);

            return num;
        }

        static string readString(string prompt, string error)
        {
            string input = "";
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine(error);
                }

            } while (input == "");

            return input;
        }

        static char readChar(string prompt, string error)
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
                    if (!char.IsLetter(op))
                    {
                        Console.WriteLine(error);
                    }
                }
                catch
                {
                    Console.WriteLine(error);
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
