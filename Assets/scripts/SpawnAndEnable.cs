using UnityEngine;

public class SpawnAndEnable : MonoBehaviour
{
    [Header("Object to spawn")]
    public GameObject objectToSpawn;

    [Header("Object to enable")]
    public GameObject buttons;

    // Call this function, e.g., from a UI Button OnClick
    public void SpawnBoughtItem()
    {
        if (objectToSpawn != null)
        {
            // Instantiate at position (1, -1, 0) with default rotation
            Instantiate(objectToSpawn, new Vector3(1f, -1f, 0f), Quaternion.identity);
        }

        if (buttons != null)
        {
            buttons.SetActive(true);
        }
    }

    public void DeleteBoughtItem()
    {
        if (objectToSpawn != null)
        {
            Destroy(objectToSpawn);
        }
    }
}
