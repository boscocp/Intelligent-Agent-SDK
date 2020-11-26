using System;
using System.Collections.Generic;

public class Tile_Old
{
        private int _id, _linha, _coluna;
        public int Id {get {return _id;} set {_id = value;}}
        public int Linha {get {return _linha;} set {_linha = value;}}
        public int Coluna {get {return _coluna;} set {_coluna = value;}}

        public Tile_Old(int id, int linha, int coluna)
        {
            _id = id;
            _linha = linha;
            _coluna = coluna;
        }
        
}