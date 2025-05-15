using UnityEngine;
using UnityEngine.UI;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO wheatDesignSO; 
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerStateUI playerStateUI;

    private RectTransform playerBoosterTransform;
    private Image playerBoosterImage;
    
    
    private void Awake()
    {
        playerBoosterTransform = playerStateUI.GetBoosterSpeedTransform();
        playerBoosterImage = playerBoosterTransform.GetComponent<Image>();
    }


    public void Collect()
    {
        playerController.SetMovementSpeed(wheatDesignSO.IncreaseDecreaseMultiplier, wheatDesignSO.ResetBoostDuration);

        playerStateUI.PlayBoosterUIAnimations(playerBoosterTransform, playerBoosterImage, playerStateUI.GetGoldBoosterWheatImage(), wheatDesignSO.ActiveSprite,
        wheatDesignSO.PassiveSprite, wheatDesignSO.ActiveWheatSprite, wheatDesignSO.PassiveWheatSprite, wheatDesignSO.ResetBoostDuration);

        Destroy(gameObject);
    }
}
