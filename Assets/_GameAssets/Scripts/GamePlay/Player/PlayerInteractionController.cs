using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.WheatTypes.GOLD_WHEAT))
        {
           other.gameObject.GetComponent<GoldWheatCollectible>().Collect();
        }
        if (other.CompareTag(Const.WheatTypes.HOLY_WHEAT))
        {
           other.gameObject.GetComponent<HolyWheatCollectible>().Collect();
        }
        if (other.CompareTag(Const.WheatTypes.ROTTEN_WHEAT))
        {
           other.gameObject.GetComponent<RottenWheatCollectible>().Collect();
        }
    }
}
