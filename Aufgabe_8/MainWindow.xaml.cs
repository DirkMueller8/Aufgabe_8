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

namespace Aufgabe_8
{
    // Author: Dirk Mueller
    // Date: 16.03.2021
    //
    // Algorithm:
    // 1. Take the input from the user and check the validity of it
    // 2. Each row (starting with 1) to be printed is a combination of spaces and star symbols
    // 3. There are spaces as much as triangle height minus row number
    // 4. The amount of stars is 1, 3, 5, 7,... in rows 1, 2, 3, 4, 5, respectively
    //

    public partial class MainWindow : Window
    {
        List<string> wordList = new List<string>
        { "Wetter", "Milch", "Datum", "Pflanze", "Ampel", "Telefon", "Toast", "Abitur", "Kante", "Wippe"};
        List<int> charPositionList = new List<int>();
        Random random = new Random();
        //char[] charArray = new char[];
        int randomNumber;
        string word;
        string stars = "";

        public MainWindow()
        {
            InitializeComponent();
            word = wordList[randomNumber];
            lblFinished.Content = word; ;
            for (int i = 0; i < word.Length; i++)
            {
                stars += "*";
            }
            txtWordDisplay.Text = "";
        }

        private void btnNewWord_Click(object sender, RoutedEventArgs e)
        {
            randomNumber = random.Next(1, 11);
            StringBuilder sb = new StringBuilder("", word.Length);
            for (int i = 0; i < word.Length; i++)
            {
                sb.Append("*");
            }
            txtWordDisplay.Text = sb.ToString();
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            char letterGuessed;
            if (txtGuess.Text.Length == 0)
            {
                MessageBox.Show("You did not give a character");
            }
            else if (txtGuess.Text.Length > 1)
            {
                MessageBox.Show("More than one character. Please try again");
            }
            else
            {
                letterGuessed = Convert.ToChar(txtGuess.Text);
                Process(letterGuessed);
            }
        }
        private void Process(char letterGuessed)
        {
            char[] charWord = new char[word.Length];
            //char eingabe;
            string starsReplaced;
            int AmountOfGuesses = 0;

            for (int i = 0; i < word.Length; i++)
            {
                charWord[i] = word[i];
            }

            starsReplaced = ReplaceByLetterFound(letterGuessed, stars, charWord);
            txtWordDisplay.Text = starsReplaced;

            if (AllStarsReplacedByLetters(starsReplaced))
            {
                lblFinished.Content = "Congratulations! You did it in " + AmountOfGuesses + " guesses";
                stars = "";
                word = "";
            }
            // Replace star sequence by newly formed sequence:
            stars = starsReplaced;
        }

        static string ReplaceByLetterFound(char letter, string stars, char[] word)
        {
            StringBuilder sb = new StringBuilder(stars);
            for (int i = 0; i < word.Length; i++)
            {
                // If 
                if (word[i] == letter)
                {
                    sb[i] = letter;
                }
            }
            return sb.ToString();
        }

        static Boolean AllStarsReplacedByLetters(string stars)
        {
            StringBuilder sb = new StringBuilder(stars);
            for (int i = 0; i < stars.Length; i++)
            {
                // If 
                if (stars[i] == '*')
                {
                    return false;
                }
            }
            return true;
        }
    }
}