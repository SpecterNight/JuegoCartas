using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JuegoPrincipal : MonoBehaviour{
    private BarajaController barajaController;
    private PrefabController prefabController;

    [SerializeField] private Sprite[] spriteCartas;
    private Carta[,] tablero = new Carta[13,4];

    private Vector3[] posiciones = new Vector3[13];
    
    private Carta cartaSeleccionada;
    [SerializeField] private GameObject botonOrdenar;
    [SerializeField] private GameObject botonBarajarR;
    [SerializeField] private GameObject botonBarajarP;
    [SerializeField] private GameObject botonJugar;
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject mensajeFin;
    [SerializeField] private TMP_Text mensajePartida;

    private GameObject animacion;
    // Start is called before the first frame update
    void Start(){
        inicializarJuego();
        panel.SetActive(true);
        obtenerPosiciones();
        animacion = GameObject.Find("animBarajar");
        animacion.SetActive(false);
        animacion.GetComponent<Transform>().position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void inicializarJuego(){
        barajaController = new BarajaController();
        prefabController = new PrefabController();
        prefabController.destruirClones();
        prefabController.CartasSprite = spriteCartas;
        mensajeFin.SetActive(false);
        panel.SetActive(false);
        activarBarajar(true);
        botonJugar.SetActive(false);
        botonOrdenar.SetActive(false);
        barajaController.inicializarBaraja();
    }

    public void inicializarTablero(){
        int aux = 0;
        Carta[] cartas = barajaController.cartas;
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
                Sprite caraCarta = spriteCartas[System.Array.FindIndex(spriteCartas, s => s.name.Equals(tablero[i,j].Palo+"_"+tablero[i,j].Numero))];
                target.GetComponent<CartaMovimiento>().inicializar(botonOrdenar, caraCarta, tablero[i,j],posicionInicial);
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
            Vector3 posFinal = posiciones[cartaSeleccionada.Numero-1];
            posFinal.z = posFinal.z +(0.03f*4);
            posFinal.y = posFinal.y +(0.01f*4);
            objetoCarta.GetComponent<CartaMovimiento>().PosicionFinal = posFinal;
            objetoCarta.GetComponent<CartaMovimiento>().MoverIntermedio = true;
            objetoCarta.GetComponent<CartaMovimiento>().Mover = true;
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
        panel.SetActive(false);
        barajaController.barajar_Rapido();
        activarBarajar(false);
        botonJugar.SetActive(true);
        animacion.SetActive(true);
    }

    public void barajarPila(){
        panel.SetActive(false);
        barajaController.barajar_Pila();
        activarBarajar(false);
        botonJugar.SetActive(true);
        animacion.SetActive(true);
    }

    public void comenzarJuego(){
        inicializarTablero();
        botonJugar.SetActive(false);
        repartirCartas();
        botonOrdenar.SetActive(true);
        animacion.SetActive(false);
    }

    private void activarBarajar(bool activar){
        botonBarajarP.SetActive(activar);
        botonBarajarR.SetActive(activar);
    }
    private void activarFin(bool esGanador){
        if(esGanador){
            mensajePartida.text = "Victoria";
            mensajePartida.color = Color.yellow;
        }else{
            mensajePartida.text = "Inténtalo de nuevo";
            mensajePartida.color = Color.red;
        }
        mensajeFin.SetActive(true);
    }

    public void salirJuego(){
        Application.Quit();
    } 
}