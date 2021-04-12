using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FetchDataFromSave : MonoBehaviour
{
    [SerializeField] private SavingScript save;
    [SerializeField] private Text myText;
    public bool iHaveASave = false;

    [SerializeField] private enum target
    {
        SCORE,
        COMBO,
        GOLDS,
        SAVE
    }
    [SerializeField] private target imLookingFor;

    private void Awake()
    {
        save.LoadFromFile();
        StartCoroutine(WaitABit());
        
    }

    IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(0.5f);
        switch (imLookingFor)
        {
            case target.SCORE:
                myText.text = save.loadInt("score", 0).ToString();
                Debug.Log("Your high score is " + save.loadInt("score", 0));
                break;
            case target.COMBO:
                myText.text = save.loadInt("combo", 0).ToString();
                break;
            case target.GOLDS:
                myText.text = save.loadInt("golds", 0).ToString();
                break;
            case target.SAVE:
                if (save.loadBool("exists", false))
                {
                    iHaveASave = true;
                }
                break;
        }
    }
}
