using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaUtil : MonoBehaviour
{
    public static Tile_Old GetTile(int posicao)
    {
        return MapManager.Instance.Mapa.GetMapa()[posicao];
    }

    public static Mapa_Old CriaMapa(int lines, int columns)
    {
        Mapa_Old mapa = new Mapa_Old(lines, columns);
        MapaUtil.CriaMapa(mapa);
        return mapa;
    }

    public static void CriaMapa(Mapa_Old mapa)
    {
        int contador = 0;
        for (int i = 0; i < mapa.Linhas; i++)
        {
            for (int j = 0; j < mapa.Colunas; j++)
            {
                Tile_Old tile = new Tile_Old(contador, i, j);
                mapa.GetMapa().Add(tile);
                contador++;
            }
        }
    }
}
