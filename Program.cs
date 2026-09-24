using System;
using System.Globalization;
namespace Desafio_backend_sprint1_Lian_Negrão
{
    internal class Program
    {

        static int proximoCodigo = 1;

        static void Main(string[] args)
        {

            Pedido pedido = new Pedido(1);
            bool continuar = true;

           
            do
            {
                Console.Clear();
                ExibirMenu();
                string opcao = Console.ReadLine()!;

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
                            // espaço entre menu e resultado
                            Console.WriteLine();
                            pedido.ExibirResumo();
                            // espaço extra antes da mensagem de voltar
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.Write("Pressione ENTER para voltar ao menu...");
                            Console.ReadLine();
                            break;
                        case "5":
                            pedido.Fechar();
                            Console.WriteLine("Pedido fechado! Resumo final:");
                            // espaço entre menu e resultado
                            Console.WriteLine();
                            pedido.ExibirResumo();
                            // espaço extra antes da mensagem de voltar
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.Write("Pressione ENTER para voltar ao menu...");
                            Console.ReadLine();
                            // Não encerra o programa: volta ao menu principal
                            break;
                        case "7":
                            // Só limpa (substitui) o pedido atual se ele já estiver fechado
                            if (pedido.Fechado)
                            {
                                pedido = new Pedido(proximoCodigo++);
                                Console.WriteLine("Pedido fechado removido. Novo pedido criado.");
                            }
                            else
                            {
                                Console.WriteLine("Não é possível limpar: o pedido atual ainda está aberto. Feche o pedido antes de limpar.");
                            }
                            Console.WriteLine();
                            Console.Write("Pressione ENTER para voltar ao menu...");
                            Console.ReadLine();
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
                    // espaço extra antes da mensagem de voltar
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("Pressione ENTER para voltar ao menu...");
                    Console.ReadLine();
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
            Console.WriteLine();
            Console.WriteLine("1 - Adicionar Lanche");
            Console.WriteLine("2 - Adicionar Bebida");
            Console.WriteLine("3 - Remover Item");
            Console.WriteLine("4 - Ver Pedido");
            Console.WriteLine("5 - Fechar Pedido");
            Console.WriteLine("6 - Sair ");
            Console.WriteLine("7 - Limpar pedido fechado");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
        }

        static void AdicionarLanche(Pedido pedido)
        {
            Console.Write("Nome do lanche: ");
            string descricao = Console.ReadLine();

            Console.Write("Preço base (ex: 12.50): ");
            try
            {
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
            catch (FormatException)
            {
                Console.WriteLine("Valor inválido ao adicionar lanche! Digite apenas números no preço.");
                Console.WriteLine();
                Console.Write("Pressione ENTER para voltar ao menu...");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar lanche: {ex.Message}");
                Console.WriteLine();
                Console.Write("Pressione ENTER para voltar ao menu...");
                Console.ReadLine();
                return;
            }
        }

        static void AdicionarBebida(Pedido pedido)
        {
            Console.Write("Nome da bebida: ");
            string descricao = Console.ReadLine();

            Console.Write("Preço base (ex: 5.00): ");
            try
            {
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
            catch (FormatException)
            {
                Console.WriteLine("Valor inválido ao adicionar bebida! Digite apenas números no preço.");
                Console.WriteLine();
                Console.Write("Pressione ENTER para voltar ao menu...");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar bebida: {ex.Message}");
                Console.WriteLine();
                Console.Write("Pressione ENTER para voltar ao menu...");
                Console.ReadLine();
                return;
            }
        }

        static void RemoverItem(Pedido pedido)
        {
            pedido.ExibirResumo();
            Console.Write("Digite o código do item a remover: ");
            try
            {
                int codigo = int.Parse(Console.ReadLine());

                bool removido = pedido.RemoverItemPorCodigo(codigo);

                if (removido)
                    Console.WriteLine("Item removido com sucesso!");
                else
                    Console.WriteLine("Item não encontrado.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Código inválido! Digite apenas números ao remover item.");
                Console.WriteLine();
                Console.Write("Pressione ENTER para voltar ao menu...");
                Console.ReadLine();
                return;
            }
        }
    }
}
