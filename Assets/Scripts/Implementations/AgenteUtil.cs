﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteUtil
{
    public static IEnumerator MoverFrente(Agente agente, List<string> obstaculos)
    {
        
        agente.Destino = agente.transform.position + agente.transform.forward;
        if(!VerificarObstaculo(agente,obstaculos,(int)agente.Destino.x,(int)agente.Destino.z))//TODO: atualizar o ImapaObjeto linha e coluna
        {
            agente.Moving = true;
            var velocidade = Time.deltaTime * GameManager.Instance.VelocidadeJogo;
            
            while (agente.Moving)
            {
                agente.transform.position = Vector3.MoveTowards(agente.transform.position, agente.Destino, velocidade);
                float dist = Vector3.Distance(agente.Destino, agente.transform.position);
                if (dist <= agente.DistanciaMinima)
                {
                    agente.transform.position = new Vector3(Mathf.Round(agente.Destino.x), Mathf.Round(agente.Destino.y), Mathf.Round(agente.Destino.z));
                    agente.Moving = false;
                }
                yield return null;
            }
        }
    }

    public static IEnumerator Rotacionar(Agente agente)
    {
        agente.Moving = true;
        var velocidade = Time.deltaTime * GameManager.Instance.VelocidadeJogo;
        agente.Destino = agente.transform.position + Vector3.left;
        Quaternion rotacao = Quaternion.LookRotation(agente.Destino - agente.transform.position);
        while (agente.Moving)
        {
            agente.transform.rotation = Quaternion.RotateTowards(agente.transform.rotation, rotacao, velocidade * 100f);
            float angle = Vector3.Angle(agente.Destino - agente.transform.position, agente.transform.forward);
            if (angle <= Mathf.Abs(agente.DistanciaMinima))
            {
                agente.transform.LookAt(agente.Destino);
                agente.Moving = false;
            }
            yield return null;
        }
    }

    public static void RotateLeft(Agente agente)
    {
        agente.transform.Rotate(0, -90, 0, Space.World);
    }

    public static void RotateRigth(Agente agente)
    {
        agente.transform.Rotate(0, 90, 0, Space.World);
    }

    public static bool VerificarObstaculo(Agente agente, List<string> obstaculos, int linha, int coluna)
    {
        foreach (string tipo in agente.Sentir(linha,coluna))
        {
            if(obstaculos.Contains(tipo)) return true;
        }
        return false;
    }
}