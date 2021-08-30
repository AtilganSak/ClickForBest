using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DOBase : MonoBehaviour
{
    public float duration = 1;
    public float doDelay;
    public float revertDelay;
    public Ease ease;
    public LoopType loopType;
    public bool resetOnEnable = true;
    public bool doOnStart = false;
    public bool loopOnStart = false;
    public bool allowTwin;

    public UnityEvent doComplete;
    public UnityEvent doRevertComplete;

    public TweenKey tweenKey;

    [HideInInspector] public bool connect = true;
    [HideInInspector] public int orderIndex;

    internal bool doRun;
    internal bool doRevertRun;

    private float percentageDuration;

    WaitForSeconds wait;

    internal Transform c_Transform;
    internal Tween tween;

    [System.Serializable]
    public struct TweenKey
    {
        [Range(1, 99)]
        public float percentage;
        public UnityEvent doReachedEvent;
        public UnityEvent doRevertReachedEvent;
        [HideInInspector] public bool ok;
    }

    private void OnEnable()
    {
        c_Transform = transform;

        if (tweenKey.percentage < 1)
            tweenKey.percentage = 1;

        percentageDuration = duration * (tweenKey.percentage / 100);

        wait = new WaitForSeconds(percentageDuration);

        VirtualEnable();

        if (resetOnEnable)
            ResetDO();
    }
    private void Start()
    {
        if (doOnStart)
            DO();

        if (loopOnStart)
            DOLoop();

        VirtualStart();
    }
    private IEnumerator DOTweenKey()
    {
        while (!tweenKey.ok)
        {
            yield return wait;
            if (tween != null)
            {
                if (!tweenKey.ok)
                {
                    tweenKey.ok = true;
                    if (doRun)
                        tweenKey.doReachedEvent.Invoke();
                    else if (doRevertRun)
                        tweenKey.doRevertReachedEvent.Invoke();
                    doRun = false;
                    doRevertRun = false;
                }
            }
        }
    }
    private void Reset()
    {
        tweenKey.percentage = 1;
    }
    internal virtual void VirtualStart() { }
    internal virtual void VirtualEnable() { }

    #region Encapsulate
    public void SetDuration(float value) => duration = value;
    public float GetDureation() => duration;
    #endregion

    public bool IsTweening()
    {
        if (tween != null && tween.IsActive())
        {
            return tween.IsPlaying();
        }
        else
        {
            return false;
        }
    }
    [EasyButtons.Button]
    public virtual void DO()
    {
        tweenKey.ok = false;
        doRun = true;
        //if(tweenKey.doReachedEvent.GetPersistentEventCount() > 0)
            StartCoroutine(DOTweenKey());
    }
    [EasyButtons.Button]
    public virtual void DOLoop() { }
    [EasyButtons.Button]
    public virtual void DORevert()
    {
        tweenKey.ok = false;
        doRevertRun = true;
        //if (tweenKey.doRevertReachedEvent.GetPersistentEventCount() > 0)
            StartCoroutine(DOTweenKey());
    }
    [EasyButtons.Button("Reset")]
    public virtual void ResetDO() { }
    [EasyButtons.Button]
    public virtual void Kill() { }
}
