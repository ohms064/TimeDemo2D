using UnityEngine;
using System.Collections;
using Assets.Scripts.Managers;

public class Coin : MonoBehaviour {

    //Esto está hardcodeado pero se tendría que tener un arreglo con el número de monedas talvez
    //o cada nivel tener su descripción en un archivo.
    public bool isFirstCoin = true;

    // Use this for initialization
    void Start() {
        if ( isFirstCoin ) {
            if ( !FileManager.instance.currentGameStatusJson.firstLevelCoin ) {
                Destroy( this.gameObject );
            }
        }
        else {
            if ( !FileManager.instance.currentGameStatusJson.secondLevelCoin ) {
                Destroy( this.gameObject );
            }
        }
    }

    void OnTriggerEnter2D( Collider2D col ) {
        if ( isFirstCoin ) {
            FileManager.instance.currentGameStatusJson.firstLevelCoin = false;
        }
        else {
            FileManager.instance.currentGameStatusJson.secondLevelCoin = false;
        }
        FileManager.instance.currentGameStatusJson.coins++;
        Destroy( this.gameObject );
    }
}
