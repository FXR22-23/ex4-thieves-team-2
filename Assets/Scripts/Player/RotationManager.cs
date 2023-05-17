using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationManager : MonoBehaviour
{

    public float rotationSpeed = 720;

    public Transform refTrans;
   
    void Update()
    {
        
        if (transform.rotation != refTrans.rotation) {
            transform.rotation = refTrans.rotation;
        }
        

       
    }
} 
