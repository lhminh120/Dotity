using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameManager _gameManager;
    private void Awake()
    {
        var gameData = Instantiate(_gameData, transform);
        var gameManager = Instantiate(_gameManager, transform);
    }
}