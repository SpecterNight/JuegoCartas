using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JuegoPrincipal : MonoBehaviour{
    private BarajaController barajaController;
    private PrefabController prefabController;

    public Sprite[] spriteCartas;
    private Carta[,] tablero = new Carta[13,4];

    private Vector3[] posiciones = new Vector3[13];
    
    private Carta cartaSeleccionada;
    public GameObject botonOrdenar;
    public GameObject botonBarajarR;
    public GameObject botonBarajarP;
    public GameObject botonJugar;
    public GameObject panel; 
    public GameObject mensajeFin;
    public TMP_Text mensajePartida;
    // Start is called before the first frame update
    void Start(){
        inicializarJuego();
        panel.SetActive(true);
        obtenerPosiciones();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void inicializarJuego(){
        barajaController = new BarajaController();
        prefabController = new PrefabController();
        prefabController.destruirClones();
        prefabController.cartasSprite = spriteCartas;
        mensajeFin.SetActive(false);
        panel.SetActive(false);
        activarBarajar(true);
        botonJugar.SetActive(false);
        botonOrdenar.SetActive(false);
        barajaController.inicializarBaraja();
        //inicializarTablero();
    }

    public void inicializarTablero(){
        int aux = 0;
        Carta[] cartas = barajaController.cartas;
        //barajaController.imprimir(cartas);
        for (int i = 0; i < 13; i++){
            for (int j = 0; j < 4; j++){
                tablero[i,j] = cartas[aux+j];
            }
            aux += 4;
        }
    }

    public void obtenerPosiciones (){
        GameObject lugar;
        for (int i = 0; i < posiciones.Length; i++){
            lugar = GameObject.Find("Deck"+(i+1));
            posiciones[i] = lugar.GetComponent<Transform>().position;
        }
    }

    public void repartirCartas(){
        float yOffset = 0.0f;
        float zOffset = 0.0f;
        Vector3 posicionInicial;
        for (int i = 0; i < 13; i++){
            yOffset = 0.0f;
            zOffset = 0.0f;
            for (int j = 0; j < 4; j++){
                posicionInicial = new Vector3(posiciones[i].x,posiciones[i].y+yOffset,posiciones[i].z+zOffset);
                prefabController.mostrarPrefab((i+1),tablero[i,j].Numero,tablero[i,j].Palo);
                GameObject target = GameObject.Find(tablero[i,j].Palo+tablero[i,j].Numero+("(Clone)"));
                target.GetComponent<CartaController>().boton = botonOrdenar;
                target.GetComponent<CartaController>().caraCarta = spriteCartas[System.Array.FindIndex(spriteCartas, s => s.name.Equals(tablero[i,j].Palo+"_"+tablero[i,j].Numero))];
                target.GetComponent<CartaController>().carta = tablero[i,j];
                target.GetComponent<CartaController>().posicionInicial = posicionInicial;
                target.GetComponent<CartaController>().moverInicial = true;
                target.GetComponent<CartaController>().mover = true;
                yOffset += 0.1f;
                zOffset += 0.03f;
            }
        }
        cartaSeleccionada = tablero[12,0];
        for (int i = 0; i < 3; i++){
            tablero[12, i] = tablero[12, (i+1)];
        }
    }

    public void ordenarCarta(){
        bool esGanador = true;
        botonOrdenar.SetActive(false);
        if(!cartaSeleccionada.EstaOrdenada){
            Carta cartaAux = tablero[cartaSeleccionada.Numero-1,0];
            for (int i = 0; i < 3; i++){
                tablero[(cartaSeleccionada.Numero)-1, i] = tablero[(cartaSeleccionada.Numero)-1, (i+1)];
            }
            GameObject objetoCarta = GameObject.Find(cartaSeleccionada.Palo+cartaSeleccionada.Numero+("(Clone)"));
            objetoCarta.GetComponent<CartaController>().posicionFinal = posiciones[cartaSeleccionada.Numero-1];
            objetoCarta.GetComponent<CartaController>().moverIntermedio = true;
            objetoCarta.GetComponent<CartaController>().mover = true;
            cartaSeleccionada.EstaOrdenada = true;
            tablero[cartaSeleccionada.Numero-1, 3] = cartaSeleccionada;
            cartaSeleccionada = cartaAux;
        }else{
            for (int i = 0; i < 13; i++){
                for (int j = 0; j < 4; j++){
                    if((i+1)!=tablero[i,j].Numero){
                        esGanador = false;
                        break;
                    }
                }
            }
            activarFin(esGanador);
        }
    }

    public void barajarRapido(){
        barajaController.barajar_Rapido();
        activarBarajar(false);
        botonJugar.SetActive(true);

    }

    public void barajarPila(){
        barajaController.barajar_Pila();
        activarBarajar(false);
        botonJugar.SetActive(true);
    }

    public void comenzarJuego(){
        panel.SetActive(false);
        inicializarTablero();
        botonJugar.SetActive(false);
        repartirCartas();
        botonOrdenar.SetActive(true);
    }

    private void activarBarajar(bool activar){
        botonBarajarP.SetActive(activar);
        botonBarajarR.SetActive(activar);
    }
    private void activarFin(bool esGanador){
        if(esGanador){
            mensajePartida.text = "Ganador";
            mensajePartida.color = Color.green;
        }else{
            mensajePartida.text = "Perdedor";
            mensajePartida.color = Color.red;
        }
        mensajeFin.SetActive(true);
    }

    public void salirJuego(){
        Application.Quit();
    } 
}