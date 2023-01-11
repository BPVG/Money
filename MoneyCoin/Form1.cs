using System.Xml;

namespace MoneyCoin
{
    public class ExchangeOperations
    {
        public static readonly Dictionary<string, decimal> ExchangeRateToEuro = new();
        public static List<string> FromCurrency = new();
        public static List<string> ToCurrency = new();

        public static void LoadRates()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");

            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes)
            {
                ExchangeRateToEuro.Add(node.Attributes["currency"].Value, decimal.Parse(node.Attributes["rate"].Value));
                FromCurrency.Add(node.Attributes["currency"].Value);
                ToCurrency.Add(node.Attributes["currency"].Value);
            }
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ExchangeOperations.LoadRates();
        }

        class var
        {
            public static string from = "";
            public static string to = "";
            public static decimal? x = null;
            public static decimal? y = null;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "USD":
                    var.from = "USD";
                    break;
                case "EUR":
                    var.from = "EUR";
                    break;
                case "GBP":
                    var.from = "GBP";
                    break;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedItem)
            {
                case "USD":
                    var.to = "USD";
                    break;
                case "EUR":
                    var.to = "EUR";
                    break;
                case "GBP":
                    var.to = "GBP";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var.x = decimal.Parse(textBox1.Text);
            }
            catch (FormatException)
            {
                textBox1.Text = "Error!";
            }

            if (var.from != "EUR")
            {
                var.x = var.x / ExchangeOperations.ExchangeRateToEuro[var.from];
            } else
            {
                var.x = var.x * 1;
            }
            if (var.to != "EUR")
            {
                textBox2.Text = Convert.ToString(var.x * ExchangeOperations.ExchangeRateToEuro[var.to]);
            } else
            {
                textBox2.Text = Convert.ToString(var.x * 1);
            }
        }
    }
}