using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGenerator : MonoBehaviour
{
    private static LevelGenerator instance;

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartEasyList;
    [SerializeField] private List<Transform> levelPartMediumList;
    [SerializeField] private List<Transform> levelPartHardList;
    [SerializeField] private Transform player;

    private int score;
    public TextMeshProUGUI scoreText;

    private enum Difficulty
    {
         Easy,
         Medium,
         Hard,
    }

    private Vector3 lastEndPosition;
    private int levelPartsSpawned;

    private void Awake()
    {
        instance = this;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 2;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        score = GetLevelPartSpawned();
        scoreText.text = score.ToString("0");

        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            //Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        List<Transform> difficultyLevelPartList;

        switch (GetDifficulty())
        {
            default:
            case Difficulty.Easy:   difficultyLevelPartList = levelPartEasyList; break;
            case Difficulty.Medium: difficultyLevelPartList = levelPartMediumList; break;
            case Difficulty.Hard:   difficultyLevelPartList = levelPartHardList; break;

        }

        Transform chosenLevelPart = difficultyLevelPartList[Random.Range(0, difficultyLevelPartList.Count)];

        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        levelPartsSpawned++;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private Difficulty GetDifficulty()
    {
        if (levelPartsSpawned >= 32)
        {
            return Difficulty.Hard;
        }

        if (levelPartsSpawned >= 17)
        {
            return Difficulty.Medium;
        }

        return Difficulty.Easy;
    }

    public static int GetLevelPartSpawned()
    {
        return instance.levelPartsSpawned;
    }
}
