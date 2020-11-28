using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour, IMapa
{
    private int[] _tamanho;
    private const string BORDA = "borda";
    private const string CHAO = "chao";
    private const string OBJETOS = "objetos";
    public bool bordas = true;
    public GameObject chaoPrefab;
    public List<GameObject> prefabs;
    public List<string> tipos;
    private List<GameObject> todosObjetos = new List<GameObject>();
    private void Awake()
    {
        if (prefabs.Count <= 0)
        {
            Debug.LogError("Não encontrado nenhum prefab");
        }

        if (prefabs.Count != tipos.Count)
        {
            Debug.Log("Número de prefabs diferente do numero de tipos");
        }
    }
    public void Criar(int linhas, int colunas)
    {
        InstanciarChao(linhas, colunas);
        _tamanho = new int[] {linhas,colunas};
        if (bordas) InstanciarBorda(linhas, colunas);
    }
    public IMapaObjeto AdicionarObjeto(int linha, int coluna, string tipo)
    {
        GameObject objetosParent = RecuperarParentPorTipo(OBJETOS);
        GameObject parent = RecuperarParentPorTipo(tipo);
        parent.transform.SetParent(objetosParent.transform);
        var prefabAux = RecuperarPrefab(tipo);
        var mapaObjeto = InstanciarObjeto(tipo, prefabAux, parent, linha, coluna);
        return mapaObjeto;
    }
    public bool RemoverObjeto(int linha, int coluna, string tipo)
    {
        foreach (GameObject objeto in todosObjetos)
        {
            MapaObjeto mapaObjeto = objeto.GetComponent<MapaObjeto>();
            if (mapaObjeto
                && mapaObjeto.RecuperarLinha() == linha
                && mapaObjeto.RecuperarColuna() == coluna 
                && mapaObjeto.RecuperarTipo() == tipo)
            {
                todosObjetos.Remove(objeto);
                Destroy(objeto);
                return true;
            }
        }
        return false;
    }
    public List<IMapaObjeto> RecuperarObjetosPor(string tipo)
    {
        List<IMapaObjeto> retorno = new List<IMapaObjeto>();
        foreach (GameObject objeto in todosObjetos)
        {
            MapaObjeto mapaObjeto = objeto.GetComponent<MapaObjeto>();
            if (mapaObjeto && mapaObjeto.RecuperarTipo() == tipo) retorno.Add(mapaObjeto);
        }
        return retorno;
    }
    public List<IMapaObjeto> RecuperarObjetosPor(int linha, int coluna)
    {
        List<IMapaObjeto> retorno = new List<IMapaObjeto>();
        foreach (GameObject objeto in todosObjetos)
        {
            MapaObjeto mapaObjeto = objeto.GetComponent<MapaObjeto>();
            if (mapaObjeto
                && mapaObjeto.RecuperarLinha() == linha
                && mapaObjeto.RecuperarColuna() == coluna)
            {
                retorno.Add(mapaObjeto);
            }

        }
        return retorno;
    }
    public void Destruir()
    {
        foreach (GameObject objeto in RecuperarParents())
        {
            Destroy(objeto);
        }
        todosObjetos.Clear();
    }
    private GameObject RecuperarPrefab(string tipo)
    {
        GameObject prefab;
        if (tipos.Contains(tipo))
        {
            int index = tipos.IndexOf(tipo);
            if (prefabs.Count < index)
            {
                Debug.Log("Número de prefabs diferente do numero de tipos");
                index = 0;
            }
            prefab = prefabs[index];
        }
        else
        {
            Debug.Log("Tipo " + tipo + " não encontrado.");
            prefab = prefabs[0];
        }
        return prefab;
    }
    private MapaObjeto InstanciarObjeto(string tipo, GameObject prefab, GameObject parent, int linha, int coluna)
    {
        GameObject instancia = Instantiate(prefab, new Vector3(linha, 0, coluna), Quaternion.identity);
        instancia.transform.SetParent(parent.transform);
        instancia.name = tipo + "_" + linha + "_" + coluna;
        var mo = instancia.AddComponent<MapaObjeto>();
        mo.Atualizar(linha, coluna, tipo);
        todosObjetos.Add(instancia);
        return mo;
    }
    private void InstanciarChao(int linhas, int colunas)
    {
        GameObject prefab = chaoPrefab;
        GameObject parent = CriarParent(CHAO);
        for (int linha = 0; linha < linhas; linha++)
        {
            for (int coluna = 0; coluna < colunas; coluna++)
            {
                InstanciarObjeto(CHAO, prefab, parent, linha, coluna);
            }
        }
    }
    private void InstanciarBorda(int linhas, int colunas)
    {
        GameObject parent = CriarParent(BORDA);
        for (int linha = 0; linha < linhas; linha++)
        {
            InstanciarObjeto(BORDA, prefabs[0], parent, linha, 0);
            InstanciarObjeto(BORDA, prefabs[0], parent, linha, colunas - 1);
        }
        for (int coluna = 1; coluna < colunas - 1; coluna++)
        {
            InstanciarObjeto(BORDA, prefabs[0], parent, 0, coluna);
            InstanciarObjeto(BORDA, prefabs[0], parent, linhas - 1, coluna);
        }
    }
    private List<GameObject> RecuperarParents()
    {
        List<GameObject> tiposGO = new List<GameObject>();
        foreach (var item in todosObjetos)
        {
            if (item.GetComponent<IMapaObjeto>() == null)
            {
                tiposGO.Add(item);
            }
        }
        return tiposGO;
    }
    private GameObject RecuperarParentPorTipo(string tipo)
    {
        List<GameObject> tiposGO = RecuperarParents();
        int index = tiposGO.FindIndex((t) => t.name.Equals(tipo));
        GameObject tipoGO;
        if (index >= 0)
        {
            tipoGO = tiposGO[index];
        }
        else
        {
            tipoGO = CriarParent(tipo);
        }
        return tipoGO;
    }
    private GameObject CriarParent(string nome)
    {
        GameObject parent = new GameObject();
        parent.name = nome;
        todosObjetos.Add(parent);
        return parent;
    }

    public int[] RecuperarTamanho()
    {
        return _tamanho;
    }
}
