using System;
using System.Collections.Generic;

namespace Lawrider_car_rental_system.Properties
{
    class DataProcessingClass
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

    }
}
