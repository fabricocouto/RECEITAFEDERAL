using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RECEITAFEDERAL
{
    public class Empresa
    {
        public int CodEmresa { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NaturezaJuridica { get; set; }
        public string AtividadeEconomicaPrimaria { get; set; }
        public string AtividadeEconomicaSecundaria { get; set; }
        public string NumeroDaInscricao { get; set; }
        public string MatrizFilial { get; set; }
        public string SituacaoCadastral { get; set; }
        public string DataSituacaoCadastral { get; set; }
        public string MotivoSituacaoCadastral { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }
        public string Cnae { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}