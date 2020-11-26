using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAspirador : Agente
{
    private string nome = "Nome";//TODO: por na hora de instanciar o agente
    public int maximoPassos = 150;
    private int _score = 0; //PEAS Performance, Envioriment, Atuador e Sensores
    public int Score { get => _score; set => _score = value; }
    private int steps = 0;
    public override void Start()
    {
        base.Start();
        Moving = true;
        nome = titulo.text;
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
        StartCoroutine(AgenteUtil.MoverFrente(this, _obstaculos));
        if(!AgenteUtil.VerificarObstaculo(this,_obstaculos,(int)Destino.x,(int)Destino.z)) Score -= 1;
    }
    private void RemoveSujeira()
    {
        if (GameManager.Instance.MapaAtual.RemoverObjeto((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.z), "sujeira"))
        {
            Debug.Log((int)transform.position.x+" "+(int)transform.position.z);
            Score += 10;
        }
    }
    private bool VerificarSujeira()
    {
        //Score -= 1;
        return Sentir((int)transform.position.x, (int)transform.position.z).Contains("sujeira");
    }
}
