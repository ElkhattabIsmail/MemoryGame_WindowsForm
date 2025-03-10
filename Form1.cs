using Project_03_Memory_Game.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project_03_Memory_Game
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
     
        private PictureBox  firstSelectedPicture  = null;
        private PictureBox secondSelectedPicture = null;

        private bool AccessToClickOnCards = true; // to fix The Quik Click of The player hhh
        private int Counter = 0; // for Time
        private int countRightPics = 0; // for check if win or no
        private int Score = 0; // score
        private int RestartCounter = 60;
        private int CountSamePictures = 0; //this to know if the game is over ,every time two picture are the same we add 2
                                   //to the counter and if it became 12 Game is Over

        // fully list
        private List<PictureBox> picBoxList = new List<PictureBox>();
        private void InitializePictureBoxes()
        {
            // Get all PictureBox controls on the form
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    if (control.Tag.ToString()  == "NotYou")
                        continue;
                    picBoxList.Add((PictureBox)control);
                }
            }
        }

        /* // Create a single Random instance as a class-level field*/
        private Random Rnd = new Random();

        private void ChangeTwoPicBoxesLocationRandomly()
        {
            int RandNum1 = Rnd.Next(0, picBoxList.Count);
            int RandNum2 = Rnd.Next(0, picBoxList.Count);

            // Make sure the two random indices are different
            while (RandNum1 == RandNum2)
            {
                RandNum2 = Rnd.Next(0, picBoxList.Count);
            }

            // Swap Boxes
            Point temp = picBoxList[RandNum1].Location;
            picBoxList[RandNum1].Location = picBoxList[RandNum2].Location;
            picBoxList[RandNum2].Location = temp;
        }
        private void ShuffleCards(int ShufflingTimes)
        {
            for (int i = 0; i < ShufflingTimes; i++)
            {
                ChangeTwoPicBoxesLocationRandomly();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializePictureBoxes();
            ShuffleCards(10);// function to shuffle your cards
            Counter = RestartCounter;
        }


        bool IsImagesHaveSameTags(string Tag1, string Tag2)
        {   //we gonna compare them using the tags 
            return Tag1 == Tag2;
        }

        public SoundPlayer Correct = new SoundPlayer(@"C:\Users\ULTRAPC\Downloads\wrong-answer-129254 (1).wav");
        public SoundPlayer Wrong = new SoundPlayer(@"C:\Users\ULTRAPC\Downloads\the-correct-answer-33-183620.wav");
        public SoundPlayer CountDown = new SoundPlayer(@"C:\Users\ULTRAPC\AppData\Local\CapCut\Videos\1226(1).WAV");

        private bool IsCountDownSoundPlayer = false;
        async void ChangeImages(string Tag1, string Tag2)
        {
            if (IsImagesHaveSameTags(Tag1, Tag2))
            {
                if (!IsCountDownSoundPlayer)
                {
                    Correct.Play();
                }
                pbRightWrong.Image = Resources.RightChoice;

                firstSelectedPicture.Tag = "Same";
                secondSelectedPicture.Tag = "Same";

                Score += 5;
                CountSamePictures++;
                // update label Score
                lblScore.Text = Score.ToString();

                if (CountSamePictures == 12)
                {
                    Tmr4CounterAndEndGame.Enabled = false;
                    Tmr4CounterAndEndGame.Stop();
                    if (MessageBox.Show("Congrat! You Won The Game Do You Want To Play More ?", "Have a Winner (❁´◡`❁) ",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Counter = RestartCounter;
                        RestartGameWithoutAsking();
                    }
                    else
                        this.Close();
                }
            }
            else
            {
                if (!IsCountDownSoundPlayer)
                {
                    Wrong.Play();
                }

                pbRightWrong.Image = Resources.WrongChoice;
                

                AccessToClickOnCards = false; // in This time we must disable all cards click  , in timer tick function
                await Task.Delay(500);  // while time is long while can player click more than one card and then cheated on the game

                firstSelectedPicture.Image = Properties.Resources.YogiCardBack;
                secondSelectedPicture.Image = Properties.Resources.YogiCardBack;
                firstSelectedPicture.BackColor = Color.Transparent;
                secondSelectedPicture.BackColor = Color.Transparent;

                EnabledAllPics(); // After Changing pics is The guess wrong Enable The pics Again
                AccessToClickOnCards = true; // allow player to click

            }
            //now we start tracking again
            firstSelectedPicture = null;
            secondSelectedPicture = null;
        }
        void RestartGameWithoutAsking()
        {
            // Reset The Important Values
            Tmr4CounterAndEndGame.Enabled = true;
            Tmr4CounterAndEndGame.Start();
            IsCountDownSoundPlayer = false;

            ResetPicsTags();
            ResetImagesBg();
            ShuffleCards(10);

            firstSelectedPicture = null;
            secondSelectedPicture = null;

            Score = 0;

            lblCounter.ForeColor = Color.Cyan;
            CountSamePictures = 0;
            countRightPics = 0;
            lblCounter.Text = Counter.ToString();
            lblScore.Text = "0";
        }
        void ResetPicsTags()
        {
            pb1.Tag = "1";
            pb2.Tag = "1";

            pb3.Tag = "2";
            pb4.Tag = "2";

            pb5.Tag = "3";
            pb6.Tag = "3";

            pb7.Tag = "4";
            pb8.Tag = "4";

            pb9.Tag = "5";
            pb10.Tag = "5";

            pb11.Tag = "6";
            pb12.Tag = "6";

            pb13.Tag = "7";
            pb14.Tag = "7";

            pb15.Tag = "8";
            pb16.Tag = "8";

            pb17.Tag = "9";
            pb18.Tag = "9";

            pb19.Tag = "10";
            pb20.Tag = "10";

            pb21.Tag = "11";
            pb22.Tag = "11";

            pb23.Tag = "12";
            pb24.Tag = "12";

        }
        void RestartTheGame(int CounterTime)
        {
            
            if (MessageBox.Show("Do you want to restart the game", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                == DialogResult.Yes)
            {
                IsCountDownSoundPlayer = false;
                // incase 
                if (btnPlay_Pause.Text == "Start")
                {
                    btnPlay_Pause.Text = "Pause";
                    btnPlay_Pause.BackColor = Color.Crimson;
                    // start the timers if User Pause it them
                    Tmr4CounterAndEndGame.Start();
                    TmrAccessCards.Start();
                    EnabledAllPics();
               
                }

                // Reser All Values And Shuffle Cards

                ResetPicsTags();
                ResetImagesBg();
                ShuffleCards(10);


                Counter = CounterTime;

                firstSelectedPicture = null;
                secondSelectedPicture = null;

                Score = 0;

                pbRightWrong.Image = Resources.RightChoice;

                CountSamePictures = 0;
                countRightPics = 0;

                lblCounter.ForeColor = Color.Cyan;
                lblCounter.Text = Counter.ToString();
                lblScore.Text = "0";
               
            }
         
        }
        void ResetImagesBg()
        {
            foreach (PictureBox pb in picBoxList)
            {
                pb.Image = Resources.YogiCardBack;
            }
        }
        void DisabeledAllPics()
        {
            foreach (PictureBox pb in picBoxList)
            {
                pb.Enabled = false;
            }
        }
        void EnabledAllPics()
        {
            foreach (PictureBox pb in picBoxList)
            {
                pb.Enabled = true;
            }
        }
        void EndOfGame()
        {
            Tmr4CounterAndEndGame.Stop();
            Tmr4CounterAndEndGame.Enabled = false;

            if (MessageBox.Show("Game Over You Lose Do you want to play again !", "Game Over (┬┬﹏┬┬)",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Counter = RestartCounter;
                RestartGameWithoutAsking();
            }
            else
                this.Close();

        }
        private void Accessing_timer1_Tick(object sender, EventArgs e)
        {
            if (AccessToClickOnCards == false )
            {
                DisabeledAllPics();
            }
        }
        // timer to update labels Only
        private void Timer4CounterAndEndGame_Tick(object sender, EventArgs e)
        {
            if (Counter <= 10)
            {
                IsCountDownSoundPlayer = true;
                lblCounter.ForeColor = Color.Red;
                // Run the CountDown Sound 
                CountDown.Play();
             
                //label2.ForeColor = Color.Red;
            }
            Counter--;
            lblCounter.Text = Counter.ToString();
            if (Counter == 0)
            {
                EndOfGame();
            }
        }

        void ChangeImagesAndCompareThem(PictureBox firsSelected, PictureBox secondSelected)
        {
            ChangeImages(firstSelectedPicture.Tag.ToString(), secondSelected.Tag.ToString());
            firsSelected.Enabled = true;
            secondSelected.Enabled = true;
            return;
        }

        private string BackToFirstTag = "";

        void PerformTheClick(PictureBox MyImage)
        {
            if (MyImage.Tag.ToString() == "Same")
            {
                return;
            }


            if (MyImage.Tag.ToString() == "1") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Chrollo1;

                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;
                  
                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
 
            }

            if (MyImage.Tag.ToString () == "2") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Gen2;

                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
            }

            if (MyImage.Tag.ToString ()  == "3") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Gon3;


                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;

            }
            if (MyImage.Tag.ToString () == "4") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.hisoka4;


                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;

            }
            if (MyImage.Tag.ToString() == "5") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.HxH5;


                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;

            }
            if (MyImage.Tag.ToString() == "6") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.illomi6;


                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
            }
            if (MyImage.Tag.ToString() == "7") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Kill7;


                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
            }
            if (MyImage.Tag.ToString() == "8") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Kora8;


                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
            }

            if (MyImage.Tag.ToString() == "9") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Meruim9;

                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
            }

            if (MyImage.Tag.ToString() == "10") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Prince_4__10;


                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
            }

            if (MyImage.Tag.ToString() == "11") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.spider_11;

                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;
            }

            if (MyImage.Tag.ToString() == "12") //we check the Tag of the clicked Pic So we know which pic we give   
            {
                MyImage.Image = Resources.Vetan12;

                if (firstSelectedPicture == null)//first click this will be empty like we did line 23 (go up)
                {
                    firstSelectedPicture = MyImage;
                    BackToFirstTag = firstSelectedPicture.Tag.ToString();
                    firstSelectedPicture.Tag = "pic"; // Change Tag To avoiding the error game
                    return;
                }
                else if (secondSelectedPicture == null)
                {   //in the second click it doesnt enter on first condition because firstselection is null
                    secondSelectedPicture = MyImage;

                    if (secondSelectedPicture.Tag.ToString() == "pic")
                    {
                        secondSelectedPicture = null;
                        return;
                    }
                    firstSelectedPicture.Tag = BackToFirstTag;

                    ChangeImagesAndCompareThem(firstSelectedPicture, secondSelectedPicture);
                }
                return;

            }

        }
        private void OnPictureClick(object sender, EventArgs e)
        {
            PerformTheClick((PictureBox)sender);
        }
   
        private void Cms60s_Click(object sender, EventArgs e)
        {
            RestartTheGame(60);
            RestartCounter = 60;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartTheGame(RestartCounter);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Do You Want Exit From the game", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
               == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Cms45s_Click(object sender, EventArgs e)
        {
            RestartTheGame(45);
            RestartCounter = 45;
        }

        private void Cms90s_Click(object sender, EventArgs e)
        {
            RestartTheGame( 90);
            RestartCounter = 90;
        }

        private void Cms120s_Click(object sender, EventArgs e)
        {
            RestartTheGame(120);
            RestartCounter = 120;
        }

        private void Cms180s_Click(object sender, EventArgs e)
        {
            RestartTheGame(RestartCounter = 180);
            RestartCounter = 180;
        }

        private void changeTitleColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lblTitle.ForeColor = colorDialog1.Color;

            }
        }

        private void changeScoreColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label3.ForeColor = colorDialog1.Color;
                lblScore.ForeColor = label3.ForeColor;
            }
        }

        private void changeTimeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label2.ForeColor = colorDialog1.Color;
                lblCounter.ForeColor = colorDialog1.Color;
            }
        }

        private void option1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.BgHunter;
        }

        private void option2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.Hisoka_Bg;
        }

        private void option3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.Kilua_Bg;
        }

        private void option4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.backgroundimg;

        }

        private void btnPause_Play_Click(object sender, EventArgs e)
        {
            if (btnPlay_Pause.Text == "Pause")
            {
                Tmr4CounterAndEndGame.Stop();
                TmrAccessCards.Stop();
                DisabeledAllPics();


                btnPlay_Pause.Text = "Start";
                btnPlay_Pause.BackColor = Color.Lime;
                return;
            }
            Tmr4CounterAndEndGame.Start();
            TmrAccessCards.Start();
            EnabledAllPics();
            btnPlay_Pause.Text = "Pause";
            btnPlay_Pause.BackColor = Color.Crimson;
        }
    }
}
