using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
       
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
       
            _postId = postId;
        }
        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Note Management");
            Console.WriteLine(" 3) Add Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");


            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                   
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

        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"URL: {post.Url}");
            Console.WriteLine($"Publication Date: {post.PublishDateTime}"); 
           

        }
        //private void AddTag()
        //{
        //    Post post = _postRepository.Get(_postId);

        //    Console.WriteLine($"Which tag would you like to add to {author.FullName}?");
        //    List<Tag> tags = _tagRepository.GetAll();

        //    for (int i = 0; i < tags.Count; i++)
        //    {
        //        Tag tag = tags[i];
        //        Console.WriteLine($" {i + 1}) {tag.Name}");
        //    }
        //    Console.Write("> ");

        //    string input = Console.ReadLine();
        //    try
        //    {
        //        int choice = int.Parse(input);
        //        Tag tag = tags[choice - 1];
        //        _authorRepository.InsertTag(author, tag);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Invalid Selection. Won't add any tags.");
        //    }
        //}
    }
}
