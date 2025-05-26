using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHowerController : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.Play(SoundType.ButtonHoverSound);
    }
}
