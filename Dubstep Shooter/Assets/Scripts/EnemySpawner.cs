using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float yRandomRange;
    [SerializeField] private GameObject _gameObjectToSpawn;
    [SerializeField] private Camera _mainCamera;
    
    private void Start()
    {
        InvokeRepeating( "Spawn", 0f, 1f );
    }
    
    public void Spawn()
    {
        Vector2 screenBounds = _mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 
                _mainCamera.transform.position.z));
        Vector3 newPosition = new Vector3(screenBounds.x + 1, Random.Range(yRandomRange * -1, yRandomRange), 0);
        
        Instantiate(_gameObjectToSpawn, newPosition, Quaternion.identity);
    }
}