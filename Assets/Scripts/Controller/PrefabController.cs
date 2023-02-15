using UnityEngine;

public class PrefabController{
    private GameObject cartaPrefab = Resources.Load<GameObject>("cartaPrefab");
    public Sprite[] cartasSprite;
    public void mostrarPrefab(Vector3 posicion, int numeroCarta, string palo){
        GameObject prefab = cartaPrefab;
        prefab.GetComponent<SpriteRenderer>().sprite = cartasSprite[System.Array.FindIndex(cartasSprite, s => s.name.Equals(palo+"_"+numeroCarta))];
        prefab.GetComponent<Transform>().localScale = new Vector3(1.244947f, 1.494595f,1);
        Quaternion rotacion = new Quaternion();
        UnityEngine.Object.Instantiate(prefab, posicion, rotacion);
    }
    
    public void destruirClones(){
        var clones = GameObject.FindGameObjectsWithTag("clon");
        foreach (var clon in clones){
            UnityEngine.Object.Destroy(clon);
        }
    }
}
