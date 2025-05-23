using ScriptableObjects;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] ScriptableObjects.Resources resource;
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.PlayerDidHit(resource);

        if (resource.OneTime)
        {
            Destroy(gameObject);
        }
    }
}
