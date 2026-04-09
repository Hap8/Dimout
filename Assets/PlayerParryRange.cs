using UnityEngine;

public class PlayerParryRange : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    
}
