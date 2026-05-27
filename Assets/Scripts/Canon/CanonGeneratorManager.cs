using UnityEngine;

public class CanonGeneratorManager : MonoBehaviour
{
    [SerializeField] private Transform canonGenerationParent;
    [SerializeField] private GameObject canonPlatformPrefab;

    [SerializeField] private int cannonsToCreate;

    [SerializeField] private float avaliableSpace = 6;

    [SerializeField] private float canonGeneratorParentZPos;

    private void Start()
    {
        float spaceBetweenObjects = avaliableSpace / cannonsToCreate;

        float totalWidth = spaceBetweenObjects * (cannonsToCreate - 1);

        float offset = totalWidth / 2;

        for (int i = 0; i < cannonsToCreate; i++)
        {
            GameObject createdObjects = Instantiate(canonPlatformPrefab, canonGenerationParent);

            createdObjects.transform.localPosition = new Vector3((spaceBetweenObjects * i) - offset, 0, 0);
        }

        canonGenerationParent.transform.position = new Vector3(0,0,canonGeneratorParentZPos);
    }
}
