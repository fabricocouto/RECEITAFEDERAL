# RECEITAFEDERAL

usando o codigo para obter dados do cnpj direto na receita

para carregar o captcha

use no load

 pictureBox1.Image = ConsultaCNPJReceita.CarregaCaptcha();
 
 
 para consultar os dados usando captcha e o cnpj desejado
 
                string tmp = ConsultaCNPJReceita.consulta.Consulta(txtcnpj.Text, captcha.Text);
                string[] tmps = null;
                tmps = ConsultaCNPJReceita.retornodados(txtcnpj.Text, captcha.Text);

                          if (tmps.Length > 0)
                {

                    txtRazao.Text = tmps[1].ToString().Trim();
                    txtFantasia.Text = tmps[2].ToString().Trim();

                    //Datas
                    txtDataSituacaoCadastral.Text = tmps[6].ToString().Trim();
                    //Situações
                    txtSituacaoCadastral.Text = tmps[5].ToString().Trim();
                    //Endereco
                    txtLogradouro.Text = tmps[7].ToString().Trim();
                    txtBairro.Text = tmps[9].ToString().Trim();
                    txtCidade.Text = tmps[10].ToString().Trim();
                    txtUF.Text = tmps[12].ToString().Trim();
                    txtCEP.Text = tmps[11].ToString().Trim();
                    txtComplemento.Text = tmps[12].ToString().Trim();
                    txtNumero.Text = tmps[8].ToString().Trim();
                    txtEmail.Text = tmps[14].ToString().Trim();
                    txtTelefone.Text = tmps[15].ToString().Trim();
                  
                }
                

![Sem título](https://user-images.githubusercontent.com/20323161/196717244-749badd7-1dbc-4a60-9e7a-4c04c0617aae.png)
