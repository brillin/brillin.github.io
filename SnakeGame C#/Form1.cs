// 2020-05-05 Snake Spel.
// Komplettering av David T för att visa att jag kan kodkonventionerna.

// Ett vanligt snake spel som rör sig med piltangenterna.
// Några finesser som jag gjorde var:
// Starta om knappen för att starta om när man dött.
// Man kan förstora och minska spelplanet genom att dra ut fönstret.
// Det finns 3 olika matbitar röd, blå och lila.
// Den röda gör så att du kan gå igenom väggar i 5 sekunder.
// Den blåa gör så att du går snabbare i 5 sekunder.
// Den lila är en vanlig matbit som gör inget.
// Det finns en timer för att se hur långt tid det tog, samt timer för alla powerups och matbit.
// Om du ätit nyss en powerup kommer bara lila frukter komma så länge din powerup varar.
// Efter man har ätit 3 matbitar levlas det upp, ormen rör sig lite snabbare.
// Vid level 3 (9 matbitar) kommer hinder slumpas ut som du måste undvika.
// Man kan pausa spelet.
// Det är 2 matbitar som slumpas åt gången.
// Matbiten syns bara för 10 sekunder, om du inte äter kommer ormen krympa.
// Highscore lista som håller koll på ditt högsta poäng , i en separat fil.

