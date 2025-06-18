using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
using DG.Tweening;

public class HeadDisplay : MonoBehaviour
{
    private Dictionary<(string, CharacterEmotions.Emotions), Sprite> Dict = new Dictionary<(string, CharacterEmotions.Emotions), Sprite>();
    
    public List<CharacterEmotions> CharEmotions;

    public Image imageLeft;
    public RectTransform imageTransformLeft;
    public Image imageRight;
    public RectTransform imageTransformRight;

    [SerializeField] private int leftImageXPosition;
    [SerializeField] private int leftImageYPosition;
    [SerializeField] private int rightImageXPosition;
    [SerializeField] private int rightImageYPosition;
    [SerializeField] private int tweenTime;

    private string lastPerso;

    private void Start()
    {
        foreach (var CharEmotion in CharEmotions)
        {
            Dict.Add((CharEmotion.name, CharEmotion.emotion), CharEmotion.sprite);
        }
    }

    [YarnCommand("FaceReset")]
    public void FaceReset()
    {
        lastPerso = "";
        imageLeft.enabled = false;
        imageRight.enabled = false;
    }
    
    [YarnCommand("FaceUpdate")]
    public void FaceUpdate(string name, string emotion)
    {
        //image.sprite = Dict[(name, (CharacterEmotions.Emotions)emotion)];
        CharacterEmotions.Emotions currentEmotion;
        if (Enum.TryParse(emotion, out currentEmotion))
        {
            Sprite sprite;
            if (Dict.TryGetValue((name, currentEmotion), out sprite))
            {
                if (name == "MonsiCalbar" || name == "MonsiPantalon")
                {
                    imageRight.enabled = false;
                    imageLeft.enabled = true;
                    imageLeft.sprite = sprite;
                    imageLeft.SetNativeSize();
                    if (name != lastPerso)
                    {
                        imageLeft.color = new Color(1, 1, 1, 0);
                        imageTransformLeft.position = new Vector3(imageTransformLeft.position.x - 200,
                            imageTransformLeft.position.y, 0);
                        imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition,
                            leftImageYPosition, 0), tweenTime).SetEase(Ease.OutCubic);
                        imageLeft.DOFade(1, tweenTime);
                    }
                }
                else
                {
                    imageLeft.enabled = false;
                    imageRight.enabled = true;
                    imageRight.sprite = sprite;
                    imageRight.SetNativeSize();
                    if (name != lastPerso)
                    {
                        imageRight.color = new Color(1, 1, 1, 0);
                        imageTransformRight.position = new Vector3(imageTransformRight.position.x + 200,
                            imageTransformRight.position.y, 0);
                        imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition,
                            rightImageYPosition, 0), tweenTime).SetEase(Ease.OutCubic);
                        imageRight.DOFade(1, tweenTime);
                    }
                }

                lastPerso = name;
            }
            else
            {
                Debug.Log("Invalid Name / Invalid combination of name + emotion");
            }
        }
        else
        {
            Debug.Log("Invalid Emotion");
        }
    }
}
