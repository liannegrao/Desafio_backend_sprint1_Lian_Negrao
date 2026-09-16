using System;

namespace Desafio_backend_sprint1_Lian_Negrão
{
    public abstract class ItemCardapio
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public double PrecoBase { get; set; }

        public ItemCardapio(int codigo, string descricao, double precoBase)
        {
            Codigo = codigo;
            Descricao = descricao;
            PrecoBase = precoBase;
        }
        public abstract double CalcularPreco();

        public virtual void ExibirDetalhes()
        {
            Console.WriteLine($"[{Codigo}] {Descricao} - Preço: R$ {CalcularPreco():F2}");
        }
    }
}