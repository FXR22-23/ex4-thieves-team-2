using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Utils;
using Debug = UnityEngine.Debug;

public class AudioManager : Singleton<AudioManager>
{
    public FMODUnity.EventReference[] eventReferences;
    private List<FMOD.Studio.EventInstance> _eventInstances;

    public enum Sounds
    {
        // ADD SOUNDS NAME HERE IN THE SAME ORDER THAT YOU PUT IN THE eventReferences 
        // Add name of events here such as :
        // FALL
        // TELEPORT etc etc
        
    }

    protected override void Awake()
    {
        base.Awake();
        _eventInstances = new List<EventInstance>();
        foreach (var fmodEvent in eventReferences)
        {
            EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent.Guid);
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            _eventInstances.Add(instance);
        }
    }
    /// <summary>
    /// Playing sound from the player
    /// </summary>
    /// <param name="sound"></param>
    /// <returns></returns>
    public EventInstance PlaySound(Sounds sound)
    {
        _eventInstances[(int)sound].start();
        _eventInstances[(int)sound].set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        return _eventInstances[(int)sound];
    }


    public void StopSound(Sounds sound)
    {
        _eventInstances[(int)sound].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        // _eventInstances[(int)sound].release(); // releasing will kill the event
    }
    /// <summary>
    /// Playing sound from the player for one burst
    /// </summary>
    /// <param name="sound"></param>
    public void PlayOneShot(Sounds sound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventReferences[(int)sound]);
    }
    /// <summary>
    /// Playing sound from the game object
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="attach"></param>
    public EventInstance PlayOneShotAttach(Sounds sound, GameObject attach)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventReferences[(int)sound], attach);
        return _eventInstances[(int)sound];
    }

    public bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    public IEnumerator WaitForSoundToFinish(EventInstance instance)
    {
        while (AudioManager.Instance.IsPlaying(instance))
        {
            yield return null;
        }
    }

    public EventInstance GetSoundEventInstance(Sounds sound)
    {
        return _eventInstances[(int)sound];
    }

    /// <summary>
    /// Use this when an object has an event Emitter attached to it.
    /// </summary>
    /// <param name="obj"></param>
    public void PlayFmodEventEmitter(GameObject obj)
    {
        StudioEventEmitter eventEmitter = obj.GetComponent<StudioEventEmitter>();
        if (!eventEmitter)
        {
            Debug.Log($"Event Emitter is not found in game object {obj.name}");
            return;
        }

        eventEmitter.Play();
    }

    public void SetParameter(string parameterName, float value, Sounds sound)
    {
        _eventInstances[(int)sound].setParameterByName(parameterName, value);
    }

    public float GetParameter(string parameterName, Sounds sound)
    {
        float value;
        _eventInstances[(int)sound].getParameterByName(parameterName, out value);
        return value;
    }
}