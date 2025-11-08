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
    [SerializeField] private float speakTweenTime = 0.5f;
    [SerializeField] private Color passiveColor;
    [SerializeField] private List<string> mainCharacterNames = new()
    {
        "MonsiCalbar",
        "MonsiPantalon"
    };

    private CharacterReferences lastCharacter;
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
        public CharacterReferences Opposite;
        public Vector3 BaseScale;
        public string LastName = "";

        public CharacterReferences(Transform transform, Image image, float xPosition, float xAppearOffset)
        {
            Transform = transform;
            Image = image;
            XPosition = xPosition;
            XAppearOffset = xAppearOffset;
            BaseScale = Transform.localScale;
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

        leftCharacter = new CharacterReferences(imageTransformLeft, imageLeft, leftImageXPosition, -200);
        rightCharacter = new CharacterReferences(imageTransformRight, imageRight, rightImageXPosition, 200);
        leftCharacter.Opposite = rightCharacter;
        rightCharacter.Opposite = leftCharacter;
    }

    [YarnCommand("FaceReset")]
    public void FaceReset()
    {
        
    }
    
    [YarnCommand("FaceUpdate")]
    public void FaceUpdate(string characterName, string emotionName)
    {
        if (Enum.TryParse(emotionName, out CharacterEmotions.Emotions emotion))
        {
            if (Dict.TryGetValue((characterName, emotion), out Sprite sprite))
            {
                // Main character is on the left, other characters are on the right.
                UpdateCharacter(mainCharacterNames.Contains(characterName) ? leftCharacter : rightCharacter,
                    sprite,
                    characterName);
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
        UpdateCharacterEmotion(character.Image, sprite);

        if (character.LastName != newName)
        {
            CharacterAppears(character);
        }
        
        CharacterSpeaks(character);
        CharacterUnspeaks(character.Opposite);
        
        character.LastName = newName;
        lastCharacter = character;
    }
    
    private void UpdateCharacterEmotion(Image image, Sprite sprite)
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

    private void CharacterSpeaks(CharacterReferences character)
    {
        character.Transform.DOScale(character.BaseScale * 1.1f, speakTweenTime).SetEase(AppearEase);
    }

    private void CharacterUnspeaks(CharacterReferences character)
    {
        character.Transform.DOScale(character.BaseScale, speakTweenTime).SetEase(AppearEase);
    }

    private void CharacterDisappears(CharacterReferences character)
    {
        character.Transform.DOLocalMove(new Vector3(character.XPosition + character.XAppearOffset, rightImageYPosition, 0), tweenTime)
            .SetEase(AppearEase);
        character.Image.DOColor(passiveColor, tweenTime).SetEase(AppearEase);
    }

    [YarnCommand("LeftCharacterDisappears")]
    public void LeftCharacterDisappears()
    {
        CharacterDisappears(leftCharacter);
    }

    [YarnCommand("RightCharacterDisappears")]
    public void RightCharacterDisappears()
    {
        CharacterDisappears(rightCharacter);
    }

    [YarnCommand("switchTalker")]
    public void switchTalker()
    {
        if (lastCharacter == leftCharacter)
        {
            imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition - 200, leftImageYPosition, 0), tweenTime)
                .SetEase(Ease.OutCubic);
            imageLeft.DOColor(passiveColor, tweenTime).SetEase(AppearEase);
            imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition, rightImageYPosition, 0), tweenTime)
                .SetEase(AppearEase);
            imageRight.DOColor(Color.white, tweenTime).SetEase(AppearEase);
            lastCharacter = rightCharacter;
        }
        else if (lastCharacter == rightCharacter)
        {
            imageTransformRight.DOLocalMove(new Vector3(rightImageXPosition + 200, rightImageYPosition, 0), tweenTime)
                .SetEase(Ease.OutCubic);
            imageRight.DOColor(passiveColor, tweenTime).SetEase(AppearEase);
            imageTransformLeft.DOLocalMove(new Vector3(leftImageXPosition, leftImageYPosition, 0), tweenTime)
                .SetEase(AppearEase);
            imageLeft.DOColor(Color.white, tweenTime).SetEase(AppearEase);
            lastCharacter = leftCharacter;
        }
    }
}
