﻿using UnityEngine;

namespace Assets.Scripts.Utils.TweenPeaks.Tweens
{
    public class MoveToTween : TweenBase
    {
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private bool _isLocal;
        private float? _targetX;
        private float? _targetY;
        private float? _targetZ;

        private bool _isAnchoredPosition;
        private RectTransform _rectTransform;

        public static MoveToTween Run(GameObject item, Vector3 position, float duration)
        {
            MoveToTween tween = Create<MoveToTween>(item, duration);
            tween._targetPosition = position;
            return tween;
        }

        public static MoveToTween Run(RectTransform rectTransform, Vector3 anchoredPosition, float duration)
        {
            MoveToTween tween = Create<MoveToTween>(rectTransform.gameObject, duration);
            tween._isAnchoredPosition = true;
            tween._targetPosition = anchoredPosition;
            tween._rectTransform = rectTransform;
            return tween;
        }

        public static MoveToTween RunX(GameObject item, float x, float duration)
        {
            MoveToTween tween = Create<MoveToTween>(item, duration);
            tween._targetX = x;
            return tween;
        }

        public static MoveToTween RunY(GameObject item, float y, float duration)
        {
            MoveToTween tween = Create<MoveToTween>(item, duration);
            tween._targetY = y;
            return tween;
        }

        public static MoveToTween RunZ(GameObject item, float z, float duration)
        {
            MoveToTween tween = Create<MoveToTween>(item, duration);
            tween._targetZ = z;
            return tween;
        }

        public MoveToTween SetLocal(bool isLocal)
        {
            _isLocal = isLocal;
            return this;
        }

        protected override void OnStart()
        {
            _startPosition = GetStartPosition();

            if (_targetX.HasValue)
                _targetPosition = new Vector3(_targetX.Value, _startPosition.y, _startPosition.z);
            else if (_targetY.HasValue)
                _targetPosition = new Vector3(_startPosition.x, _targetY.Value, _startPosition.z);
            else if (_targetZ.HasValue)
                _targetPosition = new Vector3(_startPosition.x, _startPosition.y, _targetZ.Value);
        }

        private Vector3 GetStartPosition()
        {
            if (_isAnchoredPosition)
                return _rectTransform.anchoredPosition;

            if (_isLocal)
                return transform.localPosition;

            return transform.position;
        }

        protected override void UpdateValue(float time)
        {
            Vector3 nextPosition = new Vector3(
                EaseFunc(_startPosition.x, _targetPosition.x, time),
                EaseFunc(_startPosition.y, _targetPosition.y, time),
                EaseFunc(_startPosition.z, _targetPosition.z, time));

            if (_isAnchoredPosition)
            {
                _rectTransform.anchoredPosition = nextPosition;
            }
            else
            {
                if (_isLocal)
                    transform.localPosition = nextPosition;
                else
                    transform.position = nextPosition;
            }
        }
    }
}
