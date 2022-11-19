using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private float offsetFromBorder = 2;
    [SerializeField] private GameObject _gameObjectToSpawn;
    [SerializeField] private Camera _mainCamera;
    private void Start()
    {
        FirstSpawn();
    }
    
    public void FirstSpawn()
    {
        Vector2 screenBounds = _mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 
                _mainCamera.transform.position.z));
        Vector3 newPosition = new Vector3(screenBounds.x * -1 + offsetFromBorder, -4.20f, 0);
        
        Instantiate(_gameObjectToSpawn, newPosition, Quaternion.identity);
    }
}
