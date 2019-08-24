using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlluminatiConsole
{
    class LogInMenu
    {
        private readonly string FILEPATHLOGIN = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\LogInText.txt";
        private readonly string FILEPATHCUSTOMERS = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\CustomersText.txt";
        private List<Customer> customersList = new List<Customer>();
        private readonly int txtNamePosition = 13;
        private readonly int txtPasswordPosition = 16;
        private readonly string placeholderNameText = "Enter name";
        private readonly string placeholderPasswordText = "Enter password";
        private readonly string textField = "__________________";
        private string name = "";
        private string password = "";


        public void Initialize()
        {
            Console.Clear();
            LoadData();
            ShowLogIn();
            Console.ReadLine();
        }

        private void LoadData()
        {
            using (StreamReader srCustomer = new StreamReader(FILEPATHCUSTOMERS))
            {
                string lineCustomer = "";
                while ((lineCustomer = srCustomer.ReadLine()) != null)
                {
                    if (!String.IsNullOrEmpty(lineCustomer))
                    {
                        string[] linesCustomer = lineCustomer.Split('|');
                        Customer customerToAdd = new Customer(int.Parse(linesCustomer[0]), linesCustomer[1], linesCustomer[2]);
                        customersList.Add(customerToAdd);
                    }
                }
            }
        }

        private void ShowLogIn()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHLOGIN, 37, 2);
            Console.SetCursorPosition(45, 20);
            Console.WriteLine("If you want to sign up type: 420");
            AllowInput();
        }

        private void AllowInput()
        {
            ShowTextFields();
            SetStartingPointerPosition();
        }

        private void ShowTextFields()
        {
            Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
            Console.Write(placeholderNameText);
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition + 1);
            Console.Write(textField);
            Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
            Console.Write(placeholderPasswordText);
            Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition + 1);
            Console.Write(textField);
        }

        private void SetStartingPointerPosition()
        {
            Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
            if (Console.CursorTop == txtNamePosition)
            {
                ASCIIAnimator.Instance.ClearCurrentConsoleLine();
                Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition);
            }
            else if (Console.CursorTop == txtPasswordPosition)
            {
                ASCIIAnimator.Instance.ClearCurrentConsoleLine();
                Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition);
            }
            CheckUserInput();
        }

        private void CheckUserInput()
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                if (Console.CursorTop == txtNamePosition)
                {
                    name = Console.ReadLine();
                    if (name.Equals(placeholderNameText) || name.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                        ASCIIAnimator.Instance.ClearCurrentConsoleLine();
                        Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                        Console.Write(placeholderNameText);
                    }
                    else if (name.Equals("420"))
                    {
                        Console.Clear();
                        SignUpMenu signUpMenu = new SignUpMenu();
                        signUpMenu.Initialize();
                    }
                    if (password.Equals(placeholderPasswordText) || password.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                        ASCIIAnimator.Instance.ClearCurrentConsoleLine();
                    }
                    Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition);
                }
                else
                {
                    password = Console.ReadLine();
                    if (password.Equals(placeholderPasswordText) || password.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                        ASCIIAnimator.Instance.ClearCurrentConsoleLine();
                        Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                        Console.Write(placeholderPasswordText);
                    }
                    else if (password.Equals("420"))
                    {
                        Console.Clear();
                        SignUpMenu signUpMenu = new SignUpMenu();
                        signUpMenu.Initialize();
                    }
                    if (name.Equals(placeholderNameText) || name.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                        ASCIIAnimator.Instance.ClearCurrentConsoleLine();
                    }
                    CheckCredentials(name, password);
                    Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition);
                }
            }
            CheckUserInput();
        }

        private void CheckCredentials(string name, string password)
        {
            for (int i = 0; i < customersList.Count; i++)
            {
                if (name.Equals(customersList[i].Name) && password.Equals(customersList[i].Password))
                {
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Initialize(name);
                }
            }
        }
    }
}
