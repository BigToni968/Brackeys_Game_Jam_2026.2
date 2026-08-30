using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class UI_CustomButton : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string prefix;
    [SerializeField] private Color standert = Color.white;
    [SerializeField] private Color hover = Color.white;
    
    private string _defaultText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrEmpty(_defaultText))
            _defaultText = text.text;
        text.color = hover;
        text.SetText(prefix + _defaultText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = standert;
        text.SetText(_defaultText);
    }
}