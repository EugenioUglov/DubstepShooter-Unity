using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdEnemy : Enemy
{
    private int _scoreAdditionalOnDie = 1;
    
    
    private void Start()
    {
        Vector3 rotationDirection = Vector3.zero;
        
        Score score = GameObject.FindObjectOfType<Score>();

        int scoreValue = score.GetScore();
        
        int moveSpeed = scoreValue / 10 + 1;
        int rotationSpeed = scoreValue / 10;

        if (scoreValue >= 50)
        {
            rotationDirection = new Vector3(Random.Range(-5, 6),Random.Range(-5, 6), Random.Range(-15, 10));
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        if (scoreValue >= 40)
        {
            rotationDirection = new Vector3(Random.Range(-5, 6),Random.Range(-5, 6), Random.Range(-15, 10));
            GetComponent<SpriteRenderer>().color = Color.cyan;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (scoreValue >= 30)
        {
            rotationDirection = new Vector3(Random.Range(-5, 6),Random.Range(-5, 6), 0);
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (scoreValue >= 20)
        {
            rotationDirection = new Vector3(Random.Range(-10, 10),0, Random.Range(-15, 10));
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (scoreValue >= 10)
        {
            rotationDirection = new Vector3(0,0, Random.Range(-5, 6));
            GetComponent<SpriteRenderer>().color = Color.white;
        }
         
        
        GetComponent<Translator>().Speed = moveSpeed;
        GetComponent<Rotator>().Speed = rotationSpeed;
        GetComponent<Rotator>().Direction = rotationDirection;
        
        
        StartCoroutine(DestroyOnOutOfCameraView());
    }

    private IEnumerator DestroyOnOutOfCameraView()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        float width = renderer.bounds.size.x;
        Camera mainCamera = Camera.main;

        
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 
                mainCamera.transform.position.z));
        
        while (true)
        {
            float leftOutPosition = screenBounds.x * -1 - width / 2;
            
            if (transform.position.x <= leftOutPosition)
            {
                Destroy(gameObject);
            }
        
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    public override void TakeDamage(int damageValue)
    {
        _health -= damageValue;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Score score = GameObject.FindObjectOfType<Score>();
        CameraZoomManager cameraZoomManager = GameObject.FindObjectOfType<CameraZoomManager>();
        
        if (cameraZoomManager != null) cameraZoomManager.Zoom();
        if (score != null) score.AddScore(_scoreAdditionalOnDie);
        
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
