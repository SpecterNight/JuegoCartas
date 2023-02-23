using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CartaController : MonoBehaviour{
    public bool mover = false;
    Vector3 velocity;
    public Vector3 posicionFinal;
    public Vector3 posicionIntermedia = new Vector3(-8.0f,0.0f,0.0f);
    public Vector3 posicionInicial;
    public bool moverIntermedio = false;
    public bool moverInicial = false;
    public bool moverFinal = false;
    public float velocidadMovimiento = 5;
    public Carta carta;
    public GameObject boton;
    public Sprite caraCarta;
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
    
    public void moverCarta(Vector3 posicionDestino){
        transform.position = Vector3.SmoothDamp(transform.position, posicionDestino,ref velocity,0.1f,velocidadMovimiento-1);
        mover = !(transform.position == posicionFinal);
    }

    private void mostrarCara(){
       // GetComponent<SpriteRenderer>().sprite = cartasSprite[System.Array.FindIndex(cartasSprite, s => s.name.Equals(palo+"_"+numeroCarta))];
        GetComponent<SpriteRenderer>().sprite = caraCarta;
    }
}
