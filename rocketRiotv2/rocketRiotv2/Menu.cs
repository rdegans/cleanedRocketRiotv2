using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Media;
using System.IO;
using System.Windows.Threading;

namespace rocketRiotv2
{
    public class Menu
    {
        Canvas canvas;
        Button btnPause = new Button();
        Button btnHighScores = new Button();
        Button btnBack = new Button();
        Button btnStartGame;
        Button btnInstruct = new Button();
        Button btnRecord = new Button();
        Label lblHighScores = new Label();
        Label lblInstruct = new Label();
        Label lblTitle = new Label();
        Label lblScore;
        TextBox txtName = new TextBox();
        DispatcherTimer gameTimer;
        bool paused = false;
        int score;
        FontFamily font;
        string[] highScores = new string[5];
        string[] highScoreData;
        ImageBrush spritefill;//Image for the player
        BitmapImage bitmapImage;//Image file to use
        public Menu (Canvas c, DispatcherTimer gT, Button btnSG, Label lblS)
        {
            canvas = c;
            gameTimer = gT;
            btnStartGame = btnSG;
            lblScore = lblS;
            font = new FontFamily("Impact");
            bitmapImage = new BitmapImage(new Uri("background.png", UriKind.Relative));
            spritefill = new ImageBrush(bitmapImage);
            spritefill.Stretch = Stretch.UniformToFill;       
            canvas.Background = spritefill;
        }
        public void generate()
        {
            btnPause.Content = "||";
            btnPause.FontSize = 30;
            btnPause.FontFamily = font;
            btnPause.Background = Brushes.Gray;
            Canvas.SetLeft(btnPause, 720);
            Canvas.SetTop(btnPause, 8);
            btnPause.Height = 60;
            btnPause.Width = 60;
            btnPause.Click += btnPause_Click;

            btnStartGame.Content = "Start Game";
            btnStartGame.FontSize = 50;
            btnStartGame.FontFamily = font;
            btnStartGame.Background = Brushes.Yellow;
            Canvas.SetLeft(btnStartGame, 275);
            Canvas.SetTop(btnStartGame, 50);
            btnStartGame.BorderThickness = new Thickness(1);
            btnStartGame.BorderBrush = Brushes.Black;
            canvas.Children.Add(btnStartGame);

            btnHighScores.Content = "High Scores";
            btnHighScores.FontSize = 40;
            btnHighScores.FontFamily = font;
            btnHighScores.Background = Brushes.Yellow;
            Canvas.SetLeft(btnHighScores, 290);
            Canvas.SetTop(btnHighScores, 125);
            btnHighScores.BorderThickness = new Thickness(1);
            btnHighScores.BorderBrush = Brushes.Black;
            canvas.Children.Add(btnHighScores);
            btnHighScores.Click += btnHighScores_Click;

            btnBack.Content = "Back";
            btnBack.FontSize = 40;
            btnBack.FontFamily = font;
            btnBack.Background = Brushes.Yellow;
            Canvas.SetLeft(btnBack, 358);
            Canvas.SetTop(btnBack, 500);
            btnBack.BorderThickness = new Thickness(1);
            btnBack.BorderBrush = Brushes.Black;
            btnBack.Click += btnBack_Click;

            btnInstruct.Content = "Instructions";
            btnInstruct.FontSize = 40;
            btnInstruct.FontFamily = font;
            btnInstruct.Background = Brushes.Yellow;
            Canvas.SetLeft(btnInstruct, 287);
            Canvas.SetTop(btnInstruct, 190);
            btnInstruct.BorderThickness = new Thickness(1);
            btnInstruct.BorderBrush = Brushes.Black;
            canvas.Children.Add(btnInstruct);
            btnInstruct.Click += btnInstruct_Click;

            Canvas.SetLeft(lblScore, 10);
            Canvas.SetTop(lblScore, 10);
            lblScore.Content = "Score :";
            lblScore.Background = Brushes.Yellow;
            lblScore.FontSize = 40;
            lblScore.FontFamily = font;

            lblHighScores.FontSize = 38;
            lblHighScores.FontFamily = font;
            lblHighScores.Background = new SolidColorBrush(Color.FromArgb(125, 255, 255, 255));
            lblHighScores.Foreground = Brushes.Black;
            Canvas.SetLeft(lblHighScores, 0);
            Canvas.SetTop(lblHighScores, 0);

            lblInstruct.Content = "Use arrow keys to fly up\nGravity pulls you down\nAvoid zappers to live\nCollect coins to increase score\nPause in top right";
            lblInstruct.FontSize = 38;
            lblInstruct.FontFamily = font;
            lblInstruct.Background = new SolidColorBrush(Color.FromArgb(125, 255, 255, 255));
            lblInstruct.Foreground = Brushes.Black;
            Canvas.SetLeft(lblInstruct, 0);
            Canvas.SetTop(lblInstruct, 0);

            lblTitle.Foreground = Brushes.Gold;
            lblTitle.Background = Brushes.Gray;
            lblTitle.FontSize = 100;
            lblTitle.FontFamily = font;
            lblTitle.Content = "Rocket Riot";
            Canvas.SetLeft(lblTitle, 160);
            Canvas.SetTop(lblTitle, 300);
            canvas.Children.Add(lblTitle);

            btnRecord.Content = "Record";
            btnRecord.FontSize = 40;
            btnRecord.FontFamily = font;
            btnRecord.Background = Brushes.Yellow;
            Canvas.SetLeft(btnRecord, 358);
            Canvas.SetTop(btnRecord, 500);
            btnRecord.BorderThickness = new Thickness(1);
            btnRecord.BorderBrush = Brushes.Black;
            btnRecord.Click += btnRecord_Click;

            txtName.Background = new SolidColorBrush(Color.FromArgb(125, 255, 255, 255));
            txtName.Foreground = Brushes.Black;
            txtName.FontSize = 40;
            txtName.FontFamily = font;
            txtName.Text = "Name";
            Canvas.SetLeft(txtName, 358);
            Canvas.SetTop(txtName, 400);
        }
        public void startGame()
        {
            canvas.Children.Remove(btnStartGame);
            canvas.Children.Remove(btnHighScores);
            canvas.Children.Remove(btnInstruct);
            canvas.Children.Remove(lblTitle);
            canvas.Children.Add(btnPause);
            canvas.Children.Add(lblScore);
        }
        public void startScreen()
        {
            canvas.Children.Add(btnStartGame);
            canvas.Children.Add(btnHighScores);
            canvas.Children.Add(btnInstruct);
            canvas.Children.Add(lblTitle);
            canvas.Children.Remove(btnPause);
            canvas.Children.Remove(lblScore);
            lblScore.Content = "Score: ";
        }
        public void loseGame(int scoreIn)
        {
            canvas.Children.Add(btnRecord);
            canvas.Children.Add(txtName);
            txtName.Text = "Name";
            score = scoreIn;
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (paused)
            {
                gameTimer.Start();
                btnPause.Content = "||";
                paused = false;
            }
            else
            {
                gameTimer.Stop();
                btnPause.Content = ">";
                paused = true;
            }
        }
        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            string[] newHighScores = new string[5];
            bool newHigh = false;
            StreamReader sr = new StreamReader("HighScores.txt");
            for (int i = 0; i < 5; i++)
            {
                highScores[i] = sr.ReadLine();
            }
            sr.Close();
            StreamWriter sw = new StreamWriter("HighScores.txt");
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    int currentScore;
                    int.TryParse(highScores[i].Split(',')[1], out currentScore);
                    if (score > currentScore && !newHigh)
                    {
                        sw.WriteLine(txtName.Text + "," + score);
                        newHigh = true;
                        sw.WriteLine(highScores[i]);
                    }
                    else
                    {
                        sw.WriteLine(highScores[i]);
                        //not writing when replacing
                    }
                }
                catch
                {
                    if (!newHigh)
                    {
                        sw.WriteLine(txtName.Text + "," + score);
                        newHigh = true;
                    }
                }
            }
            sw.Flush();
            sw.Close();
            canvas.Children.Add(btnStartGame);
            canvas.Children.Add(btnHighScores);
            canvas.Children.Add(btnInstruct);
            canvas.Children.Add(lblTitle);
            canvas.Children.Remove(btnPause);
            canvas.Children.Remove(lblScore);
            canvas.Children.Remove(txtName);
            canvas.Children.Remove(btnRecord);
            lblScore.Content = "Score: ";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Remove(btnBack);
            canvas.Children.Remove(lblHighScores);
            canvas.Children.Add(btnHighScores);
            canvas.Children.Remove(lblInstruct);
            canvas.Children.Add(btnInstruct);
            canvas.Children.Add(btnStartGame);
        }
        private void btnInstruct_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Remove(btnStartGame);
            canvas.Children.Remove(btnHighScores);
            canvas.Children.Remove(btnInstruct);
            canvas.Children.Add(btnBack);
            canvas.Children.Add(lblInstruct);
        }
        private void btnHighScores_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Remove(btnStartGame);
            canvas.Children.Remove(btnHighScores);
            canvas.Children.Remove(btnInstruct);
            canvas.Children.Add(btnBack);
            canvas.Children.Add(lblHighScores);
            StreamReader sr = new StreamReader("HighScores.txt");
            for (int i = 0; i < 5; i++)
            {
                string readingScore = sr.ReadLine();
                highScores[i] = readingScore;
            }
            sr.Close();
            lblHighScores.Content = "";
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    highScoreData = highScores[i].Split(',');
                    lblHighScores.Content += (i + 1) + ". " + highScoreData[0] + " " + highScoreData[1] + Environment.NewLine;
                }
                catch
                {
                    lblHighScores.Content += (i + 1) + ". " + "No Scores" + " " + "No Scores" + Environment.NewLine;
                }
            }
        }
    }
}
