using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour
{

    public StudioEventEmitter sem;

    public GameObject winMessage;
    
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name == "Plane_128") {
            sem.Play();
            winMessage.SetActive(true);
        }
    }
}
