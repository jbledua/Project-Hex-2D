using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class MaskToTile : MonoBehaviour
{
    public Texture2D image;
    public GameObject maskDisplay;

    public Tilemap tilemap;

    public Tile layerTile;
    //public Tile groundTile;
    public int desiredNumberOfTiles = 50;  // Set this to the number of tiles you want along each axis

    void Start()
    {
        // Get the Sprite Renderer from the GameObject
        SpriteRenderer spriteRenderer = maskDisplay.GetComponent<SpriteRenderer>();

        // Create a new Sprite using the image (Texture2D)
        Sprite sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);

        // Assign the Sprite to the Sprite Renderer
        spriteRenderer.sprite = sprite;

        int squareSize = Mathf.FloorToInt((float)image.width / desiredNumberOfTiles);
        int numberOfTiles = desiredNumberOfTiles;  // Calculate the number of tiles along each axis

        // Calculate the scale factor based on the desired size and original image size
        float scaleFactor = 1f / (float)squareSize; // Inverse of squareSize to convert from pixels to Unity units

        // Calculate the size of the maskDisplay based on the desired number of tiles and scale factor
        Vector3 maskDisplaySize = new Vector3(numberOfTiles * scaleFactor, numberOfTiles * scaleFactor, 1f);
        maskDisplay.transform.localScale = maskDisplaySize;

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

                if (mostCommonColor == Color.white)
                {
                    tilemap.SetTile(new Vector3Int((x / squareSize) - (numberOfTiles / 2), (y / squareSize) - (numberOfTiles / 2), 0), layerTile);
                }
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}