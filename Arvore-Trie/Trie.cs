using System;
using System.Collections.Generic;
using System.Text;

namespace Arvore_Trie
{
    sealed class Trie
    {
        private char info;
        private bool ehFimDePalavra = false;
        private Trie filhoEsq = null;
        private Trie irmaoDir = null;

        public char Info
        {
            get { return info; }
            private set { info = value; }
        }

        public bool EhFimDePalavra
        {
            get { return ehFimDePalavra; }
            set { ehFimDePalavra = value; }
        }

        public Trie FilhoEsq
        {
            get { return filhoEsq; }
            set { filhoEsq = value; }
        }

        public Trie IrmaoDir
        {
            get { return irmaoDir; }
            set { irmaoDir = value; }
        }

        public Trie() { }
        public Trie(char info)
        {
            this.Info = info;
        }

        public static Trie CriaNoRaizTrie()
        {
            return new Trie('\0');
        }

        public static Trie CriaNoTrie(char elem)
        {
            return new Trie(elem);
        }

        public bool TemFilhoTrie()
        {
            return Trie.TemFilhoTrie(this);
        }

        public static bool TemFilhoTrie(Trie no)
        {
            return no.FilhoEsq != null;
        }

        public static bool TemProximoIrmaoDir(Trie no)
        {
            return no.IrmaoDir.IrmaoDir != null;
        }

        public void AdicTrie(string palavra)
        {
            Trie.AdicTrie(this, palavra);
        }

        public static void AdicTrie(Trie raiz, string palavra)
        {
            Trie noPai = raiz;
            for (int i = 0; i < palavra.Length ; i++)
            {
                Console.WriteLine($"letra '{palavra[i].ToString()}' {i}"); // DEBUG

                char letra = palavra[i];
                if (!TemFilhoTrie(noPai))
                {
                    Trie novoNo = CriaNoTrie(letra);
                    noPai.FilhoEsq = novoNo;
                    noPai = novoNo;
                    Console.WriteLine($"Novo no adicionado, {letra}"); // DEBUG
                }
                else
                {
                    Trie prevNo = noPai;
                    Trie prim;
                    bool letraJaExiste = false;
                    for (prim = noPai.FilhoEsq; prim != null; prevNo = prim, prim = prim.IrmaoDir)
                    {
                        Console.WriteLine(prim.Info.Equals(letra)); // DEBUG
                        if (prim.Info.Equals(letra))
                        {
                            Console.WriteLine($"{prim.Info} == {letra}"); // DEBUG
                            letraJaExiste = true;
                            break;
                        } 
                    }

                    if (!letraJaExiste)
                    {
                        Trie novoNo = CriaNoTrie(letra);
                        prevNo.IrmaoDir = novoNo;
                        noPai = novoNo;
                        Console.WriteLine($"{prevNo.Info} -> {novoNo.Info}"); // DEBUG
                    }
                    else
                    {
                        noPai = prim;
                    }
                }
            }

            if (!noPai.EhFimDePalavra)
                noPai.EhFimDePalavra = true;
        }
        public static bool BuscaNaArvoreTrie(Trie avr, String word)
        {
            if (avr == null)
            {
                Console.WriteLine("A palavra nao esta na arvore");
                return false;
            }
            /*
            Console.WriteLine("\n indormação do no: " + avr.Info);
            Console.WriteLine("Palavra a ser buscada: " + word);
            Console.WriteLine("tamanho restante da palavra: " + word.Length);
            */
            if (avr.Info == '\0') //No raiz == null, percorre filho esquerdo
            {
                return BuscaNaArvoreTrie(avr.filhoEsq, word.Substring(0, word.Length));
            }
            else if (word.Length == 1 && avr.Info == word[0])
            {
                if (avr.ehFimDePalavra)
                {
                    Console.WriteLine("A palavra esta na arvore");
                    return true;
                }
                else
                {
                    Console.WriteLine("palavra nao encontrada, nao há fim de marcador");
                    return false;
                }
            }
            else if (avr.Info == word[0])
            {
                return BuscaNaArvoreTrie(avr.filhoEsq, word.Substring(1, word.Length - 1));

            }
            else if (avr.irmaoDir != null)
            {
               return BuscaNaArvoreTrie(avr.irmaoDir, word);
            }
            
            Console.WriteLine("Palavra nao encontrada");
            return false;
        }
        
