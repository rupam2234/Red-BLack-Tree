using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Black_Tree_Final
{
    class Nodes
    {
        private int data;
        private NodeColor color;
        private Nodes leftChild;
        private Nodes rightChild;
        private Nodes parent; 

        public Nodes(int data)
        {
            this.data = data;
            this.color = NodeColor.Red;
        }

        // using toString method to get a string that represent the object class 

        public override string ToString()
        {
            // toString method will give us the data (node values) whenever we implement that in the RB tree or any other tree

            return " " + this.data;
        }

        public int getData()
        {
            return data;
        }

        public void setData(int data)
        {
            this.data = data;
        }

        public NodeColor GetColor()
        {
            return color;
        }

        public void SetColor(NodeColor color)
        {
            this.color = color;
        }

        public Nodes getLeftchild()
        {
            return leftChild;
        }

        public void setLeftChild(Nodes leftChild)
        {
            this.leftChild = leftChild; 
        }

        public Nodes getRightChild()
        {
            return rightChild;
        }

        public void setRightChild(Nodes rightChild)
        {
            this.rightChild = rightChild;        
        }

        public Nodes getParent()
        {
            return parent;
        }

        public void setParent(Nodes parent)
        {
            this.parent = parent;
        }

    }
}
