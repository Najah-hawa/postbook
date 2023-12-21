/*
    skapa en gäst bok där man kan lägga till titel och innehåll för varje post
    The datafile,'book.json', created is in the format of Json.

    Written by Najah hawa / Mid Sweden University
*/
using System;
using System.Collections.Generic;
using System.IO;

using System.Text.Json;

namespace posts
{
    public class Postbook
    {
        private string filename = @"book.json";
        private List<Post> posts = new List<Post>();
        
        //metod för att läsa posts som finns i jsonfil. 
        public Postbook(){ 
            if(File.Exists(@"book.json")==true){ 
                string? jsonString = File.ReadAllText(filename);
               posts = JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }
        //metod för att lägga till post 
        public Post addPost(Post post){
            posts.Add(post);
            marshal();         
            return post;
        }
        //metod för att radera post
        public int delPost(int index){
            posts.RemoveAt(index);
            marshal();
            return index;
        }

        //metod för att return posts från loggboken
        public List<Post> getPosts(){
            return posts;
        }
        //metod för att lägga post i json fil
        private void marshal()
        {
            // Serialize all the objects and save to file
            var jsonString = JsonSerializer.Serialize(posts);
            File.WriteAllText(filename, jsonString);
        }
    }

    public class Post
    {
        private string? owner;        
        public string? Owner
        {
            set {this.owner = value;}
            get {return this.owner;}
        }


        private string? innehall;        
        public string? Innehall
        {
            set {this.innehall = value;}
            get {return this.innehall;}
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
 
            Postbook postbook = new Postbook();
            int i=0;

            while(true){
                Console.Clear();Console.CursorVisible = false;
                Console.WriteLine("Välkommen till gästboken\n\n");
                Console.WriteLine("1. Lägg till post");
                Console.WriteLine("2. Ta bort post");
                Console.WriteLine("X. Avsluta\n");

                //läsa in posts som finns i json fil
                i=0;
                 foreach(Post post in postbook.getPosts()){
                 Console.WriteLine("[" + i++ + "] " + post.Owner + ": " + post.Innehall);
                }
             

                int inp = (int) Console.ReadKey(true).Key;
                switch (inp) {
                    case '1':
                      Console.Clear();
                        Console.CursorVisible = true; 
                        Post obj = new Post();

                        Console.Write("Ange ägarens namn för post: ");
                        string? text = Console.ReadLine();
                        obj.Owner = text;
                        if(String.IsNullOrEmpty(text)) { Console.WriteLine ("is null or empty, posts sparas inte. Vänligen försök igen" ) ;   }
        
                        Console.Write("Ange innehall: ");
                        string? innehall = Console.ReadLine();
                       // om innehåll är mindre än 3 bokstäver sparas inte inlägg 
                        int length = innehall.Length;
                        if (length < 3) { break ;};
                         obj.Innehall = innehall;
                        if(!String.IsNullOrEmpty(text)) postbook.addPost(obj); 
                      
                        break;
                    case '2': 
                      Console.Clear();
                        Console.CursorVisible = true;
                        Console.Write("Ange index att radera: ");
                        //visa posts i loggboken för att visa index som man vill radera
                      i=0;
                      foreach(Post post in postbook.getPosts()){
                      Console.WriteLine("[" + i++ + "] " + post.Owner + post.Owner);
                       }
                        string? index = Console.ReadLine();
                        postbook.delPost(Convert.ToInt32(index));

                        break;
                  
                    
                }
 
            }

        }
    }
}
