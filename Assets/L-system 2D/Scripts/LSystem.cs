using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}
public class LSystem : MonoBehaviour
{  
    //l-system configuration files
    public TextAsset system1;
    public TextAsset system2;
    public TextAsset system3;
    public TextAsset system4;
    public TextAsset treeA;
    public TextAsset treeB;
    public TextAsset treeC;
    public TextAsset treeD;
    public TextAsset treeE;
    public TextAsset treeF;
    private int GENERATIONS;
    public float angle;
    private string axiom;
    private Dictionary<char, string> rules;

    public float startWidth = 10;
    public float endWidth = 10;

    //game object to render configuration   
    public GameObject branch;
    public GameObject leaf;
    public GameObject cube;

    [HideInInspector]
    public float length = 20;
    
    private Stack<TransformInfo> transformStack;
    private string currentString = string.Empty;
    private int lsystemNum = 1;
    private List<GameObject> branches;
    private List<GameObject> leaves;
    private Vector3 initialTransform;
    private Quaternion initialRotation;
    private Vector3 mousePrevPos = Vector3.zero;
    private Vector3 mousePosDelta = Vector3.zero;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        initialTransform = transform.position;
        initialRotation = transform.rotation;
        parseFile(system1);
        transformStack = new Stack<TransformInfo>();
        branches = new List<GameObject>();
        leaves = new List<GameObject>();
        generate();

    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            this.GENERATIONS++;
            generate();
        }
        else if (Input.GetKeyDown(KeyCode.B) && GENERATIONS != 0)
        {
            this.GENERATIONS--;
            deleteElements();
            resetTransform();
            generate();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.angle++;
            deleteElements();
            resetTransform();
            generate();

        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.angle--;
            deleteElements();
            resetTransform();
            generate();

        }
        else if (Input.GetKey(KeyCode.W))
        {
            this.length++;
            deleteElements();
            resetTransform();
            generate();

        }
        else if (Input.GetKey(KeyCode.S) && length != 0)
        {
            this.length--;
            deleteElements();
            resetTransform();
            generate();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lsystemNum <= 10)
        {
            lsystemNum++;
            deleteElements();
            changeLsystemtree();
            generate();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lsystemNum >= 1)
        {
            lsystemNum--;
            deleteElements();
            changeLsystemtree();
            generate();
        }
        Menu.generations = GENERATIONS.ToString();
        Menu.angle = angle;
        Menu.length = length;
        Menu.axiomText = axiom;
        Menu.lsystemNum = lsystemNum;
       
        foreach(KeyValuePair<char,string> r in rules)
        {
            if(r.Key =='X')
            {
                Menu.rule1Text = r.Value;
            }else if(r.Key == 'F'){
                Menu.rule2Text = r.Value;
            }
        }
        rotateTree();
       
    }
    private void generate()
    {
        if (rules != null)
        { 
        currentString = axiom;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < GENERATIONS; i++)
        {
            foreach (char c in currentString)
            {
                if (rules.ContainsKey(c))
                {
                    sb.Append(rules[c]);
                }
                else
                {
                    sb.Append(c.ToString());
                }
            }
            currentString = sb.ToString();
        }
        }
       
        for(int k = 0; k < currentString.Length; k++)
        {   
            switch (currentString[k])
            {
                case 'F':
                    Vector3 initialPos = transform.position;
                    transform.Translate(Vector3.up * length);
                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.GetComponent<LineRenderer>().SetWidth(startWidth, endWidth);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPos);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    branches.Add(treeSegment);
                    break;
                case 'X':
                    break;
                case '+':
                    transform.Rotate(Vector3.back * angle);                 
                    break;
                case '-':
                    transform.Rotate(Vector3.forward * angle);
                    break;
                case '[':
                    //saving transform input
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;
                case ']':
                    //go back to previously saved positions;
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;
                case '&':
                    transform.Rotate(Vector3.left * angle);
                    break;
                case '/':
                    transform.Rotate(Vector3.right * angle);
                    break;
                case 'L':
                    GameObject leafInstance = Instantiate(leaf,transform.position,Quaternion.identity);
                    leaves.Add(leafInstance);
                    break;
                default:
                    throw new InvalidOperationException("Invalid L-tree operation");
            }
        }
    }
    private void deleteElements()
    {
        foreach(GameObject go in branches)
        {
            Destroy(go);
        }
        if(leaves!=null)
        {
            foreach(GameObject go in leaves)
            {
                Destroy(go);
            }
        }
    }
    private void parseFile(TextAsset file)
    {
        LsystemParser.Parse(file.text, out axiom, out angle, out GENERATIONS, out rules);
    }
    private void changeLsystemtree()
    {
        resetTransform();
        switch (lsystemNum)
        {
            case 1:
                parseFile(system1);
                break;
            case 2:
                parseFile(system2);
                break;
            case 3:
                parseFile(system3);
                break;
            case 4:
                parseFile(system4);
                break;
            case 5:
                parseFile(treeA);
                break;
            case 6:
                parseFile(treeB);
                break;
            case 7:
                parseFile(treeC);
                break;
            case 8:
                parseFile(treeD);
                break;
            case 9:
                parseFile(treeE);
                break;
            case 10:
                parseFile(treeF);
                break;
        }
    }
    private void resetTransform()
    {
        transform.position = initialTransform;
        transform.rotation = initialRotation;
    }
   private void rotateTree()
    {
        if (Input.GetAxis("Mouse ScrollWheel")>0)
        {
            camera.fieldOfView--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camera.fieldOfView++;
        }

    }
    
}

