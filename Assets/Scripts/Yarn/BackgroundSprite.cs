using UnityEngine;

namespace Yarn
{
    [CreateAssetMenu(fileName = "CharacterEmotions", menuName = "ScriptableObjects/BackgroundImage")]
    public class BackgroundSprite : ScriptableObject
    {
        public string spriteName;
        public Sprite sprite;
    }
}