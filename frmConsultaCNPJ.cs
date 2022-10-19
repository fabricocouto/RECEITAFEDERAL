using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace RECEITAFEDERAL
{
    public partial class frmConsultaCNPJ : Form
    {

        public frmConsultaCNPJ()
        {
            InitializeComponent();
        }
        private void frmConsultaCNPJ_Load(object sender, EventArgs e)
        {
            picLetras.Image = ConsultaCNPJReceita.CarregaCaptcha();
       
        }

        private void btConsultar_Click(object sender, EventArgs e)
        {

                string tmp = ConsultaCNPJReceita.consulta.Consulta(txtCNPJ.Text, txtLetras.Text);
                string[] tmps = null;
                tmps = ConsultaCNPJReceita.retornodados(tmp, txtCNPJ.Text);

                if (tmps.Length > 0)
                {

                    txtRazao.Text = tmps[0].ToString().Trim();
                    txtFantasia.Text = tmps[1].ToString().Trim();

                    //Datas
                    txtDataSituacaoCadastral.Text = tmps[5].ToString().Trim();
                    //Situações
                    txtSituacaoCadastral.Text = tmps[4].ToString().Trim();
                    //Endereco
                    txtLogradouro.Text = tmps[6].ToString().Trim();
                    txtBairro.Text = tmps[8].ToString().Trim();
                    txtCidade.Text = tmps[9].ToString().Trim();
                    txtUF.Text = tmps[11].ToString().Trim();
                    txtCEP.Text = tmps[10].ToString().Trim();
                    txtComplemento.Text = tmps[12].ToString().Trim();
                    txtNumero.Text = tmps[7].ToString().Trim();
                    txtEmail.Text = tmps[13].ToString().Trim();
                    txtTelefone.Text = tmps[14].ToString().Trim();
                  
                }
           
       
        }
   
     
        private void btTrocarImagem_Click(object sender, EventArgs e)
        {
            picLetras.Image = ConsultaCNPJReceita.CarregaCaptcha();
        }
  
        private void frmConsultaCNPJ_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.F2)
            {
                btTrocarImagem.PerformClick();
            }
            if (e.KeyCode == Keys.Enter)
            {
                btConsultar.PerformClick();
            }
         
        }
    }
}