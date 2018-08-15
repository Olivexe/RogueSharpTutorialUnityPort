using UnityEngine;
using UnityEngine.UI;
using RogueSharpTutorial.Utilities;

namespace RogueSharpTutorial.View
{
    public class TileUnity : PooledObject
    {
        [SerializeField] GameObject backgroundObject;
        [SerializeField] GameObject spriteObject;
        [SerializeField] GameObject textObject;

        public bool IsAsciiTile { get; set; }

        /// <summary>
        /// Enable or disable the background and character sprites and text object. Will disable/enable the underlying gameobject for each.
        /// </summary>
        /// <param name="active"></param>
        public bool TileActive
        {
            set
            {
                if (IsAsciiTile)
                {
                    backgroundObject.SetActive(value);
                    spriteObject.SetActive(false);
                    textObject.SetActive(value);
                }
                else
                {
                    backgroundObject.SetActive(value);
                    spriteObject.SetActive(value);
                    textObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Set or Get the sprite of the Background Image.
        /// </summary>
        public Sprite BackgroundImage
        {
            get { return backgroundObject.GetComponent<SpriteRenderer>().sprite; }
            set { backgroundObject.GetComponent<SpriteRenderer>().sprite = value; }
        }

        /// <summary>
        /// Set or Get the alpha component of the background image as float. The alpha value must be between 0 and 1.
        /// </summary>
        /// <param name="a"></param>
        public float BackgroundAlpha
        {
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;

                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(backgroundObject.GetComponent<SpriteRenderer>().color.r,
                                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.g,
                                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.b,
                                                                                  value);
            }
            get
            {
                return backgroundObject.GetComponent<SpriteRenderer>().color.a;
            }
        }

        /// <summary>
        /// Set or Get the alpha component of the background image as int. The alpha value must be between 0 and 255.
        /// </summary>
        /// <param name="a"></param>
        public int BackgroundAlphaInt
        {
            set
            {
                if (value < 0) value = 0;
                if (value > 255) value = 255;

                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(backgroundObject.GetComponent<SpriteRenderer>().color.r,
                                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.g,
                                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.b,
                                                                                  value / 255);
            }
            get
            {
                return (int)(backgroundObject.GetComponent<SpriteRenderer>().color.a * 255);
            }
        }

        /// <summary>
        /// Set the color of the background of the tile. Maintain the the old alpha value. No getter attached to this property.
        /// </summary>
        /// <param name="color"></param>
        public Color BackgroundColor
        {
            set
            {
                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(value.r, value.g, value.b, backgroundObject.GetComponent<SpriteRenderer>().color.a);
            }
        }

        /// <summary>
        /// Set or Get the color of the background of the tile.
        /// </summary>
        /// <param name="color"></param>
        public Color BackgroundColorWithAlpha
        {
            get
            {
                return backgroundObject.GetComponent<SpriteRenderer>().color;
            }
            set
            {
                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(value.r, value.g, value.b, value.a);
            }
        }

        /// <summary>
        /// Set or Get the sprite of the foreground (primary sprite) Image.
        /// </summary>
        public Sprite SpriteImage
        {
            get { return spriteObject.GetComponent<SpriteRenderer>().sprite; }
            set { spriteObject.GetComponent<SpriteRenderer>().sprite = value; }
        }

        /// <summary>
        /// Set or Get the alpha component of the foreground (primary sprite) image as float. The alpha value must be between 0 and 1.
        /// </summary>
        /// <param name="a"></param>
        public float MainSpriteAlpha
        {
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;

                spriteObject.GetComponent<SpriteRenderer>().color = new Color(spriteObject.GetComponent<SpriteRenderer>().color.r,
                                                                              spriteObject.GetComponent<SpriteRenderer>().color.g,
                                                                              spriteObject.GetComponent<SpriteRenderer>().color.b,
                                                                              value);
            }
            get
            {
                return spriteObject.GetComponent<SpriteRenderer>().color.a;
            }
        }

        /// <summary>
        /// Set or Get the alpha component of the foreground (primary sprite) image as int. The alpha value must be between 0 and 255.
        /// </summary>
        /// <param name="a"></param>
        public int MainSpriteAlphaInt
        {
            set
            {
                if (value < 0) value = 0;
                if (value > 255) value = 255;

                spriteObject.GetComponent<SpriteRenderer>().color = new Color(spriteObject.GetComponent<SpriteRenderer>().color.r,
                                                                              spriteObject.GetComponent<SpriteRenderer>().color.g,
                                                                              spriteObject.GetComponent<SpriteRenderer>().color.b,
                                                                              value / 255);
            }
            get
            {
                return (int)(spriteObject.GetComponent<SpriteRenderer>().color.a * 255);
            }
        }

        /// <summary>
        /// Set the color of the foreground (primary sprite) of the tile. Maintain the the old alpha value. No getter attached to this property.
        /// </summary>
        /// <param name="color"></param>
        public Color MainSpriteColor
        {
            set
            {
                spriteObject.GetComponent<SpriteRenderer>().color = new Color(value.r, value.g, value.b, spriteObject.GetComponent<SpriteRenderer>().color.a);
            }
        }

        /// <summary>
        /// Set or Get the color of the foreground (primary sprite) of the tile.
        /// </summary>
        /// <param name="color"></param>
        public Color MainSpriteColorWithAlpha
        {
            get
            {
                return spriteObject.GetComponent<SpriteRenderer>().color;
            }
            set
            {
                spriteObject.GetComponent<SpriteRenderer>().color = new Color(value.r, value.g, value.b, value.a);
            }
        }

        /// <summary>
        /// Set the value of the text object in the tile.
        /// </summary>
        public char Text
        {
            get
            {
                string val = textObject.GetComponent<Text>().text;
                char result = string.IsNullOrEmpty(val) ? ' ' : val[0];
                return result;
            }
            set
            {
                string val = "";
                val += value;
                textObject.GetComponent<Text>().text = val;
            }
        }

        /// <summary>
        /// Set or Get the alpha component of the text object as float. The alpha value must be between 0 and 1.
        /// </summary>
        /// <param name="a"></param>
        public float TextAlpha
        {
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;

                textObject.GetComponent<Text>().color = new Color(textObject.GetComponent<Text>().color.r,
                                                                  textObject.GetComponent<Text>().color.g,
                                                                  textObject.GetComponent<Text>().color.b,
                                                                  value);
            }
            get
            {
                return textObject.GetComponent<Text>().color.a;
            }
        }

