using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // change position over time, moce it to left
    // once it's past the screen (left), discard

    private float leftEdge;
    private void Start()
    {
        // make conversion from screen to world space by accessing camera 
        // Camera has to be tagged "main"
        // only x axis, left edge is zero, right would be 1920
        // offsetting by extra -2f for prefabs to completely pass the screen
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
    private void Update()
    {
        // obstacles move as fast as ground/game
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        if(transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }

    }

}
