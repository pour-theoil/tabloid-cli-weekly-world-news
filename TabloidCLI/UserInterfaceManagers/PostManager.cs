using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;
        private AuthorRepository _authorRepository;
        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
            _authorRepository = new AuthorRepository(connectionString);
        }


        // Set up Post Management Menu
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Management Menu");
            Console.WriteLine(" 1) Add a Post");
            Console.WriteLine(" 2) List Posts");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Add();
                    return this;
                case "2":
                    List();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Publication Date: ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            Console.Write("");
            //ListAuthors();

            Console.WriteLine("Select an Author: ");
            post.Author = ChooseAuthor();

            Console.WriteLine("Select a Blog: ");
            post.Blog = ChooseBlog();

            _postRepository.Insert(post);
            //Console.WriteLine("Select a Blog: ");
            //post.Blog = 
        }

        private Author ChooseAuthor(string prompt = null)
        {

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        // !! NEED BLOG REPOSITORY BEFORE I CAN PROCEED !!

        private Blog ChooseBlog(string prompt = null)
        {
            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach(Post post in posts)
            {
                Console.Write($"{post.Title} ");
                Console.WriteLine(post.Url);
            }
        }
    }
}
