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
	public static GameStates gameState = GameStates.gameInActive;
	
	public Transform edgeTopRight;
	public Transform edgeTopLeft;
	public Transform edgeBottomRight;
	public Transform edgeBottomLeft;
	
	public Color edgeColorNotActive;
	public Color edgeColorActive;
	
	//public float edgeActiveDuration = 0.5F;
	
	private bool edgeTopRightActive;
	private bool edgeTopLeftActive;
	private bool edgeBottomRightActive;
	private bool edgeBottomLeftActive;
	
	public int maxActive = 2;
	
	public Transform levelCube01;
	public Transform levelCube02;
	public Transform levelCube03;
	public Transform levelCube04;
	
	public Transform sideTopRight;
	public Transform sideTopLeft;
	public Transform sideBottomRight;
	public Transform sideBottomLeft;
	
	public float sideGrowSpeed = 3.0F;
	
	private float nextLevelIncrement = 40.0F;
	private float tmpnNextLevelIncrement = 40.0F;
	private float nextLevelTime = 0.0F;
	
	private float nextBallIncrement = 15.0F;
	private float tmpnNextBallIncrement = 15.0F;
	private float nextBallTime = 0.0F;

	private ArrayList ballActive = new ArrayList();	
	
	public Transform gameLogo;
	public Transform ball;
	
	public Transform audioMusic;
	
	public AudioClip music1;
	public AudioClip music2;
	public AudioClip music3;
	public AudioClip music4;
	
	//public AudioClip startAnounce;
	public AudioClip endGameAudio;
	
	public Transform levelUpMusic;
	
	public Transform startAnouncer;
	
	private bool startAudio = false;
	
	public void endGame () {
		gameState = GameStates.gameEnding;
	}
	
	// Use this for initialization
	void Start () {
		
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
			gameState = GameStates.gameActive;
		}else if(gameState == GameStates.gameActive) {
			setEdgeActive();
			liteUpEdges();
			setSideActive();
			createBallsActive();
			levelIncrease();
			
		}else if(gameState == GameStates.gameEnding) {
			audioMusic.audio.Stop();
			
			audio.PlayOneShot(endGameAudio);
		
			startAudio = false;
			
			edgeTopLeft.GetChild(0).renderer.material.color = edgeColorNotActive;
			edgeTopRight.GetChild(0).renderer.material.color = edgeColorNotActive;
			edgeBottomLeft.GetChild(0).renderer.material.color = edgeColorNotActive;
			edgeBottomRight.GetChild(0).renderer.material.color = edgeColorNotActive;

			gameState = GameStates.gameEnd;
		}else if(gameState == GameStates.gameEnd) {
			gameLogo.gameObject.active = true;
			gameLevel = GameLevels.level00;
			clearAllBalls();
			gameState = GameStates.gameInActive;
		
		}else if(gameState == GameStates.gameInActive) {
			gameStartCheck();
		}
	}
	
	void gameStartCheck () {
		if(Input.GetKey("s") && Input.GetKey("d")) {
			gameLogo.gameObject.active = false;
			tmpnNextLevelIncrement = nextLevelIncrement;
			tmpnNextBallIncrement = nextBallIncrement;
			gameState = GameStates.gameStart;
		}
	}
	IEnumerator startAudioPlay () {
		
		//audio.PlayOneShot(startAnounce);
		startAnouncer.audio.Play();
		
		yield return new WaitForSeconds(0.5F);
		audioMusic.audio.clip = music1;
		audioMusic.audio.Play();
		
	}
	void levelIncrease () {
		if(gameLevel == GameLevels.level00) {
			gameLevel = GameLevels.level01;
			nextLevelTime = Time.time + tmpnNextLevelIncrement;

			if(!startAudio) {
				StartCoroutine(startAudioPlay());
				startAudio = true;
			}
			
			//move to level change
			setSideCubePositionAndScale();
			setEdgePosition();
			setLevelCubePosition();
		}
		
		if(Time.time > nextLevelTime && !(gameLevel == GameLevels.level04)) {
			if(gameLevel == GameLevels.level01) {
				clearAllBalls();
				gameLevel = GameLevels.level02;
				//tmpnNextLevelIncrement = tmpnNextLevelIncrement * 0.75F;
				nextLevelTime = Time.time + tmpnNextLevelIncrement;
				
				
				levelUpMusic.audio.Play();
				
				audioMusic.audio.clip = music2;
				audioMusic.audio.Play();
				
				//move to level change
				setSideCubePositionAndScale();
				setEdgePosition();
				setLevelCubePosition();
				ballActive.Add(createBall());
				
			} else if(gameLevel == GameLevels.level02) {
				clearAllBalls();
				gameLevel = GameLevels.level03;
				//tmpnNextLevelIncrement = tmpnNextLevelIncrement * 0.75F;
				nextLevelTime = Time.time + tmpnNextLevelIncrement;
				
				levelUpMusic.audio.Play();
				
				audioMusic.audio.clip = music3;
				audioMusic.audio.Play();
				
				//move to level change
				setSideCubePositionAndScale();
				setEdgePosition();
				setLevelCubePosition();
				ballActive.Add(createBall());
				
			} else if(gameLevel == GameLevels.level03) {
				clearAllBalls();
				gameLevel = GameLevels.level04;
				
				levelUpMusic.audio.Play();
				
				audioMusic.audio.clip = music4;
				audioMusic.audio.Play();
				
				//move to level change
				setSideCubePositionAndScale();
				setEdgePosition();
				setLevelCubePosition();
				ballActive.Add(createBall());
				
			}
		}
		
		
		
	}
	
	void createBallsActive () {
		if(gameLevel == GameLevels.level00) {
			ballActive.Add(createBall());
			//nextBallTime = Time.time + tmpnNextBallIncrement;
			
		}
		
		if(Time.time > nextBallTime) {
			ballActive.Add(createBall());
			//nextBallTime = Time.time + tmpnNextBallIncrement;
		}
	}
	
	Transform createBall () {
		Transform ballNew = Instantiate(ball, new Vector3(0, 0.5F, 0), Quaternion.identity) as Transform;
		nextBallTime = Time.time + tmpnNextBallIncrement;
		return ballNew;
	}
	
	void clearAllBalls () {
		for(int i = 0; i < ballActive.Count; i++) {
			Transform activeBall = ballActive[i] as Transform;
			Destroy(activeBall.gameObject);
		}
		ballActive.Clear();
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
	
	
	void liteUpEdges () {
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
	
	void setSideActive () {
		
		int tmpActive = 0;
		
		Vector3 startScale = new Vector3(0, 1.0F, 1.0F);
		Vector3 endScale = new Vector3(1.0F, 1.0F, 1.0F);
		
		// SIDE TOP RIGHT
		if(edgeTopRightActive && edgeBottomRightActive) {
	
			if(tmpActive < maxActive) {
				tmpActive = tmpActive + 1;
				sideTopRight.gameObject.active = true;
				sideTopRight.localScale = Vector3.MoveTowards(sideTopRight.localScale, endScale, Time.deltaTime * sideGrowSpeed);
			} else {
				sideTopRight.localScale = startScale;
				sideTopRight.gameObject.active = false;
			}
		} else {
			sideTopRight.localScale = startScale;
			sideTopRight.gameObject.active = false;
		}
		
		// SIDE BOTTOM LEFT
		if(edgeTopLeftActive && edgeBottomLeftActive) {
			if(tmpActive < maxActive) {
				tmpActive = tmpActive + 1;
			
				sideBottomLeft.gameObject.active = true;
				sideBottomLeft.localScale = Vector3.MoveTowards(sideBottomLeft.localScale, endScale, Time.deltaTime * sideGrowSpeed);
			} else {
				sideBottomLeft.localScale = startScale;
				sideBottomLeft.gameObject.active = false;				
			}
		} else {
			sideBottomLeft.localScale = startScale;
			sideBottomLeft.gameObject.active = false;
		}
		
		// SIDE TOP LEFT
		if(edgeTopLeftActive && edgeTopRightActive && (tmpActive < maxActive)) {
			if(tmpActive < maxActive) {
				tmpActive = tmpActive + 1;
				sideTopLeft.gameObject.active = true;
				sideTopLeft.localScale = Vector3.MoveTowards(sideTopLeft.localScale, endScale, Time.deltaTime * sideGrowSpeed);				
			} else  {
				sideTopLeft.localScale = startScale;
				sideTopLeft.gameObject.active = false;
			}

		} else {
			sideTopLeft.localScale = startScale;
			sideTopLeft.gameObject.active = false;
		}
		
		// SIDE BOTTOM RIGHT
		if(edgeBottomLeftActive && edgeBottomRightActive && (tmpActive < maxActive)) {
			if(tmpActive < maxActive) {
				tmpActive = tmpActive + 1;
				sideBottomRight.gameObject.active = true;
				sideBottomRight.localScale = Vector3.MoveTowards(sideBottomRight.localScale, endScale, Time.deltaTime * sideGrowSpeed);
			} else  {
				sideBottomRight.localScale = startScale;
				sideBottomRight.gameObject.active = false;	
			}
		} else {
			sideBottomRight.localScale = startScale;
			sideBottomRight.gameObject.active = false;
		}
		
	}
	
	void setEdgePosition () {
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
	
	
	void setSideCubePositionAndScale () {
		Vector3 topRightPos = new Vector3(0, 0, 0);
		Vector3 topLeftPos = new Vector3(0, 0, 0);
		Vector3 bottomRightPos = new Vector3(0, 0, 0);
		Vector3 bottomLeftPos = new Vector3(0, 0, 0);
		
		Vector3 cubeScale = new Vector3(0, 0, 0);
		
		switch(gameLevel) {
		case GameLevels.level00 :
			topRightPos = new Vector3(0.6F, 0, 0);
			topLeftPos = new Vector3(0, 0, 0.6F);
			bottomRightPos = new Vector3(0, 0, -0.6F);
			bottomLeftPos = new Vector3(-0.6F, 0, 0);
			
			cubeScale = new Vector3(0, 0, 0);
	
			break;
		case GameLevels.level01 :
			topRightPos = new Vector3(0.6F, 0, 0);
			topLeftPos = new Vector3(0, 0, 0.6F);
			bottomRightPos = new Vector3(0, 0, -0.6F);
			bottomLeftPos = new Vector3(-0.6F, 0, 0);
			
			cubeScale = new Vector3(1.5F, 0.6F, 0.3F);
			
			break;
		case GameLevels.level02 :
			topRightPos = new Vector3(1.6F, 0, 0);
			topLeftPos = new Vector3(0, 0, 1.6F);
			bottomRightPos = new Vector3(0, 0, -1.6F);
			bottomLeftPos = new Vector3(-1.6F, 0, 0);
			
			cubeScale = new Vector3(3.5F, 0.6F, 0.3F);
			
			break;
		case GameLevels.level03 :
			topRightPos = new Vector3(2.6F, 0, 0);
			topLeftPos = new Vector3(0, 0, 2.6F);
			bottomRightPos = new Vector3(0, 0, -2.6F);
			bottomLeftPos = new Vector3(-2.6F, 0, 0);
			
			cubeScale = new Vector3(5.5F, 0.6F, 0.3F);
			
			break;
		case GameLevels.level04 :
			topRightPos = new Vector3(3.6F, 0, 0);
			topLeftPos = new Vector3(0, 0, 3.6F);
			bottomRightPos = new Vector3(0, 0, -3.6F);
			bottomLeftPos = new Vector3(-3.6F, 0, 0);
			
			cubeScale = new Vector3(7.5F, 0.6F, 0.3F);
			
			break;
		default:
			break;
		}
		
		sideTopRight.position = topRightPos;
		sideTopLeft.position = topLeftPos;
		sideBottomRight.position = bottomRightPos;
		sideBottomLeft.position = bottomLeftPos;
		
		
		sideTopRight.GetChild(0).transform.localScale = cubeScale;
		sideTopLeft.GetChild(0).transform.localScale = cubeScale;
		sideBottomRight.GetChild(0).transform.localScale = cubeScale;
		sideBottomLeft.GetChild(0).transform.localScale = cubeScale;
	}
	
	
	void setLevelCubePosition () {
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
