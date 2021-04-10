using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject cell;

    public List<Cell> cells = new List<Cell>();

    // Start is called before the first frame update
    void Start()
    {
        
        for(int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                GameObject cellInstance = GameObject.Instantiate(cell);
                cellInstance.transform.position = new Vector3(x, 2, z);
                cells.Add(cellInstance.GetComponent<Cell>());
            }
        }

        for (int x = 0; x < 5; x++)
        {

            for (int z = 0; z < 5; z++)
            {

                int index = (x * 5) + z;

                Debug.Log(index);

                try
                {
                    if(z > 0)
                    {
                        cells[index].neighbours.Add(cells[index - 5 - 1]); // TOP LEFT
                    }                    
                }
                catch { }

                try
                {
                    cells[index].neighbours.Add(cells[index - 5]); // TOP
                }
                catch { }

                try
                {
                    if (z < 4)
                    {
                        cells[index].neighbours.Add(cells[index - 5 + 1]); // TOP RIGHT
                    }
                }
                catch { }

                try
                {
                    if (z > 0)
                    {
                        cells[index].neighbours.Add(cells[index - 1]); // LEFT
                    }
                }
                catch { }

                try
                {
                    if (z < 4)
                    {
                        cells[index].neighbours.Add(cells[index + 1]); // RIGHT
                    }
                }
                catch { }

                try
                {
                    if (z > 0)
                    {
                        cells[index].neighbours.Add(cells[index + 5 - 1]); // BOTTOM LEFT
                    }
                }
                catch { }

                try
                {
                    cells[index].neighbours.Add(cells[index + 5]); // BOTTOM
                }
                catch { }

                try
                {
                    if (z < 4)
                    {
                        cells[index].neighbours.Add(cells[index + 5 + 1]); // BOTTOM RIGHT
                    }
                }
                catch { }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
