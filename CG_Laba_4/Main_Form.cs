namespace CG_Laba_4
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }

        private void sutherlandCohen_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            SutherlandCohen_Form sutherlandCohenForm = new SutherlandCohen_Form();
            sutherlandCohenForm.Show();
        }

        private void middlePoint_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MiddlePoint_Form middlePointForm = new MiddlePoint_Form();
            middlePointForm.Show();
        }

        private void cyrusBeck_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            CyrusBeck_Form cyrusBeckForm = new CyrusBeck_Form();
            cyrusBeckForm.Show();
        }
    }
}