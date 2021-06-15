using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundcolorManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;


        public BackgroundcolorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute()
        {

            Console.WriteLine(" 1) Change Backgound Color");
            Console.WriteLine(" 2) Reset Background Color");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ChangeColor();
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "3":

                    return this;
                case "4":

                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void ChangeColor()
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            for(int i = 0; i < 6; i++)
            {
                Console.ForegroundColor = colors[i];
                Console.WriteLine($"{i}) {colors[i]}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Choose new background color: ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                
                Console.BackgroundColor = colors[choice];
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
            }
        }
    }
}
       
    
