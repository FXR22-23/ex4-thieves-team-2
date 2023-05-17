using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerSoundManager : MonoBehaviour
{

    public StudioEventEmitter sem;

    public GameObject loseMessage;

    public FMOD.Studio.EventInstance chaseMusic;


    void Start() {
        chaseMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Chase_BGM");
        chaseMusic.start();
    }

    public void SetChaseParams(bool enemySawPlayer, bool enemyHeardPlayer) {
        if (enemyHeardPlayer) {
            chaseMusic.setParameterByName("HeardByGuard", 1f);
        } else {
            chaseMusic.setParameterByName("HeardByGuard", 0f);
        }

        if (enemySawPlayer) {
            chaseMusic.setParameterByName("SeenByGuard", 1f);
        } else {
            chaseMusic.setParameterByName("SeenByGuard", 0f);
        }
    }


    public void TouchDiamond() {
        chaseMusic.setParameterByName("TouchesTheDaimond", 1f); // Daimond vs Diamond - typo was in the fmod files originally so dont change
    }
    
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name == "Enemy") {
            Debug.Log("colided Enemty!");
            sem.Play();
            loseMessage.SetActive(true);
        } else if (collision.gameObject.name == "Platonic") {
            TouchDiamond();
        }
    }

    
    
}
