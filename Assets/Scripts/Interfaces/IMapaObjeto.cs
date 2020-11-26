public interface IMapaObjeto
{
    int RecuperarLinha();
    int RecuperarColuna();
    string RecuperarTipo();
    IMapaObjeto Atualizar(int linha, int coluna, string tipo);
}