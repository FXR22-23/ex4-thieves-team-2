using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerSoundManager : MonoBehaviour
{

    public StudioEventEmitter sem;

    public StudioEventEmitter footstepSem;

    public GameObject loseMessage;

    public FMOD.Studio.EventInstance chaseMusic;

    public Rigidbody rb;

    public float nextUpdate = 1;

    private bool hasLost;


    public Vector3 oldPos;

    public FMODUnity.EventReference fmodEvent;


    void Start() {
        chaseMusic = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        chaseMusic.setVolume(1);
        chaseMusic.start();
        //footstepSem.Play();

        oldPos = transform.position;

        rb = GetComponent<Rigidbody>();
    }

    public void SetHeardPlayer(bool enemyHeardPlayer)
    {
        if (enemyHeardPlayer)
        {
            chaseMusic.setParameterByName("HeardByGuard", 1f);
        }
        
    }

    public void SetSawPlayer(bool enemySawPlayer)
    {
        if (enemySawPlayer)
        {
            chaseMusic.setParameterByName("SeenByGuard", 1f);
        }
        
    }

    


    public void TouchDiamond() {
        chaseMusic.setParameterByName("TouchesTheDaimond", 1f); // Daimond vs Diamond - typo was in the fmod files originally so dont change
    }
    
    void OnCollisionEnter(Collision collision) {
        if(!hasLost && collision.gameObject.name == "Enemy") {
            hasLost = true;
            sem.Play();
            loseMessage.SetActive(true);
        } else if (collision.gameObject.name == "Platonic") {
            TouchDiamond();
        }
    }


    void Update() {

         if(Time.time>=nextUpdate){
             nextUpdate=Mathf.FloorToInt(Time.time)+2;
             if (rb.position != oldPos) {
                footstepSem.Play();
            }
         }

         oldPos = rb.position;

        
        
    }

    
    
}
