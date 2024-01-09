using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Graphs
{
    public partial class Form1 : Form
    {
        string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        Random rand = new Random();
        Graphics g;
        Pen pen = new Pen(Color.Black, 4);
        
        bool isVertexWait = false;
        int activeVertex = 1;
        int sourseVertex = 1;

        bool IsClicked = false;

        List<List<int>> matrix = new List<List<int>> { new List<int> { 0 } };
        List<List<int>> connections = new List<List<int>>();
        List<List<int>> distancesList = new List<List<int>>();

        List<List<List<int>>> paths = new List<List<List<int>>>();

        List<List<int>> cycles = new List<List<int>>();
        int cycleInd = 0;

        List<int> jointList = new List<int>();

        int counter;

        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            pen.EndCap = LineCap.ArrowAnchor;
        }

        private string GetLetter(int n)
        {
            string res = "";
            while(n > 0)
            {
                if (n > 26) res += "A";
                else res += ABC[n - 1];
                n -= 26;
            }
            return res;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (redact.Checked)
            {
                if (isVertexWait)
                {
                    isVertexWait = false;
                    Controls.Find(matrix[0][activeVertex].ToString(), true)[0].BackColor = SystemColors.Control;
                    return;
                }
                string s = AddVertex().ToString();
                CreateButton(new Point(e.X, e.Y), s);
            }
        }

        private void CreateButton(Point pos, string text)
        {
            Button b = new Button();
            b.MouseUp += new System.Windows.Forms.MouseEventHandler(button1_MouseUp);
            b.MouseClick += new System.Windows.Forms.MouseEventHandler(button1_MouseClick);
            b.MouseDown += new System.Windows.Forms.MouseEventHandler(button1_MouseDown);
            b.Size = new Size(27, 27);
            b.Location = new Point(pos.X - 13, pos.Y - 13);
            b.Text = (IsLetters.Checked) ? GetLetter(int.Parse(text)) : text;
            b.Name = text;
            this.Controls.Add(b);
        }

        private int AddVertex()
        {
            int n = matrix[matrix.Count - 1][0] + 1;
            matrix.Add(new List<int>());
            matrix[matrix.Count - 1].Add(n);
            for (int i = 1; i < matrix.Count - 1; i++) matrix[matrix.Count - 1].Add(0);
            for (int i = 0; i < matrix.Count; i++) matrix[i].Add(0);
            for (int i = 1; i < matrix.Count; i++) matrix[0][i] = matrix[i][0];
            return n;
        }

        private void DeleteVertex(int n)
        {
            activeVertex = 1;
            sourseVertex = 1;
            int ind = matrix[0].IndexOf(n);
            for (int i = 0; i < matrix.Count; i++) matrix[i].RemoveAt(ind);
            matrix.RemoveAt(ind);
            Controls.Remove((Button)Controls.Find(n.ToString(), true)[0]);

        }

        private void DeleteGraph()
        {
            while (matrix.Count != 1)
            {
                DeleteVertex(matrix[0][1]);
            }
            DrawGraph();
            activeVertex = 1;
            sourseVertex = 1;
        }

        private string PrintMatrix(List<List<int>> arr)
        {
            string s = "";
            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr[i].Count; j++)
                {
                    s += (IsLetters.Checked) ? GetLetter(arr[i][j]) + " " : arr[i][j] + " ";
                }
                s += "\n";
            }
            return s;
        }

        private string PrintMatrix(List<int> arr)
        {
            string s = "";
            for (int i = 0; i < arr.Count; i++) s += (IsLetters.Checked) ? GetLetter(arr[i]) + " " : arr[i] + " ";
            s += "\n";
            s += "Count: " + arr.Count + "\n";
            return s;
        }

        private void DrawGraph()
        {
            g.Clear(SystemColors.Control);

            for (int i = 1; i < matrix.Count; i++)
                ((Button)Controls.Find(matrix[0][i].ToString(), true)[0]).BackColor = SystemColors.Control;

            for (int i = 1; i < matrix.Count; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        Button a = (Button)Controls.Find(matrix[i][0].ToString(), true)[0];
                        Button b = (Button)Controls.Find(matrix[0][j].ToString(), true)[0];

                        if (IsOriented.Checked)
                        {
                            int dx = b.Location.X - a.Location.X;
                            int dy = b.Location.Y - a.Location.Y;

                            float dl = (float)Math.Sqrt(dx * dx + dy * dy);

                            float delta = 1 - 17f / dl;

                            if(matrix[i][j] == 1 && matrix[j][i] == -1)
                                g.DrawLine(pen, a.Location.X + 13, a.Location.Y + 13, 13 + a.Location.X + (int)(dx * delta), 13 + a.Location.Y + (int)(dy * delta));
                            else if(matrix[i][j] == -1 && matrix[j][i] == 1)
                                g.DrawLine(pen, b.Location.X + 13, b.Location.Y + 13, 13 + b.Location.X - (int)(dx * delta), 13 + b.Location.Y - (int)(dy * delta));
                            else
                                g.DrawLine(pen, a.Location.X + 13, a.Location.Y + 13, b.Location.X + 13, b.Location.Y + 13);
                        }
                        else
                        {
                            g.DrawLine(pen, a.Location.X + 13, a.Location.Y + 13, b.Location.X + 13, b.Location.Y + 13);
                        }                       
                    }
                }
            }
        }

        private void GraphFromFile()
        {
            if (MessageBox.Show("Вы действительно хотите стереть текущий граф?", "Удаление графа", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteGraph();
            }
            else return;
            string filename = textBox1.Text;
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string s = sr.ReadToEnd();
                    s = s.Replace("\r\n", "\n");
                    string[] lines = s.Split('\n');
                    matrix.Clear();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        matrix.Add(new List<int>());
                        string[] cols = lines[i].Split();
                        foreach (string num in cols) matrix[i].Add(int.Parse(num));
                    }

                    for (int i = 1; i < matrix.Count; i++)
                    {
                        CreateButton(new Point((int)(240 + Math.Cos(((float)i / (matrix.Count - 1)) * 2 * Math.PI) * 100), (int)(240 + Math.Sin(((float)i / (matrix.Count - 1)) * 2 * Math.PI) * 100)), matrix[0][i].ToString());
                    }
                }
                DrawGraph();
                redact.Checked = true;
            }
            catch { MessageBox.Show("Некорректные входные данные"); }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (redact.Checked)
                {
                    DeleteVertex(int.Parse(((Button)sender).Name));
                    //Controls.Remove((Button)sender);
                    DrawGraph();
                    return;
                }
                if (distances.Checked)
                {
                    sourseVertex = matrix[0].IndexOf(int.Parse(((Button)sender).Name));
                    DrawGraph();
                    DrawDistance();
                }
            }
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            IsClicked = true;
            if (redact.Checked)
            {
                if (isVertexWait)
                {
                    sourseVertex = matrix[0].IndexOf(int.Parse(((Button)sender).Name));
                    if (IsOriented.Checked)
                    {
                        matrix[activeVertex][sourseVertex] = (Math.Abs(matrix[activeVertex][sourseVertex]) + 1) % 2;
                        matrix[sourseVertex][activeVertex] = -((Math.Abs(matrix[sourseVertex][activeVertex]) + 1) % 2);
                    }
                    else
                    {
                        matrix[activeVertex][sourseVertex] = (matrix[activeVertex][sourseVertex] + 1) % 2;
                        matrix[sourseVertex][activeVertex] = (matrix[sourseVertex][activeVertex] + 1) % 2;
                    }
                    DrawGraph();
                    isVertexWait = false;
                    Controls.Find(matrix[0][activeVertex].ToString(), true)[0].BackColor = SystemColors.Control;
                    return;
                }
                isVertexWait = true;
                ((Button)sender).BackColor = Color.Yellow;
                activeVertex = matrix[0].IndexOf(int.Parse(((Button)sender).Name));
            }
            if (distances.Checked)
            {
                activeVertex = matrix[0].IndexOf(int.Parse(((Button)sender).Name));
                DrawGraph();
                DrawDistance();
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsClicked)
            {
                IsClicked = false;
                return;
            }
            if (redact.Checked)
            {
                ((Button)sender).Location = new Point(MousePosition.X - this.Location.X - 19, MousePosition.Y - this.Location.Y - 39);
                DrawGraph();
                if (distances.Checked) DrawDistance();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PrintMatrix(matrix), "Матрица смежности");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить граф?", "Удаление графа", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteGraph();
                comboBox1.SelectedIndex = -1;
                textBox2.Text = "";
                isVertexWait = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GraphFromFile();
        }

        private void connectivity_CheckedChanged(object sender, EventArgs e)
        {
            if (matrix.Count < 2) return;

            DrawGraph();
            FillConnections();

            for (int i = 0; i < connections.Count; i++)
            {
                Color col = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                for (int j = 0; j < connections[i].Count; j++)
                {
                    Button b = (Button)Controls.Find(connections[i][j].ToString(), true)[0];
                    toolTip1.SetToolTip(b, "Принадлежит компонету: " + i + 1);
                    b.BackColor = col;
                }
            }
        }


        //Определение коспонентов связности
        private void FillConnections()
        {
            connections.Clear();

            // Компоненты сильной связности в орграфе

            if (IsOriented.Checked)
            {
                time = 0;
                List<List<int>> matrixR = GetReverseGraph();

                int[] exitTime = new int[matrixR.Count - 1];
                bool[] visited = new bool[matrixR.Count];
                for (int i = 1; i < matrix.Count; i++)
                {
                    //time = 0;
                    if (!visited[i]) DFSTime(matrix[0][i], visited, exitTime);
                }

                //for(int i = 0; i < exitTime.Length; i++) Console.WriteLine(exitTime[i]);
                visited = new bool[matrixR.Count];
                for(int i = 0; i < exitTime.Length; i++)
                {
                    List<int> vx_ = new List<int>();
                    int max_ind = Array.IndexOf(exitTime, exitTime.Max());
                    exitTime[max_ind] = exitTime.Min() - 1;
                    if (!visited[max_ind + 1]) DFSConnections(matrix[0][max_ind + 1], visited, vx_);
                    if(vx_.Count > 0) connections.Add(vx_);
                }

                return;
            }


            // Через BFS

            if (_bfs.Checked)
            {
                Queue<int> q = new Queue<int>();
                int[] banned = new int[matrix.Count - 1];
                if (matrix.Count < 2) return;
                for (int i = 1; i < matrix.Count; i++)
                {
                    if (banned[i - 1] != 1)
                    {
                        connections.Add(new List<int>());
                        connections[connections.Count - 1].Add(matrix[i][0]);
                        q.Enqueue(matrix[i][0]);
                        banned[i - 1] = 1;
                        while (q.Count > 0)
                        {
                            int vert = q.Dequeue();
                            for (int j = 1; j < matrix.Count; j++)
                            {
                                int ind = matrix[0].IndexOf(vert);
                                if (matrix[ind][j] > 0 && banned[j - 1] != 1)
                                {
                                    q.Enqueue(matrix[0][j]);
                                    banned[j - 1] = 1;
                                    connections[connections.Count - 1].Add(matrix[0][j]);
                                }
                            }
                        }
                    }
                }
            }

            // Через DFS

            if (_dfs.Checked)
            {
                bool[] visited = new bool[matrix.Count];
                for (int i = 1; i < matrix.Count; i++)
                {
                    if (!visited[i])
                    {
                        List<int> vx = new List<int>();
                        int[] exittime = new int[matrix.Count];
                        DFSConnections(matrix[0][i], visited, vx);
                        connections.Add(vx);
                    }
                }
            }
        }

        int time = 0;

        private void DFSTime(int vertex, bool[] visited, int[] exitTime)
        {
            int ind = matrix[0].IndexOf(vertex);
            visited[ind] = true;
            for (int i = 1; i < matrix.Count; i++)
            {
                if (matrix[ind][i] > 0 && !visited[i])
                {
                    time++;
                    DFSTime(matrix[0][i], visited, exitTime);
                }
            }
            exitTime[ind - 1] = time;
            time++;
        }

        private void DFSConnections(int vertex, bool[] visited, List<int> vx)
        {
            vx.Add(vertex);
            int ind = matrix[0].IndexOf(vertex);
            visited[ind] = true;
            for (int i = 1; i < matrix.Count; i++)
            {
                if (matrix[i][ind] > 0 && !visited[i])
                {
                    DFSConnections(matrix[0][i], visited, vx);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PrintMatrix(connections) + '\n' + "Всего: " + connections.Count, "Компонеты связности");
        }

        private void redact_CheckedChanged(object sender, EventArgs e)
        {
            if (!redact.Checked) return;
            DrawGraph();
            for (int i = 1; i < matrix[0].Count; i++)
            {
                Button b = (Button)Controls.Find(matrix[0][i].ToString(), true)[0];
                b.Text = (IsLetters.Checked) ? GetLetter(int.Parse(b.Name)) : b.Name;
                b.BackColor = SystemColors.Control;
                toolTip1.SetToolTip(b, "ЛКМ - управление связями\nВести - перемещать\nПКМ - удалить");
            }
        }

        private void distances_CheckedChanged(object sender, EventArgs e)
        {
            DrawGraph();

            //Рассчёт расстояний
            FillDistances();

            //Графическое отображение
            DrawDistance();
        }


        //Рассчёт расстояний
        private void FillDistances()
        {
            distancesList = new List<List<int>>();
            paths = new List<List<List<int>>>();
            for (int i = 0; i < matrix.Count; i++)
            {
                distancesList.Add(new List<int>());
                paths.Add(new List<List<int>>());
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (i == 0) { distancesList[i].Add(matrix[i][j]); paths[i].Add(new List<int>() { matrix[i][j] }); }
                    else if (j == 0) { distancesList[i].Add(matrix[i][j]); paths[i].Add(new List<int>() { matrix[i][j] }); }
                    else { distancesList[i].Add(0); paths[i].Add(new List<int>()); }
                }
            }
            for (int i = 1; i < matrix.Count; i++)
            {
                Queue<int> q = new Queue<int>();
                List<int> qind = new List<int>();
                List<int> path = new List<int>() { matrix[0][i] };
                paths[i][i] = new List<int>(path);
                int[] banned = new int[matrix.Count - 1];
                q.Enqueue(matrix[i][0]);
                qind.Add(1);
                banned[i - 1] = 1;
                while (q.Count > 0)
                {
                    int vert = q.Dequeue();
                    int r = qind[0];
                    qind.RemoveAt(0);
                    path = new List<int>(paths[i][matrix[0].IndexOf(vert)]);
                    int ind = matrix[0].IndexOf(vert);
                    banned[ind - 1] = 1;
                    for (int k = 1; k < matrix.Count; k++)
                    {
                        if (matrix[ind][k] > 0 && banned[k - 1] != 1)
                        {
                            q.Enqueue(matrix[0][k]);
                            qind.Add(r + 1);
                            //banned[k - 1] = 1;
                            if (r < distancesList[i][k] || distancesList[i][k] == 0)
                            {
                                path.Add(matrix[0][k]);
                                paths[i][k] = new List<int>(path);
                                path.Reverse();
                                paths[k][i] = new List<int>(path);
                                distancesList[i][k] = r;
                                distancesList[k][i] = r;
                                path.Reverse();
                                path.RemoveAt(path.Count - 1);
                            }
                        }
                    }
                }
            }
        }


        //Система рисования путей
        private void DrawDistance()
        {
            if (matrix.Count < 2) return;
            for (int i = 1; i < matrix.Count; i++)
            {
                Button b = (Button)Controls.Find(matrix[0][i].ToString(), true)[0];
                toolTip1.SetToolTip(b, (distancesList[activeVertex][i] != 0) ? distancesList[activeVertex][i].ToString() : "нет связи");
                if (distancesList[activeVertex][i] == 0) b.BackColor = Color.DarkGray;
                else b.BackColor = Color.FromArgb((int)(distancesList[activeVertex][i] / (float)matrix.Count * 255), 255 - (int)(distancesList[activeVertex][i] / (float)matrix.Count * 255), 0);
            }
            for (int i = 0; i < paths[activeVertex][sourseVertex].Count - 1; i++)
            {
                Button a = (Button)Controls.Find(paths[activeVertex][sourseVertex][i].ToString(), true)[0];
                Button b = (Button)Controls.Find(paths[activeVertex][sourseVertex][i + 1].ToString(), true)[0];

                g.DrawLine(new Pen(Color.Red, 5), a.Location.X + 13, a.Location.Y + 13, b.Location.X + 13, b.Location.Y + 13);
            }
            ((Button)Controls.Find(matrix[0][activeVertex].ToString(), true)[0]).BackColor = Color.Yellow;
            ((Button)Controls.Find(matrix[0][sourseVertex].ToString(), true)[0]).BackColor = Color.Yellow;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PrintMatrix(distancesList), "Таблица расстояний");
            MessageBox.Show(PrintMatrix(paths[1]));
        }


        //Поиск всех циклов методом DFS
        private void cylesButton_CheckedChanged(object sender, EventArgs e)
        {
            DrawGraph();

            DFSCycles();

            if (cycles.Count > 0) DrawCycle(cycleInd);
        }

        private void AddToList(List<int> a, List<int> b)
        {
            foreach (int i in b) { a.Add(i); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PrintMatrix(cycles) + "\n" + "Всего: " + cycles.Count, "Циклы");
        }

        private void DFS(int startVertex, int currentVertex, bool[] visited, List<int> path)
        {
            int cInd = matrix[0].IndexOf(currentVertex);
            visited[cInd] = true;
            for (int i = 1; i < matrix.Count; i++)
            {
                if (matrix[cInd][i] > 0)
                {
                    if (!visited[i])
                    {
                        visited[i] = true;
                        path.Add(matrix[0][i]);
                        DFS(startVertex, matrix[0][i], visited, path);
                        path.RemoveAt(path.Count - 1);
                        visited[i] = false;
                    }
                    else if (matrix[0][i] == startVertex && path.Count > 2)
                    {
                        if (!Contains(cycles, path))
                        {
                            cycles.Add(new List<int>());
                            foreach (var x in path) cycles[cycles.Count - 1].Add(x);
                        }
                    }
                }
            }
        }
        private void DFSCycles()
        {
            cycleInd = 0;
            cycles.Clear();
            for (int i = 1; i < matrix.Count; i++)
            {
                List<int> path = new List<int>() { matrix[0][i] };
                bool[] visited = new bool[matrix.Count];
                DFS(matrix[0][i], matrix[0][i], visited, path);
            }
        }


        //Рисование циклов
        private void DrawCycle(int n)
        {
            DrawGraph();

            Button a = (Button)Controls.Find(cycles[n][cycles[n].Count - 1].ToString(), true)[0];
            Button b = (Button)Controls.Find(cycles[n][0].ToString(), true)[0];
            a.BackColor = Color.Aqua;
            toolTip1.SetToolTip(a, "Входит в графы раз:\n" + ContainsCount(cycles, cycles[n][cycles[n].Count - 1]));
            g.DrawLine(new Pen(Color.Aqua, 5), a.Location.X + 13, a.Location.Y + 13, b.Location.X + 13, b.Location.Y + 13);

            for (int i = 0; i < cycles[n].Count - 1; i++)
            {
                a = (Button)Controls.Find(cycles[n][i].ToString(), true)[0];
                b = (Button)Controls.Find(cycles[n][i + 1].ToString(), true)[0];
                a.BackColor = Color.Aqua;
                toolTip1.SetToolTip(a, "Входит в графы раз:\n" + ContainsCount(cycles, cycles[n][i]));
                g.DrawLine(new Pen(Color.Aqua, 5), a.Location.X + 13, a.Location.Y + 13, b.Location.X + 13, b.Location.Y + 13);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (cycles.Count == 0) return;
            cycleInd = (cycleInd - 1) % cycles.Count;
            if (cycleInd < 0) cycleInd += cycles.Count;
            DrawCycle(cycleInd);
        }

        private bool Contains(List<List<int>> a, List<int> b)
        {
            if (a.Count == 0) return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (!new HashSet<int>(a[i]).SetEquals(b)) continue;
                return true;
            }
            return false;
        }

        private int ContainsCount(List<List<int>> a, int vert)
        {
            int n = 0;
            for (int i = 0; i < cycles.Count; i++)
            {
                if (a[i].Contains(vert)) n++;
            }
            return n;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (cycles.Count == 0) return;
            cycleInd = (cycleInd + 1) % cycles.Count;
            DrawCycle(cycleInd);
        }


        //2 - ракраска графа
        private void coloring_CheckedChanged(object sender, EventArgs e)
        {
            if (!coloring.Checked) return;

            DrawGraph();
            FillConnections();

            //Через циклы

            if (_dfs.Checked)
            {
                FillDistances();

                DFSCycles();

                if (HasOddCycles(cycles))
                {
                    MessageBox.Show("2-раскраска невозможна");
                    return;
                }

                for (int x = 0; x < connections.Count; x++)
                {
                    for (int i = 0; i < connections[x].Count; i++)
                    {
                        Button b = (Button)Controls.Find(connections[x][i].ToString(), true)[0];
                        if (distancesList[matrix[0].IndexOf(connections[x][0])][matrix[0].IndexOf(connections[x][i])] % 2 == 0) b.BackColor = Color.Red;
                        else b.BackColor = Color.Green;
                    }
                }
            }


            //Через обход в ширину

            if (_bfs.Checked)
            {
                if (matrix.Count < 2) return;

                int[] painted = new int[matrix.Count];

                for (int x = 0; x < connections.Count; x++)
                {
                    Queue<int> q = new Queue<int>();
                    q.Enqueue(connections[x][0]);
                    bool[] visited = new bool[matrix.Count];
                    painted[connections[x][0]] = 1;
                    int[] path = new int[matrix.Count];
                    while (q.Count > 0)
                    {
                        int vert = q.Dequeue();
                        int ind = matrix[0].IndexOf(vert);
                        visited[ind] = true;
                        for (int i = 1; i < matrix.Count; i++)
                        {
                            if (matrix[ind][i] > 0 && !visited[i])
                            {
                                q.Enqueue(matrix[0][i]);
                                if (painted[i] > 0)
                                {
                                    if ((path[ind] + 1) % 2 + 1 != painted[i])
                                    {
                                        MessageBox.Show("2-Раскраска текущего графа невозможна");
                                        return;
                                    }
                                }
                                else
                                {
                                    painted[i] = (path[ind] + 1) % 2 + 1;
                                }
                                path[i] = path[ind] + 1;
                            }
                        }
                    }
                }
                for (int i = 1; i < matrix.Count; i++)
                {
                    Button b = (Button)Controls.Find(matrix[0][i].ToString(), true)[0];
                    if (painted[i] % 2 == 0) b.BackColor = Color.Red;
                    else b.BackColor = Color.Green;
                }
            }
        }

        private bool HasOddCycles(List<List<int>> a)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Count % 2 == 1) return true;
            }
            return false;
        }


        //Поиск цикла BFS-ом
        private void button10_Click(object sender, EventArgs e)
        {
            FillConnections();
            if (matrix.Count < 2) return;

            int[] parents = new int[matrix.Count];
            List<int> backPath = new List<int>();
            for (int x = 0; x < connections.Count; x++)
            {
                Queue<int> q = new Queue<int>();
                q.Enqueue(connections[x][0]);
                bool[] visited = new bool[matrix.Count];
                int[] path = new int[matrix.Count];
                while (q.Count > 0)
                {
                    int vert = q.Dequeue();
                    int ind = matrix[0].IndexOf(vert);
                    visited[ind] = true;
                    for (int i = 1; i < matrix.Count; i++)
                    {
                        if (matrix[ind][i] > 0)
                        {
                            if (!visited[i])
                            {
                                parents[i] = vert;
                                q.Enqueue(matrix[0][i]);
                            }
                            else if (parents[ind] != i)
                            {
                                int p1 = parents[i];
                                int p2 = parents[ind];

                                backPath.Add(p1);

                                Button x1 = (Button)Controls.Find(matrix[0][i].ToString(), true)[0];
                                Button x2 = (Button)Controls.Find(vert.ToString(), true)[0];

                                g.DrawLine(new Pen(Color.AliceBlue, 5), x1.Location.X + 13, x1.Location.Y + 13, x2.Location.X + 13, x2.Location.Y + 13);

                                if (!backPath.Contains(p2))
                                {
                                    backPath.Add(p2);
                                    while (true)
                                    {
                                        Button a1 = (Button)Controls.Find(matrix[0].IndexOf(i).ToString(), true)[0];
                                        Button a2 = (Button)Controls.Find(vert.ToString(), true)[0];
                                        Button b1;
                                        Button b2;
                                        if (p1 != 0)
                                        {
                                            b1 = (Button)Controls.Find(matrix[0].IndexOf(p1).ToString(), true)[0];
                                            g.DrawLine(new Pen(Color.AliceBlue, 5), a1.Location.X + 13, a1.Location.Y + 13, b1.Location.X + 13, b1.Location.Y + 13);
                                            i = p1;
                                            p1 = parents[p1];
                                            if (backPath.Contains(p1)) break;
                                            backPath.Add(p1);
                                        }
                                        if (p2 != 0)
                                        {
                                            b2 = (Button)Controls.Find(matrix[0].IndexOf(p2).ToString(), true)[0];
                                            g.DrawLine(new Pen(Color.AliceBlue, 5), a2.Location.X + 13, a2.Location.Y + 13, b2.Location.X + 13, b2.Location.Y + 13);
                                            vert = p2;
                                            p2 = parents[p2];
                                            if (backPath.Contains(p2)) break;
                                            backPath.Add(p2);
                                        }
                                    }
                                }

                                try
                                {
                                    Button c1 = (Button)Controls.Find(matrix[0].IndexOf(i).ToString(), true)[0];
                                    Button c2 = (Button)Controls.Find(vert.ToString(), true)[0];
                                    Button c3 = (Button)Controls.Find(matrix[0].IndexOf(p1).ToString(), true)[0];

                                    g.DrawLine(new Pen(Color.AliceBlue, 5), c1.Location.X + 13, c1.Location.Y + 13, c3.Location.X + 13, c3.Location.Y + 13);
                                    g.DrawLine(new Pen(Color.AliceBlue, 5), c2.Location.X + 13, c2.Location.Y + 13, c3.Location.X + 13, c3.Location.Y + 13);
                                }
                                catch { }

                                return;
                            }
                            visited[i] = true;
                        }

                    }
                }
            }
        }

        private void _bfs_CheckedChanged(object sender, EventArgs e)
        {
            redact.Checked = true;
            if (_bfs.Checked)
            {
                cylesButton.Enabled = false;
                distances.Enabled = true;
                button10.Enabled = true;
                joint.Enabled = false;
            }
            else
            {
                cylesButton.Enabled = true;
                distances.Enabled = false;
                button10.Enabled = false;
                joint.Enabled = true;
            }
        }

        private void joint_CheckedChanged(object sender, EventArgs e)
        {
            if (!joint.Checked) return;
            jointList.Clear();
            DrawGraph();

            bool[] visited = new bool[matrix.Count];
            int[] f = new int[matrix.Count];
            int[] entryTime = new int[matrix.Count];

            for(int i = 1; i < matrix.Count; i++)
            {
                counter = 0;
                if (!visited[i]) DFSJointSearch(matrix[0][i], visited, entryTime, f);
            }
            
            JointPointPaint();
        }

        private void JointPointPaint()
        {
            for(int i = 0; i < jointList.Count; i++)
            {
                ((Button)Controls.Find(jointList[i].ToString(), true)[0]).BackColor = Color.Turquoise;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PrintMatrix(jointList), "Точки сочленения: ");
        }

        private void DFSJointSearch(int vertex, bool[] visited, int[] entryTime, int[] f, int time = 0, int parentV = 0)
        {
            int ind = matrix[0].IndexOf(vertex);
            entryTime[ind] = time;
            visited[ind] = true;
            int min = time;
            for(int i = 1; i < matrix.Count; i++)
            {
                if (matrix[ind][i] > 0)
                {
                    if (matrix[0][i] == parentV) continue;
                    if (!visited[i])
                    {
                        if(parentV == 0) counter++;
                        DFSJointSearch(matrix[0][i], visited, entryTime, f, time + 1, vertex);
                        min = Math.Min(min, f[i]);
                        min = Math.Min(min, entryTime[i]);
                        if (parentV != 0) {
                            if (f[i] >= entryTime[ind]) if (!jointList.Contains(matrix[0][ind])) jointList.Add(matrix[0][ind]);
                        }
                        else
                            if(counter > 1) 
                                if (!jointList.Contains(matrix[0][ind])) jointList.Add(matrix[0][ind]);
                    }
                    else
                    {
                        min = Math.Min(min, entryTime[i]);
                    }
                }
            }
            //if(counter == 0) if (!jointList.Contains(matrix[0][ind])) jointList.Add(matrix[0][ind]);
            f[ind] = min;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = comboBox1.Text;

            DeleteGraph();

            string graphname = comboBox1.Text;
            string s = ReadGraphs("graphsFile.txt");
            //s = s.Replace("\n", "\n");
            string[] graphs = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < graphs.Length; i++)
            {
                string[] curGraph = graphs[i].Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                //Парсинг имени
                curGraph[0] = curGraph[0].Replace("\n", "");
                if (curGraph[0] == graphname)
                {
                    //Парсинг матрицы
                    string[] rows = curGraph[1].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    matrix = new List<List<int>>();
                    for (int j = 0; j < rows.Length; j++)
                    {
                        string[] items = rows[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        matrix.Add(new List<int>());
                        for (int x = 0; x < items.Length; x++)
                        {
                            matrix[matrix.Count - 1].Add(int.Parse(items[x]));
                        }
                    }

                    //Парсинг позиций
                    string[] posns = curGraph[2].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < posns.Length; j++)
                    {
                        string[] curPos = posns[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        Button b = new Button();
                        b.MouseUp += new System.Windows.Forms.MouseEventHandler(button1_MouseUp);
                        b.MouseClick += new System.Windows.Forms.MouseEventHandler(button1_MouseClick);
                        b.MouseDown += new System.Windows.Forms.MouseEventHandler(button1_MouseDown);
                        b.Size = new Size(27, 27);
                        b.Location = new Point(int.Parse(curPos[0]), int.Parse(curPos[1]));
                        b.Text = matrix[0][j + 1].ToString();
                        b.Name = matrix[0][j + 1].ToString();
                        this.Controls.Add(b);
                    }

                    //Вывод графа

                    DrawGraph();
                    redact.Checked = true;

                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("graphsFile.txt"))
            {
                string s = ReadGraphs("graphsFile.txt");
                string[] names = GetGraphNames(s);
                comboBox1.Items.Clear();
                foreach(var w in names) if(w != null) comboBox1.Items.Add(w);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if(matrix.Count < 2)
            {
                MessageBox.Show("Пустой граф нельзя сохранить");
                return;
            }

            string graphName = textBox2.Text;

            if (graphName.Length == 0)
            {
                MessageBox.Show("Имя графа не может быть пустым");
                return;
            }
            string s = "";
            string h;
            if (File.Exists("graphsFile.txt"))
            {
                s = ReadGraphs("graphsFile.txt");
                string[] names = GetGraphNames(s);
                if (names.Contains(graphName))
                {
                    if(MessageBox.Show("Такое имя графа уже существует\nИзменить граф?", "Изменение графа", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int ind = Array.IndexOf(names, graphName);
                        string[] graphs = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        //s = s.Replace("\n", "\n");
                        h = GraphToString(graphName);
                        for(int i = 0; i < graphs.Length; i++)
                        {
                            if (graphs[i].Split('+')[0].Replace("\n", "") == graphName)
                            {
                                graphs[i] = h;
                                break;
                            }
                        }
                        s = String.Join("/", graphs);
                        WriteGraphs("graphsFile.txt", s);

                        MessageBox.Show("Успешно");
                    }
                    return;
                }
            }

            h = GraphToString(graphName);
            s += "/" + h;

            WriteGraphs("graphsFile.txt", s);

            comboBox1.Items.Add(graphName);

            MessageBox.Show("Успешно");
        }

        private string GraphToString(string graphName)
        {
            string s = "";
            string matr = PrintMatrix(matrix);
            string pos = "";
            for (int i = 1; i < matrix.Count; i++)
            {
                Button b = (Button)Controls.Find(matrix[0][i].ToString(), true)[0];
                pos += b.Location.X + " " + b.Location.Y;
                pos += "\n";
            }
            string curGraphString = "\n" + graphName + "\n+\n" + matr + "+\n" + pos;
            s += curGraphString;
            return s;
        }

        private string ReadGraphs(string filename)
        {
            string s = "";
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    s = sr.ReadToEnd();
                }
            } catch { MessageBox.Show("Непредвиденная ошибка при загрузке данных"); }
            return s;
        } 
        private void WriteGraphs(string filename, string graphsString)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(filename, false)) 
                {
                    sr.Write(graphsString);
                }
            }
            catch { MessageBox.Show("Непридвиденная ошибка при сохранении данных"); }
        }
        private string[] GetGraphNames(string s)
        {
            s = s.Replace("\n", "");
            string[] graphs = s.Split('/');
            string[] names = new string[graphs.Length];
            for(int i = 0; i < graphs.Length; i++)
                if (graphs[i].Length > 4) names[i] = graphs[i].Split('+')[0].Replace("\n", "");
            return names;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Для начала, выберите какой - либо граф из спика");
                return;
            }

            string s = ReadGraphs("graphsFile.txt");
            //s = s.Replace("\r\n", "\n");

            List<string> graphs = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            graphs.RemoveAt(comboBox1.SelectedIndex);
            s = string.Join("/", graphs);

            WriteGraphs("graphsFile.txt", s);

            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            comboBox1.SelectedIndex = -1;

            DeleteGraph();

            MessageBox.Show("Успешно");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DrawGraph();
            redact.Checked = true;
            if (IsOriented.Checked)
            {
                distances.Enabled = false;
                cylesButton.Enabled = false;
                coloring.Enabled = false;
                joint.Enabled = false;
                button15.Enabled = true;
            }
            else
            {
                distances.Enabled = true;
                cylesButton.Enabled = true;
                coloring.Enabled = true;
                joint.Enabled = true;
                button15.Enabled = false;
            }
        }



        // Компоненты сильной связности орграфа

        private List<List<int>> GetReverseGraph()
        {
            List<List<int>> res = new List<List<int>> { new List<int>() };
            for (int i = 0; i < matrix.Count; i++) res[res.Count - 1].Add(matrix[0][i]);
            for(int i = 1; i < matrix.Count; i++)
            {
                res.Add(new List<int>());
                res[res.Count - 1].Add(matrix[i][0]);
                for (int j = 1; j < matrix[i].Count; j++) res[res.Count - 1].Add(-matrix[i][j]);
            }
            return res;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try 
            {
                System.Diagnostics.Process.Start("notepad.exe", "graphsFile.txt");
            }
            catch { MessageBox.Show("Не удаётся открыть файл"); }
        }

        // Топологическая сортировка
        private void button15_Click(object sender, EventArgs e)
        {
            flag = false;
            if (matrix.Count < 2) return;
            List<int> topolog = new List<int>();
            int[] visited = new int[matrix.Count];
            for(int i = 1; i < matrix.Count; i++)
            {
                if (visited[i] == 0) DFSForSorting(matrix[0][i], visited, topolog);
            }
            if (topolog.Count == matrix.Count - 1)
            {
                topolog.Reverse();
                MessageBox.Show(PrintMatrix(topolog));
            }
        }

        bool flag = false;

        private void DFSForSorting(int vertex, int[] visited, List<int> sorted)
        {
            int ind = matrix[0].IndexOf(vertex);
            visited[ind] = 1;
            for(int i = 1; i < matrix.Count; i++)
            {
                if (matrix[ind][i] > 0) 
                {
                    if (visited[i] == 0) DFSForSorting(matrix[0][i], visited, sorted);
                    else if (visited[i] == 1)
                    {
                        if(!flag) MessageBox.Show("Топологическая сортировка невозможна");
                        flag = true;
                        return;
                    }
                }
            }
            visited[ind] = 2;
            sorted.Add(vertex);
        }

        private void IsLetters_CheckedChanged(object sender, EventArgs e)
        {
            for(int i = 1; i < matrix.Count(); i++)
            {
                Button b = (Button)Controls.Find(matrix[0][i].ToString(), true)[0];

                if (IsLetters.Checked) b.Text = GetLetter(matrix[0][i]);
                else b.Text = b.Name;
            }
        }
    }
}

