using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {
	public string scene;

	void OnMouseDown()
	{
		SceneManager.LoadScene(scene);
	}
}