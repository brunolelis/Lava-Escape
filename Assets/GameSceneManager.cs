using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
	[SerializeField] private GameObject highscore;
	[SerializeField] private GameObject retry, menu;

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		highscore.SetActive(false);
		Time.timeScale = 1f;
		retry.SetActive(false);
		menu.SetActive(false);
	}

	public void Menu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}

	public void Play()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}

	public void History()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(2);
	}

	public void Time1()
	{
		Time.timeScale = 1f;
	}

	public void Time0()
	{
		Time.timeScale = 0f;
	}

	public void Tutorial()
	{
		SceneManager.LoadScene(3);
	}

	public void Hard()
	{
		SceneManager.LoadScene(4);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
