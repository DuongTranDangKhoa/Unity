using UnityEngine;

public class InfiniteScrolling : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private bool canMoveY;
    [SerializeField] private float parallaxEffect;

    private float length;
    private float xPosition;

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMoveX = cam.transform.position.x * parallaxEffect;

        float moveY = canMoveY ? cam.transform.position.y : transform.position.y;

        transform.position = new Vector3(xPosition + distanceToMoveX, moveY);

        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}