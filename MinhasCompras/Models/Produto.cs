using System; 
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks; 
using SQLite; 

namespace MinhasCompras.Models//Criando uma coleção de classes = namespace
{
      //Criando a classe produto
    public class Produto
    {
        //Atributos da classe
        string? _descricao;
        double _quantidade;
        double _preco;

        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        // Metodos get e set para a descrição
        public string? Descricao
        {
            get => _descricao; //Retorna o valor do atributo descrição
            set
            {
                //Verifica se o valor da descrição é null
                if (value == null)
                    throw new Exception("Descrição Inválida");

                _descricao = value;//Atribui o valor ao atributo descrição
            }
        }

        //Metodos get e set para a quantidade 
        public double Quantidade
        {
            get => _quantidade; //Retorna o valor do atributo quantidade
            set
            {
                //Tenta converter para duouble e atribui ao atributo quantidade 
                if (!double.TryParse(value.ToString(), out _quantidade))
                    _quantidade = 0;

                //Verifica se o valor de qauntidade é menor ou igual a 0
                if (value == 0 || value < 0)
                    throw new Exception("Quantidade Inválida.");

                _quantidade = value;//Atribui o valor ao atributo quantidade
            }
        }

        //Metodos get e set para o preço
        public double Preco
        {
            get => _preco; //Retorna o valor do atributo apreço 
            set
            {
                //Tenta converter para double e atribui ao atributo preco
                if (!double.TryParse(value.ToString(), out _preco))
                    _preco = value; // Se a conversão falhar, mantém o valor atual

                //Verifica se o valor de preço é menor ou igual a 0
                if (value <= 0)
                    throw new Exception("Preço Inválido.");

                _preco = value;//Atribui o valor ao atributo preço
            }
        }

        //Metodo que calcula o total do produto
        public double Total
        {
            get => Preco * Quantidade;
        }
    }
}
