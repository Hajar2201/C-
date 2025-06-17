using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue");
            Console.WriteLine("Entrez une opération");

            string input = Console.ReadLine();

            if (TryParseOperation(input, out double result))
            {
                Console.WriteLine($"Résultat: {result}");
            }
            else
            {
                Console.WriteLine("Opération invalide. Veuillez entrer une opération valide.");
            }
        }

        static bool TryParseOperation(string input, out double result)
        {
            result = 0;

            string[] parts = input.Split(' ');

            if (parts.Length != 3)
                return false;

            if (!double.TryParse(parts[0], out double operand1) || !double.TryParse(parts[2], out double operand2))
                return false;

            switch (parts[1])
            {
                case "+":
                    result = operand1 + operand2;
                    break;
                case "-":
                    result = operand1 - operand2;
                    break;
                case "*":
                    result = operand1 * operand2;
                    break;
                case "/":
                    if (operand2 != 0)
                        result = operand1 / operand2;
                    else
                        return false; // Division par zéro
                    break;
                default:
                    return false; // Opérateur invalide
            }

            return true;
        }
    }
}
