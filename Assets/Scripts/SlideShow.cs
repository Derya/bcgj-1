using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (GUITexture))]
public class SlideShow : MonoBehaviour
{
   public Texture2D[] slides = new Texture2D[1];
   public float changeTime = 10.0f;
   private int currentSlide = 0;
   private float timeSinceLast = 1.0f;

    GUITexture guiTexture;


   void Start()
   {
        guiTexture = GetComponent<GUITexture>();
        guiTexture.texture = slides[currentSlide];
        guiTexture.pixelInset = new Rect(-slides[currentSlide].width/2, -slides[currentSlide].height/2, slides[currentSlide].width, slides[currentSlide].height);
        currentSlide++;
   }
 
   void Update()
   {
        if(timeSinceLast > changeTime && currentSlide < slides.Length)
        {
            guiTexture.texture = slides[currentSlide];
            guiTexture.pixelInset = new Rect(-slides[currentSlide].width/2, -slides[currentSlide].height/2, slides[currentSlide].width, slides[currentSlide].height);
            timeSinceLast = 0.0f;
            currentSlide++;
        }
        // comment out this section if you don't want the slide show to loop
        // -----------------------
        if(currentSlide == slides.Length)
        {
            currentSlide = 0;
        }
        // ------------------------
        timeSinceLast += Time.deltaTime;
   }
}