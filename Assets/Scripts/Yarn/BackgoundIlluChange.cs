using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;


public class BackgoundIlluChange : MonoBehaviour
{
    private Dictionary<string, Sprite> Dict = new Dictionary<string, Sprite>();
    public List<BackgroundSprite> list;
    public Image BackgroundImage;

    private void Start()
    {
        foreach (var img in list)
        {
            Dict.Add(img.spriteName, img.sprite);
        }
    }

    [YarnCommand("BackgroundUpdate")]
    public void BackgroundUpdate(string imgName)
    {
        Sprite sprite;
        if (Dict.TryGetValue(imgName, out sprite))
        {
            BackgroundImage.sprite = sprite;
        }
        else
        {
            Debug.Log("Invalid BackgroundImage name");
        }
    }
}
