public class Carta{
    private int _numero;
    private string _palo;
    private bool _estaOrdenada;
    public Carta(int numero, string palo){
        Numero = numero;
        Palo = palo;
        _estaOrdenada = false;
    }

    public int Numero {get{return _numero;} set{_numero = value;}}
    public string Palo {get{return _palo;} set{_palo = value;}}
    public bool EstaOrdenada {get{return _estaOrdenada;}set{_estaOrdenada = value;}}
}