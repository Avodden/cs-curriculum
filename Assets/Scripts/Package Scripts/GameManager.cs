using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{


    public static GameManager gm;
    public int coins_amount = 0;
    public int health_amount = 100;

    public TextMeshProUGUI coinTest;
    public TextMeshProUGUI healthTest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
       else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        coinTest.text = "Coins: " + coins_amount;
        healthTest.text = "Health: " + health_amount;
    }

    // Update is called once per frame
    void Update()
    {
        coinTest.text = "Coins: " + coins_amount;
        healthTest.text = "Health: " + health_amount;
    }
}
