using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float ScrollSpeed = 0.05f;
    private Vector2 _savedOffset;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _savedOffset = _renderer.material.mainTextureOffset;
    }

    private void Update()
    {
        float y = Mathf.Repeat(Time.time * ScrollSpeed, 1);
        Vector2 offset = new Vector2(_savedOffset.x, y);
        _renderer.material.mainTextureOffset = offset;
    }

    private void OnDisable()
    {
        _renderer.material.mainTextureOffset = _savedOffset;
    }
}
