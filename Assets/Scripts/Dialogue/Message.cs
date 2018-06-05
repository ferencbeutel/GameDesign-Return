using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public string Narrative
    {

        get
        {
            return narrative.Replace("\\n", "\n");
        }
        set
        {
            narrative = value;
        }
    }
    public string narrator;
    [SerializeField]
    private string narrative;
}
