using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    [SerializeField] private ObjectPoolTypeE typeE;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRateTime = 10f;
    [SerializeField] private float fireRate = 0;

    private float fireRateTimer;

    private void Update()
    {
        fireRateTimer -= Time.deltaTime * fireRateTime * fireRate;

        if (Input.GetKey(KeyCode.Z) && fireRateTimer <= 0)
        {
            Singleton.Instance.ObjectPoolManager.InstantiateFromPool(typeE, shootPoint.position, Quaternion.identity);
            fireRateTimer = fireRateTime;
        }
    }
}
