using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class Maze : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    { 
        public bool visited;
        public GameObject north; //1
        public GameObject east;  //2
        public GameObject west;  //3
        public GameObject south; //4
        public Vector3 centro;
        public Vector3 enemigosN;
        public Vector3 enemigosE;
        public Vector3 enemigosW;
        public Vector3 enemigosS;
        public Vector3 bateria;

        public int contar()
        {
            int result = 0;

            if (north != null)
            {
                result ++;
            }
            if (east != null)
            {
                result ++;
            }
            if (west != null)
            {
                result ++;
            }
            if (south != null)
            {
                result ++;
            }
            return result;
        }
    }
    
    public GameObject wall;
    public float wallLenght = 1.0f;
    public int xSize = 5;
    public int ySize = 5;
    private Vector3 initialPos;
    private GameObject wallHolder;
    private int totalCells;
    private int visitedCells = 0;
    private bool startedBuilding = false;
    private int currentNeighbour = 0;
    private List<int> lastCells;
    private int backingUp = 0;
    private int currentCell = 0;
    private int wallToBreak = 0;


    public Cell[] cells;
    public List<Cell> lista;
    
    private int cantidad;
    private bool jugadorr = true;
    private int objetos;
    private int celda;
    private int jugadorI;
    public GameObject jugador; 
    public GameObject cofreObj;
    private int cofreI;
    public GameObject bateriaObj;
    private int bateriaI;
    public GameObject balasObj;
    private int balasI;
    public GameObject puerta;

    private bool ene1 = true;
    public GameObject ene1Slasher;
    private int ene1SlasherI;
    private bool ene2 = true;
    public GameObject ene2Boss;
    private int ene2BossI;

    private GameObject pared;
    private bool proof = true;
    
    

    private void Awake()
    {
        
    }

    void Start()
    {
        CreateWalls();
        GenerateObj();
    }

    void GenerateObj()
    {
        lista = new List<Cell>();
        
        for (int i = 0; i < cells.Length; i++)
        { 
            if (cells[i].contar() == 3)
            {
                lista.Add(cells[i]);
            }
        }

        //destruir pared para poner puerta
        pared = cells[1].south;
        DestroyImmediate(pared);
        Instantiate(puerta, new Vector3(-4.79f,0,-7.68f), Quaternion.Euler(0,0,0));

        while (objetos <= 20 && lista.Count > 0)
        {
            celda = Random.Range(0, lista.Count);
            if (cofreI <= 8)
            {
                if (lista[celda].east == null)
                {
                    Instantiate(cofreObj, lista[celda].centro, Quaternion.Euler(180, -90, 0));
                }
                if (lista[celda].north == null)
                {
                    Instantiate(cofreObj, lista[celda].centro, Quaternion.Euler(180, 0, 0));
                }
                if (lista[celda].south == null)
                {
                    Instantiate(cofreObj, lista[celda].centro, Quaternion.Euler(180, 180, 0));
                }
                if (lista[celda].west == null)
                {
                    Instantiate(cofreObj, lista[celda].centro, Quaternion.Euler(180, 90, 0));
                }
                
                
                if (ene1 && ene1SlasherI <= 5)
                {
                    
                    if (lista[celda].east == null)
                    {
                        GameObject enemigo1 = Instantiate(ene1Slasher, lista[celda].enemigosE, Quaternion.Euler(0, -90, 0));
                        Slasher enemigo1Slash = enemigo1.GetComponent<Slasher>();
                        if (enemigo1Slash != null)
                        {
                            enemigo1Slash.player = jugador.transform;
                            enemigo1Slash.jug = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    
                    if (lista[celda].north == null)
                    {
                        GameObject enemigo1 = Instantiate(ene1Slasher, lista[celda].enemigosN, Quaternion.identity);
                        Slasher enemigo1Slash = enemigo1.GetComponent<Slasher>();
                        if (enemigo1Slash != null)
                        {
                            enemigo1Slash.player = jugador.transform;
                            enemigo1Slash.jug = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    
                    if (lista[celda].south == null)
                    {
                        GameObject enemigo1 = Instantiate(ene1Slasher, lista[celda].enemigosS, Quaternion.Euler(0, 180, 0));
                        Slasher enemigo1Slash = enemigo1.GetComponent<Slasher>();
                        if (enemigo1Slash != null)
                        {
                            enemigo1Slash.player = jugador.transform;
                            enemigo1Slash.jug = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    
                    if (lista[celda].west == null)
                    {
                        GameObject enemigo1 = Instantiate(ene1Slasher, lista[celda].enemigosW, Quaternion.Euler(0, 90, 0));
                        Slasher enemigo1Slash = enemigo1.GetComponent<Slasher>();
                        if (enemigo1Slash != null)
                        {
                            enemigo1Slash.player = jugador.transform;
                            enemigo1Slash.jug = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    ene1 = false;
                }
                else if (ene2 && ene2BossI <= 3)
                {
                    if (lista[celda].east == null)
                    {
                        GameObject enemigo2 = Instantiate(ene2Boss, lista[celda].enemigosE, Quaternion.Euler(0, -90, 0));
                        Boss enemigo2Boss = enemigo2.GetComponent<Boss>();
                        if (enemigo2Boss != null)
                        {
                            enemigo2Boss.player = jugador.transform;
                            enemigo2Boss.juga = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    
                    if (lista[celda].north == null)
                    {
                        GameObject enemigo2 = Instantiate(ene2Boss, lista[celda].enemigosN, Quaternion.identity);
                        Boss enemigo2Boss = enemigo2.GetComponent<Boss>();
                        if (enemigo2Boss != null)
                        {
                            enemigo2Boss.player = jugador.transform;
                            enemigo2Boss.juga = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    
                    if (lista[celda].south == null)
                    {
                        GameObject enemigo2 = Instantiate(ene2Boss, lista[celda].enemigosS, Quaternion.Euler(0, 180, 0));
                        Boss enemigo2Boss = enemigo2.GetComponent<Boss>();
                        if (enemigo2Boss != null)
                        {
                            enemigo2Boss.player = jugador.transform;
                            enemigo2Boss.juga = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    
                    if (lista[celda].west == null)
                    {
                        GameObject enemigo2 = Instantiate(ene2Boss, lista[celda].enemigosW, Quaternion.Euler(0, 90, 0));
                        Boss enemigo2Boss = enemigo2.GetComponent<Boss>();
                        if (enemigo2Boss != null)
                        {
                            enemigo2Boss.player = jugador.transform;
                            enemigo2Boss.juga = jugador.GetComponent<PlayerMove>();
                        }
                    }
                    ene2 = false;
                }
                if (ene2 == false)
                {
                    ene1 = true;
                }
                if (ene1 == false)
                {
                    ene2 = true;
                }
                cofreI += 1;
            }
            else if (balasI <= 5)
            {
                if (lista[celda].east == null)
                {
                    Instantiate(balasObj, lista[celda].centro, Quaternion.Euler(0, -90, 0));
                }
                if (lista[celda].north == null)
                {
                    Instantiate(balasObj, lista[celda].centro, Quaternion.Euler(0, 0, 0));
                }
                if (lista[celda].south == null)
                {
                    Instantiate(balasObj, lista[celda].centro, Quaternion.Euler(0, 180, 0));
                }
                if (lista[celda].west == null)
                {
                    Instantiate(balasObj, lista[celda].centro, Quaternion.Euler(0, 90, 0));
                }
                
                balasI += 1;
                
            }
            else if (bateriaI <= 7)
            {
                if (lista[celda].east == null)
                {
                    Instantiate(bateriaObj, lista[celda].bateria, Quaternion.Euler(0, -90, 0));
                }
                if (lista[celda].north == null)
                {
                    Instantiate(bateriaObj, lista[celda].bateria, Quaternion.Euler(0, 0, 0));
                }
                if (lista[celda].south == null)
                {
                    Instantiate(bateriaObj, lista[celda].bateria, Quaternion.Euler(0, 180, 0));
                }
                if (lista[celda].west == null)
                {
                    Instantiate(bateriaObj, lista[celda].bateria, Quaternion.Euler(0, 90, 0));
                }
                bateriaI += 1;
            }
            lista.RemoveAt(celda);
            objetos += 1;
        }
    }

    void CreateWalls()
    {
        wallHolder = new GameObject(); 
        wallHolder.name = "Maze";
        
        initialPos = new Vector3((-xSize / 2) + wallLenght / 2, 0.0f, (-ySize / 2) + wallLenght / 2);
        Vector3 myPos = initialPos;
        GameObject tempWall;

        //paredes eje X
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLenght) - wallLenght / 2, 0.0f, initialPos.z + (i * wallLenght) - wallLenght / 2);
                
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                tempWall.transform.parent = wallHolder.transform;
            }
        }
        
        //paredes eje Y
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLenght), 0.0f, initialPos.z + (i * wallLenght) - wallLenght);
                tempWall = Instantiate(wall, myPos, Quaternion.identity);
                tempWall.transform.parent = wallHolder.transform;
                
            }
        }
        CreteCells();
    }

    void CreteCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCells = xSize * ySize;
        GameObject[] allWalls;
        int children = wallHolder.transform.childCount;
        allWalls = new GameObject[children];
        cells = new Cell[xSize*ySize];
        int eastWestProcess = 0;
        int childProcess = 0;
        int termnCount = 0;
        
        //cuenta los hijos
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        //asigna las paredes a las celdas
        for (int cellprocess = 0; cellprocess < cells.Length; cellprocess++)
        {
            cells[cellprocess] = new Cell();
            cells[cellprocess].east = allWalls[eastWestProcess];
            cells[cellprocess].south = allWalls[childProcess+(xSize+1)*ySize];
            if (termnCount == xSize)
            {
                eastWestProcess += 2;
                termnCount = 0;
            }
            else
            {
                eastWestProcess++;
            }

            termnCount++;
            childProcess++;
            cells[cellprocess].west = allWalls[eastWestProcess]; 
            cells[cellprocess].north = allWalls[(childProcess+(xSize+1)*ySize)+xSize-1];

            float distance = Vector3.Distance(cells[cellprocess].north.transform.position, cells[cellprocess].south.transform.position);
            float elevBateria = 0.03f;
            
            cells[cellprocess].centro = new Vector3(cells[cellprocess].south.transform.position.x, cells[cellprocess].south.transform.position.y, cells[cellprocess].south.transform.position.z + distance/2);
            cells[cellprocess].bateria = new Vector3(cells[cellprocess].south.transform.position.x, cells[cellprocess].south.transform.position.y + elevBateria, cells[cellprocess].south.transform.position.z + distance/2);
            
            //north (izq)
            cells[cellprocess].enemigosN = new Vector3(cells[cellprocess].south.transform.position.x, cells[cellprocess].south.transform.position.y, cells[cellprocess].south.transform.position.z + distance);
            //east(abajo)
            cells[cellprocess].enemigosE = new Vector3(cells[cellprocess].south.transform.position.x - distance/2, cells[cellprocess].south.transform.position.y, cells[cellprocess].south.transform.position.z + 1);
            //west(arriba)
            cells[cellprocess].enemigosW = new Vector3(cells[cellprocess].south.transform.position.x + distance/4, cells[cellprocess].south.transform.position.y, cells[cellprocess].south.transform.position.z);
            //south(derecha)
            cells[cellprocess].enemigosS = new Vector3(cells[cellprocess].south.transform.position.x, cells[cellprocess].south.transform.position.y, cells[cellprocess].south.transform.position.z - distance/4);
        }
        CreateMaze();
    }

    void CreateMaze()
    {
        while (visitedCells < totalCells)
        {
            if (startedBuilding)
            {
                GiveMeNeighbor();
                if (cells[currentNeighbour].visited == false && cells[currentCell].visited == true)
                {
                    BreakWall();
                    cells[currentNeighbour].visited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbour;
                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                currentCell = Random.Range(0, totalCells);
                cells[currentCell].visited = true;
                visitedCells ++;
                startedBuilding = true;
            }
            //Invoke("CreateMaze", 0.0f);
            
            
        }
    }

    void BreakWall()
    {
        switch (wallToBreak)
        {
            case 1: DestroyImmediate(cells[currentCell].north); break;
            case 2: DestroyImmediate(cells[currentCell].east); break;
            case 3: DestroyImmediate(cells[currentCell].west); break;
            case 4: DestroyImmediate(cells[currentCell].south); break;
        }
    }

    void GiveMeNeighbor()
    {
        int lenght = 0;
        int[] neighbors = new int[4];
        int[] connectingWall = new int[4];
        int check = 0;

        check = (currentCell + 1) / xSize;
        check -= 1;
        check *= xSize;
        check += ySize;
        
        //west
        if (currentCell + 1 < totalCells && (currentCell+1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbors[lenght] = currentCell + 1;
                connectingWall[lenght] = 3;
                lenght++;
            }
        }
        
        //east
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbors[lenght] = currentCell - 1;
                connectingWall[lenght] = 2;
                lenght++;
            }
        }
        
        //north
        if (currentCell + xSize < totalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbors[lenght] = currentCell + xSize;
                connectingWall[lenght] = 1;
                lenght++;
            }
        }
        
        //south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbors[lenght] = currentCell - xSize;
                connectingWall[lenght] = 4;
                lenght++;
            }
        }
        
        

        if (lenght != 0)
        {
            int theChosenOne = Random.Range(0, lenght);
            currentNeighbour = neighbors[theChosenOne];
            wallToBreak = connectingWall[theChosenOne];
        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
