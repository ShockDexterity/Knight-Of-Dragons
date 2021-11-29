using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemDisplay : MonoBehaviour
{
    public Image tens;
    public Image ones;
    public Sprite[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        if (tens == null) { tens = GameObject.Find("Tens").GetComponent<Image>(); }
        if (ones == null) { ones = GameObject.Find("Ones").GetComponent<Image>(); }

        numbers = Resources.LoadAll<Sprite>("gems/numbers");
        tens.sprite = numbers[0];
        ones.sprite = numbers[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLoot(int amt)
    {
        tens.sprite = numbers[amt / 10];
        ones.sprite = numbers[amt % 10];
    }
}
