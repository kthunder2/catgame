using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public GameObject categories;
    public GameObject cat_menu;
    public GameObject furniture_menu;
    public GameObject windows_menu;

    public Camera mainCamera; // Assign this in the Inspector

    public bool isMenuOpen = false;
    private GameObject currentOpenMenu = null;

    private Vector3 defaultCameraPosition = new Vector3(0, 0, -10);
    private Vector3 furnitureCameraPosition = new Vector3(0, -1, -10);

    private void Start()
    {
        instance = this;
        if (mainCamera != null)
            mainCamera.transform.position = defaultCameraPosition;
    }

    // Called when the main button is clicked
    public void ToggleShopMenu()
    {
        if (!TextDisplay.instance.isActive)
        {
            isMenuOpen = !isMenuOpen;
            categories.SetActive(isMenuOpen);

            // If closing the main menu, also close any open submenus
            if (!isMenuOpen && currentOpenMenu != null)
            {
                currentOpenMenu.SetActive(false);
                currentOpenMenu = null;
            }

            UpdateCameraPosition();
        }
    }

    // Call this from a button and pass the menu to toggle
    public void ToggleChildMenu(GameObject menu)
    {
        if (menu == null) return;

        // If the clicked menu is already open, close it
        if (currentOpenMenu == menu)
        {
            menu.SetActive(false);
            currentOpenMenu = null;
        }
        else
        {
            // Close the previously open menu
            if (currentOpenMenu != null)
            {
                currentOpenMenu.SetActive(false);
            }

            // Open the clicked menu
            menu.SetActive(true);
            currentOpenMenu = menu;
        }

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if (mainCamera == null) return;

        if (currentOpenMenu == furniture_menu)
        {
            mainCamera.transform.position = furnitureCameraPosition;
        }
        else
        {
            mainCamera.transform.position = defaultCameraPosition;
        }
    }
}
