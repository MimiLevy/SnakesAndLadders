using System.Windows.Forms;

namespace SnakesAndLadders
{

    public partial class frmSnakesAndLadders : Form
    {
        int stepsforcurrentturn = 0;
        int newindex = 0;
        enum TurnEnum { Red, Blue };
        TurnEnum currentturn = TurnEnum.Red;

        List<TableLayoutPanel> lstsquars;
        List<List<TableLayoutPanel>> lstladdersspots;
        List<List<TableLayoutPanel>> lstsnakesspots;
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

            lstladdersspots = new()
            {
                new() {tbl3, tbl19},
                new() {tbl8, tbl14},
                new() {tbl21, tbl40},
                new() {tbl24, tbl36},
                new() {tbl32, tbl50},
                new() {tbl58, tbl64},
                new() {tbl66, tbl74},
                new() {tbl71, tbl89},
                new() {tbl83, tbl99},
                new() {tbl87, tbl94},
            };

            lstsnakesspots = new()
            {
                new() {tbl17, tbl6},
                new() {tbl41, tbl22},
                new() {tbl48, tbl14},
                new() {tbl79, tbl60},
                new() {tbl86, tbl78},
                new() {tbl93, tbl68},
            };
        }

        private int PickRandomNumber()
        {
            Random rnd = new();
            int n = rnd.Next(1, 7);
            return n;
        }

        private TableLayoutPanel GetCurrentTbl()
        {
            var currenttbl = (TableLayoutPanel)GetCurrentPawn().Parent;
            return currenttbl;
        }

        private Panel GetCurrentPawn()
        {
            Panel currentpawn = currentturn == TurnEnum.Red ? pnlRedPawn : pnlBluePawn;
            return currentpawn;
        }
        private void TakeSteps()
        {
            var currentindex = lstsquars.IndexOf(GetCurrentTbl());
            var nexttbl = lstsquars[currentindex + 1];

            GetCurrentTbl().Controls.Remove(GetCurrentPawn());
            nexttbl.Controls.Add(GetCurrentPawn());
            
            if(newindex == currentindex + 1)
            {
                btnStep.Enabled = false;
                btnThrowTheDice.Enabled = true;
                currentturn = currentturn == TurnEnum.Red ? TurnEnum.Blue : TurnEnum.Red;
                lblMessage.Text = "Current Turn: " + currentturn.ToString() + " – Throw the dice!";
            }
        }
        private void DoTurn()
        {
            stepsforcurrentturn = PickRandomNumber();
            newindex = lstsquars.IndexOf(GetCurrentTbl()) + stepsforcurrentturn;
            lblMessage.Text = "Take " + stepsforcurrentturn.ToString() + " steps!";
            btnStep.Enabled = true;
            btnThrowTheDice.Enabled = false;
        }
        private void StartGame()
        {
            lblMessage.Text = "Current Turn: Red – Throw the dice!";
            btnStep.Enabled = false;
            btnThrowTheDice.Enabled = true;
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

