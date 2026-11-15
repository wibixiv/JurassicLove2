using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneButton : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public Image image;
    public bool clicked;
    public Button button;
    void Start()
    {
        button.onClick.AddListener(delegate() {click();});
    }
    void click()
    {
        if(clicked) return;
        FNAFGameManager.FNAFManager.str +=TMP.text;
        image.color = Color.gray;
        clicked = true;
    }
    public void UnClick()
    {
        clicked = false;
        image.color = Color.white;
    }
}
