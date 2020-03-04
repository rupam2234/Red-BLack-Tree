using System;

namespace Red_Black_Tree_Final
{
    class RBTree : ITree
    {

        public Nodes root;

        // for traversal in the tree

        public void Traversal()
        {

            if (root != null)
            {
                inorderTraversal(root);
            }

        }

        // for inorder traversal 

        private void inorderTraversal(Nodes node)
        {
            if (node.getLeftchild() != null)
            {
                inorderTraversal(node.getLeftchild());
            }

            Console.WriteLine(node + " ");

            if (node.getRightChild() != null)
            {
                inorderTraversal(node.getRightChild());
            }
        }

        // ----------------------- traversal ends here ------------------------


        // for insertion of a node in a tree

        private Nodes insertIntoTree(Nodes root, Nodes node)
        {
            if (root == null)
            {
                return node;
            }

            if (node.getData() < root.getData())
            {
                root.setLeftChild(insertIntoTree(root.getLeftchild(), node));
                root.getLeftchild().setParent(root);
            }

            else if (node.getData() > root.getData())
            {
                root.setRightChild(insertIntoTree(root.getRightChild(), node));
                root.getRightChild().setParent(root);
            }
            return root;
        }

        public void Insert(int data)
        {
            Nodes node = new Nodes(data);

            root = insertIntoTree(root, node);

            // we will check if there is any kind of vioalation to the RB tree property

            fixViolatioon(node);
        }

        // ------------------- insertion ends here ---------------------


        // during the insertion if the RB property is violated anywhere we will fix it by rotating nodes and recoloring 

        private void fixViolatioon(Nodes node)
        {
            // first we create two references "ParentNode and GrandParent Node" and initialize them to null

            Nodes ParentNode = null;
            Nodes GrandParentNode = null;

            /*now we will iterate from the inserted node upto the root of the tree 
            to check if the nodes are following the properties of a Red black tree or not */

            // lets consider the node we have taken as perameter is not the root Node
            // Since it's not the root node the color of the node doesn't have to be black at all

            while (node != root && node.GetColor() != NodeColor.Black && node.getParent().GetColor() == NodeColor.Red)
            {
                // defining the parent node and grandParent node that previously set to null

                ParentNode = node.getParent();
                GrandParentNode = node.getParent().getParent();

                // if the parent node of the node that we would like to insert is leftchild of the GrandParent

                if (ParentNode == GrandParentNode.getLeftchild())
                {
                    Nodes uncle = GrandParentNode.getRightChild();

                    // now we will check if the uncle is present in the tree and whether the color of uncle is red!

                    if (uncle != null && uncle.GetColor() == NodeColor.Red)
                    {
                        // if the uncle is also red then it will violate the RB property so...

                        GrandParentNode.SetColor(NodeColor.Red);  // set grand parent color to Red
                        ParentNode.SetColor(NodeColor.Black);  // set parent color to black
                        uncle.SetColor(NodeColor.Black);  // set uncle color to black

                        // to check these coloring properties in other portion of the tree we will consider the node as grandparent

                        node = GrandParentNode;

                    }

                    //  if the uncle is black ...

                    else
                    {
                        // check if the inserted node is on the right side of the parent or not

                        if (node == ParentNode.getRightChild())
                        {
                            LeftRotation(ParentNode);
                            node = ParentNode;

                            //node.setLeftChild(ParentNode);

                            ParentNode = node.getParent();
                        }

                        // and if after the rotation the iserted node is on the lefthand side of the parent we will apply a right rotation on the grandParentNode

                        RightRotation(GrandParentNode);

                        // now we can swap the color between the grandparent and parent node to fix the coloring property

                        NodeColor tempColor = ParentNode.GetColor();
                        ParentNode.SetColor(GrandParentNode.GetColor());
                        GrandParentNode.SetColor(tempColor);
                        node = ParentNode;

                        // this complete code blcok will coverup the cases when the uncle is black 

                    }
                }

                // if the parent node of the node that we would like to insert is RightChild+ of the GrandParent

                else
                {
                    Nodes uncle = GrandParentNode.getLeftchild();

                    // when uncle is Red 

                    if (uncle != null && uncle.GetColor() == NodeColor.Red)
                    {
                        GrandParentNode.SetColor(NodeColor.Red);
                        ParentNode.SetColor(NodeColor.Black);
                        uncle.SetColor(NodeColor.Black);

                        // again to check for the full tree RB property setisfection

                        node = GrandParentNode;
                    }

                    else
                    {

                        // if the inserted node is leftChild of parentNode 

                        if (node == ParentNode.getLeftchild())
                        {
                            RightRotation(ParentNode);
                            node = ParentNode;
                            ParentNode = node.getParent();
                        }

                        // left rotation on the grand parent node

                        LeftRotation(GrandParentNode);

                        NodeColor tempColor = ParentNode.GetColor();
                        ParentNode.SetColor(GrandParentNode.GetColor());

                        GrandParentNode.SetColor(tempColor);

                        // after rotation node will come to the parent node position
                        node = ParentNode;
                    }


                }
            }

            // now at last check if the root node is black or not (important property of Red black tree)

            if (root.GetColor() == (NodeColor.Red))
            {
                root.SetColor(NodeColor.Black);
            }
        }

