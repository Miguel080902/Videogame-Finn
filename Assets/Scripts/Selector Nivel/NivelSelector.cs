using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NivelSelector : MonoBehaviour
{
    public GameObject nivelButtonPrefab; 
    public Transform buttonContainer; 
    public int totalLevels = 10;
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject buttonObj = Instantiate(nivelButtonPrefab, buttonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = "Nivel " + i;

            int levelIndex = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                Time.timeScale = 1f; 
                SceneManager.LoadScene("Nivel_" + levelIndex);
            });
        }
    }
}
