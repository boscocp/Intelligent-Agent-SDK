using System.Collections;
using System.Collections.Generic;

public class BuscaUtil_Old
{
    // Start is called before the first frame update
    public static List<int> MontaCaminho(int tileInicial, int tileFinal, Dictionary<int, int> Pais)
    {
        List<int> listaAuxiliar = new List<int>();
        int tileAux = tileFinal;
        
        while (tileAux!=tileInicial)
        {
            listaAuxiliar.Add(tileAux);
            tileAux = Pais[tileAux];
            
        }
        listaAuxiliar.Reverse();
        return listaAuxiliar;
    }
}
