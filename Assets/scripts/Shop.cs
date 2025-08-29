using UnityEngine;

public class Shop : MonoBehaviour
{

    public static Shop instance;
    public GameObject categories;
    public GameObject cat_menu;
    public GameObject furniture_menu;
    public GameObject windows_menu;

    public bool isMenuOpen = false;
    private GameObject currentOpenMenu = null;

    // Called when the main button is clicked
    public void ToggleShopMenu()
    {
        isMenuOpen = !isMenuOpen;
        categories.SetActive(isMenuOpen);

        // If closing the main menu, also close any open submenus
        if (!isMenuOpen && currentOpenMenu != null)
        {
            currentOpenMenu.SetActive(false);
            currentOpenMenu = null;
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
    }
}
