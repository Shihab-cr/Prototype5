using UnityEngine;
using UnityEngine.UI;
public class DifficultyHandler : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int difficultyLevel;
    public AudioClip[] buttonClickClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetDifficulty()
    {
        gameManager.StartGame(difficultyLevel);
        Debug.Log(gameObject.name+" was clicked");
        if (buttonClickClips.Length > 0)
        {
            int index = Random.Range(0, buttonClickClips.Length);
            AudioSource.PlayClipAtPoint(buttonClickClips[index], transform.position);
        }
    }
}
