using System;
using System.Collections.Generic;

namespace Lawrider_car_rental_system
{


    class DisplayClass
    {
        public static char bookAVehicleMenu()
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

            char choice = Read.readChar("Enter an option: ", "Please enter one character from the above options.");
            return choice;
        }
        public static char displayLoginMenu()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("                  * LOGIN MENU *");
            Console.WriteLine("A) Login");
            Console.WriteLine("B) Register");
            Console.WriteLine("C) Forgot password");
            Console.WriteLine("D) Exit");
            Console.WriteLine("-----------------------------------------------------------");

            char choice = Read.readChar("Enter an option: ", "Please enter one character from the above options.");
            return char.ToUpper(choice);
        }
        public static void printGarageDictionary(Dictionary<string, Vehicle> searchDict, List<string> key)
        {
            Console.WriteLine("Make     | " + " Model     | " + "Year     | " + "Fuel capacity     | " + "Suit cases     | " + "Cost/day |");
            int count = key.Count;
            key.Sort();

            for (int i = 0; i < count; ++i)
            {
                Console.WriteLine(i + 1 + ") " + searchDict[key[i]].getMake + "   |  " + searchDict[key[i]].getModel + "    | " + searchDict[key[i]].getYear + "     |  " + searchDict[key[i]].getFuelCapacity + "             |  " + searchDict[key[i]].getSuitCases + "            |  " + searchDict[key[i]].getCost);
            }
        }

        public static void displayMainMenu()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("                         * MENU *");
            Console.WriteLine("1) Book a vehicle");
            Console.WriteLine("2) Change my details");
            Console.WriteLine("3) Logout/Exit");
            Console.WriteLine("-----------------------------------------------------------");
        }
        public static void printReciept(Dictionary<string, Vehicle> searchDict, List<string> key, int choice)
        {
            Console.WriteLine("--------------------------------------------RECIEPT----------------------------------------------------------\n");
            Console.WriteLine("Make     | " + " Model     | " + "Year     | " + "Fuel capacity     | " + "Suit cases     | " + "Cost/day |");
            Console.WriteLine("1) " + searchDict[key[choice]].getMake + "   |  " + searchDict[key[choice]].getModel + "    | " + searchDict[key[choice]].getYear + "     |  " + searchDict[key[choice]].getFuelCapacity + "             |  " + searchDict[key[choice]].getSuitCases + "            |  " + searchDict[key[choice]].getCost);
            Console.WriteLine("\n-----------------------------------------THANK YOU-----------------------------------------------------------");
        }
    }
}

