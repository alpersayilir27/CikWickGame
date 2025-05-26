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

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
    }
}
