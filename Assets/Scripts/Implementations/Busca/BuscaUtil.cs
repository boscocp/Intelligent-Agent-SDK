using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaUtil : MonoBehaviour
{
    public static List<Vector2Int> AcharVizinhos(int linha, int coluna, int linhas, int colunas)
    {
        List<Vector2Int> vizinhos = new List<Vector2Int>();
        //ortogonais
        if (linha < linhas - 1) vizinhos.Add(new Vector2Int(linha + 1, coluna));
        if (coluna < colunas - 1) vizinhos.Add(new Vector2Int(linha, coluna + 1));
        if (linha > 0) vizinhos.Add(new Vector2Int(linha - 1, coluna));
        if (coluna > 0) vizinhos.Add(new Vector2Int(linha, coluna - 1));
        //cantos diagonais
        if (coluna > 0 && linha > 0) vizinhos.Add(new Vector2Int(linha - 1, coluna - 1));
        if (linha < linhas - 1 && coluna > 0) vizinhos.Add(new Vector2Int(linha + 1, coluna - 1));
        if (linha < linhas - 1 && coluna < colunas - 1) vizinhos.Add(new Vector2Int(linha + 1, coluna + 1));
        if (coluna < colunas - 1 && linha > 0) vizinhos.Add(new Vector2Int(linha - 1, coluna + 1));
        return vizinhos;
    }

    public static List<Vector2Int> MontarCaminho(Vector2Int inicio, Vector2Int destino, Dictionary<Vector2Int, Vector2Int> maes)
    {
        Vector2Int posicaoAuxiliar = maes[destino];
        List<Vector2Int> caminho = new List<Vector2Int>();
        caminho.Add(destino);
        while (posicaoAuxiliar != inicio)
        {
            caminho.Add(posicaoAuxiliar);
            posicaoAuxiliar = maes[posicaoAuxiliar];
        }
        caminho.Reverse();
        return caminho;
    }

    public static Vector2Int RecuperarPosicaoInt(Vector3 posicao)
    {
        int linha = (int)Mathf.Round(posicao.x);
        int coluna = (int)Mathf.Round(posicao.z);
        return new Vector2Int(linha, coluna);
    }
}
