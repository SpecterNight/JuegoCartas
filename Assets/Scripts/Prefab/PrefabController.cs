using UnityEngine;

public class PrefabController{
    private Vector3 posicionInicial = new Vector3(0,-10,0);
    private GameObject cartaPrefab = Resources.Load<GameObject>("cartaPrefab");
    private Sprite[] cartasSprite;

    public Sprite[] CartasSprite {set{cartasSprite = value;}}
    
    public void mostrarPrefab(int deck, int numeroCarta, string palo){
        GameObject prefab = cartaPrefab;
        GameObject ubicacion = GameObject.Find("Deck"+deck);
        prefab.GetComponent<SpriteRenderer>().sprite = cartasSprite[System.Array.FindIndex(cartasSprite, s => s.name.Equals("backBlack_1"))];
        prefab.GetComponent<Transform>().localScale = new Vector3(1.244947f, 1.494595f,1);
        prefab.name = palo+numeroCarta;
        Quaternion rotacion = new Quaternion();
        UnityEngine.Object.Instantiate(prefab, posicionInicial, rotacion,ubicacion.transform);
    }
    
    public void destruirClones(){
        var clones = GameObject.FindGameObjectsWithTag("clon");
        foreach (var clon in clones){
            UnityEngine.Object.Destroy(clon);
        }
    }
}
