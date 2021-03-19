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
    // Date: 19.03.2021
    //
    // Algorithm:
    // 1. Create random word
    // 2. Have user give a letter
    // 3. Check the existence of the letter in the word
    // 4. If the letter exists replace it by a star and display it on screen
    // 5. When all letters are guessed inform the use
    // 6. If more than 10 guesses were place abort the game

    public partial class MainWindow : Window
    {
        List<string> wordList = new List<string>
        { "Wetter", "Milch", "Datum", "Pflanze", "Ampel", "Telefon", "Toast", "Abitur", "Kante", "Wippe"};
        //List<int> charPositionList = new List<int>();
        Random random = new Random();
        int randomNumber;
        string word = "";
        string stars = "";
        static int AmountOfGuesses = 0;
        StringBuilder sb;

        public MainWindow()
        {
            InitializeComponent();
            lbLIntroduction.Content = "Press 'Create new' and a word is there for you \nto be guessed letter by letter.";
        }

        private void btnNewWord_Click(object sender, RoutedEventArgs e)
        {
            randomNumber = random.Next(1, 11);
            // Pick word randomly:
            word = wordList[randomNumber];
            lblFinished.Content = word;
            for (int i = 0; i < word.Length; i++)
            {
                stars += "*";
            }
            txtWordDisplay.Text = "";
            sb = new StringBuilder("", word.Length);
            for (int i = 0; i < word.Length; i++)
            {
                sb.Append("*");
            }
            txtWordDisplay.Text = sb.ToString();
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            char letterGuessed;
            if (word == "")
            {
                MessageBox.Show("First press 'Create new', please.");
            }
            else if (txtGuess.Text.Length == 0)
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

            for (int i = 0; i < word.Length; i++)
            {
                charWord[i] = word[i];
            }

            if (AmountOfGuesses <= 10)
            {
                starsReplaced = ReplaceByLetterFound(letterGuessed, stars, charWord);
                txtWordDisplay.Text = starsReplaced;

                if (AllStarsReplacedByLetters(starsReplaced))
                {
                    lblFinished.Content = "Congratulations! You did it in " + AmountOfGuesses + " guesses";
                    stars = "";
                    word = "";
                    txtWordDisplay.Text = "";
                    sb.Clear();
                }
                // Replace star sequence by newly formed sequence:
                stars = starsReplaced;
            }
            else
            {
                MessageBox.Show("Sorry, you have exceeded the limit of 10 guesses");
            }
        }

        static string ReplaceByLetterFound(char letter, string stars, char[] word)
        {
            StringBuilder sb = new StringBuilder(stars);
            for (int i = 0; i < word.Length; i++)
            {
                // If 
                if (Char.ToUpper(word[i]) == Char.ToUpper(letter))
                {
                    sb[i] = word[i];
                }
            }
            AmountOfGuesses++;
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