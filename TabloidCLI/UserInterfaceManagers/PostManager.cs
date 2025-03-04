﻿using System;
using System.Collections.Generic;
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
            _blogRepository = new BlogRepository(connectionString);
        }

        /// <summary>
        /// Set up Post Management Menu
        /// </summary>
        /// <returns>
        /// Returns Menu for post management. 
        /// Selecting an option executes its specified method.
        /// </returns>
        public IUserInterfaceManager Execute()
        {
            //Print menu for post management
            Console.WriteLine("Post Management Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add a Post");
            Console.WriteLine(" 3) Edit a Post");
            Console.WriteLine(" 4) Delete a Post");
            Console.WriteLine(" 5) View post details");
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
                case "5":
                    Post post = ChoosePost();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                    
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

      /// <summary>
      /// Adds new post object to the Post Table
      /// </summary>
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

            Console.WriteLine("Select an Author: ");
            post.Author = ChooseAuthor();

            Console.WriteLine("Select a Blog: ");
            post.Blog = ChooseBlog();

            _postRepository.Insert(post);
        }
        /// <summary>
        /// Fetches all authors and prints a list of the authors
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        private Author ChooseAuthor(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please Select an Author: ";
            }

            Console.WriteLine(prompt);

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
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        /// <summary>
        /// Choose Blog
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>

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
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Post ChoosePost(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            //loop over posts and print numbered list to choose from 
            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($"{i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        /// <summary>
        /// Edit a post
        /// </summary>
        private void Edit()
        {
            Post postToEdit = ChoosePost("Please choose a Post to edit");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Post Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New Post URL (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.WriteLine("Select New Author: ");
            postToEdit.Author = ChooseAuthor();

            Console.WriteLine("Select New Blog ");
            postToEdit.Blog = ChooseBlog();

            Console.WriteLine("Select New Date d/m/yy (blank to leave unchanged): ");
            string date = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(date))
            {
                postToEdit.PublishDateTime = DateTime.Parse(date);
            }

            _postRepository.Update(postToEdit);
        }
        /// <summary>
        /// List all post names and URLs
        /// </summary>
        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.Write($"{post.Title} ");
                Console.WriteLine(post.Url);
            }
        }
        /// <summary>
        /// Executes Delete function in the postRepository
        /// </summary>
        private void Remove()
        {
            Post postToDelete = ChoosePost("Which post would you like to remove?");
            if(postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }


    }
}
