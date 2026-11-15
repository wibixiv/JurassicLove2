using UnityEngine;

public class NerdMinigameController : MonoBehaviour
{
    public float speed, xMin,xMax;
    void Update()
    {
        float dir =0;
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) dir =-1f;
        else if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)) dir = 1f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + dir*speed,xMin,xMax),transform.position.y,transform.position.z);
    }
}
