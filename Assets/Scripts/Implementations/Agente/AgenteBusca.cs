using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteBusca : Agente
{
    public int[] _irAteh = new int[2] { 0, 0 };
    private IBusca _busca;

    public override void Start()
    {
        base.Start();
        _busca = gameObject.GetComponent<IBusca>();
    }

    public override void Ligar()
    {
        StartCoroutine(LigarRotina());
    }

    IEnumerator LigarRotina()//TODO: tentar tirar do IEnumerator e deixar o processamento fora
    {
        if (AgenteUtil.VerificarObstaculo(this, _irAteh[0], _irAteh[1]))
        {
            Debug.Log("Nao consegue, pois parede ou sem caminho ");
        }
        else
        {
            List<int[]> caminho = new List<int[]>();
            caminho = _busca.Executar(_irAteh);
            
            foreach (int[] passo in caminho)
            {
                Debug.Log(passo[0]+" "+passo[1]);
                Destino = new Vector3Int(passo[0], 0, passo[1]);
                transform.LookAt(new Vector3(Destino.x,transform.position.y,Destino.z));
                MoverFrente();
                yield return new WaitForSeconds(TimeCorroutine);
            }
        }

        yield return null;
    }
    private void MoverFrente()
    {
        List<IMapaObjeto> agentesMO = GameManager.Instance.MapaAtual.RecuperarObjetosPor("agente");
        List<Agente> agentes = new List<Agente>();
        
        foreach (MapaObjeto objeto in agentesMO)
        {
            agentes.Add(objeto.GetComponent<Agente>());
        }
        StartCoroutine(AgenteUtil.MoverFrente(this, agentes));
        //if(!AgenteUtil.VerificarObstaculo(this,_obstaculos,Destino.x,Destino.z)) Score -= 1;
    }
}
