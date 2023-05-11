using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmBienvenido : Form
    {
        public frmBienvenido()
        {
            InitializeComponent();
        }

        private void timerAbrirFrom_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 100)
                this.Opacity += 0.5;
            progressBar1.Value++;
            
            if(progressBar1.Value == 100)
            {
                timerAbrirFrom.Stop();
                timerCerrarForm.Start();
            } 

        }

        private void timerCerrarForm_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity  == 0)
            {
                timerCerrarForm.Stop();
                this.Close();
            } 
        }

        private void frmBienvenido_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            timerAbrirFrom.Start();
        }

 
    }
}
