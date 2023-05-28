using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    public enum CameraNames
    {
        FirstPerson,
        Hallway1,
        Hallway2,
        RunWay,
        Security
    }

    public Transform Player;
    public List<CinemachineVirtualCamera> Cameras;

    public CinemachineVirtualCamera ActiveCam;

    public void SwitchCam(CameraNames cam)
    {
        var currentCam = Cameras[(int)cam];
        if (cam == CameraNames.Hallway1)
        {
            currentCam.GetComponent<CinemachineDollyCart>().m_Speed = 3;

        }
        currentCam.Priority = 10;
        ActiveCam = currentCam;
        Debug.Log($"Switch to {cam}");
        foreach (var camera in Cameras)
        {
            if(camera != currentCam)
                camera.Priority = 0;
        }
    }

    private void Start()
    {
        InputSystem.EnableDevice(Keyboard.current);
    }

    private void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            SwitchCam(CameraNames.FirstPerson);
        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            SwitchCam(CameraNames.Hallway1);
        }
        if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            SwitchCam(CameraNames.Hallway2);
        }
        if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
        {
            SwitchCam(CameraNames.RunWay);
        }
        if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
        {
            SwitchCam(CameraNames.Security);
        }
        
    }
}
