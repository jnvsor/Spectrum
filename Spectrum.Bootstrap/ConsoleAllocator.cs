﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Spectrum.Bootstrap
{
    class ConsoleAllocator
    {
        private static StreamWriter _outputWriter;
        private static TextWriter _originalStream;

        private static bool _allocated;

        public static void Create()
        {
            if (_allocated)
                return;

            AllocConsole();

            _originalStream = Console.Out;
            _outputWriter = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            Console.SetOut(_outputWriter);

            _allocated = true;
        }

        public static void Destroy()
        {
            if (!_allocated)
                return;

            FreeConsole();

            Console.SetOut(_originalStream);
            _allocated = false;
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}
