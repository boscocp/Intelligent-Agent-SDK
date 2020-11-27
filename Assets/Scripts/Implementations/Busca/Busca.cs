using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Busca : MonoBehaviour
{
    public List<int[]> AcharVizinhos(int linha, int coluna)
    {
        MapaScriptableObject mapaScriptableObject = GameManager.Instance.MapaAtual.GetComponent<MapaScriptableObject>();
        int linhas = mapaScriptableObject.linhas;
        int colunas = mapaScriptableObject.colunas;
        return BuscaUtil.AcharVizinhos(linha, coluna, linhas, colunas);
    }
}