        // ---------------- RB tree property violation ends here -------------------


        // method to implement right rotation 

        private void RightRotation(Nodes node) // sometime the rotatable node is going to be the parent or grandparent of the node we have inserted
        {
            Console.WriteLine("Rotating to the right on node" + node);

            Nodes tempLeftNode = node.getLeftchild();

            // holding a temporary loaction which will help to hold the node after rotation 

            node.setLeftChild(tempLeftNode.getRightChild());

            if (node.getLeftchild() != null)
            {

                // to keep track of the parent node we need to set parent as well 

                node.getLeftchild().setParent(node);
            }

            // parent of inserted node is set to the parent of tempNode

            tempLeftNode.setParent(node.getParent());

            // if we find that the parent of tempNode is null set tempNode as parent 

            if (tempLeftNode.getParent() == null)
            {
                root = tempLeftNode;
            }

            else if (node == node.getParent().getLeftchild())
            {
                node.getParent().setLeftChild(tempLeftNode);
            }

            else
                node.getParent().setRightChild(tempLeftNode);

            tempLeftNode.setRightChild(node);
            node.setParent(tempLeftNode);
        }

        // ------------------ Right rotation ends here ------------------



        // Implementing leftRotaion  (symmetric case with the right rotation)

        private void LeftRotation(Nodes node)

        {
            Console.WriteLine("Rotating to the left on node" + node);

            Nodes tempRightNode = node.getRightChild();

            node.setRightChild(tempRightNode.getLeftchild());

            if (node.getRightChild() != null)
            {
                node.getRightChild().setParent(node);
            }

            tempRightNode.setParent(node.getParent());

            if (tempRightNode.getParent() == null)
            {
                root = tempRightNode;
            }

            else if (node == node.getParent().getLeftchild())
            {
                node.getParent().setLeftChild(tempRightNode);
            }

            else
                node.getParent().setRightChild(tempRightNode);

            tempRightNode.setLeftChild(node);
            node.setParent(tempRightNode);
        }

        // --------------- End of right rotation ----------------


        // For displaying 

        public void DisplayTree()
        {
            Console.WriteLine();
            Console.WriteLine("Currently available nodes in the tree: ");
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
                return;
            }
            if (root != null)
            {
                inorderTraversal(root);
            }
        }

        // --------------- display method ends here ------------ 


        // method to find a node 

        public Nodes Find(int key)
        {
            bool searchStatus = false;

            // We always start searching from the node 

            Nodes TempNode = root;

            Nodes item = null;

            // until we have not found the node

            while (searchStatus == false)
            {
                // if the rootnode is empty or there is no node at all

                if (TempNode == null)
                {
                    break;
                }

                // if the given data is lesser then the data of Rootnode then we will check in leftchild of the rootnode 

                if (key < TempNode.getData())
                {
                    TempNode = TempNode.getLeftchild();
                }

                // if the given data is lesser then the data of Rootnode then we will check in rightchild of the rootnode 

                else if (key > TempNode.getData())
                {
                    TempNode = TempNode.getRightChild();
                }

                // if the given data is matched with the current node

                if (key == TempNode.getData())
                {
                    searchStatus = true;
                    item = TempNode;
                }

                if (item == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Unable to find the value {0}", key);
                }
            }

            if (searchStatus == true)
            {
                Console.WriteLine();
                Console.WriteLine("{0} is in the tree", key);
            }

            return TempNode;
        }

        // ---------------- end search method ----------------

        
            
        // to delete a node from the tree

