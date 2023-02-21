using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaController : MonoBehaviour{
    public bool mover = false;
    private float tiempoTranscurrido = 0f;
    private float duracionMovimiento = 1f;
    Vector3 velocity;
    public Vector3 posicionFinal;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(mover){
            /*tiempoTranscurrido += Time.deltaTime;
            float porcentaje = tiempoTranscurrido/duracionMovimiento;
            transform.position = Vector3.Lerp(transform.position, posicionFinal, porcentaje);
            */
            moverCarta();
        }
    }
    
    public void moverCarta(){
        if(mover){
            transform.position = Vector3.SmoothDamp(transform.position, posicionFinal,ref velocity,0.5f,10);
            mover = !(transform.position == posicionFinal);
        }
    }
}
