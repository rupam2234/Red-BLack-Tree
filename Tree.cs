using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Black_Tree_Final
{
    public interface ITree
    {
        public void Traversal();

        public void Insert(int data);

        public void DisplayTree();

        public void Delete(int key);
    }
}
