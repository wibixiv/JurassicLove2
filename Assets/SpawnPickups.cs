using UnityEngine;
using System.Collections;

public class SpawnPickups : MonoBehaviour
{
    public GameObject[] pickupPrefabs;
    public BoxCollider2D spawnBox;

    private void Start()
    {
        StartCoroutine(SpwawnCo());
    }
    private IEnumerator SpwawnCo()
    {
        while(true)
        {
            SpawnPickup();
            yield return new WaitForSeconds(Random.Range(0.5f,1f));
        }
    }

    private void SpawnPickup()
    {
        GameObject g = pickupPrefabs[Random.Range(0,pickupPrefabs.Length)];
        Instantiate(g,(Vector3) new Vector2(Random.Range(spawnBox.bounds.min.x,spawnBox.bounds.max.x),Random.Range(spawnBox.bounds.min.y,spawnBox.bounds.max.y)),Quaternion.identity);
    }
}
