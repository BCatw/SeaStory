using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TagImage
{
    public string tag;
    public Sprite sprite;
}

[System.Serializable]
[CreateAssetMenu(fileName ="TagImageObj",menuName ="ScriptableObj/TagImageObj")]
public class TagImageObj: ScriptableObject
{
    public Sprite defaultImage;
    [SerializeField] List<TagImage> tagImages;

    public Sprite GetImage(string tag)
    {
        Sprite sprite = defaultImage;

        foreach(TagImage ti in tagImages)
        {
            if (ti.tag == tag) sprite = ti.sprite;
        }

        return sprite;
    }

}
