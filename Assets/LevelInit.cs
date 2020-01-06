using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            this.obj = new GameObject("Level");
            var spriteSize = 32;
            var pivot = new Vector2(0.5f, 0.5f);

            var texture2D = Resources.Load<Texture2D>("Textures");

            if (texture2D == null) {
                throw new System.Exception("Texture not loaded");
            }
            
            var sprites = new Sprite[48];

            for (int i = 0, current = 0; i < 6; ++i) {
                for (int j = 0; j < 8; ++j, ++current)
                {
                    sprites[current] = Sprite.Create(
                        texture2D,
                        new Rect(spriteSize * j, spriteSize * i, spriteSize, spriteSize),
                        pivot
                    );
                }
            }

            var rend = obj.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;

            rend.transform.position = Vector3.zero;
            rend.sprite = sprites[0];

            print("Success!!!");
        }
        catch (System.Exception ex)
        {
            print(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
