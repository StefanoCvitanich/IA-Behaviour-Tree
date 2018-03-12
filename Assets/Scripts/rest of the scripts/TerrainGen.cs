using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour {

    public int rows;
    public int columns;
    public float separation;
    public float maxOffset;
    public GameObject[] islands;
    public GameObject kelpBank;
    public GameObject[] palmPfabs;
    public GameObject[] stonePfabs;
    public Vector2 playerSpawn;
    public int palms;
    public int stones;

    private static Vector3 playerSpawnPoint;

    private int currentRow = 0;
    private int currentColumn = 0;
    private Vector3 currentPos;

    Vector3 originalPos;
    GameObject objRef;
    Island islandRef;
    int islandPick;
    float chance;
    int palmsLeft;
    int stonesLeft;
    int palmSpawns;
    int stoneSpawns;
    List<Vector3> palmSpawnPositions;
    List<Vector3> stoneSpawnPositions;

	void Awake ()
    {
        palmSpawnPositions = new List<Vector3>();
        stoneSpawnPositions = new List<Vector3>();
        palmSpawns = 0;
        stoneSpawns = 0;
        palmsLeft = palms;
        stonesLeft = stones;
        originalPos = transform.position;
        currentPos = transform.position;

        GenerateTerrain();
	}

    void GenerateTerrain()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                islandPick = Random.Range(0, islands.Length);
                objRef = Instantiate(islands[islandPick], currentPos, Quaternion.identity);
                islandRef = objRef.GetComponent<Island>();
                palmSpawns += islandRef.palmSpawns.Length;
                stoneSpawns += islandRef.stoneSpawns.Length;

                objRef.transform.Rotate(0, Random.Range(0, 360), 0);
                objRef.transform.Translate(Random.Range(-maxOffset, maxOffset), 0, Random.Range(-maxOffset, maxOffset));

                Vector3 islandPos = objRef.transform.position;
                objRef = Instantiate(kelpBank, islandPos, Quaternion.identity);
                objRef.transform.Rotate(Vector3.up * Random.Range(0, 360));
                float rotation = objRef.transform.rotation.eulerAngles.y;
                objRef.transform.Translate(transform.forward * Random.Range(separation / 4, separation / 2));

                objRef = Instantiate(kelpBank, islandPos, Quaternion.identity);
                objRef.transform.Rotate(Vector3.up * (rotation + 90 + Random.Range(-10, 10)));
                rotation += 90;
                objRef.transform.Translate(transform.forward * Random.Range(separation / 4, separation / 2));

                objRef = Instantiate(kelpBank, islandPos, Quaternion.identity);
                objRef.transform.Rotate(Vector3.up * (rotation + 90 + Random.Range(-10, 10)));
                rotation += 90;
                objRef.transform.Translate(transform.forward * Random.Range(separation / 4, separation / 2));

                objRef = Instantiate(kelpBank, islandPos, Quaternion.identity);
                objRef.transform.Rotate(Vector3.up * (rotation + 90 + Random.Range(-10, 10)));
                objRef.transform.Translate(transform.forward * Random.Range(separation / 4, separation / 2));

                for (int k = 0; k < islandRef.palmSpawns.Length; k++)
                {
                    palmSpawnPositions.Add(islandRef.palmSpawns[k].position);
                }
                for (int k = 0; k < islandRef.stoneSpawns.Length; k++)
                {
                    stoneSpawnPositions.Add(islandRef.stoneSpawns[k].position);
                }

                if (i+1 == playerSpawn.x && j+1 == playerSpawn.y)
                {
                    islandRef.playerSpawn.gameObject.SetActive(true);
                    playerSpawnPoint = islandRef.playerSpawn.position;
                }

                currentColumn++;
                currentPos.Set(currentPos.x + separation, currentPos.y, currentPos.z);
            }
            currentRow++;
            currentColumn = 0;
            currentPos.Set(originalPos.x, currentPos.y, currentPos.z + separation);
        }
        GenerateResources();
    }

    void GenerateResources()
    {
        if (palmsLeft > palmSpawnPositions.Count)
            palmsLeft = palmSpawnPositions.Count;
        if (stonesLeft > stoneSpawnPositions.Count)
            stonesLeft = stoneSpawnPositions.Count;

        int palmSpawnsLeft = palmSpawnPositions.Count;
        int stoneSpawnsLeft = stoneSpawnPositions.Count;
        int picker = 0;
        float rand = 0;

        for (int i = 0; i < palmSpawnPositions.Count; i++)
        {
            chance = (float)palmsLeft / (float)palmSpawnsLeft;
            rand = Random.Range(0.0f, 1.0f);
            if (chance > rand)
            {
                picker = Random.Range(0, palmPfabs.Length);
                Instantiate(palmPfabs[picker], palmSpawnPositions[i], Quaternion.identity);
                palmsLeft--;
            }
            palmSpawnsLeft--;
        }

        for (int i = 0; i < stoneSpawnPositions.Count; i++)
        {
            chance = (float)stonesLeft / (float)stoneSpawnsLeft;
            rand = Random.Range(0.0f, 1.0f);
            if (chance > rand)
            {
                picker = Random.Range(0, stonePfabs.Length);
                Instantiate(stonePfabs[picker], stoneSpawnPositions[i], Quaternion.identity);
                stonesLeft--;
            }
            stoneSpawnsLeft--;
        }
    }

    public static Vector3 GetSpawnPoint()
    {
        return playerSpawnPoint;
    }
}
