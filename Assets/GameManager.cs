﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public enum GAME_EVENTS{NONE, BALL};
public class GameManager : MonoBehaviour {

	public GAME_EVENTS gameEvents;
	public GAME_EVENTS victoryCondition;
	public static GameManager instance;

	public int currentLevel = 0;

	public GameObject test;
	public GameObject rootObject;
	public GameObject objectToTeleportNextScene;
	public GameObject player;
	public GameObject beginCube;
	public GameObject endCube;

	public GameObject[] scenesRoot = new GameObject[3];


	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this.gameObject);
		}
	}

	private void Start() {
		StartCoroutine(LoadNextLevelAsync());
	}

	AsyncOperation ao;
	IEnumerator LoadNextLevelAsync()
	{
		ao = SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
		yield return ao;
	}

	public void LoadNextLevel() {
		//currentLevel++;
		SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
	}

	public void SetCubes(GameObject cubeBegin, GameObject cubeEnd) {
		beginCube = cubeBegin;
		endCube = cubeEnd;
	}

	public void SetPlayer(GameObject player) {
		player = player;
	}

	public void TeleportNextScene() {
		rootObject.transform.parent = test.transform;
		rootObject.SetActive(false);
	}

	bool doneLoadingScene = false;
	public void Update() {
		if(ao != null && !doneLoadingScene)
		{
			if(ao.isDone)
			{
				doneLoadingScene = true;
				TeleportNextScene();
			}
		}

		if(gameEvents == victoryCondition) {
			// Joué l'animation du lampadaire enflammée
			// Désactivée la collision de fin de niveau.
			// 
			// Débloqué le lampadaire, virée la collision de fin de niveau et load le level.
		}
	}
}