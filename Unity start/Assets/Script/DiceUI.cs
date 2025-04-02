using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    [SerializeField] Sprite[] DiceSide;
    public Button rollButton;
    public Move characterMove;
    public AudioClip diceRollSound;
    
    private Image Originimage;
    public AudioSource _audioSource;
    bool isRolling = false;
    
    private void Awake()
    {
        Originimage = rollButton.GetComponent<Image>();
        // 버튼 클릭 이벤트에 RollDice 함수 연결
        rollButton.onClick.AddListener(OnRollButtonClicked);
    }

    private void OnDestroy()
    {
        rollButton.onClick.RemoveListener(OnRollButtonClicked);
    }

    private void Update()
    {
        // 턴 상태가 RotatingOrRolling이 아니면 아무것도 하지 않음
        if (TurnPhase.Instance.CurrentState != PlayerState.RotatingOrRolling) return;

        // (원하는 경우) 'T' 키 입력도 유지하려면 아래 코드 주석 해제
         if (Input.GetKeyDown(KeyCode.T))
        {
            RollDice();
            
         }
    }

    // 버튼 클릭 이벤트 핸들러
    private void OnRollButtonClicked()
    {
        if (TurnPhase.Instance.CurrentState != PlayerState.RotatingOrRolling) return;
        RollDice();
    }

    private void RollDice()
    {
        if (!isRolling) StartCoroutine(RollingDice());
    }

    private IEnumerator RollingDice()
    {
        isRolling = true;
        rollButton.interactable = false;
        int[] Dice = { 1, 2, 2, 3, 3, 4 };
        int diceValue = 0;

        if (diceRollSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(diceRollSound);
        }

        for (int i = 0; i < 20; i++)
        {
            int diceIndex = Random.Range(0, Dice.Length);
            Originimage.sprite = DiceSide[diceIndex];
            yield return new WaitForSeconds(0.06f);
            diceValue = Dice[diceIndex];
        }
        
        characterMove.DiceInput(diceValue);
        isRolling = false;
        rollButton.interactable = true;
    }
}
