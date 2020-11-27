using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NOTDO: copiar codigo

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public List<string> agentes;
    public int _velocidadeJogo = 2;
    public int VelocidadeJogo { get => _velocidadeJogo; set => _velocidadeJogo = value; }
    private IMapaManager _mapaManager;
    public int mapaEscolhido = 0;
    public List<ScriptableObject> mapasSO;
    private IMapa _mapaAtual;
    public Mapa MapaAtual { get => _mapaAtual as Mapa; set => _mapaAtual = value; }
    private List<IAgente> _agentesOnline = new List<IAgente>();
    private bool _agentesLigados = false;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _mapaManager = gameObject.GetComponent<IMapaManager>();
    }
    void Start()
    {
        Inicializar();
    }
    void Inicializar()
    {
        CriarMapa();
        RecuperarAgentes();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !_agentesLigados)
        {
            LigarAgentes();
            _agentesLigados = true;
        }else if(Input.GetKeyUp(KeyCode.Space))
        {
            foreach (Agente agenteOnline in _agentesOnline)
            {
                //if (agenteOnline != null) agenteOnline.Passo();
            }
        }

        else if (Input.anyKey)
        {
            int numero = RecuperaTeclaNumerica();
            if (numero != mapaEscolhido)
            {
                if (VerificarMapaExiste(numero))
                {
                    mapaEscolhido = numero;
                    Reiniciar();
                }
                else
                {
                    Debug.Log("Nao foi possivel encontrar o mapa escolhido: " + numero);
                }
            }
        }
    }

    private void Reiniciar()
    {
        DesligarAgentes();
        DestruirMapa();
        Inicializar();
    }

    void CriarMapa()
    {
        if (!VerificarListaPopulada(mapasSO))
        {
            Debug.LogError("Falta adicionar os mapas");
            return;
        }
        var mapaSO = mapasSO[mapaEscolhido] as MapaScriptableObject;
        _mapaAtual = _mapaManager.CriarMapa(mapaSO.linhas, mapaSO.colunas);

        foreach (var colecao in mapaSO.objetos)
        {
            foreach (Vector2Int posicao in colecao.posicoes)
            {
                _mapaAtual.AdicionarObjeto(posicao.x, posicao.y, colecao.tipo);
            }
        }
    }

    private void DestruirMapa()
    {
        _mapaManager.DestruirMapa();
    }

    void RecuperarAgentes()
    {
        if (!VerificarListaPopulada(agentes))
        {
            Debug.Log("Nenhum tipo de agente informado");
            return;
        }
        foreach (string agente in agentes)
        {
            foreach (IMapaObjeto mapaObjeto in _mapaManager.RecuperarMapa().RecuperarObjetosPor(agente))
            {
                var agenteAux = (mapaObjeto as MapaObjeto).GetComponent<IAgente>();
                _agentesOnline.Add(agenteAux);
            }
        }
    }

    void LigarAgentes()
    {
        if (!VerificarListaPopulada(_agentesOnline))
        {
            Debug.LogError("Não há agentes online");
            return;
        }
        if (!VerificarMapaExiste(mapaEscolhido))
        {
            Debug.LogError("Nao foi possivel encontrar o mapa escolhido");
            return;
        }
        foreach (IAgente agenteOnline in _agentesOnline)
        {
            if (agenteOnline != null) agenteOnline.Ligar();
        }
    }
    private void DesligarAgentes()
    {
        if (!VerificarListaPopulada(_agentesOnline))
        {
            Debug.Log("Não há agentes online");
            return;
        }

        foreach (var agenteOnline in _agentesOnline)
        {
            if (agenteOnline != null) agenteOnline.Desligar();
        }
    }

    private bool VerificarListaPopulada<T>(List<T> list)
    {
        return (list != null && list.Count > 0);
    }

    private bool VerificarMapaExiste(int numero)
    {
        return (numero >= 0 && numero <= mapasSO.Count - 1);
    }

    private int RecuperaTeclaNumerica()
    {
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 0 && number < 10)
            {
                return number;
            }
        }
        return mapaEscolhido;
    }
    void OnDestroy()
    {
        if (this == _instance) _instance = null;  //serve pra ter um mapa manager diferente pra cada cena
    }
}
