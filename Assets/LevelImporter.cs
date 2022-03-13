using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelImporter : MonoBehaviour
{
    private const string levelsFolder = "../Assets/Levels/";
    //                      0         1     2      3        4               5         6     7
    public enum tiles {CONCRETE = 0, DIRT, GRASS, SEED, SHEEP_CONCRETE, SHEEP_DIRT, GOAL, WATER};

    // Return the grid tiles
    public static array GetTileMapFromFile(string fileName){
        List<List<int>> grid = new  List<List<int>>();
        
        string[] lines = System.IO.File.ReadAllLines(String.Concat(levelsFolder + fileName););
        foreach (string line in lines)
        {
            int[] lineValues = line.Split(',');
            List<int> gameTiles = new List<int>();

            for(int i = 0; i < lineValues.Length; i++){
                gameTiles.add(lineValues[i]);
            }

            grid.add(gameTiles);

        }

        return grid;
    }
}
