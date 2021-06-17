using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundcolorManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private colorRepository _colorRepository;
        private string _connectionString;

        public BackgroundcolorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _colorRepository = new colorRepository(connectionString);
            _parentUI = parentUI;
            _connectionString = connectionString;
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
                    _colorRepository.SetColor(0);
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
                _colorRepository.SetColor(choice);
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
       
    
