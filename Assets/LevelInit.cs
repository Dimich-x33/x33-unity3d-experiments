using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    public Vector2Int fieldDimensions = new Vector2Int(10, 10);
    public Vector2Int textureSpritesDimensions = new Vector2Int(8, 6);
    public int spriteSize = 32;
    public Vector2 pivot = new Vector2(0.5f, 0.5f);
    public GameObject[] spriteObjects;
    public Sprite[] sprites;

    public string text = "Hello";

    public float updateSpriteRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            // this.spriteObjects = new GameObject[fieldDimensions.x * fieldDimensions.y];

            // sprites = LoadSprites("Textures");

            // FillFieldWithSprite(sprites[0]);

            // StartCoroutine(UpdateSpriteObject());

            LoadText();

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
            var r = Random.Range(0, fieldDimensions.x * fieldDimensions.y);
            var s = Random.Range(0, textureSpritesDimensions.x * textureSpritesDimensions.y);
            this.spriteObjects[r].GetComponent<SpriteRenderer>().sprite = sprites[s];

            yield return new WaitForSeconds(updateSpriteRate);
        }
    }

    public void LoadText() {
        this.text = Resources.Load<TextAsset>("map").text;
        // print(text);
    }

    public void OnGUI () {
        var style = new GUIStyle();
        style.fontSize = 40;
        style.border = new RectOffset(1, 1, 1, 1);
        style.fontStyle = FontStyle.Italic;
        style.richText = true;
        GUI.Label(new Rect(Screen.width / 2 - 100f, Screen.height / 2, 200f, 200f), this.text, style);
    }

    public Sprite[] LoadSprites(string path) {
        var texture2D = Resources.Load<Texture2D>(path);

        if (texture2D == null) {
            throw new System.Exception("Texture not loaded");
        }

        var sprites = new Sprite[textureSpritesDimensions.x * textureSpritesDimensions.y];

        for (int i = 0, c = 0; i < textureSpritesDimensions.y; ++i) {
            for (int j = 0; j < textureSpritesDimensions.x; ++j, ++c)
            {
                sprites[c] = Sprite.Create(
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
