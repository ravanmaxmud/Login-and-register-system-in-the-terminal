using System;
using System.Collections.Generic;

namespace logic
{
    internal class Program
    {
        public static List<Register> persons { get; set; } = new List<Register>();
        static void Main(string[] args)
        {
            Console.WriteLine("/register");
            Console.WriteLine("/login");
            Console.WriteLine("/show-persons");
            Console.WriteLine();
            while (true)
            {
                Console.Write("Enter command :");
                string command = Console.ReadLine();

                if (command == "/register")
                {
                    Console.Write("Please Enter Your Name :");
                    string name = Console.ReadLine();

                    Console.Write("Please Enter Your LastName :");
                    string lastName = Console.ReadLine();

                    Console.Write("Please Enter Your E-mail :");
                    string eMail = Console.ReadLine();

                    Console.Write("Please Enter Your Password :");
                    string password = Console.ReadLine();

                    Console.Write("Enter Your Password Again :");
                    string returnPassword = Console.ReadLine();
                    AddNewAccount(name, lastName, eMail, password, returnPassword);
                    Console.WriteLine();
                }
                else if (command == "/login")
                {
                    Console.Write("Please Enter Email :");
                    string loginEmail = Console.ReadLine();


                    Console.Write("Please Enter Password :");
                    string loginPassword = Console.ReadLine();
                    IsloginCorrect(loginEmail, loginPassword);


                }
                else if (command == "/show-persons")
                {
                    Console.WriteLine();
                    Console.WriteLine("Persons in database : ");
                    ShowPersons();
                }
                else
                {
                    break;
                }
            }
        }

        public static bool AddNewAccount(string name, string lastName, string eMail, string password, string checkpassword)
        {
            Register program = new Register(name, lastName, eMail, password, checkpassword);
            Validator validator = new Validator();
            if (validator.Validate(program, persons))
            {
                persons.Add(program);
                Console.WriteLine("Saved all Information ");
                return true;
            }
            return false;
        }
        public static bool IsMailUnical(string mail)
        {
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].Email == mail)
                {
                    Console.WriteLine("Bu email artiq movcuddur");
                    return false;
                }
            }
            return true;
        }
        public static void ShowPersons()
        {
            foreach (Register person in persons)
            {
                Console.WriteLine(person.GetInfo());
            }
        }
        public static bool IsLoginMail(string logiMail)
        {
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].Email == logiMail)
                {
                    return true;
                }
            }
            Console.WriteLine("Email Yanlisdir");
            return false;
        }
        public static bool IsLoginPassword(string loginPassword)
        {
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].Password == loginPassword)
                {
                    return true;
                }
            }
            Console.WriteLine("Password Yanlisdir");
            return false;
        }
        public static bool IsLogPassValid(Login validator)
        {
            return Program.IsLoginMail(validator.LoginEmail) & Program.IsLoginPassword(validator.LoginPassword);
        }
        public static bool IsloginCorrect(string loginEmail, string loginPassword)
        {
            Login login = new Login(loginEmail, loginPassword);
            if (IsLogPassValid(login))
            {
                Console.WriteLine("Sisteme Daxil Olundu");
                return true;
            }
            Console.WriteLine("Melumatlar Sehfdir");
            return false;
        }
    }

    class Validator
    {
        public bool Validate(Register validator, List<Register> registers)
        {
            return IsNameLenght(validator.FirstName) & IsLastNameLenght(validator.LastName) & IsCheckEmail(validator.Email) & IsCheckPassword(validator.Password, validator.CheckPassword) & Program.IsMailUnical(validator.Email);
        }
        public bool IsCheckLenght(string value, int start, int end)
        {
            if (value.Length >= start && value.Length <= end)
            {
                return true;
            }
            return false;
        }
        public bool IsNameLenght(string name)
        {
            if (!IsCheckLenght(name, 3, 30))
            {
                Console.WriteLine("Adin Uzunluqu Duzgun deyil");
                return false;
            }
            return true;
        }
        public bool IsLastNameLenght(string name)
        {
            if (!IsCheckLenght(name, 3, 20))
            {
                Console.WriteLine("Soyadin uzunluqu duzgun deyil");
                return false;
            }
            return true;
        }
        public bool IsCheckEmail(string value)
        {
            char email = '@';
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == email)
                {
                    return true;
                }
            }
            Console.WriteLine("Emaili Duzgun Daxil Edin (@- isaresinden istifade etmeyi unutmayin)");
            return false;
        }
        public bool IsCheckPassword(string password, string checkPassword)
        {
            if (password == checkPassword)
            {
                return true;
            }
            else
            {

                Console.WriteLine("Passwordu Yeniden Daxil Edin");
                return false;
            }
        }
    }
    class Register : Validator
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }


        public Register(string name, string lastName, string email, string password, string checkpassword)
        {

            FirstName = name;
            LastName = lastName;
            Email = email;
            Password = password;
            CheckPassword = checkpassword;
        }
        public string GetInfo()
        {
            return FirstName + " " + LastName + " " + Email;
        }
    }
    class Login
    {
        public string LoginEmail { get; private set; }
        public string LoginPassword { get; private set; }
        public Login(string loginEmail, string loginPassword)
        {

            LoginEmail = loginEmail;
            LoginPassword = loginPassword;

        }
    }
}