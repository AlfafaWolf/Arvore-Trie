using System;

namespace Arvore_Trie
{
    class Program
    {
        static void Main(string[] args)
        {
            // Exemplo
            Console.WriteLine("Hello World!");
            Trie raiz = Trie.CriaNoRaizTrie();
            Trie.AdicTrie(raiz, "ab");
            Trie.AdicTrie(raiz, "ac");
            Trie.AdicTrie(raiz, "abd");
            Trie.AdicTrie(raiz, "ae");
            Console.WriteLine(raiz.FilhoEsq.Info);
            Console.WriteLine(raiz.FilhoEsq.FilhoEsq.Info);
            Console.WriteLine(raiz.FilhoEsq.FilhoEsq.IrmaoDir.Info);
            Console.WriteLine(raiz.FilhoEsq.FilhoEsq.IrmaoDir.IrmaoDir.Info);
            Console.WriteLine(raiz.FilhoEsq.FilhoEsq.FilhoEsq.Info);

            /* Arvore Criada
             *          \0
             *          /
             *         a
             *        /
             *       b - c - e
             *      /
             *     d
             */
        }
    }
}
