using Grimoire.Services;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var spell in Spells.Wizard)
                Console.WriteLine(spell.Name);
        }
    }
}
