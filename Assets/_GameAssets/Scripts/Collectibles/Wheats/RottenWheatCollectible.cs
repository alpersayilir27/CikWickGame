using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WheatDesignSO wheatDesignSO;

    [SerializeField] private PlayerStateUI playerStateUI;

    private RectTransform playerBoosterTransform;
    private Image playerBoosterImage;
    
    
    private void Awake()
    {
        playerBoosterTransform = playerStateUI.GetBoosterSlowTransform();
        playerBoosterImage = playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        playerController.SetMovementSpeed(wheatDesignSO.IncreaseDecreaseMultiplier, wheatDesignSO.ResetBoostDuration);
        playerStateUI.PlayBoosterUIAnimations(playerBoosterTransform, playerBoosterImage, playerStateUI.GetRottenBoosterWheatImage(), wheatDesignSO.ActiveSprite,
        wheatDesignSO.PassiveSprite, wheatDesignSO.ActiveWheatSprite, wheatDesignSO.PassiveWheatSprite, wheatDesignSO.ResetBoostDuration);
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        Destroy(gameObject);
    }
}
