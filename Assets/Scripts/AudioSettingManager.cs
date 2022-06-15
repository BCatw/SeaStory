using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingManager : MonoBehaviour
{
    public enum AudioType { audio_master, audio_bgm, audio_sfx };
    AudioType audioType;
    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        AudioSettingHelper.volumeChangeEvent += SetAudioVolume;
    }

    private void Start()
    {
        List<string> types = new List<string>();
        types.Add(AudioType.audio_bgm.ToString());
        types.Add(AudioType.audio_master.ToString());
        types.Add(AudioType.audio_sfx.ToString());

        foreach (string type in types)
        {
            if (PlayerPrefs.HasKey(type))
            {
                audioMixer.SetFloat(type, PlayerPrefs.GetFloat(type));
                Debug.Log("Audio awake setting " + type + " to " + PlayerPrefs.GetFloat(type));
            }
            else if (!PlayerPrefs.HasKey(type)) audioMixer.SetFloat(type, 0);
        }
    }

    //音量大小，是否存值是因為mute不存
    public void SetAudioVolume(AudioType type ,float value, bool isSave)
    {
        string _type = type.ToString();
        audioMixer.SetFloat(_type, value);
        if(isSave) PlayerPrefs.SetFloat(_type, value);
    }
}
