using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaLargura : Busca, IBusca
// Start is called before the first frame update
{
    private Vector2Int _inicial;
    private Agente _agente;

    void Start()
    {
        _agente = gameObject.GetComponent<Agente>();
        if (!_agente) Debug.LogError("Não tem agente para busca");
    }
    public List<int[]> Executar(int[] destino)
    {
        var tamanho = GameManager.Instance.MapaAtual.RecuperarTamanho();
        Vector2Int posicaoInicial = BuscaUtil.RecuperarPosicaoInt(_agente.transform.position);
        Vector2Int posicaoDestino = new Vector2Int(destino[0], destino[1]);
        Vector2Int posicaoAtual;
        List<Vector2Int> fila = new List<Vector2Int>();
        List<Vector2Int> explorados = new List<Vector2Int>();
        fila.Add(posicaoInicial);
        while (fila.Count > 0)
        {
            posicaoAtual = fila[0];
            fila.RemoveAt(0);
            explorados.Add(posicaoAtual);
            if(posicaoAtual == posicaoDestino) break;
            else 
            {
                var vizinhos = BuscaUtil.AcharVizinhos(posicaoAtual.x, posicaoAtual.y, tamanho[0], tamanho[1]);
                foreach (Vector2Int vizinho in vizinhos)
                {
                    if(!explorados.Contains(vizinho) && ! fila.Contains(vizinho) && !AgenteUtil.VerificarObstaculo(_agente, vizinho.x, vizinho.y))
                    {
                        Maes[vizinho] = posicaoAtual;
                        fila.Add(vizinho);
                    }
                }
            }
            if (fila.Count <= 0)
            {
                Debug.Log("Não foi possivel achar o caminho");
                return null;
            }

        }
 
        List<int[]> caminho = new List<int[]>();
        foreach (var passo in BuscaUtil.MontarCaminho(posicaoInicial,posicaoDestino, Maes))
        {
            caminho.Add(new int[]{passo.x,passo.y});
        }
        return caminho;
    }
}
