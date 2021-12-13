using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lawrider_car_rental_system
{
    class Program
    {
        static void Main()
        {
            int choice = 0;
            do
            {
                displayMainMenu();
                choice = readInt("Choose an option between {0} and {1}",2, 1);
                
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
            Console.WriteLine("1) Search by Make");
            Console.WriteLine("2) Filter by year");
            Console.WriteLine("3) Filter by Fuel capacity");
            Console.WriteLine("4) Filter by price");
            Console.WriteLine("5) Show all avilable vehicles");
            Console.WriteLine("6) Go back to main menu");
            Console.WriteLine("-----------------------------------------------------------");
        }

        /*         static bool checkUp(Dictionary<string, int> yess, string name)
        {
            bool check = yess.ContainsKey(name);
            if (check == true)
            {
                return true;
            }
            return false;

        }

        static void save(Dictionary<string, int> saveMe)
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = File.Create("data.bin");
            binary.Serialize(file, saveMe);
            file.Close();
        }

        static Dictionary<string, int> load()
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = File.OpenRead("data.bin");
            Dictionary<string, int> loadMe = (Dictionary<string, int>)binary.Deserialize(file);
            file.Close();
            return loadMe;
        }*/
    }
}
