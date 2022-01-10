using System;
using System.Collections.Generic;

    class Read
    {
    public static decimal readDecimal(string prompt, decimal high, decimal low, string error)
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

    public static int readInt(string prompt, int high, int low, string error)
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

    public static int readInt(string prompt, string error)           //overload for  readInt
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

    public static string readString(string prompt, string error)
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

    public static char readChar(string prompt, string error)
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
    public static int numOfDays()
    {
        int numOfDays = (int)Read.readInt("Enter the number of days you want to rent the car for (Max 30 days): ", 30, 1, "Please enter a value between 1 and 30.");
        return numOfDays;
    }

    public static double phoneNumberValidation()
    {
        string phone; int count;
        do
        {
            count = 0;
            phone = Read.readString("Enter your phone number: ", "Please enter a valid phone number").Trim();

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

    public static string genderValidation()
    {
        char choice;
        string gender = "";
        do
        {
            choice = Read.readChar("Enter your gender (M/F): ", "Please enter between the options above");
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

    public static string emailValidation()
    {
        string email;
        bool check = false;
        string domainName;
        do
        {
            email = Read.readString("Enter your email: ", "Please enter a valid email address").Trim();
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
}

