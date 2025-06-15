using UnityEngine;

namespace Yarn
{
    [CreateAssetMenu(fileName = "CharacterEmotions", menuName = "ScriptableObjects/CharacterEmotions")]
    public class CharacterEmotions : ScriptableObject
    {
        public string name;
        public Emotions emotion;
        public Sprite sprite;
        
        public enum Emotions
        {
            joyeux,
            enerve,
            triste,
            shy
        }

    }
}