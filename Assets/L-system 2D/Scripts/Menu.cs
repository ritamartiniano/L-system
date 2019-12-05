using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    
    public Text lsystemNumber;
    public Text axiom;
    public Text Generations;
    public Text rule1;
    public Text rule2;
    public Text angleTxt;
    public Text lengthTxt;

    public static int lsystemNum;
    public static string axiomText;
    public static string generations;
    public static string rule1Text;
    public static string rule2Text;
    public static float angle;
    public static float length;
    
    // Update is called once per frame
    void Update()
    {
        lsystemNumber.text = lsystemNum.ToString();
        axiom.text = axiomText;
        Generations.text = generations;
        rule1.text = rule1Text;
        rule2.text = rule2Text;
        angleTxt.text = angle.ToString();
        lengthTxt.text = length.ToString();
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
