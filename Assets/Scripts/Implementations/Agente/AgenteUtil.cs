using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteUtil
{
    public static IEnumerator MoverFrente(Agente agente, List<Agente> agentes)
    {

        agente.Destino = Vector3Int.RoundToInt(agente.transform.position + agente.transform.forward);
        if (!VerificarObstaculo(agente, agente.Destino.x, agente.Destino.z)
            && PodeMover(agente, agentes))
        {

            agente.Moving = true;
            var velocidade = Time.deltaTime * GameManager.Instance.VelocidadeJogo;

            while (agente.Moving)
            {
                agente.transform.position = Vector3.MoveTowards(agente.transform.position, agente.Destino, velocidade);
                float dist = Vector3.Distance(agente.Destino, agente.transform.position);
                if (dist <= agente.DistanciaMinima)
                {
                    agente.AtualizarAgente();
                    agente.transform.position = agente.Destino;
                    agente.Moving = false;
                }
                yield return null;
            }
        }
    }
    public static bool PodeMover(Agente agente, List<Agente> agentes)
    {
        foreach (Agente outroAgente in agentes)
        {
            if (outroAgente.transform.position != agente.transform.position
                && outroAgente.Destino == agente.Destino
                /*&& outroAgente.Destino == agente.transform.position*/)
                return false;
        }
        return true;
    }

    // public static IEnumerator Rotacionar(Agente agente)
    // {
    //     agente.Moving = true;
    //     var velocidade = Time.deltaTime * GameManager.Instance.VelocidadeJogo;
    //     agente.Destino = agente.transform.position + Vector3.left;
    //     Quaternion rotacao = Quaternion.LookRotation(agente.Destino - agente.transform.position);
    //     while (agente.Moving)
    //     {
    //         agente.transform.rotation = Quaternion.RotateTowards(agente.transform.rotation, rotacao, velocidade * 100f);
    //         float angle = Vector3.Angle(agente.Destino - agente.transform.position, agente.transform.forward);
    //         if (angle <= Mathf.Abs(agente.DistanciaMinima))
    //         {
    //             agente.transform.LookAt(agente.Destino);
    //             agente.Moving = false;
    //         }
    //         yield return null;
    //     }
    // }

    public static void RotateLeft(Agente agente)
    {
        agente.transform.Rotate(0, -90, 0, Space.World);
        agente.Destino = Vector3Int.RoundToInt(agente.transform.position + agente.transform.forward);
    }

    public static void RotateRigth(Agente agente)
    {
        agente.transform.Rotate(0, 90, 0, Space.World);
        agente.Destino = Vector3Int.RoundToInt(agente.transform.position + agente.transform.forward);
    }

    public static bool VerificarObstaculo(Agente agente, int linha, int coluna)
    {
        foreach (string tipo in agente.Sentir(linha, coluna))
        {
            if (agente._obstaculos.Contains(tipo)) return true;
        }
        return false;
    }
}