using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;

namespace RECEITAFEDERAL
{

    public class ConsultaCNPJReceita
    {
        public static Empresa empresaConsultada;
        private readonly CookieContainer _cookies = new CookieContainer();
        private String urlBaseReceitaFederal = "https://solucoes.receita.fazenda.gov.br/Servicos/cnpjreva/";
        private String paginaValidacao = "valida.asp";
        private String paginaPrincipal = "Cnpjreva_Solicitacao_CS.asp";
        private String paginaCaptcha = "captcha/gerarCaptcha.asp";

        public enum Coluna
        {
            RazaoSocial = 0,
            NomeFantasia,
            AtividadeEconomicaPrimaria,
            AtividadeEconomicaSecundaria,
            NumeroDaInscricao,
            MatrizFilial,
            NaturezaJuridica,
            SituacaoCadastral,
            DataSituacaoCadastral,
            MotivoSituacaoCadastral,
            Endereco,
            Numero,
            Bairro,
            Cidade,
            UF,
            Complemento,
            CEP,
            Telefone,
            Email
        }
        public static string RecuperaColunaValor(String pattern, Coluna col)
        {
            String S = pattern.Replace("\n", "").Replace("\t", "").Replace("\r", "");
            switch (col)
            {
                case Coluna.RazaoSocial:
                    {
                        S = StringEntreString(S, "<!-- Início Linha NOME EMPRESARIAL -->", "<!-- Fim Linha NOME EMPRESARIAL -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.NomeFantasia:
                    {
                        S = StringEntreString(S, "<!-- Início Linha ESTABELECIMENTO -->", "<!-- Fim Linha ESTABELECIMENTO -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.NaturezaJuridica:
                    {
                        S = StringEntreString(S, "<!-- Início Linha NATUREZA JURÍDICA -->", "<!-- Fim Linha NATUREZA JURÍDICA -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.AtividadeEconomicaPrimaria:
                    {
                        S = StringEntreString(S, "<!-- Início Linha ATIVIDADE ECONOMICA -->", "<!-- Fim Linha ATIVIDADE ECONOMICA -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.AtividadeEconomicaSecundaria:
                    {
                        S = StringEntreString(S, "<!-- Início Linha ATIVIDADE ECONOMICA SECUNDARIA-->", "<!-- Fim Linha ATIVIDADE ECONOMICA SECUNDARIA -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.NumeroDaInscricao:
                    {
                        S = StringEntreString(S, "<!-- Início Linha NÚMERO DE INSCRIÇÃO -->", "<!-- Fim Linha NÚMERO DE INSCRIÇÃO -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.MatrizFilial:
                    {
                        S = StringEntreString(S, "<!-- Início Linha NÚMERO DE INSCRIÇÃO -->", "<!-- Fim Linha NÚMERO DE INSCRIÇÃO -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.Endereco:
                    {
                        S = StringEntreString(S, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.Numero:
                    {
                        S = StringEntreString(S, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.Complemento:
                    {
                        S = StringEntreString(S, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.CEP:
                    {
                        S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.Bairro:
                    {
                        S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.Cidade:
                    {
                        S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.UF:
                    {
                        S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringSaltaString(S, "</b>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.Email:
                    {
                        S = StringEntreString(S, "<!-- Início de Linha de Contato -->", "<!-- Fim de Linha de Contato -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.Telefone:
                    {
                        S = StringEntreString(S, "<!-- Início de Linha de Contato -->", "<!-- Fim de Linha de Contato -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.SituacaoCadastral:
                    {
                        S = StringEntreString(S, "<!-- Início Linha SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha SITUACAO CADASTRAL -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.DataSituacaoCadastral:
                    {
                        S = StringEntreString(S, "<!-- Início Linha SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha SITUACAO CADASTRAL -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringSaltaString(S, "</b>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                case Coluna.MotivoSituacaoCadastral:
                    {
                        S = StringEntreString(S, "<!-- Início Linha MOTIVO DE SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha MOTIVO DE SITUAÇÃO CADASTRAL -->");
                        S = StringEntreString(S, "<tr>", "</tr>");
                        S = StringEntreString(S, "<b>", "</b>");
                        return S.Trim();
                    }
                default:
                    {
                        return S;
                    }
            }
        }
        public static string StringEntreString(String Str, String StrInicio, String StrFinal)
        {
            int Ini;
            int Fim;
            int Diff;
            Ini = Str.IndexOf(StrInicio);
            Fim = Str.IndexOf(StrFinal);
            if (Ini > 0) Ini = Ini + StrInicio.Length;
            if (Fim > 0) Fim = Fim + StrFinal.Length;
            Diff = ((Fim - Ini) - StrFinal.Length);
            if ((Fim > Ini) && (Diff > 0))
                return Str.Substring(Ini, Diff);
            else
                return "";
        }
        public static string StringSaltaString(String Str, String StrInicio)
        {
            int Ini;
            Ini = Str.IndexOf(StrInicio);
            if (Ini > 0)
            {
                Ini = Ini + StrInicio.Length;
                return Str.Substring(Ini);
            }
            else
                return Str;
        }
        public static string StringPrimeiraLetraMaiuscula(String Str)
        {
            string StrResult = "";
            if (Str.Length > 0)
            {
                StrResult += Str.Substring(0, 1).ToUpper();
                StrResult += Str.Substring(1, Str.Length - 1).ToLower();
            }
            return StrResult;
        }

        public static ConsultaCNPJReceita consulta = new ConsultaCNPJReceita();
        public static Image CarregaCaptcha()
        {
            consulta = new ConsultaCNPJReceita();
            Bitmap bit = consulta.GetCaptcha();

            if (bit != null)
            {
                return bit;

            }
            return null;
        }

        public Bitmap GetCaptcha()
        {
            String htmlResult = "";
            using (var wc = new CookieAwareWebClient())
            {
                try
                {
                    wc.SetCookieContainer(_cookies);
                    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                    wc.Headers[HttpRequestHeader.KeepAlive] = "300";
                    htmlResult = wc.DownloadString(urlBaseReceitaFederal + paginaPrincipal);
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível carregar a imagem de validação.\nServiço da Receita Federal fora do ar ou bloqueado. Tente novamente mais tarde"); // RETORNA MENSAGEM IGUAL O METODO CONSULTA
                }
                if (htmlResult.Length > 0)
                {
                    var wc2 = new CookieAwareWebClient();
                    wc2.SetCookieContainer(_cookies);
                    wc2.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                    wc2.Headers[HttpRequestHeader.KeepAlive] = "300";
                    byte[] data = wc2.DownloadData(urlBaseReceitaFederal + paginaCaptcha);
                    return new Bitmap(new MemoryStream(data));
                }
                else
                {
                    MessageBox.Show("Não foi possível carregar a imagem de validação. Tente novamente mais tarde"); // RETORNA MENSAGEM IGUAL O METODO CONSULTA
                }
                return null;
            }
        }
        public String Consulta(string aCNPJ, string aCaptcha)
        {
            var request = (HttpWebRequest)WebRequest.Create(urlBaseReceitaFederal + paginaValidacao);
            request.ProtocolVersion = HttpVersion.Version10;
            request.CookieContainer = _cookies;
            request.Method = "POST";
            string postData = "";
            postData = postData + "origem=comprovante&";
            postData = postData + "cnpj=" + new Regex(@"[^\d]").Replace(aCNPJ, string.Empty) + "&";
            postData = postData + "txtTexto_captcha_serpro_gov_br=" + aCaptcha + "&";
            postData = postData + "submit1=Consultar&";
            postData = postData + "search_type=cnpj";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            StreamReader stHtml = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            String retorno = stHtml.ReadToEnd();
            if (retorno.Contains("Verifique se o mesmo foi digitado corretamente"))
                throw new System.InvalidOperationException("O número do CNPJ não foi digitado corretamente");
            if (retorno.Contains("Erro na Consulta"))
                throw new System.InvalidOperationException("Os caracteres digitados não correspondem com a imagem");
            return retorno;
        }

        public static string[] retornodados(string tmp, string cnpj)
        {
            empresaConsultada = new Empresa();
            //Empresa em si
            empresaConsultada.Cnpj = cnpj;
            empresaConsultada.RazaoSocial = RecuperaColunaValor(tmp, Coluna.RazaoSocial);
            empresaConsultada.NomeFantasia = RecuperaColunaValor(tmp, Coluna.NomeFantasia);
            empresaConsultada.NaturezaJuridica = RecuperaColunaValor(tmp, Coluna.NaturezaJuridica);
            //  empresaConsultada.AtividadeEconomicaPrimaria = RecuperaColunaValor(tmp, Coluna.AtividadeEconomicaPrimaria);
            //  empresaConsultada.AtividadeEconomicaSecundaria = RecuperaColunaValor(tmp, Coluna.AtividadeEconomicaSecundaria);
            empresaConsultada.NumeroDaInscricao = RecuperaColunaValor(tmp, Coluna.NumeroDaInscricao);
            //   empresaConsultada.MatrizFilial = RecuperaColunaValor(tmp, Coluna.MatrizFilial);
            empresaConsultada.SituacaoCadastral = RecuperaColunaValor(tmp, Coluna.SituacaoCadastral);
            empresaConsultada.DataSituacaoCadastral = RecuperaColunaValor(tmp, Coluna.DataSituacaoCadastral);
            //  empresaConsultada.MotivoSituacaoCadastral = RecuperaColunaValor(tmp, Coluna.MotivoSituacaoCadastral);
            //Endereço
            empresaConsultada.Endereco = RecuperaColunaValor(tmp, Coluna.Endereco);
            empresaConsultada.Numero = RecuperaColunaValor(tmp, Coluna.Numero);
            empresaConsultada.Bairro = RecuperaColunaValor(tmp, Coluna.Bairro);
            empresaConsultada.Cidade = RecuperaColunaValor(tmp, Coluna.Cidade);
            empresaConsultada.CEP = RecuperaColunaValor(tmp, Coluna.CEP);
            empresaConsultada.UF = RecuperaColunaValor(tmp, Coluna.UF);
            empresaConsultada.Complemento = RecuperaColunaValor(tmp, Coluna.Complemento);
            //Contato
            empresaConsultada.Email = RecuperaColunaValor(tmp, Coluna.Email);
            empresaConsultada.Telefone = RecuperaColunaValor(tmp, Coluna.Telefone);
            empresaConsultada.Cnae = RecuperaColunaValor(tmp, Coluna.AtividadeEconomicaPrimaria);
            string[] retorn = {
            empresaConsultada.Cnpj,
            empresaConsultada.RazaoSocial,
            empresaConsultada.NomeFantasia,
            empresaConsultada.NaturezaJuridica,
            //  empresaConsultada.AtividadeEconomicaPrimaria,
            //  empresaConsultada.AtividadeEconomicaSecundaria,
            empresaConsultada.NumeroDaInscricao,
            //   empresaConsultada.MatrizFilial,
            empresaConsultada.SituacaoCadastral,
            empresaConsultada.DataSituacaoCadastral,
            //  empresaConsultada.MotivoSituacaoCadastral,
            //Endereço
            empresaConsultada.Endereco,
            empresaConsultada.Numero,
            empresaConsultada.Bairro,
            empresaConsultada.Cidade,
            empresaConsultada.CEP,
            empresaConsultada.UF,
            empresaConsultada.Complemento,
            //Contato
            empresaConsultada.Email,
            empresaConsultada.Telefone,
            empresaConsultada.Cnae };
             

            return retorn;

        }

    }
}