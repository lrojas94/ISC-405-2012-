﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema_3._2
{
    class Program
    {
        static int count = 0;
        public static bool colorBFS(Node n) {
            Color colorToPaint; //Actual color to paint.
            Stack<Node> s = new Stack<Node>();
            if(n.color == Color.None)
                n.color = Color.Blue;
            s.Push(n);
            while (s.Count != 0) {
                Node parent = s.Pop();
                if (parent.check)
                    continue;
                count++;//keep track of the number of operations done (Testing purposes).
                parent.check = true;
                if (parent.color == Color.Red)
                    colorToPaint = Color.Blue;
                else
                    colorToPaint = Color.Red; 
                for (int i = 0; i < parent.children.Count; i++) {
                    Node child = parent.children[i];
                    if (child.color == parent.color)
                        return false;

                    if (child.color == Color.None) {
                        child.color = colorToPaint;
                        s.Push(child);   
                    }
                }

                
            }

            return true;
        }
        static void Main(string[] args)
        {
            List<Node> tree = new List<Node>();
            String[] nums;
            nums = Console.ReadLine().Split(' ');
            int elems = Convert.ToInt32(nums[0]),restrictions = Convert.ToInt32(nums[1]);
            for (int i = 0; i < elems; i++) {
                Node n = new Node(i + 1);
                tree.Add(n);
            }

            for (int i = 0; i < restrictions; i++)
            {
                String[] r = Console.ReadLine().Split(' ');
                int a = Convert.ToInt32(r[0]), b = Convert.ToInt32(r[1]);
                tree[a - 1].children.Add(tree[b - 1]); //Add to children. Make a directed graph.
            }
            //Since the graph is not fully connected, better make sure to paint all nodes.
            for (int i = 0; i < tree.Count; i++) {
                if (!colorBFS(tree[i])) {
                    Console.WriteLine("No solution found...");
                    return;
                }
            }

            //Print blue ones:
            List<Node> Blues = new List<Node>(), Reds = new List<Node>();
            for (int i = 0; i < tree.Count; i++)
            {
                Node n = tree[i];
                if (n.color == Color.Blue)
                    Blues.Add(n);
                else
                    Reds.Add(n);
            }

            Console.WriteLine(Blues.Count);
            for (int i = 0; i < Blues.Count; i++)
            {
                Node n = Blues[i];
                Console.Write(n.value + " ");
            }
            Console.WriteLine();
            Console.WriteLine(Reds.Count);
            for (int i = 0; i < Reds.Count; i++)
            {
                Node n = Reds[i];
                Console.Write(n.value + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Number of ops: " + count);
            Console.Read();

            
        }
    }
}