using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    void Awake()
    {
        instance = this;
    }

    public void ProcessEffect(string effect, int points)
    {
        // TODO boost effect by amount of points.
        Debug.Log($"Boosted {effect} by {points} points!");
    }
}
