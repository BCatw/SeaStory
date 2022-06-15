using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BGMHelper : MonoBehaviour
{
    [SerializeField] AudioSource audioA;
    [SerializeField] AudioSource audioB;
    public AudioSource audioNow;
    [SerializeField] AudioSource audioLast;
    [SerializeField] bool isSideA = true;

    void Awake()
    {
        isSideA = true;
        audioNow = audioA;
        audioLast = audioB;
    }

    public void OnChangeaudio(AudioClip audio)
    {
        if (audio == audioNow.clip) return;
        SideChange();
        audioNow.clip = audio;
        audioNow.Play();
        audioNow.DOFade(1, StageController.actionTime);
        audioLast.DOFade(0, StageController.actionTime);
    }

    public void OnAudioShowUp(AudioClip audio)
    {
        audioNow.clip = audio;
        audioNow.DOFade(1, StageController.actionTime);
    }

    public void OnAudioShowOff()
    {
        audioNow.DOFade(0, StageController.actionTime);
    }

    void FadeOutDone()
    {
        audioLast.Stop();
    }

    void SideChange()
    {
        switch (isSideA)
        {
            case true:
                audioNow = audioB;
                audioLast = audioA;
                break;

            case false:
                audioNow = audioA;
                audioLast = audioB;
                break;
        }

        isSideA = !isSideA;
    }
}
