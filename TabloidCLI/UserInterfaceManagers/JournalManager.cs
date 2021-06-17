using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add Journal");
            Console.WriteLine(" 3) Edit Journal");
            Console.WriteLine(" 4) Remove Journal");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> entries = _journalRepository.GetAll();
            ConsoleKeyInfo cki;
            int i = 0;
            Console.WriteLine($"Title: {entries[i].Title}");
            Console.WriteLine($"Date: {entries[i].CreateDateTime}");
            Console.WriteLine($"Content: {entries[i].Content}");
            do
            {

                cki = Console.ReadKey();

                if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (i >= entries.Count-1)
                    {
                        i = -1;
                    }
                    i++;
                    Console.WriteLine($"Title: {entries[i].Title}");
                    Console.WriteLine($"Date: {entries[i].CreateDateTime}");
                    Console.WriteLine($"Content: {entries[i].Content}");
                }
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (i == 0)
                    {
                        i = entries.Count;
                    }
                    i--;
                    Console.WriteLine($"Title: {entries[i].Title}");
                    Console.WriteLine($"Date: {entries[i].CreateDateTime}");
                    Console.WriteLine($"Content: {entries[i].Content}");
                }


            } while (cki.Key != ConsoleKey.Enter);
        }
        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Journal Entry:";
            }

            Console.WriteLine(prompt);

            List<Journal> entries = _journalRepository.GetAll();

            for (int i = 0; i < entries.Count; i++)
            {
                Journal journal = entries[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return entries[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title: ");
            journal.Title = Console.ReadLine();

            Console.Write("Content: ");
            journal.Content = Console.ReadLine();

            journal.CreateDateTime = DateTime.Now;


            _journalRepository.Insert(journal);
        }

        private void Remove()
        {
            Journal entryToDelete = Choose("Which journal entry would you like to remove?");
            if (entryToDelete != null)
            {
                _journalRepository.Delete(entryToDelete.Id);
            }
        }

        private void Edit()
        {
            Journal entryToEdit = Choose("Which journal entry would you like to edit?");
            if (entryToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                entryToEdit.Title = title;
            }
            Console.Write("New entry content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                entryToEdit.Content = content;
            }

            entryToEdit.CreateDateTime = DateTime.Now;




            _journalRepository.Update(entryToEdit);
        }

    }
}