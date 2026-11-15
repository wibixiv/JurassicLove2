using UnityEngine;

public class PhoneMovement : MonoBehaviour
{
    public Animation anim;
    bool up = true;
    public void telephoneMove()
    {
        if(up)anim.Play("PhoneDown");
        else anim.Play("PhoneUp");
        up = !up;
        FNAFGameManager.FNAFManager.phoneUp = up;
    }


}
