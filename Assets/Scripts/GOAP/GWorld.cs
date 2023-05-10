using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    static readonly GWorld instance = new GWorld();
    static WorldStates world;

    static GWorld() {
        world = new WorldStates();
    }

    private GWorld() {

    }

    public static GWorld Instance {
        get => instance;
    }

    public WorldStates GetWorld() {
        return world;
    }
}
