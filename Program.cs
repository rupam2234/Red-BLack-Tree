 using System;

namespace Red_Black_Tree_Final
{
    class Program
    {
        static void Main(string[] args)
        {

            RBTree redBalckTree = new RBTree();
            redBalckTree.Insert(10);
            redBalckTree.Insert(5);
            redBalckTree.Insert(1);
            redBalckTree.Insert(14);
            Console.WriteLine();

            Console.WriteLine("The new root is: " + redBalckTree.root);

            redBalckTree.DisplayTree();

            redBalckTree.Find(5);
            //redBalckTree.Find(11);

            //Nodes new = new Nodes();

            redBalckTree.TreeSuccessor(5);


        }
    }
}
