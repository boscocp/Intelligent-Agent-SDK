using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Busca_AEstrela_Old : Busca_Old, IBusca_Old
{
    void Start()
    {
        ConfiguraBuscaAEstrela();
    }
    private Dictionary<int, float[]> heuristicaGanhoF = new Dictionary<int, float[]>(); //0 h, 1 ganho, 2 F.
    private void ConfiguraBuscaAEstrela()
    {
        foreach (var tile in MapManager.Instance.Mapa.GetMapa())
        {
            float[] aux = new float[3]{0f,0f,0f};
            heuristicaGanhoF.Add(tile.Id,aux);
        }
        ConfiguraBusca();
    }
    public List<int> Executar()
    {
        Inicial = gameObject.GetComponent<AgenteAchaCaminho>().CurrentID;
        Final = gameObject.GetComponent<AgenteAchaCaminho>().IrAteh;

        if (MapManager.Instance.Mapa == null)
        {
            return null;
        }
        Tile_Old TileInicial = MapaUtil.GetTile(Inicial);
        Tile_Old TileObjetivo = MapaUtil.GetTile(Final);
        List<int> listaAberta = new List<int>();
        List<int> listaFechada = new List<int>();

        bool achouCaminho = false;

        int tileAtual = TileInicial.Id;
        listaAberta.Add(tileAtual);
        
        while (!achouCaminho)
        {
            
            tileAtual = ProcurarMenorF(listaAberta);
            listaAberta.Remove(tileAtual);
            listaFechada.Add(tileAtual);
            
            if(tileAtual == TileObjetivo.Id) achouCaminho = true;
            
            foreach (int tile in Vizinhos[tileAtual])
            {
                //verificar se esse tile é uma parede ou
                if (listaFechada.Contains(tile) || MapManager.Instance.Mapa.RespostasDoSensor[tile][0])// TODO: obstáculo
                {
                    continue;
                }
                else
                {
                    if (!listaAberta.Contains(tile))
                    {
                        listaAberta.Add(tile);
                        Pais[tile]=tileAtual;
                        heuristicaGanhoF[tile][0] = CalcularH(MapaUtil.GetTile(tile), MapaUtil.GetTile(TileObjetivo.Id));
                        heuristicaGanhoF[tile][1] = CalcularG(tile, tileAtual, MapManager.Instance.Mapa.Colunas);
                        heuristicaGanhoF[tile][2] = CalcularF(tile);
                    }
                    else
                    {
                        if (heuristicaGanhoF[tile][1] < heuristicaGanhoF[tileAtual][1])
                        {
                            Pais[tile] = tileAtual;
                            heuristicaGanhoF[tile][1] = CalcularG(tileAtual, tile, MapManager.Instance.Mapa.Colunas);
                            heuristicaGanhoF[tile][2] = CalcularF(tile);
                        }
                    }
                }
            }
            if (!listaAberta.Any())
            {
                Debug.Log("Nao e possivel encontrar um caminho");
                return null;
            }
        }
        
        return BuscaUtil_Old.MontaCaminho(TileInicial.Id, tileAtual, Pais);
    }

    private float CalcularF(int tile)
    {
        return heuristicaGanhoF[tile][1] + heuristicaGanhoF[tile][2];
    }

    private float CalcularG(int tileAtual, int tileVizinho, int colunas)
    {
        //saber se está na ortogonal tile de 10 por 10, diagonal 14 (hipotenusa)
        if (tileVizinho == tileVizinho - colunas || tileVizinho == tileVizinho + colunas || tileVizinho == tileVizinho - 1 || tileVizinho == tileVizinho + 1)
        {
            return tileVizinho + 10f;
        }
        else
        {
            return tileVizinho + 14f;
        }
    }

    private int ProcurarMenorF(List<int> lista)
    {
        lista.Sort();
        return lista[0];
    }

    private float CalcularH(Tile_Old tileAtual, Tile_Old tileVizinho)
    {   //calcular distancia entre 2 pontos
        float posicaoVizinhoX = (float)tileVizinho.Coluna;
        float posicaoVizinhoY = (float)tileVizinho.Linha;

        float posicaoAtualX = (float)tileAtual.Coluna;
        float posicaoAtualY = (float)tileAtual.Linha;

        float distanciaX = Math.Abs(posicaoVizinhoX - posicaoAtualX);
        float distanciaY = Math.Abs(posicaoVizinhoY - posicaoAtualY);

        distanciaX = distanciaX * distanciaX;
        distanciaY = distanciaY * distanciaY;

        double distanciaTotal = Math.Sqrt(distanciaY + distanciaX) * 10;
        return (float)distanciaTotal;
    }
}
