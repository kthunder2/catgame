using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    // Assign the GameObject you want to enable/disable in the Inspector
    public GameObject targetObject;
    public void ToggleObjectActive()
    {
        if (targetObject != null)
            targetObject.SetActive(!targetObject.activeSelf);
    }
}
