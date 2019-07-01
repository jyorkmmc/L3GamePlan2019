using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HighScoresProper
{
    public partial class HighScores : Form
    {
        TextBox[] player = new TextBox[10];
        TextBox[] score = new TextBox[10];
        int lowestScore;
        int newScore;
        int newScorePosition;
        string playerHighScore;
        public HighScores()
        {
            InitializeComponent();
            newScore = -1;
        }
        public HighScores(int s, string playerName)
        {
            InitializeComponent();
            lblNotice.Visible = true;
            newScore = s;
            playerHighScore = playerName;
        }
        private void HighScores_Load(object sender, EventArgs e)
        {
            // control arrays
            player[0] = txtName1;
            player[1] = txtName2;
            player[2] = txtName3;
            player[3] = txtName4;
            player[4] = txtName5;
            player[5] = txtName6;
            player[6] = txtName7;
            player[7] = txtName8;
            player[8] = txtName9;
            player[9] = txtName10;
            score[0] = txtScore1;
            score[1] = txtScore2;
            score[2] = txtScore3;
            score[3] = txtScore4;
            score[4] = txtScore5;
            score[5] = txtScore6;
            score[6] = txtScore7;
            score[7] = txtScore8;
            score[8] = txtScore9;
            score[9] = txtScore10;      
            try
            {
                System.IO.StreamReader inputFile = new System.IO.StreamReader(Application.StartupPath + "\\highscores.txt");
                for (int i = 0; i < 10; i++)
                {
                    player[i].Text = inputFile.ReadLine();
                    score[i].Text = inputFile.ReadLine();
                }
                inputFile.Close();
            }
            catch (Exception ex)
            {
                for (int i = 0; i < 10; i++)
                {
                    player[i].Text = "";
                    score[i].Text = "0";
                }
            }
            // find where current score fits in list if it does
            int j; //value used for the loop
            if (newScore > -1)
            {
                for (j = 0; j < 10; j++)
                {
                    if (newScore > Convert.ToInt32(score[j].Text)) //compares the newScore with the values in the source file
                        break;
                }
                newScorePosition = j;

                // move all below NewScorePosition down one position
                // if newScorePosition is 9, then it simply replaces the 10th value. However if it above 9
                //it pushes all the values below the newScorePosition downwards as it adjusts the whole list.
                if (newScorePosition != 9)
                {
                    for (int i = 9; i > newScorePosition; i--) //for all ranks below newScorePosition they get pushed down
                    {
                        player[i].Text = player[i - 1].Text; //moves the playername position down by one
                        player[i].Refresh();
                        score[i].Text = score[i - 1].Text; //moves the player's score position down by one
                        score[i].Refresh();
                    }
                }
                // add new score - open up text box at NewScorePosition - records the new value into the list
                score[newScorePosition].Text = newScore.ToString();
                player[newScorePosition].Text = playerHighScore.ToString();
                player[newScorePosition].Refresh();
                player[newScorePosition].Focus();
            }
            // writes into the source file the new set of values once all the changes have been done.
            System.IO.StreamWriter outputFile = new System.IO.StreamWriter(Application.StartupPath + "\\highscores.txt");
            for (int i = 0; i < 10; i++)
            {
                outputFile.WriteLine(player[i].Text);
                outputFile.WriteLine(score[i].Text);
            }
            outputFile.Close();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
