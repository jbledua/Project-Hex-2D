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

    // Start is called before the first frame update
    void Start()
    {
        // Get the Sprite Renderer from the GameObject
        SpriteRenderer spriteRenderer = maskDisplay.GetComponent<SpriteRenderer>();

        // Create a new Sprite using the image (Texture2D)
        Sprite sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);

        // Assign the Sprite to the Sprite Renderer
        spriteRenderer.sprite = sprite;

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

                if (mostCommonColor == Color.white)
                {
                    tilemap.SetTile(new Vector3Int(x / squareSize - numberOfTiles / 2, y / squareSize - numberOfTiles / 2, 0), layerTile);
                }

                //Tile tile = GetTileForColor(mostCommonColor);  // You'll need to implement this function
                //tilemap.SetTile(new Vector3Int(x / squareSize - numberOfTiles / 2, y / squareSize - numberOfTiles / 2, 0), tile);
            }
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
