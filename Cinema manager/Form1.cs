using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema_manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Theater theater = new Theater(9, 9);
            richTextBox1.Text = theater.Output();
        }
    }
}
