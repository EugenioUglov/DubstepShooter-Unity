using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoomManager : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomSpeed = 10;
    [SerializeField] private float _zoomFactor = 1f;
    private float _beginningZoom;
    private float _targetZoom;
    

    private void Awake()
    {
        _beginningZoom = _camera.orthographicSize;
        _targetZoom = _beginningZoom - _zoomFactor;
    }

    public void Zoom()
    {
        StartCoroutine(ZoomCoroutine());
    }

    
    private IEnumerator ZoomCoroutine()
    {
        bool isZooming = true;

        while (true)
        {
            if (isZooming)
            {
                // _camera.orthographicSize =
                //     Mathf.Lerp(_camera.orthographicSize, _targetZoom, Time.deltaTime * zoomLerpSpeed);
                _camera.orthographicSize = Mathf.MoveTowards(_camera.orthographicSize, _targetZoom, _zoomSpeed * Time.deltaTime);

                if (_camera.orthographicSize <= _targetZoom)
                {
                    isZooming = false;
                }
            }
            else
            {
                // Unzooming.

                _camera.orthographicSize = _beginningZoom;
                
                if (_camera.orthographicSize >= _targetZoom)
                {
                    yield break;
                }
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
