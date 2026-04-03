using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    private Player player;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;

    [Header("Detection")]
    [SerializeField] private LayerMask enemyLayer;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            Vector2 dir = (player.transform.position - collision.transform.position).normalized;
            player.rb.linearVelocity = new Vector2(dir.x * knockbackForce, dir.y * knockbackForce);
        }
    }

    public void UpdatePosition()
    {
        Vector2 input = player.movementInput;

        if (Mathf.Abs(input.y) > 0.1f)
        {
            transform.localPosition = new Vector3(0f, 0.7f * Mathf.Sign(input.y), 0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            transform.localPosition = new Vector3(0.5f, 0f, 0f);
            transform.localRotation = Quaternion.identity;
        }
    }

}
