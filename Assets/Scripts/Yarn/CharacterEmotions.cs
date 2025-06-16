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
            blush,
            coquette,
            deg,
            enerve,
            ennuye,
            heureux,
            nerveux,
            normal,
            super_ennuye,
            super_heureux,
            super_enerve,
            sus,
            triste,
            sleep,
            dedain,
            choque,
            worried,
            shy,
            tete,
            agace,
            rigolade,
            pensif
        }

    }
}