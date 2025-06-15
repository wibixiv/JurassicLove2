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

    public Image image;

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
            image.sprite = Dict[(name, currentEmotion)];
            image.SetNativeSize();
        }
        else
        {
            Debug.Log("Invalid Emotion");
        }
    }
}
