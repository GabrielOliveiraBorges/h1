using System;
class Program
{
    static char[,] tabuleiro = new char[3, 3];
    static int jogadas = 0;
    static bool modoMaquina = false;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Escolha o modo de jogo:");
            Console.WriteLine("1 - Jogar H x H (Humano vs Humano)");
            Console.WriteLine("2 - Jogar H x M (Humano vs Máquina)");
            Console.WriteLine("3 - Sair");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    JogarHxH();
                    break;
                case "2":
                    JogarHxM();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    continue;
            }

            Console.WriteLine("\nDeseja jogar novamente? (S/N)");
            if (Console.ReadLine().Trim().ToUpper() != "S")
                break;
        }
    }

    static void JogarHxH()
    {
        ReiniciarJogo();
        bool jogoEmAndamento = true;

        while (jogoEmAndamento && jogadas < 9)
        {
            ExibirTabuleiro();
            int jogador = (jogadas % 2 == 0) ? 1 : 2;
            char simbolo = (jogador == 1) ? 'X' : 'O';
            Console.WriteLine($"Jogador {jogador} ({simbolo}), escolha sua jogada (linha e coluna, de 1 a 3): ");

            RealizarJogadaHumana(simbolo);

            if (VerificarFimDeJogo(simbolo))
                jogoEmAndamento = false;
        }

        ExibirResultadoFinal();
    }

    static void JogarHxM()
    {
        ReiniciarJogo();
        bool jogoEmAndamento = true;

        while (jogoEmAndamento && jogadas < 9)
        {
            ExibirTabuleiro();
            int jogador = (jogadas % 2 == 0) ? 1 : 2;
            char simbolo = (jogador == 1) ? 'X' : 'O';

            if (jogador == 1)
            {
                Console.WriteLine($"Jogador {jogador} ({simbolo}), escolha sua jogada (linha e coluna, de 1 a 3): ");
                RealizarJogadaHumana(simbolo);
            }
            else
            {
                Console.WriteLine("Vez da Máquina...");
                RealizarJogadaMaquina(simbolo);
            }

            if (VerificarFimDeJogo(simbolo))
                jogoEmAndamento = false;
        }

        ExibirResultadoFinal();
    }

    static void RealizarJogadaHumana(char simbolo)
    {
        bool jogadaValida = false;
        while (!jogadaValida)
        {
            try
            {
                int linha = int.Parse(Console.ReadLine()) - 1;
                int coluna = int.Parse(Console.ReadLine()) - 1;

                if (linha >= 0 && linha < 3 && coluna >= 0 && coluna < 3 && tabuleiro[linha, coluna] == ' ')
                {
                    tabuleiro[linha, coluna] = simbolo;
                    jogadas++;
                    jogadaValida = true;
                }
                else
                {
                    Console.WriteLine("Jogada inválida! Tente novamente.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida! Use números de 1 a 3.");
            }
        }
    }

    static void RealizarJogadaMaquina(char simbolo)
    {
        Random random = new Random();
        bool jogadaValida = false;

        while (!jogadaValida)
        {
            int linha = random.Next(0, 3);
            int coluna = random.Next(0, 3);

            if (tabuleiro[linha, coluna] == ' ')
            {
                tabuleiro[linha, coluna] = simbolo;
                jogadas++;
                jogadaValida = true;
                Console.WriteLine($"Máquina jogou na posição: {linha + 1}, {coluna + 1}");
                System.Threading.Thread.Sleep(1000); // Pausa para visualização
            }
        }
    }

    static bool VerificarFimDeJogo(char simbolo)
    {
        if (VerificarVencedor(simbolo))
        {
            ExibirTabuleiro();
            int jogador = (simbolo == 'X') ? 1 : 2;
            Console.WriteLine($"Jogador {jogador} ({simbolo}) venceu!");
            return true;
        }
        return false;
    }

    static void ExibirResultadoFinal()
    {
        if (jogadas == 9)
        {
            ExibirTabuleiro();
            Console.WriteLine("Empate! O tabuleiro está cheio.");
        }
    }

    static void ReiniciarJogo()
    {
        jogadas = 0;
        InicializarTabuleiro();
    }

    static void InicializarTabuleiro()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tabuleiro[i, j] = ' ';
            }
        }
    }

    static void ExibirTabuleiro()
    {
        Console.Clear();
        Console.WriteLine("  1 2 3");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + 1 + " ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(tabuleiro[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("  -----");
        }
    }

    static bool VerificarVencedor(char simbolo)
    {
        // Verificar linhas e colunas
        for (int i = 0; i < 3; i++)
        {
            if ((tabuleiro[i, 0] == simbolo && tabuleiro[i, 1] == simbolo && tabuleiro[i, 2] == simbolo) ||
                (tabuleiro[0, i] == simbolo && tabuleiro[1, i] == simbolo && tabuleiro[2, i] == simbolo))
            {
                return true;
            }
        }

        // Verificar diagonais
        if ((tabuleiro[0, 0] == simbolo && tabuleiro[1, 1] == simbolo && tabuleiro[2, 2] == simbolo) ||
            (tabuleiro[0, 2] == simbolo && tabuleiro[1, 1] == simbolo && tabuleiro[2, 0] == simbolo))
        {
            return true;
        }

        return false;
    }
}