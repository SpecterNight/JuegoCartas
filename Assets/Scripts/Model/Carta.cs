public class Carta{
    private int _numero;
    private string _palo;
    public Carta(int numero, string palo){
        Numero = numero;
        Palo = palo;
    }

    public int Numero {get{return _numero;} set{_numero = value;}}
    public string Palo {get{return _palo;} set{_palo = value;}}
}