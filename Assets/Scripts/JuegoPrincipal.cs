using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoPrincipal : MonoBehaviour{
    private BarajaController barajaController;
    private PrefabController prefabController;

    public Sprite[] spriteCartas;
    private Carta[,] tablero = new Carta[13,4];

    private Vector3[] posiciones = new Vector3[13];
    // Start is called before the first frame update
    void Start(){
        inicializarJuego();
        obtenerPosiciones();
        repartirCartas();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void inicializarJuego(){
        barajaController = new BarajaController();
        prefabController = new PrefabController();
        prefabController.cartasSprite = spriteCartas;
        barajaController.inicializarBaraja();
        inicializarTablero();
    }

    public void inicializarTablero(){
        int aux = 0;
        Carta[] cartas = barajaController.cartas;
        barajaController.imprimir(cartas);
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
        Vector3 posicionFinal;
        for (int i = 0; i < 13; i++){
            yOffset = 0.0f;
            zOffset = 0.0f;
            for (int j = 0; j < 4; j++){
                posicionFinal = new Vector3(posiciones[i].x,posiciones[i].y-yOffset,posiciones[i].z-zOffset);
                prefabController.mostrarPrefab((i+1),tablero[i,j].Numero,tablero[i,j].Palo);
                GameObject target = GameObject.Find(tablero[i,j].Palo+tablero[i,j].Numero+("(Clone)"));
                target.GetComponent<CartaController>().posicionFinal = posicionFinal;
                target.GetComponent<CartaController>().mover = true;
                yOffset += 0.1f;
                zOffset += 0.03f;
            }
        }
    }
}
