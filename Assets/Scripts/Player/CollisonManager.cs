using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonManager : MonoBehaviour
{

       private Renderer[] rdrs;

       private static int clientNum;

       private UnityEngine.Color chaserColor = Color.red;
        private UnityEngine.Color runnerColor = Color.blue;

       private void Awake() {

        // we want half the players to be catchers
        rdrs = gameObject.GetComponentsInChildren<Renderer>();
        if (clientNum % 2 == 0) {
            
            for (int i = 0; i < rdrs.Length; i++)
            {
                rdrs[i].material.color = chaserColor;
            }
        } else {
            for (int i = 0; i < rdrs.Length; i++)
            {
                rdrs[i].material.color = runnerColor;
            }
        }
        clientNum+=1;
        
       }
    
    // Start is called before the first frame update
    void OnCollisionEnter(Collision other) {
        OnCollisionEnterServerRpc(other);
    }

    void OnCollisionEnterServerRpc(Collision other) {
        if (other.gameObject.name == "Player(Clone)") {
            rdrs = gameObject.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rdrs.Length; i++)
            {
                if (rdrs[i].material.color == runnerColor) {
                    rdrs[i].material.color = chaserColor;
                } else {
                    rdrs[i].material.color = runnerColor;
                }
                
            }

        }
    }
    
}