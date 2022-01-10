using System;
using System.Collections.Generic;
using System.IO;

namespace Lawrider_car_rental_system
{
    class SaveClass
    {
        public static void saveGarageData(Dictionary<string, Vehicle> data, string filePath)
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

        public static void saveUserDetails(Dictionary<string, Users> data, string path = @"files/" + GlobalData.usersFileName)
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

        public static void register(Dictionary<string, Users> data)
        {
            string joinedString = "";
            string email = Read.emailValidation();
            if (!data.ContainsKey(email))
            {
                string firstName = Read.readString("Enter your first name: ", "Please enter a valid first name");
                string secondName = Read.readString("Enter your second name: ", "Please enter a valid second name");
                int age = Read.readInt("Enter your age: ", 85, 15, "Please enter a valid age (Minimum 16 yrs)");
                string gender = Read.genderValidation();
                double phoneNumber = Read.phoneNumberValidation();
                string address = Read.readString("Enter your address post code: ", "Please enter a valid postcode");
                string password = Read.readString("Please enter a your password: ", "Please enter a valid password");

                //adding to dictionary for use in runtime
                data[email] = new Users(firstName, secondName, age, email, gender, phoneNumber, address, password);

                //adding into our database text file for later use
                joinedString = firstName + ',' + secondName + ',' + age + ',' + email + ',' + gender + ',' + phoneNumber + ',' + address + ',' + password + ',' + data[email].getBookingState;
                StreamWriter writter = new StreamWriter(@"files/" + GlobalData.usersFileName, append: true);
                writter.WriteLine("\n" + joinedString);
                writter.Close();

                Console.WriteLine("Account Created successfully!!!");
            }
            else
            {
                Console.WriteLine("You already have an account please login, or choose forgot password if you have forgotten your password.");
            }
        }
    }
}
