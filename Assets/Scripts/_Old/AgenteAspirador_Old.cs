using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAspirador_Old : Agente_Old, IAgente_Old
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public IEnumerator LigarAgente()
    {
        Moving = true;
        int steps = 0;
        int maxSteps = 150;
        float random = UnityEngine.Random.Range(0f, 10f);
        while (steps < maxSteps)
        {
            if (SenseDirt(this.CurrentID)) Clean(this.CurrentID);
            yield return new WaitForSeconds(_timeCorroutine / 2);
            if (random < 6f && !isParede(this.TargetID))
            {
                AgentUtil.MoveFoward(this);
                yield return new WaitForSeconds(_timeCorroutine);
            }
            else if (random < 8f)
            {
                RotateLeft();
                yield return new WaitForSeconds(_timeCorroutine / 2);
            }
            else
            {
                RotateRigth();
                yield return new WaitForSeconds(_timeCorroutine / 2);
            }
            random = UnityEngine.Random.Range(0f, 10f);
            steps++;
        }
        Moving = false;
        yield return null;
    }

    private void Clean(int currentID)
    {
        bool[]senseAux = AgentUtil.Sense(currentID);
        senseAux[1] = false;
        AgentUtil.Action(currentID,senseAux);//TODO: perguntar dica do mapa manager saber que isso foi feito e atualizar
        Destroy(GameObject.Find("sujeira_"+currentID));
    }

    private bool isParede(int targetID)
    {
        return AgentUtil.Sense(targetID)[0];
    }

    private bool SenseDirt(int currentID)
    {
        return AgentUtil.Sense(currentID)[1];
    }
}
