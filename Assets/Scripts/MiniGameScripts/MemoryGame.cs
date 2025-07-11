using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGame : MiniGame
{
    [Header("������ ���� ����")]
    [SerializeField] private Canvas MiniGameCanvas;

    [Header("��������� ���� ����")]
    [SerializeField] private GameObject tilePrefab; // ������ ��������
    [SerializeField] private Transform gridParent; // �������� ��� �����
    [SerializeField] private int gridSize = 3; // ������ ����� (��������, 3x3)
    [SerializeField] private Color highlightColor = Color.yellow; // ���� ���������
    [SerializeField] private int sequenceLength = 3; // ��������� ����� ������������������
    [SerializeField] private int totalRounds = 5; // ����� ���������� �������

    [Header("�������� ������� ����� �������� � �������� ��� ������ � ���� ����")]
    [SerializeField] private GameObject Mechanism; // ������ ��������

    private SoundPlay soundPlay;
    private List<GameObject> tiles = new List<GameObject>(); // ������ ���������
    private List<int> sequence = new List<int>(); // ������������������ �������
    private List<int> playerSequence = new List<int>(); // ������������������ ������
    private int totalTiles; // ����� ���������� ���������
    private int currentRound = 1; // ������� �����

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


    void StartNewGame() // ������ ����� ����
    {
        sequence.Clear();
        playerSequence.Clear();
        currentRound = 1;
        sequenceLength = 3; // ����� ����� ������������������
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


    // ��������� �����
    void GenerateGrid()
    {
        for (int i = 0; i < totalTiles; i++)
        {
            GameObject tile = Instantiate(tilePrefab, gridParent);
            int tileIndex = i; // ��������� ����� �������
            tile.GetComponent<Button>().onClick.AddListener(() => OnTileClick(tileIndex));
            tiles.Add(tile);
        }
    }

    // ��������� ��������� ������������������
    void GenerateSequence()
    {
        sequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            
            int randomIndex = Random.Range(0, totalTiles);
            sequence.Add(randomIndex);
        }
    }

    // ��������� ������������������
    void ShowSequence()
    {
        StartCoroutine(HighlightSequence());
    }

    IEnumerator HighlightSequence()
    {
        // ������� ����� ������������������
        List<int> sequenceCopy = new List<int>(sequence);

        foreach (int index in sequenceCopy)
        {
            GameObject tile = tiles[index];
            Image tileImage = tile.GetComponent<Image>();
            Color originalColor = tileImage.color;

            tileImage.color = highlightColor; // ���������
            soundPlay.PlaySound(3);
            yield return new WaitForSeconds(0.5f);

            tileImage.color = originalColor; // ������� �����
            yield return new WaitForSeconds(0.2f);
        }

        playerSequence.Clear(); // ������� ���� ������
    }

    // ��������� ������ ������
    void OnTileClick(int index)
    {
        if (playerSequence.Count >= sequence.Count)
            return; // ����� ���� ������, ��� ���������

        playerSequence.Add(index);
        soundPlay.PlaySound(0);

        // ��������� ������������������, ���� ����� ���� ��� ��������
        if (playerSequence.Count == sequence.Count)
        {
            CheckPlayerSequence();
        }
    }

    // �������� ������������������ ������
    void CheckPlayerSequence()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            if (playerSequence[i] != sequence[i])
            {
                Debug.Log("������������ ������������������!");
                soundPlay.PlaySound(2);
                StartNewGame(); // ������ ������
                return;
            }
        }

        Debug.Log("���������� ������������������!");
        NextRound();
    }

    // ������� � ���������� ������
    void NextRound()
    {
        if (currentRound >= totalRounds)
        {
            Debug.Log("��� ������ ���������! ������!");

            soundPlay.PlaySound(1);

            WinMiniGame();
            // ����� ����� �������� ����� �������, ��������, �������� ����� ��� �������
            return;
        }

        currentRound++;
        sequenceLength++;
        GenerateSequence();
        ShowSequence();
    }
}
