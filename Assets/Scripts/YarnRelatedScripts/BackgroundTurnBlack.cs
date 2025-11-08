using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class BackgroundTurnBlack : MonoBehaviour
{
    [SerializeField] private Image blackscreen;

    [YarnCommand("blackscreen")]
    public void ScreenFadeBlack()
    {
        blackscreen.DOFade(1, 0.25f)
            .OnComplete(() =>
            {
                blackscreen.DOFade(0, 0.25f);
            });
    }
}
