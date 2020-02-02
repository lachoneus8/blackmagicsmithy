using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeterBehaviour : MonoBehaviour
{
    public Color foreground;
    public Color background;
    public bool isHorizontal;
    public float minValue;
    public float maxValue;

    public Image foregroundImage;
    public Image backgroundImage;

    private float curValue;
    private float canvasScaleFactor;
    
    public void Init(float inCanvasScaleFactor)
    {
        canvasScaleFactor = inCanvasScaleFactor;
        foregroundImage.color = foreground;
        backgroundImage.color = background;
    }

    public void SetValue(float inCurValue)
    {
        var transform = backgroundImage.GetComponent<RectTransform>();
        var maxMeterDim = new Vector2(transform.rect.width * canvasScaleFactor, transform.rect.height * canvasScaleFactor);

        curValue = inCurValue;
        var progress = (curValue - minValue) / (maxValue - minValue);
        var rectTransform = foregroundImage.GetComponent<RectTransform>();

        Vector2 oldSize = rectTransform.rect.size;
        Vector2 deltaSize;

        if (isHorizontal)
        {
            deltaSize = new Vector2(maxMeterDim.x * progress, oldSize.y) - oldSize;
        }
        else
        {
            deltaSize = new Vector2(oldSize.x, maxMeterDim.y * progress) - oldSize;
        }
        rectTransform.offsetMin = rectTransform.offsetMin - new Vector2(deltaSize.x * rectTransform.pivot.x, deltaSize.y * rectTransform.pivot.y);
        rectTransform.offsetMax = rectTransform.offsetMax + new Vector2(deltaSize.x * (1f - rectTransform.pivot.x), deltaSize.y * (1f - rectTransform.pivot.y));
    }
}
