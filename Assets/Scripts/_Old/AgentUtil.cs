using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentUtil
{
    public static bool MoveFoward(Agente_Old agente)
    {
        if(MapManager.Instance.Mapa == null || agente == null) return false;
        if (!MapManager.Instance.Mapa.RespostasDoSensor[agente.TargetID][0])
        {
            agente.CurrentID = agente.TargetID;
            agente.SetTargetPos(agente.TargetAux);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool[] Sense(int id)//só sense PEAS performance, ambiente, sensor e atuador.
    {
        if(MapManager.Instance.Mapa == null || id <0) return null;
        return MapManager.Instance.Mapa.RespostasDoSensor[id];
    }

    public static void Action(int id, bool[] actions)//virar atuador como dar o clean fazendo o mapa se limpar?
    {
        MapManager.Instance.Mapa.RespostasDoSensor[id]= actions;
    }
}
