namespace Exam11_Simpson
{
    public partial class Form1 : Form
    {
        double result;
        DateTime dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dbh = DBHelper.GetInstance(
          "localhost",
          3306,
          "root",
          "",
          "ekzamen2023"
          );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(textBox1.Text);
            double b = Convert.ToDouble(textBox2.Text);
            int n = Convert.ToInt32(textBox3.Text);
            Integral integral = new(a, b, n);
            double f(double x) => integral.Func(x);
            result = integral.Simpson_Method(f);
            label3.Text = Convert.ToString(result);

            dt = DateTime.Now;
            label4.Text += Convert.ToString(dt);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox4.Text;
            int id = Convert.ToInt32(DBHelper.GetInstance().SelectId(login));
            label6.Text += id;
            //label6.Text += DBHelper.GetInstance().GetAll();
            DBHelper.GetInstance().InsertNew(result, id, dt);
        }
    }
}