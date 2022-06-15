using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndingHelper : MonoBehaviour
{

    [SerializeField] Image endImage;
    [SerializeField] Text text;
    [Tooltip("{0,1,2,3} = {BE, Tuo, Nie, TE}")]
    [SerializeField] Sprite[] endSprite = new Sprite[4];
    [SerializeField] float time = 2;
    [SerializeField] UnityEvent OnEnd;

    public bool isInteractable { get; set; }

    static public System.EventHandler<SceneArgs> SceneStateChanged;

    void Awake()
    {
        ActManager.EndingEvent += SetPic;
    }

    void OnDestroy()
    {
        ActManager.EndingEvent -= SetPic;
    }

    void Update()
    {
        if(isInteractable && Input.anyKeyDown)
        {
            OnContinue();
        }
    }

    void SetPic(string value)
    {
        switch (value)
        {
            case "BadEnd":
                endImage.sprite = endSprite[0];
                break;

            case "TuoEnd":
                endImage.sprite = endSprite[1];
                PlayerPrefs.SetInt("isTuo", 1);
                break;

            case "NieEnd":
                endImage.sprite = endSprite[2];
                PlayerPrefs.SetInt("isNie", 1);
                break;

            case "TrueEnd":
                endImage.sprite = endSprite[3];
                break;
        }
        OnEnd.Invoke();
    }

    public void OnContinue()
    {
        isInteractable = false;
        SceneArgs sceneArgs = new SceneArgs();
        sceneArgs.stateNumber = 0;
        SceneStateChanged(this, sceneArgs);
    }
}
