using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

public interface IAgente
{
    void Ligar();
    void Atuar();
    void Desligar();
    List<string> Sentir(int linha, int coluna);
}

