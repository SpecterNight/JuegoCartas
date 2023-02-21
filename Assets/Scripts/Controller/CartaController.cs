using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaController : MonoBehaviour{
    public bool mover = false;
    Vector3 velocity;
    public Vector3 posicionFinal;
    public Vector3 posicionIntermedia = new Vector3(-8.0f,0.0f,0.0f);
    public Vector3 posicionInicial;
    public bool estaOrdenada = false;
    public bool moverIntermedio = false;
    public bool moverInicial = false;
    public bool moverFinal = false;
    public float velocidadMovimiento = 10;
    public Carta carta;
    // Start is called before the first frame update
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
                }
            }
            if(moverFinal){
                moverCarta(posicionFinal);
                if(transform.position == posicionFinal){
                    mover = false;
                }
            }
        }
    }
    
    public void moverCarta(Vector3 posicionDestino){
        transform.position = Vector3.SmoothDamp(transform.position, posicionDestino,ref velocity,0.5f,velocidadMovimiento);
        mover = !(transform.position == posicionFinal);
    }
}
