using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ObjectPoolItem objectPoolItem;
    [SerializeField] private float moveSpeed = 10f;

    private bool canMove = true;

    private void OnEnable()
    {
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "_BD")
        {
            canMove = false;

            objectPoolItem.ResetToOrigin();
        }
    }
}