// I detta projekt använder jag mig av klasser, vilket inte ingår i kursen.
// Men det som är så bra med klasser är att det blir lätt att skapa flera av en sort.
// Man kan tänka sig en klass som en "blueprint" som man kan skapa olika saker med.
// Till exempel mina 2 frukter, först kallar jag på klassen 2 gånger o medan jag gör det slumpar jag färgerna åt dem.
// Dessutom är det väldigt användbart för att hålla koll på egenskaperna av en klass, med hjälp av punktnotation, snake.snakeX tex.
// Det är också praktiskt att man kan lägga klasser i listor vilket gör det ännu lättare när man ska skapa flera klasser.
// Klasser håller koden ren och det blir inte krångligt när man ska hitt egenskaperna till något.

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

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        // Deklarerar alla globala variabler.
        int minute = 0;
        int seconds = 0;
        int points = 0;
        int eatSeconds = 0;
        int speed = 250;
        int powerTimer = 0;
        int foodTime = 0;
        int level = 0;
        int dx = snakeSize;
        int dy = 0;

        static int snakeSize = 20;
        string powerType = "";
        string direction = "";
        string PATH_FILE = @"c:\Snake\scores.txt";
        static int panelWidth = 0;
        static int panelHeight = 0;

        bool wall = true;
        static bool randomFruit = true;
        static Random random = new Random();

        // Deklarerar alla listor.
        static List<Snake> snakeList = new List<Snake>();
        static List<Obstacle> obstacleList = new List<Obstacle>();
        List<Fruit> fruitList = new List<Fruit>();
        List<int> scores = new List<int>();

        // Deklarerar olika färger.
        SolidBrush BLACK_BRUSH = new SolidBrush(Color.Black);
        SolidBrush GREEN_BRUSH = new SolidBrush(Color.Green);
        SolidBrush RED_BRUSH = new SolidBrush(Color.Red);
        SolidBrush BLUE_BRUSH = new SolidBrush(Color.Blue);
        SolidBrush PURPLE_BRUSH = new SolidBrush(Color.Purple);

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Frukt Klass.
        /// </summary>
        public class Fruit
        {
            // Variabler för frukterna.
            public int fruitX;
            public int fruitY;
            public string color;

            /// <summary>
            /// Metod för att rita ut en ny frukt.
            /// Ger Frukterna en slumpmässig färg också.
            /// </summary>
            public void FruitDraw()
            {
                FruitCollition();
                
                int colorNumber = random.Next(100);
                if (randomFruit == true)
                {
                    if (colorNumber < 10)
                    {
                        color = "red";
                    }
                    else if (colorNumber >= 10 && colorNumber <= 20)
                    {
                        color = "blue";
                    }
                    else color = "purple";
                }
                else color = "purple";
            }

            /// <summary>
            /// Ser till så att frukterna kan inte hamna på ormen eller på hinder.
            /// Använder en boolean, så fort den hamnar fel så kör while loopen om.
            /// </summary>
            public void FruitCollition()
            {
                bool clear = false;
                while (clear == false)
                {
                    clear = true;
                    fruitX = FruitRandomX();
                    fruitY = FruitRandomY();
                    for (int i = 0; i < snakeList.Count; i++)
                    {
                        if (fruitX == snakeList[i].snakeX && fruitY == snakeList[i].snakeY)
                        {
                            clear = false;
                        }
                    }
                    for (int i = 0; i < obstacleList.Count; i++)
                    {
                        if (fruitX == obstacleList[i].obstacleX && fruitY == obstacleList[i].obstacleY)
                        {
                            clear = false;
                        }
                    }
                }
            }

            /// <summary>
            /// Slumpar x värde för fruktens position.
            /// </summary>
            /// <returns>Returnerar x värdet</returns>
            public int FruitRandomX()
            {
                int x = random.Next(0, panelWidth);
                while (x % snakeSize != 0)
                {
                    x = random.Next(0, panelWidth);
                }
                return x;
            }

            /// <summary>
            /// Slumpar y värde för fruktens position.
            /// </summary>
            /// <returns>Returnerar y värdet</returns>
            public int FruitRandomY()
            {
                int y = random.Next(0, panelHeight);
                while (y % snakeSize != 0)
                {
                    y = random.Next(0, panelHeight);
                }
                return y;
            }
        }

        /// <summary>
        /// Hinder Klass.
        /// </summary>
        public class Obstacle
        {
            // Variabler för hindren.
            public int obstacleX;
            public int obstacleY;

            /// <summary>
            /// Metod för att rita ut en ny hinder.
            /// Anvnänder samma metod med boolean, så fort det blir fel körs while loopen om.
            /// </summary>
            public void ObstacleDraw()
            {
                bool clear = false;
                obstacleX = ObstacleRandomX();
                obstacleY = ObstacleRandomY();
                while (clear == false)
                {
                    clear = true;

                    for (int i = 0; i < snakeList.Count; i++)
                    {
                        if (obstacleX == snakeList[i].snakeX && obstacleY == snakeList[i].snakeY)
                        {
                            clear = false;
                        }
                    }
                    for (int i = 0; i < obstacleList.Count; i++)
                    {
                        if (obstacleX == obstacleList[i].obstacleX && obstacleY == obstacleList[i].obstacleY)
                        {
                            clear = false;
                        }
                    }
                }
            }

            /// <summary>
            /// Slumpar x värde för hinder position.
            /// </summary>
            /// <returns>Returnerar x värdet</returns>
            public int ObstacleRandomX()
            {
                int x = random.Next(0, panelWidth);
                while (x % snakeSize != 0)
                {
                    x = random.Next(0, panelWidth);
                }
                return x;
            }

            /// <summary>
            /// Slumpar y värde för hinder position.
            /// </summary>
            /// <returns>Returnerar y värdet</returns>
            public int ObstacleRandomY()
            {
                int y = random.Next(0, panelHeight);
                while (y % snakeSize != 0)
                {
                    y = random.Next(0, panelHeight);
                }
                return y;
            }
        }

        /// <summary>
        /// Snake Klass.
        /// </summary>
        public class Snake
        {
            // Deklarerar variabler för ormen.
            public int snakeX;
            public int snakeY;

            /// <summary>
            /// Skapar ormen och sätter parametrarna till variablerna för ormen.
            /// </summary>
            /// <param name="x">orm x värde</param>
            /// <param name="y">orm y värde</param>
            public Snake(int x, int y)
            {
                snakeX = x;
                snakeY = y;
            }
        }

        /// <summary>
        /// Skapar ormen och frukten när du startar programmet.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateFile();
            PanelSize();
            HighscoreSet();
            snakeList.Add(new Snake(60, 20));
            for (int i = 0; i < 2; i++)
            {
                Fruit fruit = new Fruit();
                fruit.FruitDraw();
                fruitList.Add(fruit);
            }
        }

        /// <summary>
        /// Skapar en fil för att spara highsores.
        /// </summary>
        private void CreateFile()
        {
            string path = @"c:\Snake";
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
        /// Sorterar alla highscore och lägger det högsta på label.
        /// </summary>
        private void HighscoreSet()
        {
            string[] reading = System.IO.File.ReadAllLines(PATH_FILE);
            for (int i = 0; i < reading.Length; i++)
            {
                scores.Add(int.Parse(reading[i]));
            }
            scores.Sort();
            highscoreLabel.Text = scores[scores.Count - 1].ToString();
        }

        /// <summary>
        /// Ritar alla delarna.
        /// </summary>
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Ritar alla hinder, med svart fill.
            for (int i = 0; i < obstacleList.Count; i++)
            {
                Rectangle fruit = new Rectangle(obstacleList[i].obstacleX, obstacleList[i].obstacleY, snakeSize, snakeSize);
                e.Graphics.FillRectangle(BLACK_BRUSH, fruit);
            }
            // Ritar ut alla frukter med respektive färg.
            for (int i = 0; i < fruitList.Count; i++)
            {
                Rectangle fruit = new Rectangle(fruitList[i].fruitX, fruitList[i].fruitY, snakeSize, snakeSize);
                if (fruitList[i].color == "blue") e.Graphics.FillRectangle(BLUE_BRUSH, fruit);
                else if (fruitList[i].color == "red") e.Graphics.FillRectangle(RED_BRUSH, fruit);
                else if (fruitList[i].color == "purple") e.Graphics.FillRectangle(PURPLE_BRUSH, fruit);
            }
            // Ritar ut ormen.
            Pen pen = new Pen(Color.Black, 2);
            for (int i = 0; i < snakeList.Count; i++)
            {
                Rectangle rect = new Rectangle(snakeList[i].snakeX, snakeList[i].snakeY, snakeSize, snakeSize);
                e.Graphics.FillRectangle(GREEN_BRUSH, rect);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        /// <summary>
        /// Metod för när ormen äter.
        /// </summary>
        public void Eat()
        {
            for (int i = 0; i < fruitList.Count; i++)
            {
                // Om den har träffat på en matbit.
                if (fruitList[i].fruitX == snakeList[0].snakeX && fruitList[i].fruitY == snakeList[0].snakeY)
                {
                    // Tar hur många sekunder det är på klockan så att man kan kolla när 10 sek har gått till nästa matbit.
                    eatSeconds = seconds % 10;

                    // Lägger till ett orm block sist i kön.
                    snakeList.Add(new Snake(snakeList[snakeList.Count - 1].snakeX, snakeList[snakeList.Count - 1].snakeY));
                    EatFood(i);

                    points++; // Adderar poäng.
                    LevelChange();

                    NewFruit();
                }
            }
        } 

        /// <summary>
        /// Ändrar allt som händer när en ny matbit slumpas.
        /// </summary>
        public void NewFruit()
        {
            // Ritar ut nya frukter.
            for (int j = 0; j < fruitList.Count; j++)
            {
                fruitList[j].FruitDraw();
            }

            // Startar om klockan och skriver nya poängen och level.
            foodTime = 0;
            foodLabel.Text = "10";
            pointCount.Text = points.ToString();
            levelCount.Text = level.ToString();
        }

        /// <summary>
        /// För alla ändringar som händer när det levlas upp.
        /// </summary>
        public void LevelChange()
        {
            // Levlar upp spelaren var tredje matbit.
            if (points % 3 == 0) 
            {
                moveTimer.Interval -= 25;
                level++;
            }
            // Om man har kommit till level 3, slumpa ut hinder.
            if (level == 3 && points == 9) 
            {
                for (int j = 0; j < 10; j++)
                {
                    Obstacle obstacle = new Obstacle();
                    obstacle.ObstacleDraw();
                    obstacleList.Add(obstacle);
                }
            }
        }
 
        /// <summary>
        /// Kollar vilken sorts mat ormen har ätit.
        /// </summary>
        public void EatFood(int i)
        {
            if (fruitList[i].color == "red")
            {
                powerType = "wall";
                power.Enabled = true;
                randomFruit = false;
            }
            else if (fruitList[i].color == "blue")
            {
                powerType = "speed";
                power.Enabled = true;
                randomFruit = false;
            }
            PowerUp();
        }

        /// <summary>
        /// Ändrar programmet beroende på vilken matbit man åt.
        /// </summary>
        public void PowerUp()
        {
            if (powerType == "speed")
            {
                moveTimer.Interval -= 75;
                powerType = "";
            } 
            else if (powerType == "wall")
            {
                wall = false;
                powerType = "";
            }
        }

        /// <summary>
        /// Metod för att kolla om ormen dog.
        /// </summary>
        public bool Dead()
        {
            // Kollar om den krashar in i sig själv.
            for (int i = 1; i < snakeList.Count - 1; i++)
            {
                if (snakeList[0].snakeX == snakeList[i].snakeX && snakeList[0].snakeY == snakeList[i].snakeY)
                {
                    Reset();
                    return true;
                }
            }
            // Kollar om ormen blir för kort.
            if (snakeList.Count == 0)
            {
                Reset();
                return true;
            }
            // Kollar om ormen krashar in i väggen.
            if (WallCheck() == true) return true;
            // Kollar om ormen krashar in i hinder.
            if (ObstacleCheck() == true) return true;
            // Om inget har hänt retunera inget.
            return false; 
        }

        /// <summary>
        /// Kollar om ormen krashar in i någon hinder.
        /// </summary>
        public bool ObstacleCheck()
        {
            for (int i = 0; i < obstacleList.Count; i++)
            {
                if (snakeList[0].snakeX == obstacleList[i].obstacleX && snakeList[0].snakeY == obstacleList[i].obstacleY)
                {
                    Reset();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Kollar om ormen krashar in i väggen.
        /// Bara om väggar är på, annars kommer ormen ut ur andra sidan.
        /// </summary>
        public bool WallCheck()
        {
            if (wall == false)
            {
                if (snakeList[0].snakeX > panel1.Size.Width) snakeList[0].snakeX -= panel1.Size.Width + snakeSize;
                if (snakeList[0].snakeY > panel1.Size.Height) snakeList[0].snakeY -= panel1.Size.Height + snakeSize;
                if (snakeList[0].snakeX < 0) snakeList[0].snakeX += panel1.Size.Width;
                if (snakeList[0].snakeY < 0) snakeList[0].snakeY += panel1.Size.Height;
            }
            else if (wall == true)
            {
                if (snakeList[0].snakeX + snakeSize > panel1.Size.Width || snakeList[0].snakeX < 0 || snakeList[0].snakeY < 0 || snakeList[0].snakeY + snakeSize > panel1.Size.Height)
                {
                    Reset();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Nollställer och startar om alla variabler.
        /// Samt stoppa klockorna tills man trycker "start".
        /// </summary>
        public void Reset()
        {
            using (System.IO.StreamWriter file =
                   new System.IO.StreamWriter(PATH_FILE, true))
            {
                file.WriteLine(points.ToString());
            }
            HighscoreSet();

            ResetVariables();
        }

        /// <summary>
        /// Nollställer alla variabler.
        /// </summary>
        private void ResetVariables()
        {
            // Int.
            seconds = 0;
            points = 0;
            speed = 200;
            powerTimer = 0;
            foodTime = 0;
            eatSeconds = 0;
            level = 0;
            // Label.
            powerType = "";
            lostLabel.Text = "DU FÖRLORA!";
            pointsLabel.Text = "Du fick " + (snakeList.Count - 1).ToString() + " poäng";
            foodLabel.Text = "10";
            speedLabel.Text = "0";
            wallLabel.Text = "0";
            levelCount.Text = "0";
            pointCount.Text = "0";
            pausStart.Text = "Start";
            resetLabel.Visible = true;
            // Timer.
            moveTimer.Enabled = !moveTimer.Enabled;
            moveTimer.Interval = speed;
        }

        /// <summary>
        /// Timer för att rita om spelbrädet.
        /// Samt för att röra ormen.
        /// Först flyttas kroppen fram, sedan sist rör huvudet fram.
        /// </summary>
        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            // Om ormen är död flytta inte fram.
            if (Dead() == true) return;
            SnakeMove();
            // Rör ormkroppen.
            for (int i = 0; i < snakeList.Count - 1; i++) 
            {
                snakeList[snakeList.Count - i - 1].snakeX = snakeList[snakeList.Count - i - 2].snakeX;
                snakeList[snakeList.Count - i - 1].snakeY = snakeList[snakeList.Count - i - 2].snakeY;
            }
            snakeList[0].snakeX += dx;
            snakeList[0].snakeY += dy;
            Eat();

            panel1.Invalidate();
        }

        /// <summary>
        /// Kollar vilken tangent som var itryckt.
        /// Ändrar vilken tangent som var senast tryckt.
        /// När ormen ska röra på sig använder det sig av sista tryckta tangenten.
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Om ormen inte ska röra på sig körs inte metoden.
            if (moveTimer.Enabled == false) return;

            direction = e.KeyCode.ToString();
        }

        /// <summary>
        /// Ändrar vilket håll den rör på sig.
        /// </summary>
        private void SnakeMove()
        {
            if (direction == "Left" && dx != snakeSize)
            {
                dx = -snakeSize;
                dy = 0;
            }
            else if (direction == "Right" && dx != -snakeSize)
            {
                dx = snakeSize;
                dy = 0;
            }
            else if (direction == "Up" && dy != snakeSize)
            {
                dx = 0;
                dy = -snakeSize;
            }
            else if (direction == "Down" && dy != -snakeSize)
            {
                dx = 0;
                dy = snakeSize;
            }
        }

        /// <summary>
        /// Startar om spelet till original läge.
        /// Rensar alla listor.
        /// Skapar om allt som original.
        /// </summary>
        private void ResetLabel_Click(object sender, EventArgs e)
        {
            wall = true;
            randomFruit = true;
            lostLabel.Text = " ";
            pointsLabel.Text = " ";
            time.Text = "0:0";
            dx = snakeSize;
            dy = 0;
            obstacleList.Clear();
            snakeList.Clear();
            snakeList.Add(new Snake(60, 20));
            snakeList[0].snakeX = snakeSize;
            snakeList[0].snakeY = 60;
            for (int i = 0; i < 2; i++)
            {
                fruitList[i].FruitDraw();
            }
            panel1.Invalidate();
            resetLabel.Visible = false;
        }

        /// <summary>
        /// Stannar spelet när man trycket på paus, och vise versa.
        /// </summary>
        private void PausStart_Click(object sender, EventArgs e)
        {
            moveTimer.Enabled = !moveTimer.Enabled;
            if (pausStart.Text == "Start")
            {
                pausStart.Text = "Paus";
            }
            else
            {
                pausStart.Text = "Start";
            }
        }

        /// <summary>
        /// Hanterar klockan.
        /// </summary>
        private void Clock_Tick(object sender, EventArgs e)
        {
            if (moveTimer.Enabled == false) return;
            foodTime++;
            seconds++;
            minute = seconds / 60; 
            time.Text = minute + ":" + seconds;
            if (seconds == 60)
            {
                seconds = 0;
            }
            foodLabel.Text = (10 - foodTime).ToString();

            // Om det har gått 10 sekunder efter du ätit mat kommer den skapa nytt och göra ormen mindre.
            if (seconds % 10 == 0 + eatSeconds)
            {
                snakeList.RemoveAt(snakeList.Count - 1);
                for (int i = 0; i < fruitList.Count; i++)
                {
                    fruitList[i].FruitDraw();
                }
                foodTime = 0;
            }
        }

        /// <summary>
        /// Timer som håller koll på hur länge din powerup har gått.
        /// </summary>
        private void Power_Tick(object sender, EventArgs e)
        {
            if (moveTimer.Enabled == false) return;

            powerTimer++;
            if (wall == false) wallLabel.Text = (5 - powerTimer).ToString();
            else if (moveTimer.Interval != speed) speedLabel.Text = (5 - powerTimer).ToString();
            
            // Om det har gått 5 sekunder, återställ allt.
            if (powerTimer == 5)
            {
                randomFruit = true;
                wall = true;
                moveTimer.Interval = speed;
                power.Enabled = false;
                powerTimer = 0;
            }
        }

        /// <summary>
        /// Gör nya variabler för panel storleken när du ändrar den.
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            PanelSize();
        }

        /// <summary>
        /// Uppdaterar variabler.
        /// </summary>
        public void PanelSize()
        {
            panelWidth = panel1.Width-10;
            panelHeight = panel1.Height-10;
        }
    }
}
