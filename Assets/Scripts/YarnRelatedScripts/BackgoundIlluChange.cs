using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;


public class BackgoundIlluChange : MonoBehaviour
{
    private Dictionary<string, Sprite> Dict = new Dictionary<string, Sprite>();
    public List<BackgroundSprite> list;
    public Image BackgroundImage;
    public Image BlackImage;
    public DialogueRunner DialogueRunner;
    public HeadDisplay HeadDisplay;

    private void Start()
    {
        foreach (var img in list)
        {
            Dict.Add(img.spriteName, img.sprite);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DialogueRunner.StartDialogue("Start");
        }
    }

    [YarnCommand("BackgroundUpdate")]
    public void BackgroundUpdate(string imgName, string nextNode)
    {
        Sprite sprite;
        if (Dict.TryGetValue(imgName, out sprite))
        {
            DialogueRunner.Stop();
            BlackImage.DOFade(1f, 1f).OnComplete(()=> fadeImage(sprite, nextNode));
        }
        else
        {
            Debug.Log("Invalid BackgroundImage name");
        }
    }

    private void fadeImage(Sprite sprite, string nextNode)
    {
        BackgroundImage.sprite = sprite;
        HeadDisplay.FaceReset();
        BlackImage.DOFade(0f, 1f).OnComplete(()=> DialogueRunner.StartDialogue(nextNode));
    }
}
