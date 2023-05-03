using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // A classe BackgroundScroller é usada para rolar um fundo em loop.

    // O fundo é rolado verticalmente em velocidade constante (ScrollSpeed) usando uma textura que se repete (tiled texture).
    // O código salva a posição original do offset da textura (_savedOffset) e a atualiza continuamente no método Update()
    // para criar o efeito de movimento.
    // O método OnDisable() é usado para restaurar o offset da textura ao seu valor original (_savedOffset) quando o objeto é desativado.

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
