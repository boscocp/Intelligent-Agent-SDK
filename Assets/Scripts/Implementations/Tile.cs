using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : ITile
{
    private int _id, _linha, _coluna;
    public int Id { get { return _id; } set { _id = value; } }
    public int Linha { get { return _linha; } set { _linha = value; } }
    public int Coluna { get { return _coluna; } set { _coluna = value; } }
    private GameObject _prefab;
    public Tile()
    {
    }
    public Tile(int id, int linha, int coluna, GameObject prefab)
    {
        _id = id;
        _linha = linha;
        _coluna = coluna;
        _prefab = prefab;
    }

    public void InstanciarTile()
    {
        
    }
}
