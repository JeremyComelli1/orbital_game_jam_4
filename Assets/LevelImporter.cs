using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelImporter : MonoBehaviour
{
    //                      0         1     2      3        4               5         6     7
    public enum tiles {CONCRETE = 0, water, victory_water, SEED, SHEEP_CONCRETE, SHEEP_DIRT, GOAL, WATER};

    // Return the grid tiles
    public static List<List<string>> GetTileMapFromFile(string fileName){

        List<List<string>> grid = new  List<List<string>>();
        
        string[] lines = System.IO.File.ReadAllLines(string.Concat(fileName));
        int i = 0;
        foreach (string line in lines)
        {
            List<string> newLine = new List<string>();

            string[] lineValues = line.Split(',');


            for(int j = 0; i < lineValues.Length; j++){
                newLine.Add(lineValues[j]);
            }

            grid.Add(newLine);

        }

        return grid;
    }
}
