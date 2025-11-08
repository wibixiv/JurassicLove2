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
    [SerializeField] private List<String> mainCharacterNames = new List<String>()
    {
        "MonsiCalbar",
        "MonsiPantalon"
    };

    private CharacterReferences lastCharacter;
    private string lastCharacterName;
    private string lastEmotion;
    private const Ease AppearEase = Ease.OutCubic;

    private CharacterReferences leftCharacter;
    private CharacterReferences rightCharacter;

    private class CharacterReferences
    {
        public readonly Transform Transform;
        public readonly Image Image;
        public readonly float XPosition;
        public readonly float XAppearOffset;
        public readonly float XPassivePosition;
        public CharacterReferences Opposite;

        public CharacterReferences(Transform transform, Image image, float xPosition, float xAppearPosition, float xPassivePosition)
        {
            Transform = transform;
            Image = image;
            XPosition = xPosition;
            XAppearOffset = xAppearPosition;
            XPassivePosition = xPassivePosition;
        }
    }

    private void Start()
    {
        foreach (CharacterEmotions charEmotion in CharEmotions)
        {
            Dict.Add((charEmotion.name, charEmotion.emotion), charEmotion.sprite);
        }

        imageTransformLeft.position = new Vector3(leftImageXPosition, leftImageYPosition, 0);
        imageTransformRight.position = new Vector3(rightImageXPosition, rightImageYPosition, 0);

        leftCharacter = new CharacterReferences(imageTransformLeft, imageLeft, leftImageXPosition, -200, leftImageXPositionPassive);
        rightCharacter = new CharacterReferences(imageTransformRight, imageRight, rightImageXPosition, 200, rightImageXPositionPassive);
        leftCharacter.Opposite = rightCharacter;
        rightCharacter.Opposite = leftCharacter;
    }

    [YarnCommand("FaceReset")]
    public void FaceReset()
    {
        lastCharacterName = "";
    }
    
    [YarnCommand("FaceUpdate")]
    public void FaceUpdate(string characterName, string emotionName)
    {
        if (Enum.TryParse(emotionName, out CharacterEmotions.Emotions emotion))
        {
            if (Dict.TryGetValue((characterName, emotion), out Sprite sprite))
            {
                // Main character is on the left, other characters are on the right.
                if (mainCharacterNames.Contains(characterName))
                {
                    UpdateCharacter(leftCharacter, sprite, characterName);
                }
                else
                {
                    UpdateCharacter(rightCharacter, sprite, characterName);
                }
                
                lastCharacterName = characterName;
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

    private void UpdateCharacter(CharacterReferences character, Sprite sprite, string newName)
    {
        UpdateEmotion(character.Image, sprite);
        
        if (lastCharacterName != newName)
        {
            CharacterAppears(character);
            CharacterDisappears(character.Opposite);
        }

        lastCharacter = character;
    }
    
    private void UpdateEmotion(Image image, Sprite sprite)
    {
        image.sprite = sprite;
        image.SetNativeSize();
    }

    private void CharacterAppears(CharacterReferences character)
    {
        character.Image.color = new Color(1, 1, 1, 0);
        character.Transform.localPosition = new Vector3(character.XPosition + character.XAppearOffset,
            leftImageYPosition, 0);
        character.Transform.DOLocalMove(new Vector3(character.XPosition,
            leftImageYPosition, 0), tweenTime).SetEase(AppearEase);
        character.Image.DOFade(1, tweenTime);
    }

    private void CharacterDisappears(CharacterReferences character)
    {
        Debug.Log("aafwsfas");
        character.Transform.DOLocalMove(new Vector3(character.XPosition + character.XAppearOffset, rightImageYPosition, 0), tweenTime)
            .SetEase(AppearEase);
        character.Image.DOColor(passiveColor, tweenTime).SetEase(AppearEase);
    }

    [YarnCommand("leftCharaLeaving")]
    public void leftCharaLeaving()
    {
        imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition - 200, leftImageYPosition, 0), tweenTime)
            .SetEase(Ease.OutCubic);
        imageLeft.DOFade(0, tweenTime);
    }
    [YarnCommand("rightCharaLeaving")]
    public void rightCharaLeaving()
    {
        imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition - 200, rightImageYPosition, 0), tweenTime)
            .SetEase(Ease.OutCubic);
        imageRight.DOFade(0, tweenTime);
    }

    [YarnCommand("switchTalker")]
    public void switchTalker()
    {
        if (lastCharacter == leftCharacter)
        {
            imageTransformLeft.DOLocalMove(new Vector3(leftImageXPositionPassive, leftImageYPosition, 0), tweenTime)
                .SetEase(Ease.OutCubic);
            imageLeft.DOColor(passiveColor, tweenTime).SetEase(AppearEase);
            imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition, rightImageYPosition, 0), tweenTime)
                .SetEase(AppearEase);
            imageRight.DOColor(Color.white, tweenTime).SetEase(AppearEase);
            lastCharacter = rightCharacter;
        }
        else if (lastCharacter == rightCharacter)
        {
            imageTransformRight.DOLocalMove(new Vector3(rightImageXPositionPassive, rightImageYPosition, 0), tweenTime)
                .SetEase(Ease.OutCubic);
            imageRight.DOColor(passiveColor, tweenTime).SetEase(AppearEase);
            imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition, leftImageYPosition, 0), tweenTime)
                .SetEase(AppearEase);
            imageLeft.DOColor(Color.white, tweenTime).SetEase(AppearEase);
            lastCharacter = leftCharacter;
        }
    }
}
