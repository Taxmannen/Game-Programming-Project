using UnityEngine;

public class ItemCanon : MonoBehaviour
{
    [SerializeField] private GameObject stunBullet;
    [SerializeField] private Vector2 dir = new Vector2(1, 0);

    void Start()
    {
        InvokeRepeating("Shoot", 0, 1f);   
    }
    private void Shoot()
    {
        GameObject current = Instantiate(stunBullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = current.GetComponent<Rigidbody2D>();
        rb.velocity = dir;

        Destroy(current, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}