using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreObject;
    [SerializeField] private TextMeshProUGUI _gratulationTextMesh;

    private int _scoreValue = 0;
    private bool _isGratulateTextBlinking = false;

    private string _mainMusicName = "Dubstep compilation";

    public void AddScore(int valueToAdd = 1)
    {
        if (valueToAdd < 0)
        {
            print("Warning! You are sending value < 0 to function AddScore()");
        }

        int previousScore = _scoreValue;
        _scoreValue += valueToAdd;
        
        _scoreObject.text = _scoreValue.ToString();

        GratulatePlayer(previousScore);
        
    }
    
    public void DecreaseScore(int valueToDecrease = 1)
    {
        if(_scoreValue <= 0) return;
        int previousScore = _scoreValue;
        int newScoreValue = previousScore - valueToDecrease;
        _scoreValue = GetNormalizedScore(newScoreValue);

        _scoreObject.text = _scoreValue.ToString();
        GratulatePlayer(previousScore);
    }

    public int GetScore()
    {
        return _scoreValue;
    }

    
    
    private int GetNormalizedScore(int scoreValue)
    {
        int normalizedScore = scoreValue < 0 ? 0 : scoreValue;

        return normalizedScore;
    }

    private void GratulatePlayer(int previousScoreValue)
    {
        if (_scoreValue >= 100 && previousScoreValue < 100)
        {
            FindObjectOfType<AudioManager>().Play("Dubstep drop bass short");
            _gratulationTextMesh.text = "PSYCHO!";
        }
        else if (_scoreValue >= 50 && previousScoreValue < 50)
        {
            FindObjectOfType<AudioManager>().Play("Dubstep drop bass short");

            _gratulationTextMesh.text = "REAL DUBSTEAPER!";
        }
        else if (_scoreValue >= 40 && previousScoreValue < 40)
        {
            FindObjectOfType<AudioManager>().Play("Dubstep drop bass short");

            _gratulationTextMesh.text = "INCREDIBLE!";
        }
        else if (_scoreValue >= 30 && previousScoreValue < 30)
        {
            FindObjectOfType<AudioManager>().Play("Dubstep drop bass short");

            _gratulationTextMesh.text = "AWESOME!";
        }
        else if (_scoreValue >= 20 && previousScoreValue < 20)
        {
            FindObjectOfType<AudioManager>().Play("Dubstep drop bass short");

            _gratulationTextMesh.text = "GREAT!";
        }
        else if (_scoreValue >= 10 && previousScoreValue < 10)
        {
            FindObjectOfType<AudioManager>().Play("Dubstep drop bass short");

            _gratulationTextMesh.text = "GOOD!";
        }
        else if (_scoreValue % 10 == 9 && previousScoreValue > _scoreValue)
        {
            _gratulationTextMesh.text = "Try harder";
            StartCoroutine(BlinkGratulationText());
            return;
        }
        else
        {
            return;
        }


        StressReceiver stressReceiver = GameObject.FindObjectOfType<StressReceiver>();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
            
        audioManager.ChangePitch(_mainMusicName, 0.9f);
        
            
        StartCoroutine(GratulateCoroutine());
            
        if (stressReceiver != null) stressReceiver.InduceStress(1);
       
    }
    
    private IEnumerator GratulateCoroutine()
    {
        if (_isGratulateTextBlinking) yield return null;
        
        _isGratulateTextBlinking = true;
        
        AudioManager audioManager = FindObjectOfType<AudioManager>();


        for (int i = 0; i <= 3; i++)
        {
            _gratulationTextMesh.enabled = true;
            yield return new WaitForSeconds(0.2f);
            _gratulationTextMesh.enabled = false;
            yield return new WaitForSeconds(0.2f);
        }
        
        _gratulationTextMesh.text = "";
        audioManager.ChangePitch(_mainMusicName, 1);
        
        _isGratulateTextBlinking = false;
    }
    
    private IEnumerator BlinkGratulationText()
    {
        if (_isGratulateTextBlinking) yield return null;
        
        _isGratulateTextBlinking = true;
    
        for (int i = 0; i <= 3; i++)
        {
            _gratulationTextMesh.enabled = true;
            yield return new WaitForSeconds(0.2f);
            _gratulationTextMesh.enabled = false;
            yield return new WaitForSeconds(0.2f);
        }

        _gratulationTextMesh.text = "";
        
        _isGratulateTextBlinking = false;
    }

    private IEnumerator Blink(GameObject gameObject, int countBlinks = 3, float delayTimeBetweenBlinking = 0.2f)
    {
        for (int i = 0; i <= countBlinks; i++)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(delayTimeBetweenBlinking);
            gameObject.SetActive(false);
            yield return new WaitForSeconds(delayTimeBetweenBlinking);
        }
    }
}