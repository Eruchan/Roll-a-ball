using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// This script works as a bridge between the level and the UI and also manages when the level is finished

public class LevelManager : MonoBehaviour {

	public int NumOfPokemonToCatch;

	// UI to show after the level was completed
	public GameObject CompleteLevelUI;

	// UI to show when the time runs out
	public GameObject GameOverUI;

	// Text to display how many pokemon there are left to catch
	public Text CountText;

	// Text to display how many time is left before losing
	public Text TimeText;

	// String to write before the actual number of pokemon left
	public string CountTextString;

	// String to write before the actual time left
	public string TimeTextString;

	// How many time the player has to complete the level in seconds
	public float LevelTime;

	private int NumCaughtPokemon;
	private float TimeLeft;
	private bool TimeIsRunning;
	

	// Use this for initialization
	void Start () {
		TimeLeft = LevelTime;
		CompleteLevelUI.SetActive(false);
		GameOverUI.SetActive(false);
		CountText.text = CountTextString + " " + NumOfPokemonToCatch;
		TimeIsRunning = true;
		TimeText.text = TimeTextString + " " + ConvertSecondsToTimeString(TimeLeft);
	
	}
	
	// Update is called once per frame
	void Update () {
		if(TimeIsRunning)
		{
			TimeLeft -= Time.deltaTime;
			if(TimeLeft <= 0)
			{
				TimeText.text = TimeTextString + " 0:00";
				TimeIsRunning = false;
				GameOverUI.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				TimeText.text = TimeTextString + " " + ConvertSecondsToTimeString(TimeLeft);
			}
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	string ConvertSecondsToTimeString(float seconds)
	{
		// Unity Answer Hub format seconds to a Minute:Seconds string
		string minSec = string.Format("{0}:{1:00}", (int)seconds / 60, (int)seconds % 60);

		return minSec;
	}

	public void PokemonCaught()
	{
		NumCaughtPokemon++;
		CountText.text = CountTextString + " " + (NumOfPokemonToCatch - NumCaughtPokemon);
		if(NumCaughtPokemon >= NumOfPokemonToCatch && TimeIsRunning)
		{
			CompleteLevel();
		}
	}

	void CompleteLevel()
	{
		CompleteLevelUI.SetActive(true);
		TimeIsRunning = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void LoadNextLevel()
	{
		int CurrentLevelIndex = Application.loadedLevel;
		Application.LoadLevel(CurrentLevelIndex + 1);
	}

	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void LoadLevel(int LevelIndex)
	{
		Application.LoadLevel(LevelIndex);
	}

	public bool IsRunningTime()
	{
		return TimeIsRunning;
	}
}
