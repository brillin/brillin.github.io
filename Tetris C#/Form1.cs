// Tetris av David Tanudin
// 2020.04.23
// Jag har gjort alla utökningar och lite extra
// Klar och komenterat

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Tetris
{
    public partial class Form1 : Form
    {
        // Deklarerar globala variabler/klasser.
        List<Block> Blocks = new List<Block>();
        List<Block> ControlBlock = new List<Block>();
        List<Block> PreviewBlock = new List<Block>();
        List<int> Scores = new List<int>();

        Pen PEN = new Pen(Color.Black, 1);
        int points = 0;
        int lines = 0;
        int boolean = 1;
        string PATH_FILE = @"c:\Tetris\scores.txt";

        SolidBrush GREEN_BRUSH = new SolidBrush(Color.Green);
        SolidBrush RED_BRUSH = new SolidBrush(Color.Red);
        SolidBrush LIGHTBLUE_BRUSH = new SolidBrush(Color.LightBlue);
        SolidBrush DARKBLUE_BRUSH = new SolidBrush(Color.DarkBlue);
        SolidBrush ORANGE_BRUSH = new SolidBrush(Color.Orange);
        SolidBrush YELLOW_BRUSH = new SolidBrush(Color.Yellow);
        SolidBrush PURPLE_BRUSH = new SolidBrush(Color.Purple);
        SolidBrush LIGHTGREEN_BRUSH = new SolidBrush(Color.LightGreen);

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Klass för block.
        /// </summary>
        public class Block
        {
            //Private data members.
            public int x;
            public int y;
            public string color;

            public Block(int x, int y, string color)
            {
                this.x = x;
                this.y = y;
                this.color = color;
            }
        }

        /// <summary>
        /// Börjar spelet.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateFile();
            StartGame();
        }

        /// <summary>
        /// Skapar en fil, om det inte finns en fil.
        /// Skriver in nummret 0 så att highscore börjar på 0.
        /// </summary>
        private void CreateFile()
        {
            string path = @"c:\Tetris";
            if (!File.Exists(PATH_FILE))
            {
                System.IO.Directory.CreateDirectory(path);
                using (StreamWriter sw = File.CreateText(PATH_FILE))
                {
                    sw.WriteLine("0");
                }
            }
        }

        /// <summary>
        /// Börjar spelet, startar om alla globala klasser/variabler.
        /// </summary>
        private void StartGame()
        {
            // Läser av highscore filen, sorterar den och tar högsta värdet.
            string[] reading = System.IO.File.ReadAllLines(PATH_FILE);
            for (int i = 0; i < reading.Length; i++)
            {
                Scores.Add(int.Parse(reading[i]));
            }
            Scores.Sort();
            highLabel.Text = Scores[Scores.Count - 1].ToString();

            timer1.Interval = 750;
            lost.Text = "";
            reset.Text = "";
            pointLabel.Text = "0";
            linesLabel.Text = "0";
            levelLabel.Text = "0";
            timer1.Enabled = true;
            Blocks.Clear();
            ControlBlock.Clear();
            PreviewBlock.Clear();
            points = 0;
            lines = 0;
            boolean = 1;
            levelLabel.Text = "0";
            pausStart.Text = "Paus";
            CreateBlock(panel1.Size.Width - 20, 1);
            CreateBlock(panel2.Size.Width, 2);
        }

        /// <summary>
        /// Skapar ett block.
        /// </summary>
        private void CreateBlock(int x, int y)
        {
            // x värde till mitten av panel.
            x = ((x - 1) / 20) / 2;

            List<int> arrayx = new List<int>();
            List<int> arrayy = new List<int>();
            string color = "";
            arrayx.Clear();
            arrayy.Clear();
            color = RandomBlock(arrayx, arrayy, color, x, y);
            PlaceBlock(arrayx, arrayy, color);
        }   

        /// <summary>
        /// Placerar ut block och byter preview blocket.
        /// </summary>
        private void PlaceBlock(List<int> arrayx, List<int> arrayy, string color)
        {
            // Vid start ska den slumpa ett block för tetris och ett för preview.
            if (boolean == 1)
            {
                for (int i = 0; i < arrayx.Count; i++)
                {
                    Block block = new Block(arrayx[i], arrayy[i], color);
                    ControlBlock.Add(block);
                }
                boolean = 2;
            }
            else
            {
                PreviewToTetris(arrayx, arrayy, color);

            }
        }

        /// <summary>
        /// Efter första gången, kommer alla andra gångerna slumpa för bara preview.
        /// Eftersom den siste preview blocket kommer till själva tetrisen.
        /// </summary>
        private void PreviewToTetris(List<int> arrayx, List<int> arrayy, string color)
        {
            if (boolean == 3)
            {
                for (int i = 0; i < PreviewBlock.Count; i++)
                {
                    PreviewBlock[i].x += 1;
                    PreviewBlock[i].y -= 4;
                    ControlBlock.Add(PreviewBlock[i]);
                }
            }
            PreviewBlock.Clear();
            for (int i = 0; i < arrayx.Count; i++)
            {
                Block block = new Block(arrayx[i], arrayy[i], color);
                PreviewBlock.Add(block);
            }
            panel2.Invalidate();
            boolean = 3;
        }

        /// <summary>
        /// Väljer ut vilket typ av block det ska vara.
        /// </summary>
        private string RandomBlock(List<int> arrayx, List<int> arrayy, string color, int x, int y)
        {
            Random random = new Random();
            int number = random.Next(7);
            Debug.WriteLine("dwad");
            switch (number)
            {
                case 0:
                    color = "S";
                    arrayx.Add(x);
                    arrayx.Add(x + 1);
                    arrayx.Add(x);
                    arrayx.Add(x - 1);
                    arrayy.Add(y);
                    arrayy.Add(y - 1);
                    arrayy.Add(y - 1);
                    arrayy.Add(y);
                    break;
                case 1:
                    color = "Z";
                    arrayx.Add(x);
                    arrayx.Add(x - 1);
                    arrayx.Add(x);
                    arrayx.Add(x + 1);
                    arrayy.Add(y);
                    arrayy.Add(y - 1);
                    arrayy.Add(y - 1);
                    arrayy.Add(y);
                    break;
                case 2:
                    color = "L";
                    arrayx.Add(x);
                    arrayx.Add(x);
                    arrayx.Add(x);
                    arrayx.Add(x + 1);
                    arrayy.Add(y);
                    arrayy.Add(y - 1);
                    arrayy.Add(y - 2);
                    arrayy.Add(y);
                    break;
                case 3:
                    color = "J";
                    arrayx.Add(x);
                    arrayx.Add(x);
                    arrayx.Add(x);
                    arrayx.Add(x - 1);
                    arrayy.Add(y);
                    arrayy.Add(y - 1);
                    arrayy.Add(y - 2);
                    arrayy.Add(y);
                    break;
                case 4:
                    color = "O";
                    arrayx.Add(x);
                    arrayx.Add(x + 1);
                    arrayx.Add(x);
                    arrayx.Add(x + 1);
                    arrayy.Add(y);
                    arrayy.Add(y);
                    arrayy.Add(y - 1);
                    arrayy.Add(y - 1);
                    break;
                case 5:
                    color = "T";
                    arrayx.Add(x);
                    arrayx.Add(x + 1);
                    arrayx.Add(x - 1);
                    arrayx.Add(x);
                    arrayy.Add(y);
                    arrayy.Add(y);
                    arrayy.Add(y);
                    arrayy.Add(y - 1);
                    break;
                case 6:
                    color = "I";
                    arrayx.Add(x);
                    arrayx.Add(x + 1);
                    arrayx.Add(x + 2);
                    arrayx.Add(x - 1);
                    arrayy.Add(y);
                    arrayy.Add(y);
                    arrayy.Add(y);
                    arrayy.Add(y);
                    break;
                default:
                    break;
            }
            return color;

        }

        /// <summary>
        /// Panel painten, kör en for loop för alla blocken i hela tetris.
        /// </summary>
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < ControlBlock.Count; i++)
            {
                BlockColor(ControlBlock[i].x, ControlBlock[i].y, e, ControlBlock[i].color);
            }
            for (int i = 0; i < Blocks.Count; i++)
            {
                BlockColor(Blocks[i].x, Blocks[i].y, e, Blocks[i].color);
            }
        }

        /// <summary>
        /// Preview panel painten, kör en for loop för alla block i preview.
        /// </summary>
        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < PreviewBlock.Count; i++)
            {
                BlockColor(PreviewBlock[i].x, PreviewBlock[i].y, e, PreviewBlock[i].color);
            }
        }

        /// <summary>
        /// Ritar ut själva rectanglen, och använder fill för respektive färg.
        /// </summary>
        /// <param name="x">x värdet på block.</param>
        /// <param name="y">y värdet på block.</param>
        /// <param name="color">Färgen på block.</param>
        private void BlockColor(int x, int y, PaintEventArgs e, string color)
        {
                Rectangle rect = new Rectangle(x * 20, y * 20, 20, 20);
                if (color == "S") e.Graphics.FillRectangle(LIGHTGREEN_BRUSH, rect);
                else if (color == "Z") e.Graphics.FillRectangle(RED_BRUSH, rect);
                else if (color == "L") e.Graphics.FillRectangle(ORANGE_BRUSH, rect);
                else if (color == "J") e.Graphics.FillRectangle(DARKBLUE_BRUSH, rect);
                else if (color == "O") e.Graphics.FillRectangle(YELLOW_BRUSH, rect);
                else if (color == "I") e.Graphics.FillRectangle(LIGHTBLUE_BRUSH, rect);
                else if (color == "T") e.Graphics.FillRectangle(PURPLE_BRUSH, rect);

                e.Graphics.DrawRectangle(PEN, rect);
        }

        /// <summary>
        /// En timer för att blocket ska röra sig neråt
        /// Dessutom efter den har rört sig ska den kolla om det har nuddat botten
        /// Eller om det har träffat en annan block
        /// </summary>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            BottomCheck();
            if (DropCheck() == true)
            {
                for (int i = 0; i < ControlBlock.Count; i++)
                {
                    ControlBlock[i].y += 1;
                }
                BottomCheck();
                DropCheck();
                panel1.Invalidate();
                panel1.Update();
            }
        }

        /// <summary>
        /// Keydown, för att kolla vilken tangent man har tryckt.
        /// Kör funktionen för respektive knapp.
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Om timer är av ska den inte köra något.
            if (timer1.Enabled != true) return;

            if (e.KeyCode == Keys.Right)
            {
                Right();
            }
            else if (e.KeyCode == Keys.Left)
            {
                Left();
            }
            else if (e.KeyCode == Keys.Down) 
            {
                Down();
            }
            else if (e.KeyCode == Keys.Up)
            {
                Rotate();
                panel1.Invalidate();
                panel1.Update();

            }
            else if (e.KeyCode == Keys.Space)
            {
                timer1.Stop();
                Dropdown();
                timer1.Start();
            }
        }

        /// <summary>
        /// Funktion för att flytta block åt höger.
        /// </summary>
        private void Right()
        {
            if (WidthBorderCheck(1) == true)
            {
                for (int i = 0; i < ControlBlock.Count; i++)
                {
                    ControlBlock[i].x += 1;
                }
                panel1.Invalidate();
                panel1.Update();
            }
        }

        /// <summary>
        /// Funktion för att flytta block åt vänster.
        /// </summary>
        private void Left()
        {
            if (WidthBorderCheck(-1) == true)
            {
                for (int i = 0; i < ControlBlock.Count; i++)
                {
                    ControlBlock[i].x -= 1;
                }
                panel1.Invalidate();
                panel1.Update();
            }
        }

        /// <summary>
        /// Funktion för att flytta block åt ner.
        /// </summary>
        private void Down()
        {
            BottomCheck();
            if (DropCheck() == true)
            {
                for (int i = 0; i < ControlBlock.Count; i++)
                {
                    ControlBlock[i].y += 1;
                }
                DropCheck();
                panel1.Invalidate();
                panel1.Update();

            }
        }

        /// <summary>
        /// En funktion som kollar om nästa drag är utanför panel.
        /// </summary>
        /// <returns>Retunerar true om nästa drag går bra, false om det inte går.</returns>
        private Boolean WidthBorderCheck(int x)
        {
            for (int i = 0; i < ControlBlock.Count; i++)
            {
                if (ControlBlock[i].x + x > 9 || ControlBlock[i].x + x < 0)
                {
                    return false;
                }

                for (int j = 0; j < Blocks.Count; j++)
                {
                    if (ControlBlock[i].x + x == Blocks[j].x && ControlBlock[i].y == Blocks[j].y)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Funktion som drar ner blocket direkt.
        /// </summary>
        private void Dropdown()
        {
            // Kollar om det träffar en annan block.
            int drop = 100;
            for (int i = 0; i < ControlBlock.Count; i++)
            {
                for (int j = 0; j < Blocks.Count; j++)
                {
                    if (ControlBlock[i].x == Blocks[j].x)
                    {
                        if (Blocks[j].y - ControlBlock[i].y < drop) drop = Blocks[j].y - ControlBlock[i].y;
                    }
                }
            }

            // Kollar om den träffar botten.
            if (drop > 20)
            {
                for (int k = 0; k < ControlBlock.Count; k++)
                {
                    if (20 - ControlBlock[k].y < drop) drop = 20 - ControlBlock[k].y;
                }
            }
            drop -= 1;

            // Lägger till hur mycket utrymme det är från fallande blocket till längst ner vid det positionen.
            for (int i = 0; i < ControlBlock.Count; i++)
            {
                ControlBlock[i].y += drop;
            }
            panel1.Invalidate();
            panel1.Update();

            DropCheck();
            BottomCheck();
        }
        
        /// <summary>
        /// Roterar 90 grader förutom o blocket.
        /// Kollar om man kan rotera.
        /// </summary>
        private void Rotate()
        {
            if (ControlBlock[0].color != "O")
            {
                RotateBlock(90);
            }
            RotateCheck();
        }

        /// <summary>
        /// Roterar blocket v antal grader.
        /// Med hjälp av roteringsmatrix.
        /// </summary>
        /// <param name="v">Grader den ska roteras.</param>
        private void RotateBlock(int v)
        {
            for (int i = 1; i < ControlBlock.Count; i++)
            {
                double angle = v * Math.PI / 180;
                double test = Math.Cos(1.57);
                double cs = 0;
                double sn = Math.Sin(angle);
                int x = ControlBlock[i].x - ControlBlock[0].x;
                int y = ControlBlock[i].y - ControlBlock[0].y;

                ControlBlock[i].x = int.Parse((x * cs - y * sn).ToString()) + ControlBlock[0].x;
                ControlBlock[i].y = int.Parse((x * sn + y * cs).ToString()) + ControlBlock[0].y;
            }
        }

        /// <summary>
        /// Kollar om man kan rotera blocket.
        /// Annars om den inte kan rotera tillbaka.
        /// </summary>
        private void RotateCheck()
        {
            for (int j = 0; j < ControlBlock.Count; j++)
            {
                if (WidthBorderCheck(0) == false)
                {
                    RotateWidthCheck();
                }
            }

            for (int i = 0; i < Blocks.Count; i++)
            {
                for (int j = 0; j < ControlBlock.Count; j++)
                {
                    if (Blocks[i].x == ControlBlock[j].x && Blocks[i].y == ControlBlock[j].y)
                    {
                        RotateBlock(-90);
                    }
                }
            }
        }

        /// <summary>
        /// Om man försöker rotera utanför skjuter den in blocket ett steg.
        /// </summary>
        private void RotateWidthCheck()
        {
            string side = "";
            for (int i = 0; i < ControlBlock.Count; i++)
            {
                if (ControlBlock[i].x < 0)
                {
                    side = "left";
                }
                else if (ControlBlock[i].x > 9)
                {
                    side = "right";
                }
            }

            if (side == "left")
            {
                for (int i = 0; i < ControlBlock.Count; i++)
                {
                    ControlBlock[i].x += 1;
                }
            }
            else if (side == "right")
            {
                for (int i = 0; i < ControlBlock.Count; i++)
                {
                    ControlBlock[i].x -= 1;
                }
            }
        }

        /// <summary>
        /// Kollar om fallande blocket träffar en annan block.
        /// </summary>
        /// <returns>Retunerar om det går eller inte.</returns>
        private Boolean DropCheck()
        {
            for (int j = 0; j < ControlBlock.Count; j++)
            {
                for (int i = 0; i < Blocks.Count; i++)
                {
                    if (Blocks[i].y == ControlBlock[j].y + 1 && Blocks[i].x == ControlBlock[j].x)
                    {
                        DeadBlock();
                        CreateBlock(panel2.Size.Width, 2);
                        j = 5;
                        i = Blocks.Count + 1;
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Kollar om det har nått botten.
        /// </summary>
        private void BottomCheck()
        {
            for (int i = 0; i < ControlBlock.Count; i++)
            {
                if (ControlBlock[i].y > 18)
                {
                    DeadBlock();
                    CreateBlock(panel2.Size.Width, 2);
                }
            }
        }

        /// <summary>
        /// Hanterar block som är "färdiga".
        /// Kollar också om du har förlorat.
        /// </summary>
        private void DeadBlock()
        {
            for (int i = 0; i < ControlBlock.Count; i++)
            {
                Blocks.Add(ControlBlock[i]);
            }

            // Kollar om du förlorar.
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].y < 0)
                {
                    // Skriver poängen i en fil.
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(PATH_FILE, true))
                    {
                        file.WriteLine(points.ToString());
                    }
                    timer1.Enabled = false;
                    pausStart.Text = "";
                    lost.Text = "DU FÖRLORA";
                    reset.Text = "Starta om";
                }
            }
            ControlBlock.Clear();
            Points();
        }

        /// <summary>
        /// Kollar om du kommer få poäng.
        /// Lägger till antal poäng du får.
        /// </summary>
        private void Points()
        {
            int multipleRows = 0;
            List<int> y = new List<int>();
            for (int i = 0; i < Blocks.Count; i++)
            {
                y.Add(Blocks[i].y);
            }
            y.Sort();
            multipleRows = PointsCheck(y, multipleRows);
            if (multipleRows == 4) points += 2000;
            else points += (multipleRows * 100)*multipleRows;
            pointLabel.Text = points.ToString();
            y.Clear();
        }

        /// <summary>
        /// Kollar om en full rad är fyllt.
        /// </summary>
        private int PointsCheck(List<int> y, int multipleRows)
        {
            for (int i = 0; i < (panel1.Size.Height - 1) / 20; i++)
            {
                int count = 0;
                for (int j = 0; j < y.Count; j++)
                {
                    if (y[j] == i)
                    {
                        count++;
                    }

                    // Gör intervallen snabbare så spelet blir snabbare.
                    if (count == 10)
                    {
                        RemoveRow(i);
                        lines++;
                        linesLabel.Text = lines.ToString();
                        timer1.Interval -= 30;
                        levelLabel.Text = (int.Parse(levelLabel.Text) + 1).ToString();
                        count++;
                        multipleRows++;
                    }
                }
            }
            return multipleRows;
        }

        /// <summary>
        /// Tar bort en rad.
        /// Gör så att alla andra block åker ner.
        /// </summary>
        /// <param name="row">Vilken rad den ska ta bort.</param>
        private void RemoveRow(int row)
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].y == row)
                {
                    Blocks.RemoveAt(i);
                    i--;
                }
            }
            Thread.Sleep(500);
            panel1.Invalidate();
            panel1.Update();

            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].y < row)
                {
                    Blocks[i].y++;
                }
            }
        }

        /// <summary>
        /// Paus Start knappen, pausar startar timer.
        /// </summary>
        private void PausStart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true) pausStart.Text = "Start";
            else pausStart.Text = "Paus";
            timer1.Enabled = !timer1.Enabled;
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            StartGame();
        }
    }
}
