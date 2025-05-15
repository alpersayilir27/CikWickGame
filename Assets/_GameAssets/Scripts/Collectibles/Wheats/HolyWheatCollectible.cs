using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private WheatDesignSO wheatDesignSO; 
    [SerializeField] private PlayerStateUI playerStateUI;

    private RectTransform playerBoosterTransform;
    private Image playerBoosterImage;
    
    
    private void Awake()
    {
        playerBoosterTransform = playerStateUI.GetBoosterJumpTransform();
        playerBoosterImage = playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        playerController.SetJumpForce(wheatDesignSO.IncreaseDecreaseMultiplier, wheatDesignSO.ResetBoostDuration);
        playerStateUI.PlayBoosterUIAnimations(playerBoosterTransform, playerBoosterImage, playerStateUI.GetHolyBoosterWheatImage(), wheatDesignSO.ActiveSprite,
        wheatDesignSO.PassiveSprite, wheatDesignSO.ActiveWheatSprite, wheatDesignSO.PassiveWheatSprite, wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
