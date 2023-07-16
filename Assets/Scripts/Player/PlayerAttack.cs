using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public bool enemyHit;
    public LayerMask whatIsEnemy;
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.7f);

        if (hit.collider == null) return;
        if (hit.transform.CompareTag("Enemy"))
        {
               Destroy(hit.transform.gameObject);
               
        }
    }
}
