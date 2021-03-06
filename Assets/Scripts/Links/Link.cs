﻿using UnityEngine;

public abstract class Link<TProperty, T> : MonoBehaviour
    where TProperty : Property<T>
{
    [SerializeField]
    private TProperty target;

    private void OnEnable()
    {
        SetValue(target.Value);
        target.Changed += OnTargetChanged;
    }

    private void OnDisable()
    {
        target.Changed -= OnTargetChanged;
    }

    private void OnTargetChanged(object sender, PropertyChangedEventArgs<T> e)
    {
        SetValue(e.Value);
    }

    protected abstract void SetValue(T value);

#if UNITY_EDITOR
    private void Reset()
    {
        target = FindTarget(transform);
    }

    private TProperty FindTarget(Transform transform)
    {
        if (transform == null)
            return null;

        return transform.GetComponent<TProperty>() ?? FindTarget(transform.parent);
    }
#endif
}
