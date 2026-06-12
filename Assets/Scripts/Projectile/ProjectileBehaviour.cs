using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    private Transform targetTransform;

    public void SetUpProjectile(Transform target)
    {
        targetTransform = target;
    }

    void Update()
    {
        if (targetTransform != null)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, projectileSpeed * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name+ " " + targetTransform.name);
        if (other.name == targetTransform.name)
        {
            Destroy(gameObject);
        }
    }
}
