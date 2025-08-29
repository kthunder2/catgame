using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Call this function to quit the game
    public void Quit()
    {
        // If running in the Unity editor, stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running as a standalone build, quit the application
        Application.Quit();
#endif
    }
}
