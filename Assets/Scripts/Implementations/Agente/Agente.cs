using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agente : MonoBehaviour, IAgente
{
    public Text titulo;
    public List<string> _obstaculos;//TODO: Passar pro Agente
    public List<string> Obstaculos { get => _obstaculos; set => _obstaculos = value; }
    public float TimeCorroutine { get; set; }
    public bool Ligado { get; set; }
    public bool Moving { get; set; }
    public Vector3Int Destino { get; set; }
    public float DistanciaMinima { get => _distanciaMinima; set => _distanciaMinima = value; }
    private float _distanciaMinima = 0.001f;

    public virtual void Start()
    {
        titulo.text = RecuperarNome();
        TimeCorroutine = 2f / GameManager.Instance.VelocidadeJogo;
    }

    public List<string> Sentir(int linha, int coluna)
    {
        List<string> sensacoes = new List<string>();
        List<IMapaObjeto> objetos = GameManager.Instance.MapaAtual.RecuperarObjetosPor(linha, coluna);
        foreach (IMapaObjeto objeto in objetos)
        {
            sensacoes.Add(objeto.RecuperarTipo());
        }
        return sensacoes;
    }
    public virtual void Atuar()
    {
        Debug.Log("Atuou");
    }
    public void Desligar()
    {
        Ligado = false;
        Debug.Log("Desligou");
    }

    public virtual void Ligar()
    {
        Ligado = true;
        Debug.Log("Ligou");
    }
    private string RecuperarNome()
    {
        IMapaObjeto mapaObjeto = gameObject.GetComponent<IMapaObjeto>();
        string aux = mapaObjeto.RecuperarLinha().ToString()+ mapaObjeto.RecuperarColuna().ToString();
        return mapaObjeto.RecuperarTipo()+"_"+ aux.ToString();
    }

    public void AtualizarAgente()
    {
        Vector3Int aux = Vector3Int.RoundToInt(transform.position);
        IMapaObjeto mapaObjeto = GetComponent<IMapaObjeto>();
        mapaObjeto.Atualizar(aux.x, aux.z, mapaObjeto.RecuperarTipo());
    }
}
