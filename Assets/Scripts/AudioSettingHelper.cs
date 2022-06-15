using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingHelper : MonoBehaviour
{
    [SerializeField] AudioSettingManager.AudioType audioType;
    [SerializeField] Slider slider;
    [SerializeField] Toggle toggle;
    [SerializeField] Text valueText;
    [SerializeField] float muteValue = -80.0f;
    static public System.Action<AudioSettingManager.AudioType, float, bool> volumeChangeEvent;

    private void Awake()
    {
        string _type = audioType.ToString();

        if (PlayerPrefs.HasKey(_type)) slider.value = VolumeToValue(PlayerPrefs.GetFloat(_type));
        else slider.value = 100;

        if (PlayerPrefs.HasKey(_type + "_OnOff"))
        {
            bool isOn = PlayerPrefs.GetInt(_type + "_OnOff") == 1 ? true : false;
            toggle.isOn = isOn;
        }
        else
        {
            toggle.isOn = true;
        }

    }
    
    public void OnVolumeChange(float value)
    {
        valueText.text = value > 100 ? "+" + value + "%": "" + value + "%";
        float _v = ValueToVloume(value);

        volumeChangeEvent(audioType, _v, true);
    }

    public void OnSwithcOnOff(bool value)
    {

        string _type = audioType.ToString();
        
        switch (value)
        {
            case true:
                float v = PlayerPrefs.HasKey(_type) ? PlayerPrefs.GetFloat(_type) : 0;
                volumeChangeEvent(audioType, v, value);
                slider.value = VolumeToValue(v);
                break;

            case false:
                volumeChangeEvent(audioType, muteValue, value);
                break;
        }

        slider.interactable = value;
    }

    public float ValueToVloume(float value)
    {
        float _v = (value - 100) * 4 / 5;
        return _v;
    }

    public float VolumeToValue(float volume)
    {
        float _v = 100 + volume * 5 / 4;
        return _v;
    }
}
