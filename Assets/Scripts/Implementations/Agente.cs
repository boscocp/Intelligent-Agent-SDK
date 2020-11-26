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
    public Vector3 Destino { get; set; }
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
        string aux = gameObject.GetComponent<IMapaObjeto>().RecuperarLinha().ToString()+gameObject.GetComponent<IMapaObjeto>().RecuperarColuna().ToString();
        return gameObject.GetComponent<IMapaObjeto>().RecuperarTipo()+"_"+aux.ToString();
    }
}
