using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAchaCaminho : Agente_Old, IAgente_Old //patchfinder
{
    public int _irAteh =0;
    public int IrAteh  { get => _irAteh; set => _irAteh = value; }
    private IBusca _busca;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _busca = gameObject.GetComponent<IBusca>();
    }

    public IEnumerator LigarAgente()
    {
        List<int> path = new List<int>();
        path = _busca.Executar();
        if(MapManager.Instance.Mapa.RespostasDoSensor[_irAteh][0] || path == null)
        {
            Debug.Log("Nao consegue, pois parede ou sem caminho "+ MapManager.Instance.Mapa.RespostasDoSensor[_irAteh][0] + path);          
        }else{
            foreach(int tileID in path)
            {
                TargetID = tileID;//ajustar depois
                GameObject aux = GameObject.Find(Convert.ToString(TargetID));
                SetTargetPos(aux.transform.position);
                transform.LookAt(TargetPos);
                transform.Rotate (0, -90, 0, Space.World);
                yield return new WaitForSeconds(2/GameSpeed);
            }
        }
        yield return null;
    }
}
