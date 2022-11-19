using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 40;
    [SerializeField] public Rigidbody2D _rb;
    
    void Start()
    {
        // Go right.
        _rb.velocity = transform.up * _speed * -1;
        StartCoroutine(DestroyOnOutOfCameraView());
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
        }
        
        Destroy(gameObject);
    }
    private IEnumerator DestroyOnOutOfCameraView()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        float width = renderer.bounds.size.x;
        float height = renderer.bounds.size.y;
        Camera mainCamera = Camera.main;

        
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 
                mainCamera.transform.position.z));
        
        while (true)
        {
            float leftOutPosition = screenBounds.x + width / 2;
            float topOutPosition = screenBounds.y + width / 2;

            if (transform.position.x >= leftOutPosition || transform.position.y >= topOutPosition)
            {
                FindObjectOfType<Score>().DecreaseScore();
                Destroy(gameObject);
            }
        
            yield return new WaitForSeconds(0.01f);
        }
    }
}
