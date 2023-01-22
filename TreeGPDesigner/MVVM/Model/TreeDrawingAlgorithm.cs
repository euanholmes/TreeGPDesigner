using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGPDesigner.MVVM.Model
{
    //Author: Rachel Lim, Code found here: https://rachel53461.wordpress.com/2014/04/20/algorithm-for-drawing-trees/
    //Used for finding tree node coordinates which are then plotted in ui.

    public static class TreeDrawingAlgorithm
    {
        private static int nodeSize = 1;
        private static float siblingDistance = 0.0F;
        private static float treeDistance = 0.0F;

        public static void CalculateNodePositions(Node rootNode)
        {
            // initialize node x, y, and mod values
            InitializeNodes(rootNode, 0);

            // assign initial X and Mod values for nodes
            CalculateInitialX(rootNode);

            // ensure no node is being drawn off screen
            CheckAllChildrenOnScreen(rootNode);

            // assign final X values to nodes
            CalculateFinalPositions(rootNode, 0);
        }

        // recusrively initialize x, y, and mod values of nodes
        private static void InitializeNodes(Node node, int depth)
        {
            node.XPos = -1;
            node.YPos = depth;
            node.Mod = 0;

            //euan add - might be pointless exact same as YPos already there.
            node.DepthLevel = depth;

            foreach (var child in node.ChildNodes)
            {
                child.Parent = node;
                InitializeNodes(child, depth + 1);
            }
        }

        private static void CalculateFinalPositions(Node node, float modSum)
        {
            node.XPos += modSum;
            modSum += node.Mod;

            foreach (var child in node.ChildNodes)
                CalculateFinalPositions(child, modSum);

            if (node.ChildNodes.Count == 0)
            {
                node.Width = node.XPos;
                node.Height = node.YPos;
            }
            else
            {
                node.Width = node.ChildNodes.OrderByDescending(p => p.Width).First().Width;
                node.Height = node.ChildNodes.OrderByDescending(p => p.Height).First().Height;
            }
        }

        private static void CalculateInitialX(Node node)
        {
            foreach (var child in node.ChildNodes)
                CalculateInitialX(child);

            // if no children
            if (node.ChildNodes.Count == 0)
            {
                // if there is a previous sibling in this set, set X to prevous sibling + designated distance
                if (!node.IsLeftMost())
                    node.XPos = node.GetPreviousSibling().XPos + nodeSize + siblingDistance;
                else
                    // if this is the first node in a set, set X to 0
                    node.XPos = 0;
            }
            // if there is only one child
            else if (node.ChildNodes.Count == 1)
            {
                // if this is the first node in a set, set it's X value equal to it's child's X value
                if (node.IsLeftMost())
                {
                    node.XPos = node.ChildNodes[0].XPos;
                }
                else
                {
                    node.XPos = node.GetPreviousSibling().XPos + nodeSize + siblingDistance;
                    node.Mod = node.XPos - node.ChildNodes[0].XPos;
                }
            }
            else
            {
                var leftChild = node.GetLeftMostChild();
                var rightChild = node.GetRightMostChild();
                var mid = (leftChild.XPos + rightChild.XPos) / 2;

                if (node.IsLeftMost())
                {
                    node.XPos = mid;
                }
                else
                {
                    node.XPos = node.GetPreviousSibling().XPos + nodeSize + siblingDistance;
                    node.Mod = node.XPos - mid;
                }
            }

            if (node.ChildNodes.Count > 0 && !node.IsLeftMost())
            {
                // Since subtrees can overlap, check for conflicts and shift tree right if needed
                CheckForConflicts(node);
            }

        }

        private static void CheckForConflicts(Node node)
        {
            var minDistance = treeDistance + nodeSize;
            var shiftValue = 0F;

            var nodeContour = new Dictionary<int, float>();
            GetLeftContour(node, 0, ref nodeContour);

            var sibling = node.GetLeftMostSibling();
            while (sibling != null && sibling != node)
            {
                var siblingContour = new Dictionary<int, float>();
                GetRightContour(sibling, 0, ref siblingContour);

                for (int level = node.YPos + 1; level <= Math.Min(siblingContour.Keys.Max(), nodeContour.Keys.Max()); level++)
                {
                    var distance = nodeContour[level] - siblingContour[level];
                    if (distance + shiftValue < minDistance)
                    {
                        shiftValue = minDistance - distance;
                    }
                }

                if (shiftValue > 0)
                {
                    node.XPos += shiftValue;
                    node.Mod += shiftValue;

                    CenterNodesBetween(node, sibling);

                    shiftValue = 0;
                }

                sibling = sibling.GetNextSibling();
            }
        }

        private static void CenterNodesBetween(Node leftNode, Node rightNode)
        {
            var leftIndex = leftNode.Parent.ChildNodes.IndexOf(rightNode);
            var rightIndex = leftNode.Parent.ChildNodes.IndexOf(leftNode);

            var numNodesBetween = (rightIndex - leftIndex) - 1;

            if (numNodesBetween > 0)
            {
                var distanceBetweenNodes = (leftNode.XPos - rightNode.XPos) / (numNodesBetween + 1);

                int count = 1;
                for (int i = leftIndex + 1; i < rightIndex; i++)
                {
                    var middleNode = leftNode.Parent.ChildNodes[i];

                    var desiredX = rightNode.XPos + (distanceBetweenNodes * count);
                    var offset = desiredX - middleNode.XPos;
                    middleNode.XPos += offset;
                    middleNode.Mod += offset;

                    count++;
                }

                CheckForConflicts(leftNode);
            }
        }

        private static void CheckAllChildrenOnScreen(Node node)
        {
            var nodeContour = new Dictionary<int, float>();
            GetLeftContour(node, 0, ref nodeContour);

            float shiftAmount = 0;
            foreach (var y in nodeContour.Keys)
            {
                if (nodeContour[y] + shiftAmount < 0)
                    shiftAmount = (nodeContour[y] * -1);
            }

            if (shiftAmount > 0)
            {
                node.XPos += shiftAmount;
                node.Mod += shiftAmount;
            }
        }

        private static void GetLeftContour(Node node, float modSum, ref Dictionary<int, float> values)
        {
            if (!values.ContainsKey(node.YPos))
                values.Add(node.YPos, node.XPos + modSum);
            else
                values[node.YPos] = Math.Min(values[node.YPos], node.XPos + modSum);

            modSum += node.Mod;
            foreach (var child in node.ChildNodes)
            {
                GetLeftContour(child, modSum, ref values);
            }
        }

        private static void GetRightContour(Node node, float modSum, ref Dictionary<int, float> values)
        {
            if (!values.ContainsKey(node.YPos))
                values.Add(node.YPos, node.XPos + modSum);
            else
                values[node.YPos] = Math.Max(values[node.YPos], node.XPos + modSum);

            modSum += node.Mod;
            foreach (var child in node.ChildNodes)
            {
                GetRightContour(child, modSum, ref values);
            }
        }
    }
}
