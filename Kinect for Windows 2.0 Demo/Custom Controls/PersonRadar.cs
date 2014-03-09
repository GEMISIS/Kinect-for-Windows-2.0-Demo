using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Custom_Controls
{
    public partial class PersonRadar : UserControl
    {
        public PersonRadar()
        {
            InitializeComponent();
        }

        public void setPosition(Point position)
        {
            this.pictureBox1.Location = new Point(position.X, position.Y);
        }
    }
}
