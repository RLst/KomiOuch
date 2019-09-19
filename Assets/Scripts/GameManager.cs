using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Public
	public GameSettings settings;
	public Transform nekomi;

	[Header("Score")]
	public Text scoreText;
	public Text hiscoreText;

	[Header("UIs")]
	public GameObject gameOverUI;

	//Privates
	Rigidbody2D nekomiRb;
	PostMover[] postMovers;
	Floor floorKiller;
	Ceiling ceilingKiller;
	Scroller[] scrollers;
	bool gameOver = false;

	void Awake()
	{
		Debug.Assert(settings, "Settings not found!");

		//Find and cache killers
		ceilingKiller = FindObjectOfType<Ceiling>();
		floorKiller = FindObjectOfType<Floor>();
		Debug.Assert(ceilingKiller, "No ceiling killer object found!");
		Debug.Assert(floorKiller, "No floor killer object found!");

		//Find and cache all posts
		postMovers = FindObjectsOfType<PostMover>();

		//Find and cache all scrollers
		scrollers = FindObjectsOfType<Scroller>();

		//Cache nekomi's rigidbody
		nekomiRb = nekomi.GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		//Resets
		settings.score = 0;
		scoreText.text = "Score: " + settings.score.ToString();
		gameOverUI.SetActive(false);

		//Set difficulty
		SetupDifficulty(settings.difficulty);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.GetComponent<PostMover>() != null)
		{
			settings.score++;
			scoreText.text = "Score: " + settings.score.ToString();
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void StartGame()
	{
		Cursor.visible = false;

		//Start everything
		foreach (var pm in postMovers)
			pm.enabled = true;
		foreach (var s in scrollers)
			s.enabled = true;
	}

	public void EndGame()
	{
		gameOver = true;
		Cursor.visible = true;

		//Stop everything
		foreach (var pm in postMovers)
			pm.enabled = false;
		foreach (var s in scrollers)
			s.enabled = false;

		//Let nekomi roll off
		nekomiRb.constraints = RigidbodyConstraints2D.None;
		nekomiRb.AddForce(new Vector2(-20, 100), ForceMode2D.Impulse);

		//Show game over screen
		gameOverUI.SetActive(true);

		//Set the hiscore based on difficulty level
		var difficultyString = settings.difficulty.ToString("g");
		var hiscore = PlayerPrefs.GetInt("hiscore" + difficultyString);
		if (settings.score > hiscore)
		{
			PlayerPrefs.SetInt("hiscore" + difficultyString, settings.score);
			hiscore = settings.score;
		}
		hiscoreText.text = string.Format("HiScore: {0} ({1})", hiscore.ToString(), difficultyString);
	}

	public void SetupDifficulty(Difficulty difficulty)
	{
		//Reset everything
		ceilingKiller.active = false;
		floorKiller.active = false;
		ActivatePostKillers();

		switch (difficulty)
		{
			case Difficulty.Easy:
				//Only the posts are killers (already done)
			break;
			case Difficulty.Normal:
				//Posts and ceiling are killers
				ceilingKiller.active = true;
			break;
			case Difficulty.Hard:
				//Post, carpet and ceiling are killers
				ceilingKiller.active = true;
				floorKiller.active = true;
			break;
		}

		void ActivatePostKillers()
		{
			foreach (var p in postMovers)
				p.GetComponent<Killer>().active = true;
		}
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
