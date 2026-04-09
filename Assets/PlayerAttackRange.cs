using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    private Player player;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;

    [Header("Detection")]
    [SerializeField] private LayerMask enemyLayer;

    private Vector2 attackDirection = Vector2.right;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            Vector2 knockbackDirection = -attackDirection.normalized;
            player.rb.linearVelocity = knockbackDirection * knockbackForce;
        }
    }

    public void UpdatePosition()
    {
        Vector2 input = player.movementInput;

        if (input.sqrMagnitude < 0.01f)
        {
            attackDirection = Vector2.right;
        }
        else
        {
            attackDirection = player.transform.InverseTransformDirection(input).normalized;
        }

        transform.localPosition = attackDirection * 0.7f;

        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
