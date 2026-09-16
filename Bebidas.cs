using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_backend_sprint1_Lian_Negrão
{
    public enum TamanhoBebida
    {
        Pequena300,
        Media500,
        Grande1L
    }
    public class Bebida : ItemCardapio
    {
        public TamanhoBebida Tamanho { get; set; }

        // Construtor da Bebida: recebe o tamanho além dos dados que vão pro pai.
        public Bebida(int codigo, string descricao, double precoBase, TamanhoBebida tamanho)
            : base(codigo, descricao, precoBase)
        {
            Tamanho = tamanho;
        }

        public override double CalcularPreco()
        {
            double acrescimo = 0;

            // Estrutura condicional (switch) decidindo o acréscimo conforme o tamanho.
            switch (Tamanho)
            {
                case TamanhoBebida.Pequena300:
                    acrescimo = 0;
                    break;
                case TamanhoBebida.Media500:
                    acrescimo = 1.50;
                    break;
                case TamanhoBebida.Grande1L:
                    acrescimo = 3.00;
                    break;
            }

            return PrecoBase + acrescimo;
        }

        // Override da exibição, mostrando também o tamanho escolhido.
        public override void ExibirDetalhes()
        {
            string tamanhoTexto = ObterTextoTamanho();
            Console.WriteLine($"[{Codigo}] {Descricao} (Bebida - {tamanhoTexto}) - Preço: R$ {CalcularPreco():F2}");
        }
        private string ObterTextoTamanho()
        {
            switch (Tamanho)
            {
                case TamanhoBebida.Pequena300:
                    return "300ml";
                case TamanhoBebida.Media500:
                    return "500ml";
                case TamanhoBebida.Grande1L:
                    return "1L";
                default:
                    return "Desconhecido";
            }
        }
    }
}
