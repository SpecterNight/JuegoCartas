using UnityEngine;
using System;

public class BarajaController{

    public Carta[] cartas = new Carta[52]; 
    
    public void inicializarBaraja(){
        cartas = new Carta[52];
        int auxPalo = 0;
        int numCarta = 0;
        for (int i = 0; i < cartas.Length; i++){
            numCarta += 1;
            cartas[i] = new Carta(numCarta, Enum.GetName(typeof(Palo),auxPalo));
            if((numCarta)%13 == 0){
                auxPalo += 1;
                numCarta = 0;
            }
        }
        randomizarBaraja();
    }

    //Implementación del algoritmo de Fisher-Yates
    private void randomizarBaraja(){
        System.Random random = new System.Random();
        for (int i = 0; i < cartas.Length; i++){
            int r = random.Next(i, cartas.Length);
            (cartas[r], cartas[i]) = (cartas[i], cartas[r]);
        }
    }

    public void barajar_Rapido(){
        Carta[] barajaP1 = new Carta[26];
        Carta[] barajaP2 = new Carta[26];
        for (int i = 0; i < cartas.Length; i++){
            if (i<(cartas.Length/2)){
                barajaP1[i] = cartas[i];
            }else{
                barajaP2[(cartas.Length-1-i)] = cartas[i];
            }
        }
        int aux = 0;
        for (int i = 0; i < cartas.Length; i+=2){
            cartas[i] = barajaP1[aux];
            cartas[i+1] = barajaP2[aux];
            aux++;
        }
        UnityEngine.Debug.Log("barajeado Rapido");
    }

    public void barajar_Pila(){
        System.Random random = new System.Random();
        int numCambios = random.Next(5,10);
        int numPila = 0;
        Carta[] pila;
        for (int i = 0; i < numCambios; i++){
            numPila = random.Next(4,7);
            pila = new Carta[numPila];
            for (int j = 0; j < numPila; j++){
                Array.Copy(cartas,cartas.Length-numPila,pila,0,numPila);
                Array.Copy(cartas,0,cartas,numPila,cartas.Length-numPila);
                Array.Copy(pila,0,cartas,0,numPila);
            }
        }
        UnityEngine.Debug.Log("barajeado por pila");
    }

    public void imprimir(Carta[] elementos){
        String resultado = "";
        foreach (Carta item in elementos){
            resultado += item.Numero.ToString()+" | ";
        }
        Debug.Log(resultado);
    }
}
