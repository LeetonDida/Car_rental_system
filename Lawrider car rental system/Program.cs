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
                displayMenu();
                choice = readInt("Choose an option between {0} and {1}",3, 1);

            } while (choice != 3);

        }

        static void displayMenu()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("                         * MENU *");
            Console.WriteLine("1) Book a vehicle");
            Console.WriteLine("2) Book a vehicle");
            Console.WriteLine("3) Logout/Exit");
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
