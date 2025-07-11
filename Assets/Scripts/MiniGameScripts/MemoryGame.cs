using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGame : MiniGame
{
    [Header("Канвас мини игры")]
    [SerializeField] private Canvas MiniGameCanvas;

    [Header("Настройки мини игры")]
    [SerializeField] private GameObject tilePrefab; // Префаб квадрата
    [SerializeField] private Transform gridParent; // Родитель для сетки
    [SerializeField] private int gridSize = 3; // Размер сетки (например, 3x3)
    [SerializeField] private Color highlightColor = Color.yellow; // Цвет подсветки
    [SerializeField] private int sequenceLength = 3; // Начальная длина последовательности
    [SerializeField] private int totalRounds = 5; // Общее количество раундов

    [Header("Механизм который будет приведен в действие при победе в мини игре")]
    [SerializeField] private GameObject Mechanism; // Префаб квадрата

    private SoundPlay soundPlay;
    private List<GameObject> tiles = new List<GameObject>(); // Список квадратов
    private List<int> sequence = new List<int>(); // Последовательность миганий
    private List<int> playerSequence = new List<int>(); // Последовательность игрока
    private int totalTiles; // Общее количество квадратов
    private int currentRound = 1; // Текущий раунд

    public override void StartMiniGame()
    {
        StartNewGame();
        MiniGameCanvas.enabled = true;
        playerController.enabled = false;
    }
    public override void StopMiniGame()
    {
        MiniGameCanvas.enabled = false;
        playerController.enabled = true;
    }
    public override void WinMiniGame()
    {
        Mechanism.GetComponent<Mechanism>().DoSomething();
        playerController.enabled = true;
        MiniGameCanvas.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }


    void StartNewGame() // Запуск новой игры
    {
        sequence.Clear();
        playerSequence.Clear();
        currentRound = 1;
        sequenceLength = 3; // Сброс длины последовательности
        GenerateSequence();
        ShowSequence();
    }

    void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
        totalTiles = gridSize * gridSize;
        GenerateGrid();
        //StartNewGame();
    }


    // Генерация сетки
    void GenerateGrid()
    {
        for (int i = 0; i < totalTiles; i++)
        {
            GameObject tile = Instantiate(tilePrefab, gridParent);
            int tileIndex = i; // Локальная копия индекса
            tile.GetComponent<Button>().onClick.AddListener(() => OnTileClick(tileIndex));
            tiles.Add(tile);
        }
    }

    // Генерация случайной последовательности
    void GenerateSequence()
    {
        sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            
            int randomIndex = Random.Range(0, totalTiles);
            sequence.Add(randomIndex);
        }
    }

    // Подсветка последовательности
    void ShowSequence()
    {
        StartCoroutine(HighlightSequence());
    }

    IEnumerator HighlightSequence()
    {
        // Создаем копию последовательности
        List<int> sequenceCopy = new List<int>(sequence);

        foreach (int index in sequenceCopy)
        {
            GameObject tile = tiles[index];
            Image tileImage = tile.GetComponent<Image>();
            Color originalColor = tileImage.color;

            tileImage.color = highlightColor; // Подсветка
            soundPlay.PlaySound(3);
            yield return new WaitForSeconds(0.5f);

            tileImage.color = originalColor; // Возврат цвета
            yield return new WaitForSeconds(0.2f);
        }

        playerSequence.Clear(); // Очищаем ввод игрока
    }

    // Обработка кликов игрока
    void OnTileClick(int index)
    {
        if (playerSequence.Count >= sequence.Count)
            return; // Игрок ввел больше, чем требуется

        playerSequence.Add(index);
        soundPlay.PlaySound(0);

        // Проверяем последовательность, если игрок ввел все элементы
        if (playerSequence.Count == sequence.Count)
        {
            CheckPlayerSequence();
        }
    }

    // Проверка последовательности игрока
    void CheckPlayerSequence()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            if (playerSequence[i] != sequence[i])
            {
                Debug.Log("Неправильная последовательность!");
                soundPlay.PlaySound(2);
                StartNewGame(); // Начать заново
                return;
            }
        }

        Debug.Log("Правильная последовательность!");
        NextRound();
    }

    // Переход к следующему раунду
    void NextRound()
    {
        if (currentRound >= totalRounds)
        {
            Debug.Log("Все раунды завершены! Победа!");

            soundPlay.PlaySound(1);

            WinMiniGame();
            // Здесь можно добавить любое событие, например, открытие двери или награду
            return;
        }

        currentRound++;
        sequenceLength++;
        GenerateSequence();
        ShowSequence();
    }
}
