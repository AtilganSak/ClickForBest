using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DOColor : DOBase
{
    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    public Color startColor = Color.white;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    public Color endColor = Color.white;

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
                if (DOTween.IsTweening(c_Transform))
                    sourceImage.DOKill(true);
            }
            tween = sourceImage.DOColor(endColor, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());

            base.DO();
        }
        else
        {
            GetComponent<Image>().color = endColor;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    sourceImage.DOKill(true);
            }
            tween = sourceImage.DOColor(startColor, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());

            base.DORevert();
        }
        else
        {
            GetComponent<Image>().color = startColor;
        }
    }
    public override void ResetDO()
    {
        sourceImage.DOKill(true);
        sourceImage.color = startColor;
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    sourceImage.DOKill(true);
            }
            tween = sourceImage.DOColor(startColor, duration).SetDelay(revertDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            GetComponent<Image>().color = startColor;
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
        startColor = GetComponent<Image>().color;
    }
    void ApplyStartValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedStartColor");
        EditorUtility.SetDirty(this);
#endif
        GetComponent<Image>().color = startColor;
    }
    void RecordEnd()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedEndColor");
        EditorUtility.SetDirty(this);
#endif
        endColor = GetComponent<Image>().color;
    }
    void ApplyEndValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedEndColor");
        EditorUtility.SetDirty(this);
#endif
        GetComponent<Image>().color = endColor;
    }
}
