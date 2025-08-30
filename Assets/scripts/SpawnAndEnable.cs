using TMPro;
using UnityEngine;

public class SpawnAndEnable : MonoBehaviour
{
    [Header("Object to spawn")]
    public GameObject objectToSpawn;

    [Header("Object to enable")]
    public GameObject buttons;

    [Header("Object to enable")]
    public GameObject RightWindow;
    public GameObject LeftWindow;

    private bool placementMode = false; // Is the player currently placing?

    // Call this function, e.g., from a UI Button OnClick
    public void SpawnBoughtItem()
    {
        if (objectToSpawn != null)
        {
            // Instantiate at position (1, -1, 0) with default rotation
            Instantiate(objectToSpawn, new Vector3(1f, -1f, 0f), Quaternion.identity);
            Shop.instance.ToggleShopMenu(); // Close the shop menu after buying
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

    public void EnablePlacementMode()
    {
        placementMode = true;

        Debug.Log("Placement mode enabled. Click on a PolygonCollider2D to place the object.");
    }

    private void Update()
    {
        if (placementMode && Input.GetMouseButtonDown(0)) // Left mouse button
        {
            TryPlaceObject();
        }
    }

    private void TryPlaceObject()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
        if (hit.collider.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("It's an obstacle!");
        }
        else if (hit.collider != null && hit.collider is PolygonCollider2D)
        {
            Debug.Log("Placed on PolygonCollider2D: " + hit.collider.name);

            if (objectToSpawn != null)
                Instantiate(objectToSpawn, new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0f), Quaternion.identity);
            // Optionally enable your buttons UI
            if (buttons != null)
                buttons.SetActive(true);

            Shop.instance.ToggleShopMenu(); // Close the shop menu after buying

            placementMode = false; // Stop placing after one object
        }
        else
        {
            Debug.Log("Mouse click did not hit a PolygonCollider2D.");
        }
    }

    public void SpawnLeftWindow()
    {
        // Check and destroy any existing left window
        GameObject existingLeft = GameObject.FindGameObjectWithTag("leftwindow");
        if (existingLeft != null)
        {
            Destroy(existingLeft);
        }

        // Spawn new one
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, LeftWindow.transform.position, Quaternion.identity);
            Shop.instance.ToggleShopMenu(); // Close the shop menu after buying
        }

        if (buttons != null)
        {
            buttons.SetActive(true);
        }
    }

    public void SpawnRightWindow()
    {
        // Check and destroy any existing right window
        GameObject existingRight = GameObject.FindGameObjectWithTag("rightwindow");
        if (existingRight != null)
        {
            Destroy(existingRight);
        }

        // Spawn new one
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, RightWindow.transform.position, Quaternion.identity);
            Shop.instance.ToggleShopMenu(); // Close the shop menu after buying
        }

        if (buttons != null)
        {
            buttons.SetActive(true);
        }
    }


}
