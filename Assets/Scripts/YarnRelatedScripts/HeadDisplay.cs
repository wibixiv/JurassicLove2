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
    [SerializeField] private int leftImageXPositionPassive;
    [SerializeField] private int leftImageYPosition;
    [SerializeField] private int rightImageXPosition;
    [SerializeField] private int rightImageXPositionPassive;
    [SerializeField] private int rightImageYPosition;
    [SerializeField] private int tweenTime;
    [SerializeField] private Color passiveColor;

    private string lastPerso;
    private string leftPerso;
    private string rightPerso;
    private Ease _ease = Ease.OutCubic;

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
                    if (leftPerso != name)
                    {
                        leftPerso = name;
                        imageLeft.sprite = sprite;
                        imageLeft.SetNativeSize();
                        if (name != lastPerso)
                        {
                            leftImageComing();
                        }
                    }
                    else
                    {
                        if (name != lastPerso)
                        {
                            switchTalker();
                        }
                    }
                }
                else
                {
                    if (rightPerso != name)
                    {
                        rightPerso = name;
                        imageRight.sprite = sprite;
                        imageRight.SetNativeSize();
                        if (name != lastPerso)
                        {
                            rightImageComing();
                        }
                    }
                    else
                    {
                        if (name != lastPerso)
                        {
                            switchTalker();
                        }
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

    private void leftImageComing()
    {
        imageLeft.color = new Color(1, 1, 1, 0);
        imageTransformLeft.localPosition = new Vector3(leftImageXPosition - 200,
            leftImageYPosition, 0);
        imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition,
            leftImageYPosition, 0), tweenTime).SetEase(Ease.OutCubic);
        imageLeft.DOFade(1, tweenTime);
        imageTransformRight.DOLocalMove(new Vector3(rightImageXPositionPassive, rightImageYPosition, 0), tweenTime)
            .SetEase(Ease.OutCubic);
        imageRight.DOColor(passiveColor, tweenTime).SetEase(_ease);
        lastPerso = leftPerso;
    }

    private void rightImageComing()
    {
        imageRight.color = new Color(1, 1, 1, 0);
        imageTransformRight.localPosition = new Vector3(rightImageXPosition + 200,
            rightImageYPosition, 0);
        imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition,
            rightImageYPosition, 0), tweenTime).SetEase(Ease.OutCubic);
        imageRight.DOFade(1, tweenTime);
        imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition, leftImageYPosition, 0), tweenTime)
            .SetEase(_ease);
        imageLeft.DOColor(Color.white, tweenTime).SetEase(_ease);
        lastPerso = rightPerso;
    }

    [YarnCommand("leftCharaLeaving")]
    public void leftCharaLeaving()
    {
        imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition - 200, leftImageYPosition, 0), tweenTime)
            .SetEase(Ease.OutCubic);
        imageLeft.DOFade(0, tweenTime);
        if (lastPerso == leftPerso)
        {
            lastPerso = rightPerso;
        }

        leftPerso = "";
    }
    [YarnCommand("rightCharaLeaving")]
    public void rightCharaLeaving()
    {
        imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition - 200, rightImageYPosition, 0), tweenTime)
            .SetEase(Ease.OutCubic);
        imageRight.DOFade(0, tweenTime);
        if (lastPerso == rightPerso)
        {
            lastPerso = leftPerso;
        }

        rightPerso = "";
    }

    [YarnCommand("switchTalker")]
    public void switchTalker()
    {
        if (lastPerso == leftPerso)
        {
            imageTransformLeft.DOLocalMove(new Vector3(leftImageXPositionPassive, leftImageYPosition, 0), tweenTime)
                .SetEase(Ease.OutCubic);
            imageLeft.DOColor(passiveColor, tweenTime).SetEase(_ease);
            imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition, rightImageYPosition, 0), tweenTime)
                .SetEase(_ease);
            imageRight.DOColor(Color.white, tweenTime).SetEase(_ease);
            lastPerso = rightPerso;
        }
        else if (lastPerso == rightPerso)
        {
            imageTransformRight.DOLocalMove(new Vector3(rightImageXPositionPassive, rightImageYPosition, 0), tweenTime)
                .SetEase(Ease.OutCubic);
            imageRight.DOColor(passiveColor, tweenTime).SetEase(_ease);
            imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition, leftImageYPosition, 0), tweenTime)
                .SetEase(_ease);
            imageLeft.DOColor(Color.white, tweenTime).SetEase(_ease);
            lastPerso = leftPerso;
        }
    }
}
