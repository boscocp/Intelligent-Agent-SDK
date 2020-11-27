using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaUtil : MonoBehaviour
{
    public static List<int[]> AcharVizinhos(int linha, int coluna, int linhas, int colunas)
    {
        List<int[]> vizinhos = new List<int[]>();
        //ortogonais
        if (linha < linhas - 1) vizinhos.Add(new int[2] { linha + 1, coluna });
        if (coluna < colunas - 1) vizinhos.Add(new int[2] { linha, coluna + 1 });
        if (linha > 0) vizinhos.Add(new int[2] { linha - 1, coluna });
        if (coluna > 0) vizinhos.Add(new int[2] { linha, coluna - 1 });
        //cantos diagonais
        if (coluna > 0 && linha > 0) vizinhos.Add(new int[2] { linha - 1, coluna - 1 });
        if (linha < linhas - 1 && coluna > 0) vizinhos.Add(new int[2] { linha + 1, coluna - 1 });
        if (linha < linhas - 1 && coluna < colunas - 1) vizinhos.Add(new int[2] { linha + 1, coluna + 1 });
        if (coluna < colunas - 1 && linha > 0) vizinhos.Add(new int[2] { linha - 1, coluna + 1 });
        return vizinhos;
    }
}
