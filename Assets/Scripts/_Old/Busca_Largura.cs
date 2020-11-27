using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Busca_Largura_Old : Busca_Old, IBusca
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
        List<int> fila = new List<int>();
        List<int> explorados = new List<int>();
        fila.Add(TileInicial);

        while (fila.Any())
        {
            if (!fila.Any())
            {
                return null;
                Debug.Log("Nao e possivel encontrar um caminho");
            }

            tileAtual = fila[0];
            fila.RemoveAt(0);

            explorados.Add(tileAtual);

            if (tileAtual == TileObjetivo)
            {
                return BuscaUtil_Old.MontaCaminho(_inicial, _final, Pais);
            }
            else
            {
                foreach (int vizinho in Vizinhos[tileAtual])
                {
                    if (!explorados.Contains(vizinho) && !fila.Contains(vizinho) && !MapManager.Instance.Mapa.RespostasDoSensor[vizinho][0])
                    {
                        Pais[vizinho] = tileAtual;
                        fila.Add(vizinho);
                    }
                }
            }
        }
        Debug.Log("Nao e possivel encontrar um caminho");
        return null;
    }
}
