using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class Interactable : MonoBehaviour
{
    private float amountOfTimeLookingTowardsTheObject;
    public float triggeringTime = 0.5f;
    public VideoPlayer managedVideo;
    private long amountOfFrames;
    public Transform triggeredMemory;

    private void OnEnable() { Setup(); }
    private void FixedUpdate()
    {
        if (managedVideo.frame >= amountOfFrames)
        {
            triggeredMemory.SetParent(null);
            triggeredMemory.gameObject.SetActive(true);
            Destroy(this);
            return;
        }

        if (Hand.currentObjectBeingGrabbed == this.transform)
        {
            if (managedVideo.isPlaying) return;
            amountOfTimeLookingTowardsTheObject += Time.fixedDeltaTime;
            if (amountOfTimeLookingTowardsTheObject < triggeringTime) return;
            managedVideo.Play();
        }
        else
        {
            amountOfTimeLookingTowardsTheObject = 0;
            if (managedVideo.isPlaying) managedVideo.Pause();
            return;
        }
    }

    private void Setup()
    {
        managedVideo.Pause();
        amountOfFrames = (long) managedVideo.frameCount;
    }
}
