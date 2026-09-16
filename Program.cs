using System;
using System.Globalization;
namespace Desafio_backend_sprint1_Lian_Negrão
{
    internal class Program
    {
        // Contador de código, incrementado a cada item novo criado.
        static int proximoCodigo = 1;

        static void Main(string[] args)
        {
            // Cria o único pedido da execução, com número fixo 1.
            Pedido pedido = new Pedido(1);
            bool continuar = true;

            // do-while: garante que o menu apareça pelo menos uma vez,
            // e continua repetindo até o usuário escolher sair.
            do
            {
                Console.Clear();
                ExibirMenu();
                string opcao = Console.ReadLine();

                // try/catch: se o usuário digitar algo inválido em qualquer opção,
                // o programa avisa o erro em vez de quebrar.
                try
                {
                    switch (opcao)
                    {
                        case "1":
                            AdicionarLanche(pedido);
                            break;
                        case "2":
                            AdicionarBebida(pedido);
                            break;
                        case "3":
                            RemoverItem(pedido);
                            break;
                        case "4":
                            pedido.ExibirResumo();
                            Console.WriteLine();
                            Console.Write("Pressione ENTER para voltar ao menu...");
                            Console.ReadLine();
                            break;
                        case "5":
                            pedido.Fechar();
                            Console.WriteLine("Pedido fechado! Resumo final:");
                            pedido.ExibirResumo();
                            Console.WriteLine();
                            Console.Write("Pressione ENTER para sair...");
                            Console.ReadLine();
                            continuar = false;
                            break;
                        case "6":
                            continuar = false;
                            Console.WriteLine("Encerrando o sistema. Até logo!");
                            break;
                        default:
                            Console.WriteLine("Opção inválida! Tente novamente.");
                            break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // Disparada quando tenta mexer em um pedido já fechado.
                    Console.WriteLine($"Operação não permitida: {ex.Message}");
                }
                catch (FormatException)
                {
                    // Disparada quando int.Parse/double.Parse recebe um texto que não é número.
                    Console.WriteLine("Valor inválido! Digite apenas números.");
                }
                catch (Exception ex)
                {
                    // "Rede de segurança" para qualquer outro erro inesperado.
                    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                }

                Console.WriteLine();

            } while (continuar);
        }

        static void ExibirMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== SISTEMA DE PEDIDOS - LANCHONETE =====");
            Console.WriteLine("1 - Adicionar Lanche");
            Console.WriteLine("2 - Adicionar Bebida");
            Console.WriteLine("3 - Remover Item");
            Console.WriteLine("4 - Ver Pedido");
            Console.WriteLine("5 - Fechar Pedido");
            Console.WriteLine("6 - Sair sem fechar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
        }

        static void AdicionarLanche(Pedido pedido)
        {
            Console.Write("Nome do lanche: ");
            string descricao = Console.ReadLine();

            Console.Write("Preço base (ex: 12.50): ");
            string entrada = Console.ReadLine()?.Replace(",", ".");
            double precoBase = double.Parse(entrada, CultureInfo.InvariantCulture);

            Lanche lanche = new Lanche(proximoCodigo++, descricao, precoBase);

            // Loop pra adicionar quantos extras o usuário quiser.
            bool adicionandoExtras = true;
            while (adicionandoExtras)
            {
                Console.Write("Digite um ingrediente extra (ou deixe vazio para parar): ");
                string extra = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(extra))
                {
                    adicionandoExtras = false;
                }
                else
                {
                    lanche.AdicionarExtra(extra);
                }
            }

            pedido.AdicionarItem(lanche);
            Console.WriteLine("Lanche adicionado com sucesso!");
        }

        static void AdicionarBebida(Pedido pedido)
        {
            Console.Write("Nome da bebida: ");
            string descricao = Console.ReadLine();

            Console.Write("Preço base (ex: 5.00): ");
            string entrada = Console.ReadLine()?.Replace(",", ".");
            double precoBase = double.Parse(entrada, CultureInfo.InvariantCulture);

            Console.WriteLine("Tamanho: 1 - 300ml | 2 - 500ml | 3 - 1L");
            Console.Write("Escolha o tamanho: ");
            string opcaoTamanho = Console.ReadLine();

            TamanhoBebida tamanho;
            switch (opcaoTamanho)
            {
                case "1":
                    tamanho = TamanhoBebida.Pequena300;
                    break;
                case "2":
                    tamanho = TamanhoBebida.Media500;
                    break;
                case "3":
                    tamanho = TamanhoBebida.Grande1L;
                    break;
                default:
                    Console.WriteLine("Tamanho inválido, definido como 300ml por padrão.");
                    tamanho = TamanhoBebida.Pequena300;
                    break;
            }

            Bebida bebida = new Bebida(proximoCodigo++, descricao, precoBase, tamanho);
            pedido.AdicionarItem(bebida);
            Console.WriteLine("Bebida adicionada com sucesso!");
        }

        static void RemoverItem(Pedido pedido)
        {
            pedido.ExibirResumo();
            Console.Write("Digite o código do item a remover: ");
            int codigo = int.Parse(Console.ReadLine());

            bool removido = pedido.RemoverItemPorCodigo(codigo);

            if (removido)
                Console.WriteLine("Item removido com sucesso!");
            else
                Console.WriteLine("Item não encontrado.");
        }
    }
}