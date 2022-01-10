using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawrider_car_rental_system
{
    class DataHandling
    {

        public static void intRangeFilters(Dictionary<string, Vehicle> data, List<string> keys)
        {

            DisplayClass.printGarageDictionary(data, keys);
            int vehicleChoice = Read.readInt("Enter your choice from the vehicles above: ", keys.Count, 1, "Please enter a valid vehicle choice.") - 1;
            if (checkKey(data, keys, vehicleChoice, "Sorry we do not have your desired vehicle make, please search for another one.\n"))
            {
                //checking the key in the dictionary 
                if (checkKey(data, keys, vehicleChoice, "Sorry we do not have vehicles between the desired range, please try again with a different range.\n"))
                {
                    int days = Read.numOfDays();
                    decimal totalCost = calculateCost(data, keys, vehicleChoice, days);
                    concludeBooking(data, keys, vehicleChoice, totalCost, days);
                }
            }

        }

        public static bool checkKey(Dictionary<string, Vehicle> data, List<string> keys, int vehicleChoice, string error)
        {
            if (keys.Count == 0 || !data.ContainsKey(keys[vehicleChoice]) || vehicleChoice > keys.Count || vehicleChoice <= -1)
            {
                Console.Write(error);
                return false;
            }
            return true;
        }
        public static void concludeBooking(Dictionary<string, Vehicle> data, List<string> keys, int vehicleChoice, decimal totalCost, int days)
        {
            Console.WriteLine("Your total cost for renting {0} {1} will be {2}. Do you wish to continue. (Y/N)", data[keys[vehicleChoice]].getMake, data[keys[vehicleChoice]].getModel, totalCost);
            char answer = ' ';
            do
            {   //prompting user if they want to continue with the booking or not
                answer = Read.readChar("\nDo you wish to continue. (Y/N)", "Please enter one character from the above options.");
                if (answer == 'y' || answer == 'Y')
                {
                    data[keys[vehicleChoice]].getSetAvilableState = false;          //setting the avilability of the vehicle to false (booked)

                    DisplayClass.printReciept(data, keys, vehicleChoice);
                    Console.WriteLine("Your total cost: {0}\nNumber of Days {1}", totalCost, days);

                    //saving the current transaction
                    SaveClass.saveGarageData(data, GlobalData.garageFilePath);
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

        public static List<string> GetKeyFromValue(string valueVar, Dictionary<string, Vehicle> searchDict)
        {
            List<string> listOfKeys = new List<string>();

            //loop to get keys from values into a list
            foreach (string keyVar in searchDict.Keys)
            {
                //if the vehicle`s availability state is true, then it is inserted into the keys
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

        public static List<string> GetKeyFromValue(Dictionary<string, Vehicle> searchDict, int min, int max)           //overload for filtering by int range
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

        public static List<string> GetKeyFromValue(Dictionary<string, Vehicle> searchDict, decimal max, decimal min, string filterName)           //overload for filtering by decimal range
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

        public static decimal calculateCost(Dictionary<string, Vehicle> data, List<string> key, int choice, int days)
        {
            decimal totalCost = data[key[choice]].getCost * (decimal)days;
            return totalCost;
        }

        public static void forgotPassword(Dictionary<string, Users> data)
        {
            string email = Read.readString("Enter your email: ", "Please enter a valid email");

            try
            {

                double phoneNumber = Read.phoneNumberValidation();

                if ((data[email].getPhoneNumber == phoneNumber && data.ContainsKey(email)))
                {
                    string password = Read.readString("Enter a new password: ", "Please enter a password");
                    //changing the password if email and phone number match
                    data[email].getPassword = password;
                    SaveClass.saveUserDetails(data);
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

        public static void changeCustomerDetails(Dictionary<string, Users> data, string currentUser)
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

                choice = Read.readChar("Enter your choice: ", "Please enter a valid option from the ones above.");
                switch (choice)
                {

                    case 'A':
                        string newName = Read.readString("Enter your new name: ", "Please enter a valid name.");
                        data[currentUser].getFirstName = newName;
                        SaveClass.saveUserDetails(data);
                        Console.WriteLine("Name Changed successfully!!!");
                        break;
                    case 'B':
                        string newEmail = Read.emailValidation();
                        data[currentUser].getEmail = newEmail;
                        SaveClass.saveUserDetails(data);
                        Console.WriteLine("Email Changed successfully!!!");
                        break;
                    case 'C':
                        double newPhoneNumber = Read.phoneNumberValidation();
                        data[currentUser].getPhoneNumber = newPhoneNumber;
                        SaveClass.saveUserDetails(data);
                        Console.WriteLine("Phone number Changed successfully!!!");
                        break;
                    case 'D':
                        string newAddress = Read.readString("Enter your new address: ", "Please enter a valid address");
                        data[currentUser].getAddress = newAddress;
                        SaveClass.saveUserDetails(data);
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
    }
}


