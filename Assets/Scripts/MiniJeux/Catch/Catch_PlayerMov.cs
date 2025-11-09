using Unity.VisualScripting;
using UnityEngine;

public class Catch_PlayerMov : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(moveHorizontal * speed, 0);

    }

}
