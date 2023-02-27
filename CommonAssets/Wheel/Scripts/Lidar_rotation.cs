using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidar_rotation : MonoBehaviour
{
    //Scroll main texture based on time
    //Definition of the public slot available in the inspector to insert all 10 textures
    //Definition of the material "mat" onto which the modification will be applied
    public float scrollSpeed = 0.5f;
    public Texture blue, red, yellow, green, light_blue;
    public Texture blue_hdr, red_hdr, yellow_hdr, green_hdr, light_blue_hdr;
    private int current_index;
    Renderer rend;
    Material mat;
    private Texture[] textures;
    private Texture current_texture;

    private Texture[] emissive;
    private Texture current_emissive;


    //Creation of two array to store both the color textures and light maps of the lidar
    void Awake()
    {

        //Set the array for the lidar texture
        textures = new Texture[5];
        textures[0] = blue;
        textures[1] = red;
        textures[2] = yellow;
        textures[3] = green;
        textures[4] = light_blue;

        //Set the array for the lidar emissive map
        emissive = new Texture[5];
        emissive[0] = blue_hdr;
        emissive[1] = red_hdr;
        emissive[2] = yellow_hdr;
        emissive[3] = green_hdr;
        emissive[4] = light_blue_hdr;
    }

    //Defining the start texture as well as where they are to be applied
    void Start()
    {
        rend = GetComponent<Renderer>();
        print(rend.material);
        mat = rend.materials[1];
        current_index = 0;
        current_texture = blue;
        current_emissive = blue_hdr;
        mat.SetTexture("_MainTex", current_texture);
        mat.SetTexture("_EmissionMap", current_emissive);
        //mat.SetColor("_EmissionColor", Color.white);

    }

    //Applying the texture and emissive map onto the material
    void Update()
    {
        TextureKeyboardControl();
        float offset = Time.time * scrollSpeed;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        mat.SetTextureOffset("_EmissionMap", new Vector2(offset, 0));
    }


    //Allowing acces to each color with the left and right key
    private void TextureKeyboardControl()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (current_index == 4)
            {
                current_index = 0;
            }
            else
            {
                current_index++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (current_index == 0)
            {
                current_index = 4;
            }
            else
            {
                current_index--;
            }
        }
        current_texture = textures[current_index];
        current_emissive = emissive[current_index];

        mat.SetTexture("_MainTex", current_texture);
        mat.SetTexture("_EmissionMap", current_emissive);

    }
}