using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaManager : MonoBehaviour, IMapaManager
{
    private IMapa _mapa;
    private void Awake()
    {
        _mapa = gameObject.GetComponent<IMapa>();
    }
    public IMapa CriarMapa(int linhas, int colunas)
    {
        _mapa.Criar(linhas, colunas);
        return _mapa;
    }
    public void DestruirMapa()
    {
        if (_mapa != null)
        {
            _mapa.Destruir();
        }
    }

    public IMapa RecuperarMapa()
    {
        return _mapa;
    }
}
