using DG.Tweening;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public bool isOnPhone = true;
    
    [SerializeField] private float offPosY;
    [SerializeField] private float onPosY;
    [SerializeField] private float moveDuration;
    
    public void PressOnOffButton()
    {
        if (isOnPhone) TurnOff();
        else TurnOn();
    }
    
    void TurnOff()
    {
        isOnPhone = false;
        GetComponent<SpriteRenderer>().color = Color.black;
        transform.DOMove(new Vector3(transform.position.x, offPosY, 0), moveDuration).SetEase(Ease.OutQuint);
    }
    
    void TurnOn()
    {
        isOnPhone = true;
        GetComponent<SpriteRenderer>().color = Color.blue;
        transform.DOMove(new Vector3(transform.position.x, onPosY, 0), moveDuration).SetEase(Ease.OutQuint);
    }
}
