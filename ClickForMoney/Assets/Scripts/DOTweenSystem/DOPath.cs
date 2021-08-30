using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class DOPath : DOBase
{
    public PathType pathType;

    public bool local;

    public bool useTransform = true;
    public Transform[] transforms;
    public Vector3[] positions;

    private Vector3[] transformPositions;
    private Vector3[] savedPositions;

    internal override void VirtualEnable()
    {
        base.VirtualEnable();

        if (transforms != null && transforms.Length > 1)
        {
            List<Vector3> poses = new List<Vector3>();
            for (int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i] != null)
                {
                    if (!local)
                        poses.Add(transforms[i].position);
                    else
                        poses.Add(transforms[i].localPosition);
                }
            }
            transformPositions = poses.ToArray();
        }
        if (positions != null && positions.Length > 1)
        {
            List<Vector3> poses = new List<Vector3>();
            for (int i = 0; i < positions.Length; i++)
            {
                poses.Add(transform.position + positions[i]);
            }
            savedPositions = poses.ToArray();
        }
    }
    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    c_Transform.DOKill(true);
            }
            if (useTransform)
            {
                if (transformPositions != null && transformPositions.Length > 1)
                {
                    tween = c_Transform.DOPath(transformPositions, duration, pathType).SetEase(ease).SetDelay(doDelay).OnComplete(() => doComplete.Invoke());
                }
            }
            else
            {
                if (savedPositions != null && savedPositions.Length > 1)
                {
                    tween = c_Transform.DOPath(savedPositions, duration, pathType).SetEase(ease).SetDelay(doDelay).OnComplete(() => doComplete.Invoke());
                }
            }
            base.DO();
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    c_Transform.DOKill(true);
            }
            if (useTransform)
            {
                if (transformPositions != null && transformPositions.Length > 1)
                {
                    tween = c_Transform.DOPath(transformPositions.Reverse().ToArray(), duration, pathType).SetEase(ease).SetDelay(doDelay).OnComplete(() => doRevertComplete.Invoke());
                }
            }
            else
            {
                if (savedPositions != null && savedPositions.Length > 1)
                {
                    tween = c_Transform.DOPath(savedPositions.Reverse().ToArray(), duration, pathType).SetEase(ease).SetDelay(doDelay).OnComplete(() => doRevertComplete.Invoke());
                }
            }
            base.DORevert();
        }
    }
    public override void ResetDO()
    {
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
        transform.DOKill(true);
        if (useTransform)
        {
            if (transformPositions != null && transformPositions.Length > 1)
            {
                if (local)
                {
                    transform.localPosition = transforms[0].localPosition;
                }
                else
                {
                    transform.position = transforms[0].position;
                }
            }
        }
        else
        {
            if (positions != null && positions.Length > 1)
            {
                if (local)
                {
                    transform.localPosition = transform.position + positions[0];
                }
                else
                {
                    transform.position = positions[0];
                }
            }
        }
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
        EditorUtility.SetDirty(transform);
#endif
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    c_Transform.DOKill(true);
            }
            if (useTransform)
            {
                if (transformPositions != null && transformPositions.Length > 1)
                {
                    tween = c_Transform.DOPath(transformPositions.Reverse().ToArray(), duration, pathType).SetEase(ease).SetDelay(doDelay).SetLoops(-1,loopType);
                }
            }
            else
            {
                if (savedPositions != null && savedPositions.Length > 1)
                {
                    tween = c_Transform.DOPath(savedPositions.Reverse().ToArray(), duration, pathType).SetEase(ease).SetDelay(doDelay).SetLoops(-1, loopType);
                }
            }
        }
    }
    public override void Kill()
    {
        if (DOTween.IsTweening(transform))
            transform.DOKill();
    }
    private void OnDrawGizmos()
    {
        if (!useTransform)
        {
            if (local)
            {
                if (positions != null && positions.Length > 0)
                {
                    if (Application.isPlaying)
                    {
                        for (int i = 0; i < savedPositions.Length; i++)
                        {
                            Gizmos.DrawSphere(savedPositions[i], 0.3F);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < positions.Length; i++)
                        {
                            Gizmos.DrawSphere(transform.position + positions[i], 0.3F);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    Gizmos.DrawSphere(positions[i], 0.3F);
                }
            }
        }
        else
        {
            if (!local)
            {
                for (int i = 0; i < transforms.Length; i++)
                {
                    Gizmos.DrawSphere(transforms[i].position, 0.3F);
                }
            }
            else
            {
                for (int i = 0; i < transforms.Length; i++)
                {
                    Gizmos.DrawSphere(transforms[i].localPosition, 0.3F);
                }
            }
        }
    }
}