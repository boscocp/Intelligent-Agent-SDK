using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Colecao", menuName = "ScriptableObject/Coleção de Objetos", order = 1)]
public class ColecaoObjetosScriptableObject : ScriptableObject
{
    // Start is called before the first frame update
    public Vector2Int[] posicoes;     
    public string tipo;
}
