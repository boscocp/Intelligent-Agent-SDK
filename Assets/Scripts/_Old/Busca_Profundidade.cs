using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Busca_Profundidade_Old : Busca_Old, IBusca_Old

{
    private int _inicial, _final;
    public List<int> Executar()
    {
        ConfiguraBusca();
        _inicial = gameObject.GetComponent<AgenteAchaCaminho>().CurrentID;
        _final = gameObject.GetComponent<AgenteAchaCaminho>().IrAteh;

        int TileInicial = MapaUtil.GetTile(_inicial).Id;
        int TileObjetivo = MapaUtil.GetTile(_final).Id;
        int tileAtual = TileInicial;
        List<int> pilha = new List<int>();
        List<int> explorados = new List<int>();
        pilha.Add(TileInicial);

        while (pilha.Any())
        {
            if (!pilha.Any())
            {
                Debug.Log("Nao e possivel encontrar um caminho");
                return null;
            }

            tileAtual = pilha[pilha.Count - 1];
            pilha.RemoveAt(pilha.Count - 1);

            explorados.Add(tileAtual);

            if (tileAtual == TileObjetivo)
            {
                return BuscaUtil_Old.MontaCaminho(_inicial, _final, Pais);
            }
            else
            {
                foreach (int vizinho in Vizinhos[tileAtual])
                {
                    if (!explorados.Contains(vizinho) && !pilha.Contains(vizinho) && !MapManager.Instance.Mapa.RespostasDoSensor[vizinho][0])
                    {
                        Pais[vizinho] = tileAtual;
                        pilha.Add(vizinho);
                    }
                }
            }
        }
        Debug.Log("Nao e possivel encontrar um caminho");
        return null;
    }
}
