using System;
using System.IO;

namespace Web1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exitRequested = false;
            while (!exitRequested)
            {
                Console.Clear();
                DisplayMenu();
                string userChoice = Console.ReadLine();
                exitRequested = HandleMenuSelection(userChoice);
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Count words in a file");
            Console.WriteLine("2. Perform calculation");
            Console.WriteLine("0. Exit");
            Console.Write("Please choose an option: ");
        }

        static bool HandleMenuSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    CountWordsInFile();
                    return false;
                case "2":
                    ExecuteMathOperation();
                    return false;
                case "0":
                    return true;
                default:
                    Console.WriteLine("Invalid selection. Try again.");
                    WaitForUser();
                    return false;
            }
        }

        static void CountWordsInFile()
        {
            Console.WriteLine("Reading text file...");
            string filePath = "Text1.txt";

            if (File.Exists(filePath))
            {
                try
                {
                    string content = File.ReadAllText(filePath);
                    int wordCount = GetWordCount(content);
                    Console.WriteLine($"The text contains {wordCount} words.");
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"File error: {ioEx.Message}");
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            WaitForUser();
        }

        static int GetWordCount(string text)
        {
            string[] words = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        static void ExecuteMathOperation()
        {
            double firstNumber = ReadDoubleFromConsole("Enter the first number: ");
            string operation = ReadOperationFromConsole();
            double secondNumber = ReadDoubleFromConsole("Enter the second number: ");

            if (operation == "/" && secondNumber == 0)
            {
                Console.WriteLine("Division by zero is not allowed.");
            }
            else
            {
                double result = PerformOperation(firstNumber, secondNumber, operation);
                Console.WriteLine($"The result of {firstNumber} {operation} {secondNumber} is: {result}");
            }

            WaitForUser();
        }

        static double ReadDoubleFromConsole(string prompt)
        {
            double number;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write(prompt);
            }
            return number;
        }

        static string ReadOperationFromConsole()
        {
            Console.Write("Enter the operation (+, -, *, /): ");
            string operation = Console.ReadLine();

            while (operation != "+" && operation != "-" && operation != "*" && operation != "/")
            {
                Console.WriteLine("Invalid operation. Try again.");
                Console.Write("Enter a valid operation (+, -, *, /): ");
                operation = Console.ReadLine();
            }

            return operation;
        }

        static double PerformOperation(double num1, double num2, string op)
        {
            double result;
            switch (op)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    if (num2 == 0)
                        throw new InvalidOperationException("Division by zero is not allowed");
                    result = num1 / num2;
                    break;
                default:
                    throw new InvalidOperationException("Unsupported operation");
            }
            return result;
        }

        static void WaitForUser()
        {
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