        public void Delete(int key)
        {
            //first find the node in the tree to delete and assign to item pointer/reference

            Nodes itemToDelete = Find(key);

            Nodes nodeHolde1 = null;

            Nodes nodeHolder2 = null;


            // if nothing to delete then display this massage 

            if (itemToDelete == null)
            {
                Console.WriteLine("No node is avalilable to delete");
                return;
            }

            // check whether the node to deleted has any left or right child or not

            if (itemToDelete.getLeftchild() == null || itemToDelete.getRightChild() == null)
            {
                nodeHolder2 = itemToDelete;
            }

            else
            {
                nodeHolder2 = TreeSuccessor(itemToDelete); //if the rightchild and leftchild has value we need to find the sucessor 
            }

            // assign temp1 to the rightchild or leftchild of the temp2 which holds itemToDelete

            if (nodeHolder2.getLeftchild() != null)
            {
                nodeHolde1 = nodeHolder2.getLeftchild();
            }

            else if(nodeHolder2.getRightChild() != null)
            {
                nodeHolde1 = nodeHolder2.getRightChild();
            }

            // to keep track of the parent node  

            if (nodeHolde1 != null)
            {
                nodeHolder2 = nodeHolde1.getParent();
            }

            // 

            if (nodeHolder2.getParent() == null)
            {
                root = nodeHolde1;
            }

            else if (nodeHolder2 == nodeHolder2.getParent().getLeftchild())
            {
                nodeHolde1 = nodeHolder2.getParent().getLeftchild();
            }

            else
            {
                nodeHolde1 = nodeHolder2.getParent().getRightChild();
            }

            if (nodeHolder2 != itemToDelete)
            {
                nodeHolder2.setData(itemToDelete.getData());  
            }

            if (nodeHolder2.GetColor() == NodeColor.Black)
            {
                DeleteFix(nodeHolde1);
            }

        }

        // ------------------ Delete Node ends here ------------------


        // for find the sucessor node that is required duirng the deletion of a node

        public Nodes TreeSuccessor(Nodes CurrentNode)
        {

            // if the current nodes left child is present 

            if (CurrentNode.getLeftchild() != null)
            {

                // apply find minimum node method

                return Minimum(CurrentNode);  // applying minimum node finding method
            }
            else
            {
                Nodes temp = CurrentNode.getParent();
                while (temp != null && CurrentNode == temp.getRightChild())
                {
                    CurrentNode = temp;
                    temp = temp.getParent();
                }
                return temp;
            }
        }

        // ------------------ endSucessor ------------------ 


        // find minimum of a tree

        private Nodes Minimum(Nodes GivenNode)
        {

            // if the provided node's leftchild's leftchild is present then traverse to the leftchild of the current

            while (GivenNode.getLeftchild().getLeftchild() != null)
            {
                GivenNode = GivenNode.getLeftchild();
            }

            // if leftchild's rightchild is present then set givenNode to current's leftchild's rightchild 
            if (GivenNode.getLeftchild().getRightChild() != null)
            {
                GivenNode = GivenNode.getLeftchild().getRightChild();
            }
            return GivenNode;

            // ------------- end Minimum method ----------------
        }



        // method to fix RB tree after deletion

        private void DeleteFix(Nodes node)
        {
            // when the given node is not empty and not the root as well (ofcourse the color shuold be black)

            while (node != null && node != root && node.GetColor() == NodeColor.Black)
            {

                // if the node is leftchild of parentNode
                if (node == node.getParent().getLeftchild())
                {

                    // Assign newly created Node is the rightchild of the parent of the node 
                    Nodes tempNode = node.getParent().getRightChild();

                    //if tempnode color is red the code block will setisfy the case 1 
                    if (tempNode.GetColor() == NodeColor.Red)
                    {
                        tempNode.SetColor(NodeColor.Black);
                        node.getParent().SetColor(NodeColor.Red);
                        LeftRotation(node.getParent());
                        tempNode = node.getParent().getRightChild();

                        // now we can delete the "node" without violating the RB property

                    }

                    // if both the left and right hand child nodes has black color then the parent node will be recolored as red

                    if (tempNode.getLeftchild().GetColor() == NodeColor.Black && tempNode.getRightChild().GetColor() == NodeColor.Black)
                    {
                        tempNode.SetColor(NodeColor.Red);

                    }


                    // if only the rightchild has black color

                    else if (tempNode.getRightChild().GetColor() == NodeColor.Black)
                    {
                        tempNode.getLeftchild().SetColor(NodeColor.Black);
                        tempNode.SetColor(NodeColor.Red);
                        RightRotation(tempNode);
                        tempNode = node.getParent().getRightChild();

                    }

                    tempNode.SetColor(node.getParent().GetColor());
                    node.getParent().SetColor(NodeColor.Black);
                    tempNode.getRightChild().SetColor(NodeColor.Black);

                    LeftRotation(node.getParent());
                    node = root;
                }
            }

        }

        // ----------------- deleteFix ends here ----------------------

    }
}