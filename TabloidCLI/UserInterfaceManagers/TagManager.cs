﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;
        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            
            _parentUI = parentUI;
            //create a field using the connnection string and tag repo
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
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
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine(tag);
            }
        }

        private void Add()
        {

            Console.WriteLine("New Tag");
            Tag tag = new Tag();

            Console.Write("Name: ");
            tag.Name = Console.ReadLine();



            _tagRepository.Insert(tag);

        }

        private void Edit()
        {
            Tag tagToEdit = Choose("Which tag would you like to edit?");
            if (tagToEdit == null)
            {
                return;
            }
            Console.WriteLine();
            Console.Write("New name for tag(blank to leave unchanged: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                tagToEdit.Name = name;
            }

            _tagRepository.Update(tagToEdit);
        }
        /// <summary>
        /// Lists all of the tag options to choose from
        /// </summary>
      
        /// <returns></returns>
        private Tag Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Tag: ";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name} ");
            }
            Console.WriteLine("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void Remove()
        {
            Tag tagToDelete = Choose("Which tag would you like to delete?");
            if(tagToDelete != null)
            {
                _tagRepository.Delete(tagToDelete.Id);
            }
        }
    }
}
