using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 

namespace Project01
{
    public partial class index : System.Web.UI.Page
    {

        Random rnd = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                panelContainer.Style["display"] = "none";
                endGameContainer.Style["display"] = "none";
                progressionLabel.Style["display"] = "none";
                attemptsContainer.Style["display"] = "none";
                timerContainer.Style["display"] = "none";
                equationLabel.Style["display"] = "none";
            }
        }

        protected void StartGame() //main function that initializes the game and game area
        {

            progressionLabel.Text = "Level 1: O1Q1";

            attemptImg1.ImageUrl = "images/brain_enabled.png";
            attemptImg2.ImageUrl = "images/brain_enabled.png";
            attemptImg3.ImageUrl = "images/brain_enabled.png";

            endGameContainer.Style["display"] = "none";
            mainContentContainer.Style["display"] = "none";
            panelContainer.Style["display"] = "block";
            timerContainer.Style["display"] = "block";
            equationLabel.Style["display"] = "block";
            progressionLabel.Style["display"] = "block";
            attemptsContainer.Style["display"] = "block";

            timerLabel.Text = "45";
            timerProgressFill.Style["width"] = "75%";

            panelInput.Text = "|";

            Session["LoseMsg"] = "";
            Session["Countdown"] = 45;
            Session["Streak"] = 0;
            Session["Level"] = 0;
            Session["Operation"] = 0;
            Session["Expression"] = 1;
            Session["Question"] = 1;
            Session["Lives"] = 3;

            CountdownTimer.Enabled = true;

            GenerateEquation((int)Session["Operation"], (int)Session["Expression"]);
        }

        protected void EndGame(string msg) //main function that ends the game
        {
            Session["LoseMsg"] = msg;
            CountdownTimer.Enabled = false;

            panelContainer.Style["display"] = "none";
            progressionLabel.Style["display"] = "none";
            attemptsContainer.Style["display"] = "none";
            timerContainer.Style["display"] = "none";
            equationLabel.Style["display"] = "none";

            loserLabel.Text = msg;
            answerLabel.Text = "The correct evaluation was " + (string)Session["Evaluation"];
            levelLabel.Text = "Level " + ((int)Session["Level"] + 1).ToString() + ": O" + ((int)Session["Operation"] + 1).ToString() + "Q" + (string)Session["Question"].ToString();
            endGameContainer.Style["display"] = "block";
        }

        protected void GenerateEquation(int operation, int expressions) //generates inner expressions and combines them together 
        {
            string currentExpression = "";
            List<string> operators = new List<string> { " + ", " - ", " * ", " / " };

            for (int i = 0; i < expressions; i++)
            {
                currentExpression += GenerateExpression(operation);

                if (i != expressions - 1) //combine expressions together
                {
                    currentExpression += operators[rnd.Next(0, operation)];
                }

            }

            equationLabel.Text = currentExpression;
            Session["Evaluation"] = EvaluateExpression(currentExpression);

        }

        protected int GenerateRandomNumber(int min, int max) //generate a random number between a certain range
        {
            return rnd.Next(min, max);
        }

        protected int GenerateRandomDivisible(int num) //generate a number that is divisible by an integer
        {
            List<int> divisibility = new List<int> { };

            for (int i = 2; i < 25; i++)
            {
                if (num % i == 0) //check if the number is divisible by i
                {
                    divisibility.Add(i);
                }
            }
            
            if(divisibility.Count == 0)
            {
                divisibility.Add(1); //it may be be a prime number that is only divisible by itself and 1
                divisibility.Add(num);
                return divisibility[rnd.Next(divisibility.Count)];

            }
            else
            {
                return divisibility[rnd.Next(divisibility.Count)];
            }
        }
        protected Boolean GenerateRandomBoolean() //generate a random boolean, either true or false
        { 
            return Convert.ToBoolean(rnd.Next(2));
        }
        protected string GenerateExpression(int type) //generate an expression of an operation type
        {
            string expression = "";

            switch (type)
            {
                case 0:
                    expression = (GenerateRandomNumber(1, 50) + " + " + GenerateRandomNumber(1, 50)).ToString();
                    break;
                case 1:
                    expression = (GenerateRandomNumber(1, 50) + " - " + GenerateRandomNumber(1, 50)).ToString();
                    break;
                case 2:
                    expression = (GenerateRandomNumber(1, 13) + " * " + GenerateRandomNumber(1, 13)).ToString();
                    break;
                case 3:
                    int num = GenerateRandomNumber(1, 50);
                    expression = "(" + num + " / " + GenerateRandomDivisible(num).ToString() + ")";
                    break;
            }
                
            return expression;
        }

        protected string EvaluateExpression(string expression) //evaluate a given expression
        {
            DataTable dt = new DataTable();
            double evaluation = Convert.ToDouble(dt.Compute(expression, ""));
            return Math.Round(evaluation, 2).ToString();
        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        protected void BtnLeft_Click(object sender, EventArgs e)
        {
            MoveCursor(-1);
        }

        protected void BtnRight_Click(object sender, EventArgs e)
        {
            MoveCursor(1);
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteInputChar();
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        protected void BtnPlusMinus_Click(object sender, EventArgs e)
        {
            ToggleInputSign();
        }

        protected void BtnEqual_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        protected void BtnNum_Click(object sender, EventArgs e) //main panel number button event handler 
        {
            int cursorIndex = panelInput.Text.IndexOf("|");
            string btnValue = (sender as Button).Text.ToString();
            string inputText = panelInput.Text;
            int inputTextLength = panelInput.Text.Length;

            if((btnValue == "." && !inputText.Contains(".")) || btnValue != ".")
            {
                if (inputTextLength != 1)
                {
                    inputText = inputText.Insert(cursorIndex, btnValue);
                    panelInput.Text = inputText;
                }
                else
                {
                    inputText = btnValue + "|";
                    panelInput.Text = inputText;
                }
            }
        }

        protected void DeleteInputChar() //removes digit to the left of panel cursor
        {
            int cursorIndex = panelInput.Text.IndexOf("|");
            string inputText = panelInput.Text;
            if(cursorIndex > 0 && cursorIndex < inputText.Length && cursorIndex - 1 != inputText.IndexOf("-"))
            {
                inputText = inputText.Remove(cursorIndex - 1, 1);
                cursorIndex = panelInput.Text.IndexOf("|");
                panelInput.Text = inputText;
            }else if (cursorIndex - 1 == inputText.IndexOf("-"))
            {
                MoveCursor(1);
            }
        }

        protected void ToggleInputSign() //makes the panel number either positive or negative
        {
            string inputText = panelInput.Text;
            if (inputText[0].ToString() == "-")
            {
                inputText = inputText.Replace("|", "");
                inputText = inputText.Insert(inputText.Length, "|");
                inputText = inputText.Replace("-", "");
            }
            else
            {
                inputText = inputText.Replace("|", "");
                inputText = inputText.Insert(inputText.Length, "|");
                inputText = inputText.Insert(0, "-");
            }

            panelInput.Text = inputText;
        }

        protected void ClearInput() //clear all numbers inside of the panel
        {
            panelInput.Text = "|";
        }
        protected void MoveCursor(int pos) //move the panel cursor to a given position
        {
            int cursorIndex = panelInput.Text.IndexOf("|");
            int inputTextLength = panelInput.Text.Length;

            if(pos == 1 && cursorIndex != inputTextLength - 1)
            {
                string inputText = panelInput.Text.Replace("|", "");
                inputText = inputText.Insert(cursorIndex + 1, "|");
                panelInput.Text = inputText;
            }
            if (pos == -1 && cursorIndex != 0)
            {
                string inputText = panelInput.Text;
                if(inputText[0].ToString() == "-" && cursorIndex != 1)
                {
                    inputText = inputText.Replace("|", "");
                    inputText = inputText.Insert(cursorIndex - 1, "|");
                    panelInput.Text = inputText;
                }
                else if(inputText[0].ToString() != "-")
                {
                    inputText = inputText.Replace("|", "");
                    inputText = inputText.Insert(cursorIndex - 1, "|");
                    panelInput.Text = inputText;
                }
            }
        }

        protected void CheckAnswer() //check if the player entered the correct evaluation of the expression
        {
            string inputText = panelInput.Text.Replace("|","");
            if (inputText == (string)Session["Evaluation"]) //player entered the correct evaluation
            {
                Session["Streak"] = (int)Session["Streak"] + 1;

                if ((int)Session["Streak"] % 3 == 0) //player is on a streak
                {
                    Session["Streak"] = 0;
                    AddLife(1);
                }

                AddToTimer(5);
                ClearInput();

                if ((int)Session["Operation"] < 3 && (int)Session["Question"] <= 10)
                {
                    GenerateEquation((int)Session["Operation"], (int)Session["Expression"]);
                    Session["Question"] = (int)Session["Question"] + 1;
                    progressionLabel.Text = "Level " + ((int)Session["Level"] + 1).ToString() + ": O" + ((int)Session["Operation"] + 1).ToString() + "Q" + (string)Session["Question"].ToString();
                }

                if((int)Session["Operation"] < 3 && (int)Session["Question"] > 10)
                {
                    Session["Operation"] = (int)Session["Operation"] + 1;
                    Session["Question"] = 1;
                    progressionLabel.Text = "Level " + ((int)Session["Level"] + 1).ToString() + ": O" + ((int)Session["Operation"] + 1).ToString() + "Q" + (string)Session["Question"].ToString();
                    GenerateEquation((int)Session["Operation"], (int)Session["Expression"]);
                }

                if ((int)Session["Operation"] == 3 && (int)Session["Question"] <= 10)
                {
                    GenerateEquation((int)Session["Operation"], (int)Session["Expression"]);
                    Session["Question"] = (int)Session["Question"] + 1;
                    progressionLabel.Text = "Level " + ((int)Session["Level"] + 1).ToString() + ": O" + ((int)Session["Operation"] + 1).ToString() + "Q" + (string)Session["Question"].ToString();
                }

                if ((int)Session["Operation"] == 3 && (int)Session["Question"] > 10)
                {
                    Session["Operation"] = 0;
                    Session["Question"] = 1;
                    Session["Level"] = (int)Session["Level"] + 1;
                    Session["Expression"] = (int)Session["Expression"] + 1;
                    progressionLabel.Text = "Level " + ((int)Session["Level"] + 1).ToString() + ": O" + ((int)Session["Operation"] + 1).ToString() + "Q" + (string)Session["Question"].ToString();
                    GenerateEquation((int)Session["Operation"], (int)Session["Expression"]);
                }




            }
            else //player entered the incorrect evaluation of the expression
            {
                Session["Streak"] = 0;
                AddLife(-1);

                if ((int)Session["Lives"] < 0) //player has run out of lives
                {
                    EndGame("You ran out of lives"); //end the game 
                }
            }

        }

        protected void AddLife(int count) //add or remove from the player's lives
        {
            Session["Lives"] = (int)Session["Lives"] + count;

            if ((int)Session["Lives"] > 3) //cap the amount of lives to a maximum number
            {
                Session["Lives"] = 3;
            }

            switch ((int)Session["Lives"]) //update the ui element to reflect how many lives the player has
            {
                case 0:
                    attemptImg1.ImageUrl = "images/brain_disabled.png";
                    attemptImg2.ImageUrl = "images/brain_disabled.png";
                    attemptImg3.ImageUrl = "images/brain_disabled.png";
                    break;
                case 1:
                    attemptImg1.ImageUrl = "images/brain_enabled.png";
                    attemptImg2.ImageUrl = "images/brain_disabled.png";
                    attemptImg3.ImageUrl = "images/brain_disabled.png";
                    break;
                case 2:
                    attemptImg1.ImageUrl = "images/brain_enabled.png";
                    attemptImg2.ImageUrl = "images/brain_enabled.png";
                    attemptImg3.ImageUrl = "images/brain_disabled.png";
                    break;
                case 3:
                    attemptImg1.ImageUrl = "images/brain_enabled.png";
                    attemptImg2.ImageUrl = "images/brain_enabled.png";
                    attemptImg3.ImageUrl = "images/brain_enabled.png";
                    break;
            }
        }

        protected void AddToTimer(int time) //add more time to the Countdown Timer and update the progress bar
        {
            Session["Countdown"] = (int)Session["Countdown"] + time;
            timerLabel.Text = Session["Countdown"].ToString();

            if ((int)Session["Countdown"] < 61)
            {
                timerProgressFill.Style["width"] = ((Convert.ToDouble((int)Session["Countdown"]) / 60) * 100).ToString() + "%";
            }
            else
            {
                timerProgressFill.Style["width"] = "100%";
            }
        }

        protected void CountdownTimer_Tick(object sender, EventArgs e) //main countdown timer tick event handler
        {
            Session["Countdown"] = (int)Session["Countdown"] - 1; //decrease timer variable
            timerLabel.Text = Session["Countdown"].ToString(); //update timer label

            if ((int)Session["Countdown"] < 61) // draw timer bar 
            {
                timerProgressFill.Style["width"] = ((Convert.ToDouble((int)Session["Countdown"]) / 60) * 100).ToString() + "%";
            }
            else
            {
                timerProgressFill.Style["width"] = "100%";
            }
            if((int)Session["Countdown"] < 0) //player does not have time left
            {
                EndGame("The time limit has been reached");
            }
        }
    }
}