using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mapa", menuName = "ScriptableObject/Mapa", order = 1)]
public class MapaScriptableObject : ScriptableObject
{
    // Start is called before the first frame update
    public int linhas;
    public int colunas;
    public List<ColecaoObjetosScriptableObject> objetos;

}
