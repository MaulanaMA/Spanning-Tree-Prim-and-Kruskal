using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MinimumSpanningTreeApp
{
    public partial class MainForm : Form
    {
        private List<(string, string, int)> graphData;
        private Dictionary<string, Point> positions;
        private Pen edgePen = new Pen(Color.Black, 2);
        private Random rand = new Random();

        public MainForm()
        {
            InitializeComponent();
            InitializeGraphData();
            positions = new Dictionary<string, Point>();
        }

        private void InitializeGraphData()
        {
            graphData = new List<(string, string, int)>
            {
                ("A", "b", 5),
                ("B", "C", 4),
                ("B", "F", 6),
                ("C", "F", 3),
                ("B", "E", 5),
                ("B", "D", 3),
                ("F", "E", 1),
                ("F", "I", 4),
                ("I", "H", 2),
                ("H", "G", 4),
                ("G", "D", 6),
                ("D", "A", 2),
                ("D", "E", 7),
                ("D", "H", 8),
                ("F", "H", 4),
            };
        }

        private void DrawGraph(List<(string, string, int)> graph)
        {
            if (positions == null)
            {
                positions = new Dictionary<string, Point>();
            }

            canvasPanel.Refresh(); // Clear previous drawing

            using (Graphics g = canvasPanel.CreateGraphics())
            {
                foreach ((string u, string v, int weight) in graph)
                {
                    if (!positions.TryGetValue(u, out Point posU) || !positions.TryGetValue(v, out Point posV))
                    {
                        // If a node in the graphData does not have a corresponding position in the positions dictionary, skip drawing the edge
                        continue;
                    }

                    g.DrawLine(edgePen, posU, posV);
                    Point textPos = new Point((posU.X + posV.X) / 2, (posU.Y + posV.Y) / 2);
                    g.DrawString(weight.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, textPos);

                    g.FillEllipse(Brushes.SkyBlue, posU.X - 20, posU.Y - 20, 40, 40);
                    g.DrawString(u, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(posU.X - 10, posU.Y - 5));

                    g.FillEllipse(Brushes.SkyBlue, posV.X - 20, posV.Y - 20, 40, 40);
                    g.DrawString(v, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(posV.X - 10, posV.Y - 5));
                }
            }
        }

        private Point GenerateRandomPosition(int minX, int minY, int maxX, int maxY)
        {
            int padding = 40; // Padding between nodes
            int retryAttempts = 50; // Maximum number of attempts to find a non-overlapping position

            for (int i = 0; i < retryAttempts; i++)
            {
                int x = rand.Next(minX, maxX);
                int y = rand.Next(minY, maxY);

                // Check if the generated position collides with any existing positions
                bool collision = positions.Values.Any(pos => Math.Abs(pos.X - x) < padding && Math.Abs(pos.Y - y) < padding);

                if (!collision)
                {
                    return new Point(x, y);
                }
            }

            // If we couldn't find a non-overlapping position after the maximum attempts, return a random position within the canvas bounds
            return new Point(rand.Next(minX, maxX), rand.Next(minY, maxY));
        }

        private void ShowGraphButton_Click(object sender, EventArgs e)
        {
            // Generate new positions for nodes when the "Show Graph" button is clicked
            foreach (var node in graphData)
            {
                if (!positions.ContainsKey(node.Item1))
                {
                    Point position = GenerateRandomPosition(50, 50, canvasPanel.Width - 50, canvasPanel.Height - 50);
                    positions[node.Item1] = position;
                }

                if (!positions.ContainsKey(node.Item2))
                {
                    Point position = GenerateRandomPosition(50, 50, canvasPanel.Width - 50, canvasPanel.Height - 50);
                    positions[node.Item2] = position;
                }
            }

            DrawGraph(graphData);
        }

        private List<(string, string, int)> FindMinimumSpanningTree(List<(string, string, int)> graph)
        {
            // Implement Prim's algorithm to find the MST

            // Create a list to store the MST
            List<(string, string, int)> mst = new List<(string, string, int)>();

            // Create a list to track visited nodes
            List<string> visited = new List<string>();

            // Choose the starting node (you can choose any node)
            string startNode = graph[0].Item1;
            visited.Add(startNode);

            // Continue until all nodes are visited
            while (visited.Count < positions.Count)
            {
                // Find the minimum-weight edge that connects a visited node to an unvisited node
                var minEdge = graph.Where(edge => visited.Contains(edge.Item1) && !visited.Contains(edge.Item2)
                                            || visited.Contains(edge.Item2) && !visited.Contains(edge.Item1))
                                   .MinBy(edge => edge.Item3);

                // Add the minEdge to the MST
                mst.Add(minEdge);

                // Mark the destination node of minEdge as visited
                string newNode = visited.Contains(minEdge.Item1) ? minEdge.Item2 : minEdge.Item1;
                visited.Add(newNode);
            }

            return mst;
        }

        private List<(string, string, int)> FindMinimumSpanningTreeKruskal(List<(string, string, int)> graph)
        {
            // Implement Kruskal's algorithm to find the MST

            // Create a list to store the MST
            List<(string, string, int)> mst = new List<(string, string, int)>();

            // Sort the edges in ascending order by weight
            var sortedEdges = graph.OrderBy(edge => edge.Item3).ToList();

            // Create a dictionary to track the parent of each node in the disjoint-set
            Dictionary<string, string> parent = new Dictionary<string, string>();

            // Create a function to find the parent of a node in the disjoint-set
            string Find(string node)
            {
                if (!parent.ContainsKey(node))
                {
                    parent[node] = node;
                }

                if (parent[node] != node)
                {
                    parent[node] = Find(parent[node]);
                }

                return parent[node];
            }

            // Create a function to union two nodes in the disjoint-set
            void Union(string node1, string node2)
            {
                parent[Find(node1)] = Find(node2);
            }

            // Iterate through each edge in the sorted order and add it to the MST if it doesn't create a cycle
            foreach (var (u, v, weight) in sortedEdges)
            {
                if (Find(u) != Find(v))
                {
                    mst.Add((u, v, weight));
                    Union(u, v);
                }
            }

            return mst;
        }

        private void RunPrimButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Find Minimum Spanning Tree using Prim's algorithm
                List<(string, string, int)> mst = FindMinimumSpanningTree(graphData);

                // Display MST in outputTextBox
                DisplayOutput("Minimum Spanning Tree (Prim Algorithm):\n", mst);

                // Redraw the graph with only MST edges
                DrawGraph(mst);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunKruskalButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Find Minimum Spanning Tree using Kruskal's algorithm
                List<(string, string, int)> mst = FindMinimumSpanningTreeKruskal(graphData);

                // Display MST in outputTextBox
                DisplayOutput("Minimum Spanning Tree (Kruskal Algorithm):\n", mst);

                // Redraw the graph with only MST edges
                DrawGraph(mst);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayOutput(string outputText, List<(string, string, int)> mst)
        {
            // Clear the outputTextBox before displaying new results
            outputTextBox.Clear();

            // Display the title of the algorithm
            outputTextBox.AppendText(outputText);

            int totalWeight = 0;

            // Display each edge in the MST on a new line
            foreach (var (u, v, weight) in mst)
            {
                outputTextBox.AppendText($"{u} - {v}  bobot: {weight}, \n");
                totalWeight += weight;
            }

            // Display the total weight of the MST at the end
            outputTextBox.AppendText($"\nTotal Bobot dari MSTnya adalah: {totalWeight}\n");
        }
    }
}