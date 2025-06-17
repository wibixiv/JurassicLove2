using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class HeadDisplay : MonoBehaviour
{
    public Dictionary<(string, CharacterEmotions.Emotions), Sprite> Dict = new Dictionary<(string, CharacterEmotions.Emotions), Sprite>();
    
    public List<CharacterEmotions> CharEmotions;

    public Image imageLeft;
    public Image imageRight;

    private void Start()
    {
        foreach (var CharEmotion in CharEmotions)
        {
            Dict.Add((CharEmotion.name, CharEmotion.emotion), CharEmotion.sprite);
        }
    }
    
    [YarnCommand("FaceUpdate")]
    public void FaceUpdate(string name, string emotion)
    {
        //image.sprite = Dict[(name, (CharacterEmotions.Emotions)emotion)];
        CharacterEmotions.Emotions currentEmotion;
        if (Enum.TryParse(emotion, out currentEmotion))
        {
            if (name == "MonsiCalbar" || name == "MonsiPantalon")
            {
                imageRight.enabled = false;
                imageLeft.enabled = true;
                imageLeft.sprite = Dict[(name, currentEmotion)];
                imageLeft.SetNativeSize();
            }
            else
            {
                imageLeft.enabled = false;
                imageRight.enabled = true;
                imageRight.sprite = Dict[(name, currentEmotion)];
                imageRight.SetNativeSize();
            }
        }
        else
        {
            Debug.Log("Invalid Emotion");
        }
    }
}
