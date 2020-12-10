using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    [SerializeField] private GameObject retry, menu;

    public float velocity = 2.5f;

    public AudioClip lavaClip, dieClip;

    private void Update()
    {
        int score = LevelGenerator.GetLevelPartSpawned();
        if(score >= 32)
        {
            velocity = 5f;
        }

        if(score <= 32)
        {
            if (score >= 17)
            {
                velocity = 3.8f;
            }
        }

        if(score == 17)
        {
            SoundManager.instance.PlayLava(lavaClip);
        }

        if(score == 32)
        {
            SoundManager.instance.PlayLava(lavaClip);
        }

        transform.position += Vector3.up * velocity * Time.deltaTime;
    }

    public void ChangeVelocity4()
    {
        velocity = 4f;
    }

    public void ChangeVelocity5()
    {
        velocity = 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            SoundManager.instance.PlayDie(dieClip);
            Time.timeScale = 0f;

            int finalScore = LevelGenerator.GetLevelPartSpawned();
            HighscoreNameInputWindow.Show(finalScore, (string name) => {
                HighscoreTable.AddHighscoreEntry_Static(finalScore, name);
                HighscoreTable.Show();

                retry.SetActive(true);
                menu.SetActive(true);
            });
        }
    }
}
