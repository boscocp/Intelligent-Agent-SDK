using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }
    public int _lines = 0;
    public int _columns = 0;
    public int[] paredes;
    public int[] sujeira;
    private Mapa_Old _mapa;
    public Mapa_Old Mapa { get => _mapa; set => _mapa = value; }
    private List<GameObject> _visualMap = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }

        Mapa = MapaUtil.CriaMapa(_lines, _columns);
        DefineSensibilidade();
        ColocaParedeLateral();
        for (int i = 0; i < paredes.Length; i++)
        {
            Mapa.RespostasDoSensor[paredes[i]][0] = true;
        }
        for (int i = 0; i < sujeira.Length; i++)
        {
            Mapa.RespostasDoSensor[sujeira[i]][1] = true;
        }

        InstatiateMap(Mapa);
    }
    void OnDestroy() 
    {
         if (this == _instance) _instance = null;  //serve pra ter um mapa manager diferente pra cada cena
    }
    private void DefineSensibilidade()
    {
        foreach (Tile_Old tile in Mapa.GetMapa())
        {
            try
            {   //mapa manager do mundo aspirador
                bool[] aux = new bool[2] { false, false };
                Mapa.RespostasDoSensor.Add(tile.Id, aux);
            }
            catch (ArgumentException)
            {
                Debug.Log("An element with Key = \"txt\" already exists.");
            }
        }
    }

    private void ColocaParedeLateral()
    {
        foreach (Tile_Old tile in Mapa.GetMapa())
        {
            if (tile.Linha == Mapa.Linhas - 1 || tile.Coluna == Mapa.Colunas - 1 || tile.Linha == 0 || tile.Coluna == 0)
            {
                Mapa.RespostasDoSensor[tile.Id][0] = true;
            }
        }
    }

    public void EfeitoAtuador(int tile)
    {

    }

    private void InstatiateMap(Mapa_Old mapa)
    {
        GameObject mapaLayout = new GameObject();
        mapaLayout.name = "mapa";
        GameObject sujeiras = new GameObject();
        sujeiras.name = "sujeiras";
        foreach (Tile_Old tile in mapa.GetMapa())
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(mapaLayout.transform);
            cube.name = Convert.ToString(tile.Id);
            cube.GetComponent<Collider>().isTrigger = true;
            cube.transform.position = new Vector3(0 + tile.Linha, -1, 0 + tile.Coluna);
            //cria cubo adicional para parede
            if (Mapa.RespostasDoSensor[tile.Id][0])
            {
                Material newMat = Resources.Load("Blue", typeof(Material)) as Material;
                GameObject cubeWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeWall.transform.SetParent(mapaLayout.transform);
                cubeWall.name = "parede_" + Convert.ToString(tile.Id);
                cubeWall.GetComponent<Renderer>().material = newMat;
                cubeWall.transform.position = new Vector3(0 + tile.Linha, 0, 0 + tile.Coluna);
            }
            else if (Mapa.RespostasDoSensor[tile.Id][1])
            {
                Material newMat = Resources.Load("Mat_Sujeira", typeof(Material)) as Material;
                GameObject sujeira = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                sujeira.transform.SetParent(sujeiras.transform);
                sujeira.name = "sujeira_" + Convert.ToString(tile.Id);
                sujeira.GetComponent<Renderer>().material = newMat;
                sujeira.transform.position = new Vector3(0 + tile.Linha, -0.9f, 0 + tile.Coluna);
                sujeira.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2);
            }
        }
    }
}
