using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DOFill : DOBase
{
    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    public float startValue = 1;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    public float endValue;

    Image sourceImage;

    internal override void VirtualEnable()
    {
        sourceImage = GetComponent<Image>();
    }
    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
            }
            tween = sourceImage.DOFillAmount(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            base.DO();
        }
        else
        {
            GetComponent<Image>().fillAmount = endValue;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
            }
            tween = sourceImage.DOFillAmount(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            base.DORevert();
        }
        else
        {
            GetComponent<Image>().fillAmount = startValue;
        }
    }
    public override void ResetDO()
    {
        sourceImage.DOKill(true);
        sourceImage.fillAmount = startValue;
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
            }
            tween = sourceImage.DOFillAmount(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            GetComponent<Image>().fillAmount = endValue;
        }
    }
    public override void Kill()
    {
        if (DOTween.IsTweening(sourceImage))
            sourceImage.DOKill();
    }

    void RecordStart()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedStartColor");
        EditorUtility.SetDirty(this);
#endif
        startValue = GetComponent<Image>().fillAmount;
    }
    void ApplyStartValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedStartColor");
        EditorUtility.SetDirty(this);
#endif
        GetComponent<Image>().fillAmount = startValue;
    }
    void RecordEnd()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedEndColor");
        EditorUtility.SetDirty(this);
#endif
        endValue = GetComponent<Image>().fillAmount;
    }
    void ApplyEndValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedEndColor");
        EditorUtility.SetDirty(this);
#endif
        GetComponent<Image>().fillAmount = endValue;
    }
}
