using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHard : MonoBehaviour
{
    [SerializeField] private GameObject retry, menu;

    public float velocity = 3f;

    public AudioClip lavaClip, dieClip;

    private void Update()
    {
        int score = LevelGeneratorHard.GetLevelPartSpawnedHard();
        if (score >= 25)
        {
            velocity = 5.3f;
        }

        if (score <= 25)
        {
            if (score >= 10)
            {
                velocity = 4.5f;
            }
        }

        if (score == 10)
        {
            SoundManager.instance.PlayLava(lavaClip);
        }

        if (score == 25)
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
        if (collision.gameObject.name == "Player")
        {
            SoundManager.instance.PlayDie(dieClip);
            Time.timeScale = 0f;

            int finalScore = LevelGeneratorHard.GetLevelPartSpawnedHard();
            HighscoreNameInputWindow.Show(finalScore, (string name) => {
                HighscoreTableHard.AddHighscoreEntry_Static(finalScore, name);
                HighscoreTableHard.Show();

                retry.SetActive(true);
                menu.SetActive(true);
            });
        }
    }
}