        public static Trie IrmaoEsquerda(Trie possivelIrmao, Trie nodo)
        {
            if (possivelIrmao.irmaoDir == nodo) return possivelIrmao;
            else if (possivelIrmao.irmaoDir != null) return IrmaoEsquerda(possivelIrmao.irmaoDir, nodo);
            else return null;
        }

        public static Trie NodoIrmaoCorrespondente(Trie nodo, char letra) //RETORNA O NODO ENTRE IRMÃOS QUE CORRESPONDE À LETRA PASSADA
        {
            if (nodo.info == letra) return nodo;
            else if (nodo.irmaoDir != null) return NodoIrmaoCorrespondente(nodo.irmaoDir, letra);
            else return null;
        }

        public static void RemoverNodo(Trie pai, Trie nodo)
        {
            if (nodo.irmaoDir != null) //NODO TEM IRMÃO A DIREITA
            {
                pai.filhoEsq = nodo.irmaoDir; //PONTEIRO DE FILHO É AJUSTADO PARA O IRMÃO A DIREITA
            }
            else //NODO NÃO TEM FILHO NEM IRMÃO A DIREITA
            {
                pai.filhoEsq = null; //RETIRA-SE PONTEIRO DE FILHO
            }
        }        

	    public static int RemoverPalavraTrieRecursiva(Trie raiz, string palavra)
        {
            string palavraNova = palavra.Substring(1);

            if (raiz.filhoEsq.info == palavra[0])
            {
                if ((palavra.Length == 1) || (RemoverPalavraTrieRecursiva(raiz.filhoEsq, palavraNova) != 0))
                {
                    if(raiz.filhoEsq.ehFimDePalavra == true && palavra.Length > 1) 
                    {
                        return 0;
                    }
                    if (raiz.filhoEsq.filhoEsq != null) //ULTIMA LETRA ACHADA TEM FILHO
                    {
                        raiz.filhoEsq.ehFimDePalavra = false;
                        return 1;
                    }
                    RemoverNodo(raiz, raiz.filhoEsq); //ULTIMA LETRA ACHADA NÃO TEM FILHO
                    return 1;
                }
            }
            else if (raiz.filhoEsq.irmaoDir != null) //FILHO DA RAIZ TEM IRMÃOS A DIREITA
            {
                if (NodoIrmaoCorrespondente(raiz.filhoEsq.irmaoDir, palavra[0]) != null) //SE LETRA FOR ACHADA ENTRE OS IRMÃOS 
                {
                    if ((palavra.Length == 1) || (RemoverPalavraTrieRecursiva(NodoIrmaoCorrespondente(raiz.filhoEsq.irmaoDir, palavra[0]), palavraNova) != 0))
                    {
                        if(NodoIrmaoCorrespondente(raiz.filhoEsq.irmaoDir, palavra[0]).ehFimDePalavra == true && palavra.Length > 1)
                        {
                            return 0;
                        }
                        if (NodoIrmaoCorrespondente(raiz.filhoEsq.irmaoDir, palavra[0]).filhoEsq != null) //ULTIMA LETRA ACHADA TEM FILHO
                        {
                            NodoIrmaoCorrespondente(raiz.filhoEsq.irmaoDir, palavra[0]).ehFimDePalavra = false;
                            return 1;
                        }
                        else //ULTIMA LETRA ACHADA NÃO TEM FILHO
                        {
                            IrmaoEsquerda( raiz.filhoEsq, NodoIrmaoCorrespondente( raiz.filhoEsq.irmaoDir, palavra[0] ) ).irmaoDir = NodoIrmaoCorrespondente( raiz.filhoEsq.irmaoDir, palavra[0] ).irmaoDir;
                        }
                    }
                    else return 0; //PALAVRA NÃO FOI ACHADA
                }
            }
            return 0; //PALAVRA NÃO FOI ACHADA 
        }


    }
}
