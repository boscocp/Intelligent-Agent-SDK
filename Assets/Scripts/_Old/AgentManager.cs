using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public int[] _agentesId;
    public GameObject[] _agentes;
    [SerializeField] private int _gameSpeed = 1;
    public int GameSpeed { get => _gameSpeed; set => _gameSpeed = value; }
    // Start is called before the first frame update
    void Start()
    {
        for (int id = 0; id < _agentesId.Length; id++)
        {
            InstatiateAgent(id);
        }
    }

    private void InstatiateAgent(int id)
    {
        Vector3 position = GameObject.Find(Convert.ToString(_agentesId[id])).transform.position;
        position.y = 0;
        //importante
        GameObject agent_obj = Instantiate(_agentes[id], position, Quaternion.identity);
        agent_obj.GetComponent<Agente_Old>().CurrentID = _agentesId[id];
        agent_obj.GetComponent<Agente_Old>().GameSpeed = _gameSpeed;
        agent_obj.GetComponent<Agente_Old>().TimeCorroutine = _gameSpeed;
        _agentes[id] = agent_obj; //erro bsta no agentesId[id] e o objeto instanciado n tava em nenhum lugar, substitui o objeto em agentes pelo instanciado 
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))//ajeitar !Moving && !Moving
        {
            for (int id = 0; id < _agentes.Length; id++)
            {
                if(!_agentes[id].GetComponent<Agente_Old>().Moving)
                {
                    StartCoroutine(_agentes[id].GetComponent<IAgente_Old>().LigarAgente()); 
                }
            }  
        }
    }
}
