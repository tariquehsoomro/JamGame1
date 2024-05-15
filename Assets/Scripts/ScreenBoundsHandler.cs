using UnityEngine;

public class ScreenBoundsHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask boundCollidersLayerMask;

    private Vector2 topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner;
    private GameObject tempObject = null;
    private const string ScreenBoundGOStr = "ScreenBounds";
    private const int numberOfEdgeColliders = 4;
    private const int numberOfPointForEachEdgeColliders = 2;
    private bool objectFound;
    private EdgeCollider2D[] colliders2D;

    private const int layerr = 9;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Start()
    {
        GenerateScreenBoundColliders();
    }

    [ContextMenu("Test")]
    private void Test()
    {
        Debug.Log(Singleton.Instance.ProjectEssentials.GetLayerNumber(boundCollidersLayerMask));
    }

    [ContextMenu("GenerateScreenBoundColliders")]
    public void GenerateScreenBoundColliders()
    {
        CalculateCorners();
        CheckOrCreateBoundsGO();
    }

    private void CalculateCorners()
    {
        topLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        bottomRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
    }

    private void CheckOrCreateBoundsGO()
    {
        objectFound = false;
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == ScreenBoundGOStr)
            {
                tempObject = gameObjects[i];
                objectFound = true;
            }
        }

        if (!objectFound)
            tempObject = new GameObject(ScreenBoundGOStr);

        if (!tempObject.GetComponent<EdgeCollider2D>())
            Generate2DEdgeColliders(tempObject);
        else
            colliders2D = tempObject.GetComponents<EdgeCollider2D>();

        tempObject.layer = Singleton.Instance.ProjectEssentials.GetLayerNumber(boundCollidersLayerMask);
    }

    private void Generate2DEdgeColliders(GameObject gameObjectToAttachColliders)
    {
        colliders2D = new EdgeCollider2D[numberOfEdgeColliders];

        for (int i = 0; i < numberOfEdgeColliders; i++)
        {
            colliders2D[i] = gameObjectToAttachColliders.AddComponent<EdgeCollider2D>();
        }

        SetCollidersPoints();
    }

    private void SetCollidersPoints()
    {
        // Left Collider
        Vector2[] colliderPointsLeft = new Vector2[numberOfPointForEachEdgeColliders];
        colliderPointsLeft[0].x = topLeftCorner.x;
        colliderPointsLeft[0].y = topLeftCorner.y;
        colliderPointsLeft[1].x = bottomLeftCorner.x;
        colliderPointsLeft[1].y = bottomLeftCorner.y;
        colliders2D[0].points = colliderPointsLeft;

        // Bottom Collider
        Vector2[] colliderPointsBottom = new Vector2[numberOfPointForEachEdgeColliders];
        colliderPointsBottom[0].x = bottomLeftCorner.x;
        colliderPointsBottom[0].y = bottomLeftCorner.y;
        colliderPointsBottom[1].x = bottomRightCorner.x;
        colliderPointsBottom[1].y = bottomRightCorner.y;
        colliders2D[1].points = colliderPointsBottom;

        // Right Collider
        Vector2[] colliderPointsRight = new Vector2[numberOfPointForEachEdgeColliders];
        colliderPointsRight[0].x = bottomRightCorner.x;
        colliderPointsRight[0].y = bottomRightCorner.y;
        colliderPointsRight[1].x = topRightCorner.x;
        colliderPointsRight[1].y = topRightCorner.y;
        colliders2D[2].points = colliderPointsRight;

        // Top Collider
        Vector2[] colliderPointsTop = new Vector2[numberOfPointForEachEdgeColliders];
        colliderPointsTop[0].x = topRightCorner.x;
        colliderPointsTop[0].y = topRightCorner.y;
        colliderPointsTop[1].x = topLeftCorner.x;
        colliderPointsTop[1].y = topLeftCorner.y;
        colliders2D[3].points = colliderPointsTop;
    }
}
