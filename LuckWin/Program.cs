﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        // Set the console's output encoding to UTF-8
        Console.OutputEncoding = Encoding.UTF8;

        // Create a Dictionary to store the slot symbols
        Dictionary<int, string> slotSymbols = new Dictionary<int, string>();
        slotSymbols.Add(1, "🍒"); // win 5 usd 
        slotSymbols.Add(2, "🍋"); // win 10 usd
        slotSymbols.Add(3, "🍊"); // win 20 usd 
        slotSymbols.Add(4, "🍇"); // win 50 usd
        slotSymbols.Add(5, "🍉"); // win 100 usd
        slotSymbols.Add(6, "🍎"); // win 200 usd
        slotSymbols.Add(7, "🍓"); // win 300 usd 
        slotSymbols.Add(8, "🍌"); // win 400 usd 
        slotSymbols.Add(9, "🍍"); // win 500 usd 
        slotSymbols.Add(10, "7️"); // win 1000 usd

        int money = 100; // Starting money

        Console.WriteLine("Press the spacebar to manually generate new symbols on the slot machine.");
        Console.WriteLine("Press the 'A' key to enable autoplay.");

        bool autoplayEnabled = false;

        while (true)
        {
            if (autoplayEnabled || Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                if (money > 0) // Check if the player has enough money to play
                {
                    Console.Clear();
                    drawSlot(slotSymbols, money);
                    money--; // Deduct $1 from the money for each press
                }
                else
                {
                    Console.WriteLine("Game over! You have run out of money.");
                    break;
                }
            }
            else if (Console.ReadKey(true).Key == ConsoleKey.A)
            {
                autoplayEnabled = true;
                Console.Clear();
                Console.WriteLine("Autoplay enabled. Generating new symbols automatically...");
                Console.WriteLine("Press any key to disable autoplay.");
            }
            else
            {
                Console.Clear();
            }

            if (autoplayEnabled)
            {
                Console.Clear();
                drawSlot(slotSymbols,money);
                Thread.Sleep(1000); // Delay between autoplay spins
            }
        }
    }

    static void drawSlot(Dictionary<int, string> slotSymbols,int money)
    {
        // Create an instance of Random class
        Random random = new Random();

        // Draw the slot machine with random symbols
        Console.WriteLine("Slot Machine");
        Console.WriteLine("-------------");

        List<int> symbolKeys = new List<int>(); // Store the randomly generated keys

        for (int i = 0; i < 3; i++)
        {
            int randomKey = RandomSymbolKey(slotSymbols);
            symbolKeys.Add(randomKey); // Store the generated key
            string randomSymbol = slotSymbols[randomKey];
            Console.Write($"+ {randomSymbol} ");
        }

        Console.WriteLine();
        Console.WriteLine("-------------");

        // Pass the symbolKeys list and slotSymbols dictionary to the checkWin function
        bool isWin = checkWin(symbolKeys, slotSymbols);

        if (isWin)
        {
            Console.WriteLine("Congratulations! You won!");
        }

        Console.WriteLine($"Money: ${money}"); // Display the current money balance
    }

    static int RandomSymbolKey(Dictionary<int, string> slotSymbols)
    {
        Random random = new Random();
        // Get a random key from the slotSymbols dictionary
        int randomKey = random.Next(1, slotSymbols.Count + 1);
        // Retrieve the corresponding symbol
        return randomKey;
    }

    static bool checkWin(List<int> symbolKeys, Dictionary<int, string> slotSymbols)
    {
        string[] symbols = new string[3];

        for (int i = 0; i < 3; i++)
        {
            int symbolKey = symbolKeys[i];
            symbols[i] = slotSymbols[symbolKey];
        }

        return symbols[0] == symbols[1] && symbols[1] == symbols[2];
    }
}

