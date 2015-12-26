﻿using System;
using System.IO;
using System.Reflection;

namespace Spectrum.Bootstrap
{
    public static class Loader
    {
        // relative to Distance.exe
        private static string _managerDllPath = "Distance_Data/Spectrum/Spectrum.Manager.dll";

        public static void StartManager()
        {
            foreach (var arg in Environment.GetCommandLineArgs())
            {
                if (arg == StartupArguments.AllocateConsole)
                {
                    ConsoleAllocator.Create();

                    var version = Assembly.GetAssembly(typeof(Loader)).GetName().Version;

                    Console.WriteLine($"Spectrum Extension System for Distance. Version {version.Major}.{version.Minor}.{version.Build}.{version.Revision}.");
                    Console.WriteLine("Verbose mode enabled. Remove '-console' command line switch to disable.");
                    Console.WriteLine("--------------------------------------------");
                }
            }

            if (!File.Exists(_managerDllPath))
            {
                Console.WriteLine($"[STAGE1] Spectrum: Can't find the plug-in manager at path {_managerDllPath}.");
                return;
            }

            try
            {
                var managerAssembly = Assembly.LoadFrom(_managerDllPath);
                var managerType = managerAssembly.GetType("Spectrum.Manager.Manager", false);

                if (managerType == null)
                {
                    Console.WriteLine("[STAGE1] Spectrum: Invalid plug-in manager assembly loaded.");
                    return;
                }
                Updater.ManagerObject = Activator.CreateInstance(managerType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[STAGE1] Spectrum: Critical exception handled. Read below:\n{ex}");
            }
        }
    }
}
