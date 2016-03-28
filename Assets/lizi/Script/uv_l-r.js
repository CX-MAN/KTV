var scrollSpeed:float ;
//var uvtexture:Texture;//·Å2dˆD
function Update () 
{
var offset = Time.time * scrollSpeed;
renderer.material.SetTextureOffset ("_MainTex", Vector2(-offset,0));
}