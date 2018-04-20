﻿using UnityEngine;
using UnityEngine.UI;

namespace TweenPeaks.Tweens
{
    public class ColorFlashTween : TweenBase
    {
        private SpriteRenderer _spriteRenderer;
        private Graphic _graphic;
        private Color _startColor;
        private Color _targetColor;

        public static ColorFlashTween Run(GameObject item, Color color, float duration)
        {
            ColorFlashTween tween = Create<ColorFlashTween>(item, duration);
            tween._targetColor = color;
            return tween;
        }

        protected override void OnStart()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(_spriteRenderer != null)
                _startColor = _spriteRenderer.color;

            _graphic = gameObject.GetComponent<Graphic>();
            if (_graphic != null)
                _startColor = _graphic.color;
        }

        protected override void UpdateValue(float time)
        {
            time = EaseFunc(0f, 1f, time);
            Color color = time <= 0.5f
                ? Color.Lerp(_startColor, _targetColor, time*2f)
                : Color.Lerp(_targetColor, _startColor, (time - 0.5f)*2f);

            if (_spriteRenderer != null)
                _spriteRenderer.color = color;

            if (_graphic != null)
                _graphic.color = color;
        }
    }
}
