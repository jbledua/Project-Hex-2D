using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class ImageToTilemap : MonoBehaviour
{
    public Texture2D image;
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile groundTile;
    public int desiredNumberOfTiles = 10;  // Set this to the number of tiles you want along each axis


    void Start()
    {
        //int desiredNumberOfTiles = 10;  // Set this to the number of tiles you want along each axis
        int squareSize = image.width / desiredNumberOfTiles;
        int numberOfTiles = desiredNumberOfTiles;  // Calculate the number of tiles along each axis

        for (int y = 0; y < image.height; y += squareSize)
        {
            for (int x = 0; x < image.width; x += squareSize)
            {
                Dictionary<Color, int> colorCounts = new Dictionary<Color, int>();

                for (int i = 0; i < squareSize; i++)
                {
                    for (int j = 0; j < squareSize; j++)
                    {
                        Color pixelColor = image.GetPixel(x + i, y + j);

                        if (colorCounts.ContainsKey(pixelColor))
                        {
                            colorCounts[pixelColor]++;
                        }
                        else
                        {
                            colorCounts[pixelColor] = 1;
                        }
                    }
                }

                Color mostCommonColor = colorCounts.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

                Tile tile = GetTileForColor(mostCommonColor);  // You'll need to implement this function
                tilemap.SetTile(new Vector3Int(x / squareSize - numberOfTiles / 2, y / squareSize - numberOfTiles / 2, 0), tile);
            }
        }
    }


    Tile GetTileForColor(Color color)
    {

        // Calculate the grayscale value of the color
        float grayscale = 0.3f * color.r + 0.59f * color.g + 0.11f * color.b;

        // If the grayscale value is closer to 0, the color is closer to black; if it's closer to 1, the color is closer to white
        if (grayscale < 0.5f)
        {
            return wallTile;
        }
        else
        {
            return groundTile;
        }
    }

}
