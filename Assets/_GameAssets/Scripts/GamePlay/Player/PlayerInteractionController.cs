using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

         if (other.gameObject.TryGetComponent<ICollectibles>(out ICollectibles collectible))
         {
             collectible.Collect();
         }
    }
}
