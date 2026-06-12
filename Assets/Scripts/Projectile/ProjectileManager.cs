using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    void Awake()
    {
        instance = this;
    }
    public void InstantiateProjectile(Transform from, Transform to)
    {
        ProjectileBehaviour projectile = Instantiate(projectilePrefab,from.position,Quaternion.identity);

        projectile.SetUpProjectile(to);
    }
}
