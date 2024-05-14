using UnityEngine;

public class ObjectPoolItem : MonoBehaviour
{
    public void Setup(Transform parent)
    {
        transform.parent = parent;
        gameObject.SetActive(false);
    }

    public void Setup(Vector3 postion, Quaternion rotation)
    {
        transform.position = postion;
        transform.rotation = rotation;
    }

    public void ResetToOrigin()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}