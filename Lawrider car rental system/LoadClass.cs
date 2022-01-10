using System;
using System.Collections.Generic;
using System.IO;


namespace Lawrider_car_rental_system
{
    class LoadClass
    {
        public static Dictionary<string, Users> loadUserData(string file)
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
        public static Dictionary<string, Vehicle> loadGarageData(string file)            //load the data from the database
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

        public static void fileExistCheckAndCreate(string file)           //method to copy back up database file if its not avilable
        {
            string sourceDirectory = @"back up/" + file;
            string destinationDirectory = @"files/" + file;

            {
                Console.WriteLine("Something went wrong. Press any key to continue report the issue and continue.");
                Console.ReadKey();

                File.Copy(sourceDirectory, destinationDirectory);

            }
        }
    }
}
