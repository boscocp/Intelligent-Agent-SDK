using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapa
{
    void Criar(int linhas, int colunas);
    IMapaObjeto AdicionarObjeto(int linha, int coluna, string tipo);
    List<IMapaObjeto> RecuperarObjetosPor(string tipo);
    List<IMapaObjeto> RecuperarObjetosPor(int linha, int coluna);
    void Destruir();
}
