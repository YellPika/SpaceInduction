using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public sealed class BillboardBehaviour : MonoBehaviour
{
    private PlayerBehaviour player;

    [SerializeField]
    private Color color;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float maxDistance;

    private void Awake()
    {
        player = FindSceneObjectsOfType(typeof(PlayerBehaviour))
            .Cast<PlayerBehaviour>()
            .Single();
    }

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        var alpha = 1 - Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
        renderer.material.SetColor("_Color", new Color(alpha, alpha, alpha, alpha) * color);
    }
}
