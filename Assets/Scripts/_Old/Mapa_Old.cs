using System;
using System.Collections.Generic;

public class Mapa_Old
{
    private List<Tile_Old> _mapa = new List<Tile_Old>();
    private int _linhas;
    public Dictionary <int, bool[]> RespostasDoSensor = new Dictionary<int, bool[]>();
    public int Linhas {get {return _linhas;} set {_linhas = value;}}
    public int Colunas {get {return _colunas;} set {_colunas = value;}}

    private int _colunas;

    public Mapa_Old(int linhas, int colunas)
    {
        _linhas = linhas; 
        _colunas = colunas;
    }

    public List<Tile_Old> GetMapa()
    {
        return _mapa;
    }

}