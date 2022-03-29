<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Project01.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="Cache-control" content="no-cache"/>
    <title>Equ8</title>
    <style>
        @import "@fontsource/lexend-deca";

        #levelLabel{
            font-size: 1.5em;
            color: #374F66;
            font-weight: 600;
        }
        /**Attempts container**/
        #attemptsContainer{
            width: 200px;
            height: auto;
            background-color: #374F66;
            border-radius: 0px 0px 10px 10px;
            position: absolute;
            top: 0px;
            left: 100%;
            transform: translate(-100%);
            text-align: center;
        }

        .attemptImg{
            width: 50px;
            height: auto;
        }

        /**End Game Container**/
        #endGameContainer{
            width: 500px;
            height: 400px;
            background-color: #ffffff;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            padding: 20px;
            border-radius: 10px 10px 10px 10px;
            text-align: center;
        }

        #tryAgainBtn{
            position: absolute;
            top: 90%;
            left: 50%;
            transform: translate(-50%, -50%);
            border: 4px solid #cfe8ff;
            padding: 10px;
            background-color: #374F66;
            border-radius: 10px;
            font-size: 1em;
            color: white;
            font-family: "Lexend Deca";
            cursor: pointer;
        }
        /**Main Container**/
        #mainContentContainer{
            width: 500px;
            height: 430px;
            background-color: #ffffff;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            padding: 20px;
            border-radius: 10px 10px 10px 10px;
        }

        #startBtn {
            position: absolute;
            top: 90%;
            left: 50%;
            transform: translate(-50%, -50%);
            border: 4px solid #cfe8ff;
            padding: 10px;
            background-color: #374F66;
            border-radius: 10px;
            font-size: 1em;
            color: white;
            font-family: "Lexend Deca";
            cursor: pointer;
        }
        /**Timer element **/
        #timerContainer{
            width: 100px;
            height: auto;
            text-align: center;
            position: absolute;
            left: 50%;
            top: 0px;
            transform: translate(-50%);
            background-color: white;
            padding: 5px;
            border-radius: 0px 0px 10px 10px;
        }

        #timerImg{
            width: 50px;
            height: auto;
        }

        #timerLabel {
            position: absolute;
            left: 50%;
            top: 47%;
            transform: translate(-50%, -50%);
            font-weight: bold;
            font-family: "Lexend Deca";
            color: #374F66;
        }

        #timerProgress {
            width: 100%;
            height: 5px;
            background-color: white;
        }


        #timerProgressFill {
            height: 5px;
            width: 100%;
            background-color: #374F66;
        }

        body {
            background-color: #a0d6dd;
            font-family: "Lexend Deca";
        }

        #progressionLabel{
            width: auto;
            height: 20px;
            padding: 10px;
            font-size: 1.3em;
            color: #ffffff;
            background-color: #374F66;
            border-radius: 0px 0px 10px 10px;
            position: absolute;
            top: 0px;
            left: 0px;
            text-align: center;
        }

        #panelContainer {
            border-radius: 10px;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%,-50%);
            width: 500px;
            height: 500px;
            background-color: #ffffff;

        }

        #panelInput {
            width: 485px;
            font-size: 2em;
            height: 55px;
            border-radius: 10px 10px 0px 0px;
            border: none;
            background-color: #374F66;
            color: #fff;
            text-align: right;
            padding-right: 10px;
            padding-left: 5px;
        }

        .btn {
            height: 75px;
            border-radius: 10px;
            border: none;
            background-color: #cfe8ff;
            font-size: 1.5em;
            color: #374F66;
            cursor: pointer;
        }

        .blank{
            cursor: default;
        }

        .acBtn {
            background-color: #374F66;
            color: #cfe8ff;
        }

        .blank{
            background-color: #fff;
        }

        #panelButtons{
            position: absolute;
            top: 25%;
            left: 50%;
            transform: translate(-50%);
            width: 375px;
            display: grid;
            grid-template-columns: repeat(4,1fr);
            grid-template-rows: repeat(3, 1fr);
            row-gap: 15px;
            column-gap: 15px;
        }

        #panelControls .btn{
            height: 50px;
            background-color: #374F66;
            color: #cfe8ff;
        }

        #panelControls{
            position: absolute;
            left: 50%;
            top: 65px;
            transform: translate(-50%);
            width: 200px;
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            column-gap: 15px;

        }

        #equationLabel{
            font-family: "Lexend Deca";
            background-color: #fff;
            border-radius: 10px;
            font-size: 1.9em;
            position: absolute;
            top: 10%;
            left: 50%;
            transform: translate(-50%);
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div id="mainContentContainer" runat="server">
                        <img src="images/equ8_logo.gif"/>
                        <p>
                            A math expression will be displayed on the screen, and the player must correctly evaluate the expression. Entering the correct number will increase the timer by 5 seconds. Doing this three times in a row will add an extra life. However, incorrectly evaluating an expression will take a life away from you. 
                        </p>
                        <p>
                            The player will be shown math expressions that include addition, subtraction, multiplication, and division. These expressions will progressively become longer, and perhaps more difficult, to evaluate.
                        </p>
                        <asp:Button id="startBtn" runat="server" text="Start Game" OnClick="StartBtn_Click"/>
                    </div>

                    <div id="endGameContainer" runat="server">
                        <img src="images/equ8_logo.gif" />
                        <asp:Label id="levelLabel" runat="server" Text=""></asp:Label><br /><br />
                        <asp:Label id="loserLabel" runat="server" Text=""></asp:Label><br />
                        <asp:Label id="answerLabel" runat="server" Text=""></asp:Label>
                        <asp:Button id="tryAgainBtn" runat="server" Text="Try Again" OnClick="StartBtn_Click"/>
                    </div>

                    <div id="panelContainer" runat="server">
                        <asp:TextBox ID="panelInput" runat="server" ReadOnly="True">|</asp:TextBox>

                        <div id="panelControls">
                            <asp:Button class="btn" runat="server" Text="&#10094;" OnClick="BtnLeft_Click" />
                            <asp:Button class="btn" runat="server" Text="&#10095;" OnClick="BtnRight_Click" />
                        </div>

                        <div id="panelButtons">
                            <asp:Button class="btn" runat="server" text="7" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="8" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="9" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn acBtn" runat="server" text="&#129092;" OnClick="BtnDelete_Click"/>
                            <asp:Button class="btn" runat="server" text="4" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="5" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="6" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn acBtn" runat="server" text="AC" OnClick="BtnClear_Click"/>
                            <asp:Button class="btn" runat="server" text="1" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="2" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="3" OnClick="BtnNum_Click"/>
                            <asp:Button ID="blankBtn" class="btn blank" runat="server" text=""/>
                            <asp:Button class="btn" runat="server" text="." OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="0" OnClick="BtnNum_Click"/>
                            <asp:Button class="btn" runat="server" text="+/-" OnClick="BtnPlusMinus_Click"/>
                            <asp:Button class="btn acBtn" runat="server" text="=" OnClick="BtnEqual_Click"/>
                        </div>


                        <asp:Timer ID="CountdownTimer" runat="server" Interval="1000" OnTick="CountdownTimer_Tick" Enabled="False">
                        </asp:Timer>


                    </div>

                    <div id="timerContainer" runat="server">
                        <asp:Image ID="timerImg" ImageUrl="images/timer.png" runat="server"/>
                        <asp:Label ID="timerLabel" runat="server">60</asp:Label>
                        <div id="timerProgress">
                            <div id="timerProgressFill" runat="server"></div>
                        </div>
                    </div>

                    <div id="attemptsContainer" runat="server">
                        <asp:Image ID="attemptImg1" Class="attemptImg" ImageUrl="images/brain_enabled.png" runat="server"/>
                        <asp:Image ID="attemptImg2" Class="attemptImg" ImageUrl="images/brain_enabled.png" runat="server"/>
                        <asp:Image ID="attemptImg3" Class="attemptImg" ImageUrl="images/brain_enabled.png" runat="server"/>
                    </div>

                    <asp:Label ID="progressionLabel" runat="server">Level 1: Q1</asp:Label>
                    <asp:Label ID="equationLabel" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
