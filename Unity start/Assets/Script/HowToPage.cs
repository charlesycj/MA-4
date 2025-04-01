using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPage : MonoBehaviour
{
    public List<GameObject> recipePages = new List<GameObject>();
    private int pageIndex = 0;

    public GameObject frontPageButton;
    public GameObject backPageButton;

    public GameObject pages; // 페이지를 담아둘 부모 오브젝트
    public GameObject pagePrefab; // 페이지 프리팹

    private bool isInitialized = false;

    private void Awake()
    {
        InitializePages();
    }

    private void InitializePages()
    {
        if (isInitialized) return;

        // 페이지 프리팹이 설정되지 않았을 경우 에러 로그 출력
        if (pagePrefab == null)
        {
            Debug.LogError("Page Prefab is not assigned!");
            return;
        }

        // pages 오브젝트가 없을 경우 생성
        if (pages == null)
        {
            pages = new GameObject("Pages");
            pages.transform.SetParent(transform, false); // 현재 오브젝트의 자식으로 설정
        }

        // 레시피 페이지 리스트가 비어있다면 프리팹을 사용하여 페이지 생성
        if (recipePages.Count == 0)
        {
            for (int i = 0; i < 5; i++) // 예시: 5개의 페이지 생성
            {
                GameObject newPage = Instantiate(pagePrefab, pages.transform);
                newPage.SetActive(false); // 처음에는 모든 페이지를 비활성화
                recipePages.Add(newPage);
            }
        }

        isInitialized = true;
    }

    // 버튼 클릭 시 초기화 및 UI 표시
    public void OnButtonClicked()
    {
        if (!isInitialized)
        {
            InitializePages();
        }
        OnRecipeButtonClicked(); // UI 표시
    }

    private bool CheckButtons()
    {
        if (frontPageButton == null || backPageButton == null)
        {
            Debug.LogError("FrontPageButton or BackPageButton is not assigned!");
            return false;
        }
        return true;
    }

    public void OnRecipeButtonClicked()
    {
        if (!CheckButtons()) return;

        UpdateNavigationButtons();

        if (!gameObject.activeSelf) // 이미 활성화되어 있는지 확인
        {
            gameObject.SetActive(true);
        }

        recipePages[pageIndex].SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnFrontPageButtonClicked()
    {
        if (!CheckButtons()) return;

        if (pageIndex > 0)
        {
            recipePages[pageIndex].SetActive(false);
            pageIndex--;
            recipePages[pageIndex].SetActive(true);

            UpdateNavigationButtons();
        }
    }

    public void OnBackPageButtonClicked()
    {
        if (!CheckButtons()) return;

        if (pageIndex < recipePages.Count - 1)
        {
            recipePages[pageIndex].SetActive(false);
            pageIndex++;
            recipePages[pageIndex].SetActive(true);

            UpdateNavigationButtons();
        }
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void UpdateNavigationButtons()
    {
        frontPageButton.SetActive(pageIndex > 0);
        backPageButton.SetActive(pageIndex + 1 < recipePages.Count);
    }
}
