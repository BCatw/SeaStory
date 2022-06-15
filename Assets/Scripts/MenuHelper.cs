using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    [SerializeField] bool isShowing;
    [Space]
    [SerializeField] GameObject menuBlocker;
    [SerializeField] AudioSource SFXAudioSource;
    [SerializeField] AudioClip showMenuAudioClip;
    [SerializeField] AudioClip[] switchToggleAudioClips;
    [SerializeField] Dictionary<string, GameObject> ContentList; 

    void Awake()
    {
        GameplayHelper.ShowMenuEvent += OnShowMeun;
    }

    private void OnDestroy()
    {
        GameplayHelper.ShowMenuEvent -= OnShowMeun;
    }

    public void OnShowMenu(bool isShow)
    {
        SFXAudioSource.PlayOneShot(showMenuAudioClip);
        menuBlocker.SetActive(isShow);
        isShowing = isShow;
        GameplayHelper.NowUIState = isShow? GameplayHelper.UIState.Menu : GameplayHelper.UIState.None;
    }

    public void OnShowMeun()
    {
        OnShowMenu(!isShowing);
    }

    public void ToggleRandomSFX(bool isOn)
    {
        int index = Random.Range(0, switchToggleAudioClips.Length - 1);
        if (isOn) SFXAudioSource.PlayOneShot(switchToggleAudioClips[index]);
    }
}