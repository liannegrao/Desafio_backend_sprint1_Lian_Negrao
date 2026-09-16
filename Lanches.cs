using System;
using System.Collections.Generic;

namespace Desafio_backend_sprint1_Lian_Negrão
{
    public class Lanche : ItemCardapio
    {
        public List<string> IngredientesExtras { get; set; }
        private const double PRECO_POR_EXTRA = 2.50;

        public Lanche(int codigo, string descricao, double precoBase)
            : base(codigo, descricao, precoBase)
        {
            IngredientesExtras = new List<string>();
        }

        // Método pra adicionar um ingrediente extra à lista.
        public void AdicionarExtra(string ingrediente)
        {
            IngredientesExtras.Add(ingrediente);
        }

        public override double CalcularPreco()
        {
            double total = PrecoBase;

            // foreach percorre cada ingrediente extra e soma o valor no total.
            foreach (string ingrediente in IngredientesExtras)
            {
                total += PRECO_POR_EXTRA;
            }

            return total;
        }

        // Override do método de exibição, pra mostrar também os extras.
        public override void ExibirDetalhes()
        {
            Console.WriteLine($"[{Codigo}] {Descricao} (Lanche) - Preço: R$ {CalcularPreco():F2}");

            if (IngredientesExtras.Count == 0)
            {
                Console.WriteLine("   Sem ingredientes extras.");
            }
            else
            {
                Console.WriteLine("   Extras: " + string.Join(", ", IngredientesExtras));
            }
        }
    }
}