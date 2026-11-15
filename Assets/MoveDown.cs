using Unity.VisualScripting;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 1f;
    void Update()
    {
        transform.position= transform.position -new Vector3(0,Time.deltaTime*speed,0);       
    }
}
