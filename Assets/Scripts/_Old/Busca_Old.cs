using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Busca_Old : MonoBehaviour
{
    private int _inicial, _final;
    private bool _configurado = false;
    public int Inicial { get { return _inicial; } set { _inicial = value; } }
    public int Final { get { return _final; } set { _final = value; } }
    public bool Configurado { get { return _configurado; } set { _configurado = value; } }
    protected Dictionary<int, List<int>> Vizinhos = new Dictionary<int, List<int>>();
    protected Dictionary<int, int> Pais = new Dictionary<int, int>();
    private Mapa_Old _mapa;
    // Start is called before the first frame update

    public void ConfiguraBusca()
    {
        _mapa = MapManager.Instance.Mapa;
        foreach (Tile_Old tile in _mapa.GetMapa())
        {
            AcharVizinhos(tile);
        }
        Configurado = true;
    }

    public void AcharVizinhos(Tile_Old tile)
    {
        if(!Configurado)
        {
            int id = tile.Id;
        List<int> vizinhos = new List<int>();
        //vizinho da ortogonal
        //esquerda
        if (tile.Coluna > 0)
        {
            int aux = id - 1;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }
        //direita ok
        if (tile.Coluna < _mapa.Colunas - 1)
        {
            int aux = id + 1;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }
        //vizinho de cima
        if (tile.Linha > 0)
        {
            int aux = id - _mapa.Colunas;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }

        if (tile.Linha < _mapa.Linhas - 1)
        {
            int aux = id + _mapa.Colunas;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }

        //vizinhos dos cantos.
        // canto superior esquerda
        if (tile.Linha > 0 && tile.Coluna > 0)
        {
            int aux = id - _mapa.Colunas - 1;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }
        // canto superior direita
        if (tile.Linha > 0 && tile.Coluna < _mapa.Colunas - 1)
        {
            int aux = id - _mapa.Colunas + 1;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }
        // canto inferior esquerdo ok
        if (tile.Linha < _mapa.Linhas - 1 && tile.Coluna > 0)
        {
            int aux = id + _mapa.Colunas - 1;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }
        //canto inferior direita ok
        if (tile.Linha < _mapa.Linhas - 1 && tile.Coluna < _mapa.Colunas - 1)
        {
            int aux = id + _mapa.Colunas + 1;
            vizinhos.Add(_mapa.GetMapa()[aux].Id);
        }
        Vizinhos.Add(id, vizinhos);
        }
    }
}
