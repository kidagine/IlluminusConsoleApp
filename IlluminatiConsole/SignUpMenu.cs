using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlluminatiConsole
{
    class SignUpMenu
    {
        private readonly string FILEPATHSIGNUP = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\SignUpText.txt";
        private readonly string FILEPATHCUSTOMERS = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\TxtFiles\\CustomersText.txt";
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
            ShowSignIn();
            Console.ReadLine();
        }

        private void ShowSignIn()
        {
            ASCIIAnimator.Instance.CreateASCIIAnimation(FILEPATHSIGNUP, 40, 2);
            Console.SetCursorPosition(20, 20);
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
                    if (name.Equals(placeholderNameText) || name.Equals(""))
                    {
                        Console.SetCursorPosition((Console.WindowWidth - placeholderNameText.Length) / 2, txtNamePosition);
                        ASCIIAnimator.Instance.ClearCurrentConsoleLine();
                    }
                    if (!name.Equals(placeholderNameText) || !String.IsNullOrWhiteSpace(name) || !password.Equals(placeholderPasswordText) || !String.IsNullOrWhiteSpace(password))
                    {
                        MainModel.Instance.AddCustomer(name, password);
                        LogInMenu logInMenu = new LogInMenu();
                        logInMenu.Initialize();
                    }
                }
            }
            CheckUserInput();
        }
    }
}
