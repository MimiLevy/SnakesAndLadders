using System.Windows.Forms;

namespace SnakesAndLadders
{

    public partial class frmSnakesAndLadders : Form
    {
       
        enum TurnEnum { Red, Blue };
        TurnEnum currentturn = TurnEnum.Red;
        

        public frmSnakesAndLadders()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            btnThrowTheDice.Click += BtnThrowTheDice_Click;
            btnStep.Click += BtnStep_Click;
        }

     

        private string PickRandomNumber()
        {
            Random rnd = new();
            int n = rnd.Next(1, 7);
            return n.ToString();
        }

        private void TakeSteps()
        {

        }
        private void DoTurn()
        {
            lblMessage.Text = "Take " + PickRandomNumber() + " steps!";
            btnStep.Enabled = true;
            btnThrowTheDice.Enabled = false;

        }
        private void StartGame()
        {
            lblMessage.Text = "Current Turn: Red pawn – Throw the dice!";
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
