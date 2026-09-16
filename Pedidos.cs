using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_backend_sprint1_Lian_Negrão
{
    // Representa um pedido individual contendo vários itens do cardápio.
    public class Pedido
    {
        public int Numero { get; }
        public DateTime Data { get; }
        public List<ItemCardapio> Itens { get; }
        public bool Fechado { get; private set; }

        public Pedido(int numero)
        {
            Numero = numero;
            Data = DateTime.Now;
            Itens = new List<ItemCardapio>();
            Fechado = false;
        }

        public void AdicionarItem(ItemCardapio item)
        {
            if (Fechado) throw new InvalidOperationException("Pedido já está fechado.");
            if (item == null) throw new ArgumentNullException(nameof(item));
            Itens.Add(item);
        }

        public bool RemoverItemPorCodigo(int codigo)
        {
            if (Fechado) return false;
            var item = Itens.Find(i => i.Codigo == codigo);
            if (item == null) return false;
            return Itens.Remove(item);
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (var item in Itens)
            {
                total += item.CalcularPreco();
            }
            return total;
        }

        public void Fechar()
        {
            Fechado = true;
        }

        public void ExibirResumo()
        {
            Console.WriteLine($"Pedido #{Numero} - Data: {Data}");
            if (Itens.Count == 0)
            {
                Console.WriteLine("  (sem itens)");
            }
            else
            {
                foreach (var item in Itens)
                {
                    item.ExibirDetalhes();
                }
            }

            Console.WriteLine($"Total: R$ {CalcularTotal():F2}");
            Console.WriteLine(Fechado ? "Status: Fechado" : "Status: Aberto");
        }
    }

    // Gerenciador simples de múltiplos pedidos.
    public class Pedidos
    {
        private readonly List<Pedido> lista = new List<Pedido>();
        private int proximoNumero = 1;

        public Pedido CriarPedido()
        {
            var p = new Pedido(proximoNumero++);
            lista.Add(p);
            return p;
        }

        public Pedido? ObterPedido(int numero)
        {
            return lista.Find(p => p.Numero == numero);
        }

        public bool RemoverPedido(int numero)
        {
            var p = ObterPedido(numero);
            if (p == null) return false;
            return lista.Remove(p);
        }

        public void ExibirTodos()
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum pedido cadastrado.");
                return;
            }

            foreach (var p in lista)
            {
                p.ExibirResumo();
                Console.WriteLine(new string('-', 30));
            }
        }
    }
}
