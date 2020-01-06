using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    public Vector2Int fieldDimensions = new Vector2Int(10, 10);
    public int spriteSize = 32;
    public Vector2 pivot = new Vector2(0.5f, 0.5f);
    public GameObject[] spriteObjects;
    public Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            this.spriteObjects = new GameObject[fieldDimensions.x * fieldDimensions.y];

            var sprites = LoadSprites("Textures");

            FillFieldWithSprite(sprites[0]);

            StartCoroutine(UpdateSpriteObject());

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

    public IEnumerator UpdateSpriteObject() {
        while (true)
        {
            print("Tick");
            yield return new WaitForSeconds(1f);
        }
    }

    public Sprite[] LoadSprites(string path) {
        var texture2D = Resources.Load<Texture2D>(path);

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

        return sprites;
    }

    public void FillFieldWithSprite(Sprite initialSprite) {
        for (int y = 0, c = 0; y < fieldDimensions.y; ++y)
        {
            for (int x = 0; x < fieldDimensions.x; ++x, ++c)
            {
                var obj = new GameObject($"{y}*{x}");
                var rend = obj.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;

                rend.sprite = initialSprite;
                rend.transform.localScale = new Vector3(3f, 3f, 3f);
                rend.transform.position = new Vector3(x - fieldDimensions.x / 2, y - fieldDimensions.y / 2, Vector3.zero.z);
                this.spriteObjects[c] = obj;
            }
        }
    }
}
