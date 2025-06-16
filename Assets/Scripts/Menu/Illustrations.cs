using UnityEngine;

public class Illustrations : MonoBehaviour
{
    [SerializeField] private Animator animatorIllu;
    [SerializeField] private GameObject boutonRetourIllu;
    [SerializeField] private MenuManager menuManager;

    [SerializeField] private RectTransform panelRectTransform;

    public void CloseUpIllu()
    {
        animatorIllu.SetBool("Open", true);
        boutonRetourIllu.SetActive(true);
        menuManager.activeAnimator = animatorIllu;
        
        panelRectTransform.SetAsLastSibling();
    }



}
