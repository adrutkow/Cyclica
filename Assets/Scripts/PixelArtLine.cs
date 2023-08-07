using UnityEditor;
using UnityEngine;

public class PixelArtLine : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Color lineColor = Color.red;   // Color of the line

    private Texture2D lineTexture;        // The texture to draw the line on

    void Start()
    {
        // Make sure a SpriteRenderer component is attached to the GameObject
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not assigned to the PixelArtLine2D script.");
            return;
        }

        // Get the sprite texture from the SpriteRenderer
        Texture2D spriteTexture = spriteRenderer.sprite.texture;

        // Create a copy of the sprite texture to draw the line on
        lineTexture = new Texture2D(spriteTexture.width, spriteTexture.height);
        Color[] pixels = spriteTexture.GetPixels();
        lineTexture.SetPixels(pixels);

        // Draw the line on the texture
        DrawPixelArtLine(new Vector2Int(10, 10), new Vector2Int(50, 20));

        // Set the modified texture as the sprite texture
        spriteRenderer.sprite = Sprite.Create(lineTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f));
        TextureImporter textureImporter = new TextureImporter();
        textureImporter.spritePixelsPerUnit = 16;
        textureImporter.filterMode = FilterMode.Point;
        //spriteRenderer.sprite.

    }

    private void DrawPixelArtLine(Vector2 start, Vector2 end)
    {
        // Use Bresenham's line algorithm to draw the line
        int dx = Mathf.Abs((int)end.x - (int)start.x);
        int dy = Mathf.Abs((int)end.y - (int)start.y);
        int sx = (start.x < end.x) ? 1 : -1;
        int sy = (start.y < end.y) ? 1 : -1;
        int err = dx - dy;

        for (int i=0; i < 20; i++)
        {
            lineTexture.SetPixel((int)start.x, (int)start.y, lineColor);
            if (start == end)
                break;
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                start.x += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                start.y += sy;
            }
        }

        // Apply the changes to the texture
        lineTexture.Apply();
    }
}