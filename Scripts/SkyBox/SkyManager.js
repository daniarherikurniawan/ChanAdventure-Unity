#pragma strict
var stars: Material;
var sky: Material;

RenderSettings.skybox = stars;

function Start()  {

	while (true)
	{
		if ( Random.value > 0.5 )
	   {
	      if (RenderSettings.skybox == sky) 
	      {
	         RenderSettings.skybox = stars; 
	      }
	      else
	      {
	         RenderSettings.skybox = sky;
	      }
	   }  
	yield WaitForSeconds(1); //adjust the time between the possible change.
	}
}