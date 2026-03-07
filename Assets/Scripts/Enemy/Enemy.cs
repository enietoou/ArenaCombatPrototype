using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    
    public EnemyStats Stats => stats;
}
