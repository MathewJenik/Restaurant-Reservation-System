using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Logging;
using T2RMSWS.Data;
using System.Collections;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

namespace T2RMSWS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                throw;
            }
            

            //----DOCUMENTATION

            //To binary file

            //const string FileName = @"../../../SavedSittingType.bin";
            //SittingType sittingType = new SittingType();
            //sittingType.Description = "Breakfast";
            //sittingType.Id = 1;
            //if (File.Exists(FileName))
            //{
            //    Console.WriteLine("Reading saved file");
            //    Stream openFileStream = File.OpenRead(FileName);
            //    BinaryFormatter deserializer = new BinaryFormatter();
            //    sittingType = (SittingType)deserializer.Deserialize(openFileStream);
            //    sittingType.Description = "Special Breakfast";
            //    openFileStream.Close();
            //}
            //Stream SaveFileStream = File.Create(FileName);
            //BinaryFormatter serializer = new BinaryFormatter();
            //serializer.Serialize(SaveFileStream, sittingType);
            //SaveFileStream.Close();

            //doubly linked list 

            //SittingStatus opened = new SittingStatus();
            //opened.Id = 1;
            //opened.Description = "Opened";
            //SittingStatus closed = new SittingStatus();
            //closed.Id = 2;
            //closed.Description = "Closed";
            //SittingStatus bookedOut = new SittingStatus();
            //bookedOut.Id = 3;
            //bookedOut.Description = "Booked Out";

            //SittingStatus[] ss = { opened, closed, bookedOut };
            //LinkedList<SittingStatus> allSittingStatuses = new LinkedList<SittingStatus>(ss);

            ////create another status
            //SittingStatus privateEvent = new SittingStatus();
            //privateEvent.Id = 4;
            //privateEvent.Description = "Private Event";

            ////add it to the end of the doubly linked list
            //allSittingStatuses.AddLast(privateEvent);


            ////hashing data structure

            ////create hash table for areas
            //Hashtable areas = new Hashtable();

            ////add all areas to hashtable
            ////key == description 
            ////value == id
            //areas.Add("Main", "1");
            //areas.Add("Outside", "3");
            //areas.Add("Balcony", "4");

            ////change value (id) of the Main Area
            //areas["Main"] = "2";

        }

        //binary tree

        //public class Node
        //{
        //    public int Data { get; set; }
        //    public Node Left { get; set; }
        //    public Node Right { get; set; }
        //    public Node() {}
        //    public Node(int data)
        //    {
        //        this.Data = data;
        //    }
        //}
        //public class BinaryTree
        //{
        //    private Node _root;
        //    public BinaryTree()
        //    {
        //        _root = null;
        //    }
        //    public void Insert(int data)
        //    {
        //        // 1. If the tree is empty, return a new, single node 
        //        if (_root == null)
        //        {
        //            _root = new Node(data);
        //            return;
        //        }
        //        // 2. Otherwise, recur down the tree 
        //        InsertRec(_root, new Node(data));
        //    }
        //    private void InsertRec(Node root, Node newNode)
        //    {
        //        if (root == null)
        //            root = newNode;

        //        if (newNode.Data < root.Data)
        //        {
        //            if (root.Left == null)
        //                root.Left = newNode;
        //            else
        //                InsertRec(root.Left, newNode);
        //        }
        //        else
        //        {
        //            if (root.Right == null)
        //                root.Right = newNode;
        //            else
        //                InsertRec(root.Right, newNode);
        //        }
        //    }
        //    private void DisplayTree(Node root)
        //    {
        //        if (root == null) return;

        //        DisplayTree(root.Left);
        //        System.Console.Write(root.Data + " ");
        //        DisplayTree(root.Right);
        //    }
        //    public void DisplayTree()
        //    {
        //        DisplayTree(_root);
        //    }

        //}

        //----END of Documentation

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
