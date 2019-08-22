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
        private readonly string FILEPATHSINGIN = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\LogInText.txt";
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
                    string[] linesCustomer = lineCustomer.Split('|');
                    for (int i = 0; i < linesCustomer.Length; i++)
                    {
                        Customer customerToAdd = new Customer(int.Parse(linesCustomer[0]), linesCustomer[1], linesCustomer[2]);
                        customersList.Add(customerToAdd);
                    }
                }
            }
        }

        private void ShowLogIn()
        {
            using (StreamReader srLogo = new StreamReader(FILEPATHSINGIN))
            {
                string logoLine;
                while ((logoLine = srLogo.ReadLine()) != null)
                {
                    string[] logoLines = logoLine.Split('|');
                    for (int i = 0; i < logoLines.Length; i++)
                    {
                        Console.SetCursorPosition(40, Console.CursorTop);
                        foreach (char c in logoLines[i])
                        {
                            Console.Write(c);
                            if (!Char.IsWhiteSpace(c))
                            {
                                System.Threading.Thread.Sleep(5);
                            }
                        }
                        if (logoLines[i].Length > 1)
                        {
                            Console.WriteLine();
                        }
                    }
                }
                srLogo.Close();
                System.Threading.Thread.Sleep(500);
                AllowInput();
            }
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
                ClearCurrentConsoleLine();
                Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtNamePosition);
            }
            else if (Console.CursorTop == txtPasswordPosition)
            {
                ClearCurrentConsoleLine();
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
                    Debug.WriteLine(name);
                    if (name.Equals(placeholderNameText) || name.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                        ClearCurrentConsoleLine();
                        Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                        Console.Write(placeholderNameText);
                    }
                    if (password.Equals(placeholderPasswordText) || password.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                        ClearCurrentConsoleLine();
                    }
                    Console.SetCursorPosition((Console.WindowWidth - textField.Length) / 2, txtPasswordPosition);
                }
                else
                {
                    password = Console.ReadLine();
                    if (password.Equals(placeholderPasswordText) || password.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                        ClearCurrentConsoleLine();
                        Console.SetCursorPosition((Console.WindowWidth - placeholderPasswordText.Length) / 2, txtPasswordPosition);
                        Console.Write(placeholderPasswordText);
                    }
                    if (name.Equals(placeholderNameText) || name.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                        ClearCurrentConsoleLine();
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
                Debug.WriteLine($"id: {customersList[i].Id} name: {customersList[i].Name} password: {customersList[i].Password}");
                if (name.Equals(customersList[i].Name) || password.Equals(customersList[i].Password))
                {
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Initialize(name);
                }
            }
        }

        private void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
