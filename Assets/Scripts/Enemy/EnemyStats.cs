using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("Health")]
    public int maxHealth = 100;
    
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stoppingDistance = 1f;
    
    [Header("Rotation")]
    public float rotationSpeed = 1f;
    
    [Header("Attack")]
    public int damage = 10;
    public float attackRate = 1f;
    public float attackDistance = 1.5f;

    [Header("Targeting")]
    public float aggroRadius = 10f;
}
