using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDrop : WorldStaticObject
{
    public Sprite woodStack;
    public Sprite foodStack;
    public Sprite stoneStack;
    public Sprite mixedStacked;
    public SpriteRenderer spriteRenderer;

    public ResourceStack ressources;

    public void SetImage()
    {
        if (ressources.GetSize() > 0)
        {
            if (ressources.foodCount == 0)
            {
                if (ressources.woodCount == 0)
                {
                    spriteRenderer.sprite = stoneStack;
                }
                else if (ressources.stoneCount == 0)
                {
                    spriteRenderer.sprite = woodStack;
                }
                else
                {
                    spriteRenderer.sprite = mixedStacked;
                }
            }
            else
            {
                if (ressources.woodCount == 0 && ressources.stoneCount == 0)
                {
                    spriteRenderer.sprite = foodStack;
                }
                else
                {
                    spriteRenderer.sprite = mixedStacked;
                }
            }
        }
    }

    public void AddResources(ResourceStack _ajout)
    {
        ressources.Add(_ajout);
        SetImage();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
