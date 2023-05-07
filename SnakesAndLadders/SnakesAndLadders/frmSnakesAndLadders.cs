using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SnakesAndLadders
{

    public partial class frmSnakesAndLadders : Form
    {
        int stepsforcurrentturn = 0;
        int targetindex = 0;
        enum TurnEnum { Red, Blue };
        TurnEnum currentturn = TurnEnum.Red;

        List<TableLayoutPanel> lstsquars;
        List<List<TableLayoutPanel>> lstladderspoints;
        List<List<TableLayoutPanel>> lstsnakespoints;
        public frmSnakesAndLadders()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            btnThrowTheDice.Click += BtnThrowTheDice_Click;
            btnStep.Click += BtnStep_Click;
                

            lstsquars = new()
            {
                tbl1, tbl2, tbl3,tbl4, tbl5, tbl6, tbl7, tbl8, tbl9, tbl10,
                tbl11, tbl12, tbl13,tbl14, tbl15, tbl16, tbl17, tbl18, tbl19, tbl20,
                tbl21, tbl22, tbl23,tbl24, tbl25, tbl26, tbl27, tbl28, tbl29, tbl30,
                tbl31, tbl32, tbl33,tbl34, tbl35, tbl36, tbl37, tbl38, tbl39, tbl40,
                tbl41, tbl42, tbl43,tbl44, tbl45, tbl46, tbl47, tbl48, tbl49, tbl50,
                tbl51, tbl52, tbl53,tbl54, tbl55, tbl56, tbl57, tbl58, tbl59, tbl60,
                tbl61, tbl62, tbl63,tbl64, tbl65, tbl66, tbl67, tbl68, tbl69, tbl70,
                tbl71, tbl72, tbl73,tbl74, tbl75, tbl76, tbl77, tbl78, tbl79, tbl80,
                tbl81, tbl82, tbl83,tbl84, tbl85, tbl86, tbl87, tbl88, tbl89, tbl90,
                tbl91, tbl92, tbl93,tbl94, tbl95, tbl96, tbl97, tbl98, tbl99, tbl100
            };

            lstladderspoints = new()
            {
                new() {tbl3, tbl19},
                new() {tbl8, tbl14},
                new() {tbl21, tbl40},
                new() {tbl24, tbl36},
                new() {tbl32, tbl50},
                new() {tbl58, tbl64},
                new() {tbl66, tbl75},
                new() {tbl71, tbl89},
                new() {tbl83, tbl99},
                new() {tbl87, tbl94},
            };

            lstsnakespoints = new()
            {
                new() {tbl17, tbl6},
                new() {tbl41, tbl22},
                new() {tbl48, tbl15},
                new() {tbl79, tbl60},
                new() {tbl95, tbl78},
                new() {tbl93, tbl68},
            };
        }

        private int PickRandomNumber()
        {
            Random rnd = new();
            int n = rnd.Next(1, 7);
            return n;
        }

        private void EnableDisableControls()
        {
            btnStep.Enabled = btnStep.Enabled == true ? false : true;
            btnThrowTheDice.Enabled = btnThrowTheDice.Enabled == true ? false : true;
        }

        private TableLayoutPanel GetCurrentTbl()
        {
            var tbl = (TableLayoutPanel)GetCurrentPawn().Parent;
            return tbl;
        }

        private Panel GetCurrentPawn()
        {
            var pnl = currentturn == TurnEnum.Red ? pnlRedPawn : pnlBluePawn;
            return pnl;
        }

        private void RemovePawn(TableLayoutPanel tbl, Panel pawn)
        {
            tbl.Controls.Remove(pawn);
        }

        private void AddPawn(TableLayoutPanel tbl, Panel pawn)
        {
            tbl.Controls.Add(pawn);
        }

        private void DoIt(List<List<TableLayoutPanel>> lst, string message)
        {
            var targettbl = lstsquars[targetindex];
            foreach (var l in lst)
            {
                if (l[0] == targettbl)
                {
                    MessageBox.Show(message);
                    RemovePawn(targettbl, GetCurrentPawn());
                    AddPawn(l[1], GetCurrentPawn());
                }
            }
        }

        private void CheckSnakes()
        {
            DoIt(lstsnakespoints, "OUCH!!! A Snake!!!" + Environment.NewLine + "Click OK to slide down!");
        }

        private void CheckLadders()
        {
            DoIt(lstladderspoints, "HOORAY!!! A Ladder!!!" + Environment.NewLine + "Click OK to climb up!");
        }

        private void CheckWinner(TableLayoutPanel nexttbl)
        {
            if (nexttbl == tbl100)
            {
                btnStep.Enabled = false;
                btnThrowTheDice.Enabled = false;
                lblMessage.Text = "WOW! " + currentturn.ToString() + " is the winner!!!";
                lblMessage.BackColor = currentturn == TurnEnum.Red ? Color.Red : Color.Blue;
            }
        }

        private void TakeSteps()
        {
            var currentindex = lstsquars.IndexOf(GetCurrentTbl());
            var nextindex = currentindex + 1;
            var nexttbl = lstsquars[nextindex];

            CheckWinner(nexttbl);

            RemovePawn(GetCurrentTbl(), GetCurrentPawn());
            AddPawn(nexttbl, GetCurrentPawn());

            if (targetindex == nextindex)
            {
                CheckLadders();
                CheckSnakes();
                EnableDisableControls();
                currentturn = currentturn == TurnEnum.Red ? TurnEnum.Blue : TurnEnum.Red;
                lblMessage.Text = "Current Turn: " + currentturn.ToString() + " – Throw the dice!";
            }
        }

        private void DoTurn()
        {
            stepsforcurrentturn = PickRandomNumber();
            targetindex = lstsquars.IndexOf(GetCurrentTbl()) + stepsforcurrentturn;
            lblMessage.Text = "Take " + stepsforcurrentturn.ToString() + " steps!";
            EnableDisableControls();
        }

        private void StartGame()
        {

            lstsquars.ForEach(tbl => tbl.Controls.Remove(pnlRedPawn));
            lstsquars.ForEach(tbl => tbl.Controls.Remove(pnlBluePawn));
            AddPawn(tbl1, pnlRedPawn);
            AddPawn(tbl1, pnlBluePawn);
            lblMessage.BackColor = SystemColors.AppWorkspace;
            lblMessage.Text = "Current Turn: Red – Throw the dice!";
            currentturn = TurnEnum.Red;
            EnableDisableControls();
            btnStep.Enabled = false;
        }

        private void BtnStep_Click(object? sender, EventArgs e)
        {
            TakeSteps();
        }

        private void BtnThrowTheDice_Click(object? sender, EventArgs e)
        {
            DoTurn();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }
    }
}

