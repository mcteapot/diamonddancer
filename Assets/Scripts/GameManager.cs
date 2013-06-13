using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public enum GameStates {
		gameInit = 0,
		gameStart = 1,
		gameActive = 2,
		gameEnding = 3,
		gameEnd = 4,
		gameInActive = 5
	}
	
	public enum GameLevels {
		level00 = 0,
		level01 = 1,
		level02 = 2,
		level03 = 3,
		level04 = 4
	}
	
	public GameLevels gameLevel = GameLevels.level00;
	public static GameStates gameState = GameStates.gameActive;
	
	public Transform edgeTopRight;
	public Transform edgeTopLeft;
	public Transform edgeBottomRight;
	public Transform edgeBottomLeft;
	
	public Color edgeColorNotActive;
	public Color edgeColorActive;
	
	public float edgeActiveDuration = 0.5F;
	
	private bool edgeTopRightActive;
	private bool edgeTopLeftActive;
	private bool edgeBottomRightActive;
	private bool edgeBottomLeftActive;
	
	private int maxActive = 2;
	
	public Transform levelCube01;
	public Transform levelCube02;
	public Transform levelCube03;
	public Transform levelCube04;
	
	public Transform sideTopRight;
	public Transform sideTopLeft;
	public Transform sideBottomRight;
	public Transform sideBottomLeft;
	
	public float sideGrowSpeed = 1.5F;
	
	
	// Use this for initialization
	void Start () {
		gameLevel = GameLevels.level01;
		
		edgeTopLeft.GetChild(0).renderer.material.color = edgeColorNotActive;
		edgeTopRight.GetChild(0).renderer.material.color = edgeColorNotActive;
		edgeBottomLeft.GetChild(0).renderer.material.color = edgeColorNotActive;
		edgeBottomRight.GetChild(0).renderer.material.color = edgeColorNotActive;
		
		sideTopRight.gameObject.active = false;
		sideTopLeft.gameObject.active = false;
		sideBottomRight.gameObject.active = false;
		sideBottomLeft.gameObject.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		gameManage();
		
	}
	
	
	
	void gameManage () {
		if(gameState == GameStates.gameInit){
		
		}else if(gameState == GameStates.gameStart) {

		}else if(gameState == GameStates.gameActive) {
			setEdgeActive();
			liteUpEdges();
			sideCubeActive();
			
			//move to level change
			setEdgePosition();
			setLevelCubePosition();

		
		}else if(gameState == GameStates.gameEnding) {

		
		}else if(gameState == GameStates.gameEnd) {

		
		}else if(gameState == GameStates.gameInActive) {

		}
	}
	
	void setEdgeActive () {
		
		if(Input.GetKey("w") || Input.GetKey("e")){
			edgeTopRightActive = true;
			//Debug.Log("TOP");
		}else {
			edgeTopRightActive = false;
		}
		
		if(Input.GetKey("a")) {
			edgeTopLeftActive = true;
			//Debug.Log("LEFT");
		}else {
			edgeTopLeftActive = false;
		}
		
		if(Input.GetKey("f")) {
			edgeBottomRightActive = true;
			//Debug.Log("RIGHT");
		}else {
			edgeBottomRightActive = false;
		}
		
		if(Input.GetKey("x") || Input.GetKey("c")) {
			edgeBottomLeftActive = true;
			//Debug.Log("BOTTOM");
		}else {
			edgeBottomLeftActive = false;
		}

	}
	
	void liteUpEdges() {
		if(edgeTopLeftActive) {
			edgeTopLeft.GetChild(0).renderer.material.color = edgeColorActive;
		}else {
			edgeTopLeft.GetChild(0).renderer.material.color = edgeColorNotActive;
		}
		
		if(edgeTopRightActive) {
			edgeTopRight.GetChild(0).renderer.material.color = edgeColorActive;
		}else {
			edgeTopRight.GetChild(0).renderer.material.color = edgeColorNotActive;
		}
		
		if(edgeBottomLeftActive) {
			edgeBottomLeft.GetChild(0).renderer.material.color = edgeColorActive;
		}else {
			edgeBottomLeft.GetChild(0).renderer.material.color = edgeColorNotActive;
		}
		
		if(edgeBottomRightActive) {
			edgeBottomRight.GetChild(0).renderer.material.color = edgeColorActive;
		}else {
			edgeBottomRight.GetChild(0).renderer.material.color = edgeColorNotActive;
		}
	}
	
	void setEdgePosition() {
		Vector3 topRightPos = new Vector3(0, 0, 0);
		Vector3 topLeftPos = new Vector3(0, 0, 0);
		Vector3 bottomRightPos = new Vector3(0, 0, 0);
		Vector3 bottomLeftPos = new Vector3(0, 0, 0);
		
		switch(gameLevel) {
		case GameLevels.level00 :
			topRightPos = new Vector3(0.625F, 0, 0.625F);
			topLeftPos = new Vector3(-0.625F, 0, 0.625F);
			bottomRightPos = new Vector3(0.625F, 0, -0.625F);
			bottomLeftPos = new Vector3(-0.625F, 0, -0.625F);
			break;
		case GameLevels.level01 :
			topRightPos = new Vector3(0.625F, 0, 0.625F);
			topLeftPos = new Vector3(-0.625F, 0, 0.625F);
			bottomRightPos = new Vector3(0.625F, 0, -0.625F);
			bottomLeftPos = new Vector3(-0.625F, 0, -0.625F);
			break;
		case GameLevels.level02 :
			topRightPos = new Vector3(1.625F, 0, 1.625F);
			topLeftPos = new Vector3(-1.625F, 0, 1.625F);
			bottomRightPos = new Vector3(1.625F, 0, -1.625F);
			bottomLeftPos = new Vector3(-1.625F, 0, -1.625F);
			break;
		case GameLevels.level03 :
			topRightPos = new Vector3(2.625F, 0, 2.625F);
			topLeftPos = new Vector3(-2.625F, 0, 2.625F);
			bottomRightPos = new Vector3(2.625F, 0, -2.625F);
			bottomLeftPos = new Vector3(-2.625F, 0, -2.625F);
			break;
		case GameLevels.level04 :
			topRightPos = new Vector3(3.625F, 0, 3.625F);
			topLeftPos = new Vector3(-3.625F, 0, 3.625F);
			bottomRightPos = new Vector3(3.625F, 0, -3.625F);
			bottomLeftPos = new Vector3(-3.625F, 0, -3.625F);
			break;
		default:
			break;
		}
		
	edgeTopRight.position = topRightPos;
	edgeTopLeft.position = topLeftPos;
	edgeBottomRight.position = bottomRightPos;
	edgeBottomLeft.position = bottomLeftPos;
	}
	
	
	void sideCubeActive() {
		
	}
	
	
	void setLevelCubePosition() {
		Vector3 leve1Pos = new Vector3(0, 0, 0);
		Vector3 leve2Pos = new Vector3(0, 0, 0);
		Vector3 leve3Pos = new Vector3(0, 0, 0);
		Vector3 leve4Pos = new Vector3(0, 0, 0);
		
		switch(gameLevel) {
		case GameLevels.level00 :
			leve1Pos = new Vector3(0, 4, 0);
			leve2Pos = new Vector3(0, 3, 0);
			leve3Pos = new Vector3(0, 2, 0);
			leve4Pos = new Vector3(0, 1, 0);
			break;
		case GameLevels.level01 :
			leve1Pos = new Vector3(0, 1, 0);
			leve2Pos = new Vector3(0, -1, 0);
			leve3Pos = new Vector3(0, -1, 0);
			leve4Pos = new Vector3(0, -1, 0);
			break;
		case GameLevels.level02 :
			leve1Pos = new Vector3(0, 2, 0);
			leve2Pos = new Vector3(0, 1, 0);
			leve3Pos = new Vector3(0, -1, 0);
			leve4Pos = new Vector3(0, -1, 0);
			break;
		case GameLevels.level03 :
			leve1Pos = new Vector3(0, 3, 0);
			leve2Pos = new Vector3(0, 2, 0);
			leve3Pos = new Vector3(0, 1, 0);
			leve4Pos = new Vector3(0, -1, 0);
			break;
		case GameLevels.level04 :
			leve1Pos = new Vector3(0, 4, 0);
			leve2Pos = new Vector3(0, 3, 0);
			leve3Pos = new Vector3(0, 2, 0);
			leve4Pos = new Vector3(0, 1, 0);
			break;
		default:
			break;
		}
		
		levelCube01.position = leve1Pos;
		levelCube02.position = leve2Pos;
		levelCube03.position = leve3Pos;
		levelCube04.position = leve4Pos;
	}
}
