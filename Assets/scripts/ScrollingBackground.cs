using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float resetPositionX;  // where to reset
    public float startPositionX;  // where to move back

    void Update()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= resetPositionX)
        {
            transform.position = new Vector2(startPositionX, transform.position.y);
        }
    }
}
