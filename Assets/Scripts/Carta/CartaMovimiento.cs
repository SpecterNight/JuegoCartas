using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CartaMovimiento : MonoBehaviour{
    private bool mover = false;
    private Vector3 velocity;
    private Vector3 posicionFinal;
    private Vector3 posicionIntermedia = new Vector3(-8.0f,0.0f,0.0f);
    private Vector3 posicionInicial;
    private bool moverIntermedio = false;
    private bool moverInicial = false;
    private bool moverFinal = false;
    private float velocidadMovimiento = 5;
    private Carta carta;
    private GameObject boton;
    private Sprite caraCarta;
    // Start is called before the first frame update
    public Vector3 PosicionFinal{set{posicionFinal = value;}}
    public bool MoverIntermedio{set{moverIntermedio = value;}}
    public bool Mover{set{mover = value;}}
    void Start(){
         posicionIntermedia = new Vector3(-8.0f,0.0f,0.0f);
    }

    // Update is called once per frame
    void Update(){
        if(mover){
            if(moverInicial){
                moverCarta(posicionInicial);
                if(transform.position == posicionInicial){
                    moverInicial = false;
                    mover = false;
                }
            }
            if(moverIntermedio && !moverInicial){
                moverCarta(posicionIntermedia);
                if(transform.position == posicionIntermedia){
                    moverIntermedio = false;
                    moverFinal = true;
                    mostrarCara();
                }
            }
            if(moverFinal){
                moverCarta(posicionFinal);
                if(transform.position == posicionFinal){
                    mover = false;
                    boton.SetActive(true);
                }
            }
        }
    }
    
    public void inicializar(GameObject boton, Sprite caraCarta, Carta carta, Vector3 posI){
        this.boton = boton;
        this.caraCarta = caraCarta;
        this.carta = carta;
        this.posicionInicial = posI;
        this.moverInicial = true;
        this.mover = true;
    }

    public void moverCarta(Vector3 posicionDestino){
        transform.position = Vector3.SmoothDamp(transform.position, posicionDestino,ref velocity,0.1f,velocidadMovimiento-1);
        mover = !(transform.position == posicionFinal);
    }

    private void mostrarCara(){
        GetComponent<SpriteRenderer>().sprite = caraCarta;
    }
}
