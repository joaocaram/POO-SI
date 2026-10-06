using System;

namespace PoliFiguras {
    internal class PolimorfismoFormas {
    static Random aleatorio = new Random(42);
    static Conjunto formas = null;
    static int MenuPrincipal() {
        string linha = "=========================";
        //TODO: refatorar menu
        Console.Clear();
        Console.WriteLine("FORMAS E MAIS FORMAS");
        Console.WriteLine(linha);
        Console.WriteLine("1 - Criar novo conjunto aleatoriamente");
        Console.WriteLine("2 - Listar todas as formas");
        Console.WriteLine("3 - Forma com a maior área");
        Console.WriteLine(linha);
        Console.WriteLine("0 - Sair");
        Console.Write("\nSua opção: ");
        return int.Parse(Console.ReadLine());
    }
            
    static void GerarConjunto() {
        Console.Write("Quantas formas geométricas você quer gerar? ");
        int quantas = int.Parse(Console.ReadLine());
        if (quantas < 1)
            quantas = 1;
        formas = new Conjunto(quantas);
        for (int i = 0; i < quantas; i++) {
            Forma novaForma = CriarForma();
            formas.AddForma(novaForma);
        }
    }

    static void Relatorio() {
        Console.Clear();
        Console.WriteLine($"Conjunto das formas geométricas:\n{formas}");
    }

    static Forma CriarForma() {
        Console.Clear();
        Console.WriteLine("1 - Círculo");
        Console.WriteLine("2 - Quadrado");
        Console.WriteLine("3 - Retângulo");
        Console.WriteLine("4 - Triângulo Retângulo");
        Console.Write("Escolha a forma: ");
        int opcao = int.Parse(Console.ReadLine());
        double dimensao1 = Math.Round((2 + aleatorio.NextDouble() * 7.9), 2);
        double dimensao2 = Math.Round((2 + aleatorio.NextDouble() * 7.9), 2);
        Forma forma = opcao switch {
            1 => new Circulo(dimensao1),
            2 => new Quadrado(dimensao1),
            3 => new Retangulo(dimensao1, dimensao2),
            4 or _ => new TrianguloRetangulo(dimensao1, dimensao2)
        };
        return forma;
    }

    

    static void Main(string[] args) {

        int opcao = MenuPrincipal();

        Action escolha;
        while (opcao != 0) {
            escolha = opcao switch {
                1 => () => GerarConjunto(),
                2 => () => Relatorio(),
                3 => () => Console.WriteLine($"Maior pela área: {formas.MaiorForma()}")
            };
            escolha.Invoke();
            Console.WriteLine("Enter para continuar. . .");
            Console.ReadKey();
            opcao = MenuPrincipal();

        }
    }
}
    
}
