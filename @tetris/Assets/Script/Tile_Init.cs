using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Init : MonoBehaviour

{
    private int _width = 10;
    private int _height = 20;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _camera;
    [SerializeField] private GameObject[] tetriPiece = new GameObject[7];
    private GameObject presentGameObj;
    private Transform parentTransform;
    private gameObjectPro customScript;
    private float timer = 0.0f;
    Transform [,] myArray = new Transform[25,10];


    void Start()
    {
    GridGenerator();
    makePiece();
    for (int i = 0; i < 25 ; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            myArray[i, j] = null;
        }
    }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (CheckBoundry('A'))
            {
            presentGameObj.transform.position += new Vector3(-1 , 0);
            customScript.centerX -= 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(CheckBoundry('D'))
            {
            presentGameObj.transform.position += new Vector3(1 , 0);
            customScript.centerX += 1;
            }
        }
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
        moveDown();
        }
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        }
    }

    void GridGenerator()
    {
        for (int x =0 ; x < _width ; x++)
        {
            for (int y = 0 ; y < _height ; y++)
            {
                var Tile_init  = Instantiate(_tilePrefab , new Vector3(x, y), Quaternion.identity);
                Tile_init.name = $"Grid {x}_{y}";
            }

        }
        _camera.transform.position = new Vector3((float)_width/2-0.5f , (float)_height/2-0.5f,-10);
    }

     void makePiece()
    {
        int x = Random.Range(0,7);
        presentGameObj = Instantiate(tetriPiece[x] , new Vector3(5 , 21), Quaternion.identity);
        parentTransform = presentGameObj.transform;
        customScript = presentGameObj.GetComponent<gameObjectPro>();
        customScript.centerX += 5;
        customScript.centerY += 21;

    }

    void moveDown()
    {
        if(CheckBoundry('S'))
        {
            
            presentGameObj.transform.position += new Vector3(0, -1);
            customScript.centerY -= 1;
            timer = 0.0f;
        }
        else
        {
          setPosition();
            makePiece();
        }
        
        
    }


    bool CheckBoundry(char keyCommand)
{
    foreach (Transform childTransform in parentTransform)
    {
        float x = childTransform.transform.position.x;
        float y = childTransform.transform.position.y;

        if (keyCommand == 'A')
        {
            if (x - 1 < 0)
            {
                return false;
            }
            else if (myArray[(int)y, (int)(x - 1)] != null)
            {
                return false;
            }
        }
        else if (keyCommand == 'D')
        {
            if (x + 1 > 9)
            {
                return false;
            }
            else if (myArray[(int)y, (int)(x + 1)] != null)
            {
                return false;
            }
        }
        else if (keyCommand == 'S')
        {
            if (y <= 0)
            {
                return false;
            }
            else if (myArray[(int)(y-1), (int)x] != null)
            {
                return false;
            }
        }
    }

    return true;
}
    
    void setPosition()
    {
        float x = 0;
        float y = 0;
        foreach(Transform childTransform in parentTransform)
        {
            x = childTransform.transform.position.x;
            y = childTransform.transform.position.y;
            Debug.Log(childTransform.transform.position);
            

            myArray[(int)y , (int)x] = childTransform;
        }
    }


    void Rotate()
    {
        float x;
        float y;
        float a;
        float b;
        float c;
        float d;
        bool isfisibleToRotate = true;
     foreach(Transform childTransform in parentTransform)
        {
            x = customScript.centerX;
            y = customScript.centerY;
            a =childTransform.transform.position.x;
            b = childTransform.transform.position.y;

            c = -(b-y)+x;
            d = (a-x)+y;
            
            if(c<0.0 || c>9.0 || myArray[(int)d,(int)c]!= null)
            {
                isfisibleToRotate = false;
                break;
            }
            
        
        }
        if (isfisibleToRotate)
        {
        foreach(Transform childTransform in parentTransform)
        {
            x = customScript.centerX;
            y = customScript.centerY;
            a =childTransform.transform.position.x;
            b = childTransform.transform.position.y;

            c = -(b-y)+x;
            d = (a-x)+y;
            childTransform.position = new Vector3(c, d);  
        }
        }

    }

}
