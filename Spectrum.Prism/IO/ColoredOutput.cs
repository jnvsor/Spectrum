﻿using System;

namespace Spectrum.Prism.IO
{
    public class ColoredOutput
    {
        public static void WriteError(string message)
        {
            WriteColoredText($"[!] {message}", ConsoleColor.Red);
        }

        public static void WriteSuccess(string message)
        {
            WriteColoredText($"[+] {message}", ConsoleColor.Green);
        }

        public static void WriteInformation(string message)
        {
            WriteColoredText($"[i] {message}", ConsoleColor.White);
        }

        private static void WriteColoredText(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
