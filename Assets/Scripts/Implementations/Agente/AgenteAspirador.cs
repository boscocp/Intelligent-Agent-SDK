using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAspirador : Agente
{
    private string nome = "Nome";//TODO: por na hora de instanciar o agente
    public int maximoPassos = 2;
    private int _score = 0; //PEAS Performance, Envioriment, Atuador e Sensores
    public int Score { get => _score; set => _score = value; }
    private int steps = 0;
    public override void Start()
    {
        base.Start();
        Moving = true;
        nome = titulo.text;
        Destino = Vector3Int.RoundToInt(transform.position + transform.forward);
    }

    public override void Ligar()
    {
        StartCoroutine(LigarRotina());
    }

    IEnumerator LigarRotina()//TODO: tentar tirar do IEnumerator e deixar o processamento fora
    {                        //Dica: tentar fazer um esquema de foreach de lista de string de comando
        
        float random = UnityEngine.Random.Range(0f, 10f);
        while (steps < maximoPassos)
        {
            while(!Input.GetKeyDown(KeyCode.N))
            {
                yield return null;
            }
            titulo.text = nome+ ": " + Score;
            if (random < 6f)
            {

                MoverFrente();        
                yield return new WaitForSeconds(TimeCorroutine);
                if(VerificarSujeira()) RemoveSujeira();
                yield return new WaitForSeconds(TimeCorroutine / 2);
            }
            else if (random < 8f)
            {
                GirarEsquerda();
                yield return new WaitForSeconds(TimeCorroutine / 2);
            }
            else
            {
                GirarDireita();
                yield return new WaitForSeconds(TimeCorroutine / 2);
            }
            random = UnityEngine.Random.Range(0f, 10f);
            steps++;
            yield return null;
        }
        Moving = false;
        Desligar();
        Debug.Log("Score do agente "+Score);
    }

    public IEnumerator Esperar(float tempo)
    {
        yield return new WaitForSeconds(tempo);
    }
    private void GirarDireita()
    {
        AgenteUtil.RotateRigth(this);
        Score -= 1;
    }

    private void GirarEsquerda()
    {
        AgenteUtil.RotateLeft(this);
        Score -= 1;
    }

    private void MoverFrente()
    {
        List<IMapaObjeto> agentesMO = GameManager.Instance.MapaAtual.RecuperarObjetosPor("agente");
        List<Agente> agentes = new List<Agente>();
        
        foreach (MapaObjeto objeto in agentesMO)
        {
            agentes.Add(objeto.GetComponent<Agente>());
        }
        StartCoroutine(AgenteUtil.MoverFrente(this, _obstaculos, agentes));
        //if(!AgenteUtil.VerificarObstaculo(this,_obstaculos,Destino.x,Destino.z)) Score -= 1;
    }
    private void RemoveSujeira()
    {
        int linha = (int)Mathf.Round(transform.position.x);
        int coluna = (int)Mathf.Round(transform.position.z);
        if (GameManager.Instance.MapaAtual.RemoverObjeto(linha, coluna, "sujeira"))
        {
            Score += 10;
        }
    }
    private bool VerificarSujeira()
    {
        //Score -= 1;
        return Sentir((int)transform.position.x, (int)transform.position.z).Contains("sujeira");
    }
}
