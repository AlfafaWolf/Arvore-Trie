using System;
using System.IO;

namespace Arvore_Trie
{
    class Program
    {
        static void Main(string[] args)
        {
            // Obter path
            string path = Path.GetFullPath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"..\"));
            string fileName = "palavras.txt";
            // Debug Path
            Console.WriteLine($"Path do arquivo: {path}{fileName}");
            // Ler linhas do arquivo e jogar em um vetor
            string[] palavras = File.ReadAllLines(Path.Combine(path, fileName));

            // Criar raiz
            Trie raiz = Trie.CriaNoRaizTrie();

            // Adicionar palavras na Trie
            foreach (var palavra in palavras)
            {
                raiz.AdicTrie(palavra);
                //Console.WriteLine($"{palavra}");
            }

            // Remover palavras na Trie
            Trie.RemoverPalavraTrieRecursiva(raiz, "barco");
            Trie.RemoverPalavraTrieRecursiva(raiz, "casamento");
            Trie.RemoverPalavraTrieRecursiva(raiz, "rio");

            // Verificar quais palavras ainda estao na trie
            foreach (var palavra in palavras)
            {
                Console.WriteLine($"palavra: {palavra, -15} - {raiz.BuscaNaArvoreTrie(palavra)}");
            }

            // Vetor de palavras para buscar
            string[] palavras_busca = { "barca", "case", "casaca", "marcelo" };
            // Buscar palavras que nao estão na Trie
            foreach (var palavra in palavras_busca)
            {
                Console.WriteLine($"palavra: {palavra,-15} - {raiz.BuscaNaArvoreTrie(palavra)}");
            }

            Console.WriteLine("\nPalavras da Arvore: ");
            Trie.exibeTrie(raiz);

            Console.WriteLine("Digite ENTER para encerrar o programa...");
            Console.ReadLine();
        }
    }
}