        /// <summary>
        /// Set or Get the alpha component of the text object as int. The alpha value must be between 0 and 255.
        /// </summary>
        /// <param name="a"></param>
        public int TextAlphaInt
        {
            set
            {
                if (value < 0) value = 0;
                if (value > 255) value = 255;

                textObject.GetComponent<Text>().color = new Color(textObject.GetComponent<Text>().color.r,
                                                                  textObject.GetComponent<Text>().color.g,
                                                                  textObject.GetComponent<Text>().color.b,
                                                                  value / 255);
            }
            get
            {
                return (int)(textObject.GetComponent<Text>().color.a * 255);
            }
        }

        /// <summary>
        /// Set the color of the text object of the tile. Maintain the the old alpha value. No getter attached to this property.
        /// </summary>
        /// <param name="color"></param>
        public Color TextColor
        {
            set
            {
                textObject.GetComponent<Text>().color = new Color(value.r, value.g, value.b, textObject.GetComponent<Text>().color.a);
            }
        }

        /// <summary>
        /// Set or Get the color of the text object of the tile.
        /// </summary>
        /// <param name="color"></param>
        public Color TextColorWithAlpha
        {
            get
            {
                return textObject.GetComponent<Text>().color;
            }
            set
            {
                textObject.GetComponent<Text>().color = new Color(value.r, value.g, value.b, value.a);
            }
        }
    }
}