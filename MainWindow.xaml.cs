using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] Dictionary = new string[] { };
        string pickedWord;
        string obsWord;
        string guessedLetters;
        string difficulty = "hard";
        bool win = false;
        int stage = 0;
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            obsWord = "";
            Result.IsEnabled = false;
            Result.Content = "";
            win = false;
            stage = 0;
            if (easyDiff.IsChecked == true)
            {
                try
                {
                    Dictionary = File.ReadAllLines("easy.txt");
                }
                catch (IOException e){
                    Console.WriteLine("Error reading file: " + e.Message);                    
                }
                difficulty = "easy";
            }
            else if (hardDiff.IsChecked == true)
            {
                try
                {
                    Dictionary = File.ReadAllLines("words_alpha.txt");
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error reading file: " + e.Message);
                }
                difficulty = "hard";
            }
            PickWord();
            guessedLetterLabel.Content = "";
            WordBox.Text = "";
            GuessLetter.IsEnabled = true;
            GuessWord.IsEnabled = true;
            WordBox.IsEnabled = true;
            LetterBox.IsEnabled = true;
            guessedLetters = "";
            Draw();
        }

        void PickWord()
        {
            Random rnd = new Random();
            int wordIndex = 0;
            switch (difficulty)
            {
                case "hard":
                    wordIndex = rnd.Next(370104);
                    break;
                case "easy":
                    wordIndex = rnd.Next(854);
                    break;
            }
            
            pickedWord = Dictionary[wordIndex];
            wordLabel.Content = pickedWord;
            for (int i = 0; i <= pickedWord.Length; i++)            
            {
                obsWord += '_';
                obsWord += ' ';
            }
            obfusLabel.Content = obsWord;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            char[] guessedL = { };
            if (LetterBox.Text != "")
            {
                guessedL = LetterBox.Text.ToCharArray();
                List<int> correctIndex = new List<int> { };
                char[] pickedChars = pickedWord.ToCharArray();
                for (int i = 0; i < pickedWord.Length; i++)
                {
                    if (pickedChars[i] == guessedL[0])
                    {
                        correctIndex.Add(i);
                    }
                }
                LetterBox.Text = "";

                if(correctIndex.Count != 0)
                {
                    foreach(int index in correctIndex)
                    {
                        StringBuilder builder = new StringBuilder(obsWord);
                        builder.Remove((index*2)+2, 1);
                        builder.Insert((index * 2) + 2, pickedChars[index]);
                        obsWord = builder.ToString();
                        obfusLabel.Content = obsWord;
                        
                        string obfusNoSpace = obsWord.Replace(" ","");
                        obfusNoSpace = obfusNoSpace.Replace("_", "");
                        if (obfusNoSpace == pickedWord)
                        {
                            win = true;                            
                        }
                        CheckWin();
                    }                    
                }
                else
                {
                    guessedLetters += guessedL[0].ToString();
                    guessedLetters += " ";
                    guessedLetterLabel.Content = guessedLetters;
                    stage += 1;
                    Draw();
                    CheckWin();
                }
            }
        }

        void Draw()
        {
            DrawArea.Background = Brushes.AntiqueWhite;            
            switch (stage)
            {
                case 0:
                    DrawArea.Children.Clear();
                    break;
                case 1:
                    Line line = new Line();
                    line.Stroke = Brushes.Black;
                    line.X1 = 100;
                    line.X2 = 350;
                    line.Y1 = 300;
                    line.Y2 = 300;
                    line.HorizontalAlignment = HorizontalAlignment.Left;
                    line.VerticalAlignment = VerticalAlignment.Center;
                    line.StrokeThickness = 2;
                    DrawArea.Children.Add(line);
                    break;
                case 2:
                    Line line2 = new Line();
                    line2.Stroke = Brushes.Black;
                    line2.X1 = 150;
                    line2.X2 = 150;
                    line2.Y1 = 300;
                    line2.Y2 = 50;
                    line2.HorizontalAlignment = HorizontalAlignment.Left;
                    line2.VerticalAlignment = VerticalAlignment.Center;
                    line2.StrokeThickness = 2;
                    DrawArea.Children.Add(line2);
                    break;
                case 3:
                    Line line3 = new Line();
                    line3.Stroke = Brushes.Black;
                    line3.X1 = 150;
                    line3.X2 = 250;
                    line3.Y1 = 50;
                    line3.Y2 = 50;
                    line3.HorizontalAlignment = HorizontalAlignment.Left;
                    line3.VerticalAlignment = VerticalAlignment.Center;
                    line3.StrokeThickness = 2;
                    DrawArea.Children.Add(line3);
                    break;
                case 4:
                    Line line4 = new Line();
                    line4.Stroke = Brushes.Black;
                    line4.X1 = 250;
                    line4.X2 = 250;
                    line4.Y1 = 50;
                    line4.Y2 = 75;
                    line4.HorizontalAlignment = HorizontalAlignment.Left;
                    line4.VerticalAlignment = VerticalAlignment.Center;
                    line4.StrokeThickness = 2;
                    DrawArea.Children.Add(line4);
                    break;
                case 5:
                    Ellipse head = new Ellipse();
                    SolidColorBrush colorBrush = new SolidColorBrush();
                    colorBrush.Color = Color.FromRgb(0, 0, 0);
                    head.Fill = colorBrush;
                    head.StrokeThickness = 2;
                    head.Stroke = Brushes.Black;
                    head.Width = 35;
                    head.Height = 35;
                    DrawArea.Children.Add(head);
                    Canvas.SetLeft(head, 232.5f);
                    Canvas.SetTop(head, 70);
                    break;
                case 6:
                    Line line5 = new Line();
                    line5.Stroke = Brushes.Black;
                    line5.X1 = 250;
                    line5.X2 = 250;
                    line5.Y1 = 90;
                    line5.Y2 = 170;
                    line5.HorizontalAlignment = HorizontalAlignment.Left;
                    line5.VerticalAlignment = VerticalAlignment.Center;
                    line5.StrokeThickness = 2;
                    DrawArea.Children.Add(line5);
                    break;
                case 7:
                    Line line6 = new Line();
                    line6.Stroke = Brushes.Black;
                    line6.X1 = 250;
                    line6.X2 = 275;
                    line6.Y1 = 110;
                    line6.Y2 = 140;
                    line6.HorizontalAlignment = HorizontalAlignment.Left;
                    line6.VerticalAlignment = VerticalAlignment.Center;
                    line6.StrokeThickness = 2;
                    DrawArea.Children.Add(line6);
                    break;
                case 8:
                    Line line7 = new Line();
                    line7.Stroke = Brushes.Black;
                    line7.X1 = 250;
                    line7.X2 = 225;
                    line7.Y1 = 110;
                    line7.Y2 = 140;
                    line7.HorizontalAlignment = HorizontalAlignment.Left;
                    line7.VerticalAlignment = VerticalAlignment.Center;
                    line7.StrokeThickness = 2;
                    DrawArea.Children.Add(line7);
                    break;
                case 9:
                    Line line8 = new Line();
                    line8.Stroke = Brushes.Black;
                    line8.X1 = 250;
                    line8.X2 = 275;
                    line8.Y1 = 170;
                    line8.Y2 = 210;
                    line8.HorizontalAlignment = HorizontalAlignment.Left;
                    line8.VerticalAlignment = VerticalAlignment.Center;
                    line8.StrokeThickness = 2;
                    DrawArea.Children.Add(line8);
                    break;
                case 10:
                    Line line9 = new Line();
                    line9.Stroke = Brushes.Black;
                    line9.X1 = 250;
                    line9.X2 = 225;
                    line9.Y1 = 170;
                    line9.Y2 = 210;
                    line9.HorizontalAlignment = HorizontalAlignment.Left;
                    line9.VerticalAlignment = VerticalAlignment.Center;
                    line9.StrokeThickness = 2;
                    DrawArea.Children.Add(line9);
                    break;                
            }
            CheckWin();
        }

        void CheckWin()
        {
            if (win == true)
            {
                Result.Content = "YOU WIN";
                Result.IsEnabled = true;
                GuessLetter.IsEnabled = false;
                GuessWord.IsEnabled = false;
                WordBox.IsEnabled = false;
                LetterBox.IsEnabled = false;
            }
            else if (win == false && stage == 10)
            {
                Result.Content = "YOU LOSE";
                Result.IsEnabled = true;
                string spacedWord = string.Join(" ", pickedWord.ToCharArray());
                obfusLabel.Content = spacedWord;
                GuessLetter.IsEnabled = false;
                GuessWord.IsEnabled = false;
                WordBox.IsEnabled = false;
                LetterBox.IsEnabled = false;
            }
        }

        private void GuessWord_Click(object sender, RoutedEventArgs e)
        {
            if(WordBox.Text == pickedWord)
            {                
                win = true;
                string spacedWord = string.Join(" ", pickedWord.ToCharArray());
                obfusLabel.Content = spacedWord;
                CheckWin();
            }
            else
            {
                stage += 1;
                Draw();
            }
        }
    }
}
