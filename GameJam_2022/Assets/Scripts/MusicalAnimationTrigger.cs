using UnityEngine;
using SonicBloom.Koreo;
using System;

public class MusicalAnimationTrigger : MonoBehaviour
{

    public Animation animCom;

    [EventID]
    public string eventID;

    void Awake()
    {
        Koreographer.Instance.RegisterForEvents(eventID, OnAnimationTrigger);
    }

    private void OnAnimationTrigger(KoreographyEvent koreoEvent)
    {
        animCom.Stop();
        animCom.Play();
    }
}
