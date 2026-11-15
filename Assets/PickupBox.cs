using Unity.VisualScripting;
using UnityEngine;

public class PickupBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
      {
        Debug.Log("HERE");
        if(other.tag == "Positive")
        {        
            NerdMinigameManager.NerdPointChange(2);
            Destroy(other.gameObject);
        }
        if(other.tag == "Negative")
        {
            NerdMinigameManager.NerdPointChange(-1);
            Destroy(other.gameObject);
        }
    }
}
