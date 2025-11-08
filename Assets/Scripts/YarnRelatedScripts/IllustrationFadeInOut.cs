using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class IllustrationFadeInOut : MonoBehaviour
{
    [SerializeField] private Image illustration;

    [YarnCommand("illustration")]
    public void IlluFadeIn(float fadeDuration = 0.25f, float pauseDuration = 5f)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(illustration.DOFade(1f, fadeDuration));
        seq.AppendInterval(pauseDuration);
        seq.Append(illustration.DOFade(0f, fadeDuration));

    }
}
