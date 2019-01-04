using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour {

	public string startLevel;

	public string levelSelect;

	public void NewGame()
	{
		SceneManager.LoadScene (startLevel);
	}

	public void LevelSelect()
	{
		SceneManager.LoadScene (levelSelect);
	}

	public void QuitGame()
	{
		Debug.Log ("Game Exited");
		Application.Quit();
	}

}
