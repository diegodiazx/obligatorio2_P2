namespace Dominio.Comparadores;

public class CompararPasajePorPrecio:IComparer<Pasaje>
{
    public int Compare(Pasaje? x, Pasaje? y)
    {
        return x.Precio.CompareTo(y.Precio) * -1;
    }
}