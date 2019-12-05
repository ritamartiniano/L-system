using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LsystemParser : MonoBehaviour
{
    public static void Parse(string file,out string axiom,out float angle, out int generations,out Dictionary<char, string> rules)
    {
        axiom = "";
        angle = 0;
        generations = 0;
        
        rules = new Dictionary<char, string>();
        var allLines = file.Split('\n');
        foreach(string rawline in allLines)
        {
            string line = rawline.Trim();
            if (line.Length == 0)
                continue;
            else if (line.Length == 1 && line[0] == '\r')
                continue;
            string value;
            if(line.IndexOf("axiom") != -1)
            {
                value = line.Substring(line.IndexOf("=") + 1);
                value = value.Trim();
                axiom = value;
            }
            else if(line.IndexOf("angle") != -1)
            {
                value = line.Substring(line.IndexOf("=") + 1);
                value = value.Trim();
                angle = float.Parse(value);
            }
            else if(line.IndexOf("generations") != -1){

                value = line.Substring(line.IndexOf("=") + 1);
                value = value.Trim();
                generations = int.Parse(value);
            }
            else if(line.IndexOf("X") != -1)
            {
                value = line.Substring(line.IndexOf("=") + 1);
                value = value.Trim();
                rules.Add('X', value);
            }
            else if(line.IndexOf("F") != -1)
            {
                value = line.Substring(line.IndexOf("=") + 1);
                value = value.Trim();
                rules.Add('F', value);
            }
            
        }
    }
}
