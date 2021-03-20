using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Aufgabe_8
{
    // Author: Dirk Mueller
    // Date: 20.03.2021
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
        { "Water", "Groove", "Locker", "Appel", "Lemon", "Telefon", "Toast", "Boston", "Africa", "Sweden"};
        Random random = new Random();
        int randomNumber;
        string word = "";
        string stars = "";
        static int AmountOfGuesses = 0;
        StringBuilder initialStarString;

        public MainWindow()
        {
            InitializeComponent();
            lbLIntroduction.Content = "Press 'Create new' and a word is created for you \nto be guessed letter by letter.";
        }

        private void btnNewWord_Click(object sender, RoutedEventArgs e)
        {
            stars = "";
            AmountOfGuesses = 0;
            txtWordDisplay.Text = "";
            lblFinished.Content = "";

            randomNumber = random.Next(1, 11);
            // Pick word randomly from list:
            word = wordList[randomNumber];

            // Create stars according to the amount of letters in the word to be guessed:
            for (int i = 0; i < word.Length; i++)
            {
                stars += "*";
            }
            initialStarString = new StringBuilder("", word.Length);
            txtWordDisplay.Text = initialStarString.ToString();
            for (int i = 0; i < word.Length; i++)
            {
                initialStarString.Append("*");
            }
            txtWordDisplay.Text = initialStarString.ToString();
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

        // Processes the letter given by the user
        private void Process(char letterGuessed)
        {
            char[] charWord = new char[word.Length];
            string starsReplaced = "";

            for (int i = 0; i < word.Length; i++)
            {
                charWord[i] = word[i];
            }

            if (AmountOfGuesses < 10)
            {
                // When guesses still available check if the guessed letter is in the word:
                starsReplaced = ReplaceByLetterFound(letterGuessed, stars, charWord);
                txtWordDisplay.Text = starsReplaced;

                if (AllStarsReplacedByLetters(starsReplaced))
                {
                    lblFinished.Content = "Congratulations! You did it in " + AmountOfGuesses + " guesses";
                    GameTerminated();
                }
                // Replace star sequence by newly formed sequence:
                stars = starsReplaced;
            }
            else
            {
                MessageBox.Show("Sorry, you have exceeded the limit of 10 guesses");
            }
        }
        private void GameTerminated()
        {
            stars = "";
            word = "";
            txtWordDisplay.Text = "";
            initialStarString.Clear();
        }

        // Replace string of star(s) by new letter, if correct and update string:
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

        // Check if all letters were guessed:
        static Boolean AllStarsReplacedByLetters(string stars)
        {
            StringBuilder sb = new StringBuilder(stars);
            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i] == '*')
                {
                    return false;
                }
            }
            return true;
        }
    }
}