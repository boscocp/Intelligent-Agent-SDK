using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaObjeto : MonoBehaviour, IMapaObjeto
{
    private int linha;
    private int coluna;
    private string tipo;

    public IMapaObjeto Atualizar(int linha, int coluna, string tipo)
    {
        this.linha = linha;
        this.coluna = coluna;
        this.tipo = tipo;
        return this;
    }

    public int RecuperarColuna()
    {
        return coluna;
    }

    public int RecuperarLinha()
    {
        return linha;
    }

    public string RecuperarTipo()
    {
        return tipo;
    }
}
