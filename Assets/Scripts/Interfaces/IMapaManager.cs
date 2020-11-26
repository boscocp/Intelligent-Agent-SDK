using System.Collections;
using System.Collections.Generic;

public interface IMapaManager 
{
    IMapa CriarMapa(int linhas, int colunas);
    IMapa RecuperarMapa();
    void DestruirMapa();
}