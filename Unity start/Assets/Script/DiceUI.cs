using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    [SerializeField] Sprite[] DiceSide;
    public Button rollButton;
    public Move characterMove;
    private Image Originimage;
    bool isRolling = false;
    
    private void Awake()
    {
        Originimage = rollButton.GetComponent<Image>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RollDice();
        }
    }
    private void RollDice()
    {
        if (!isRolling) StartCoroutine(RollingDice());
    }

    private IEnumerator RollingDice()
    {
        isRolling = true;
        int[] Dice = { 1, 2, 2, 3, 3, 4 }; // 주사위 값 배열
        int diceValue = 0;
        for (int i = 0; i < 20; i++)
        {
            int diceIndex = Random.Range(0, Dice.Length);
            Originimage.sprite = DiceSide[diceIndex];
            yield return new WaitForSeconds(0.06f);
            diceValue = Dice[diceIndex];
        }
        
        characterMove.DiceInput(diceValue);
        isRolling = false;
    }
   
    
}
