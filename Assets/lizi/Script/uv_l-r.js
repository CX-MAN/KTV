var scrollSpeed:float ;
//var uvtexture:Texture;//��2d�D
function Update () 
{
var offset = Time.time * scrollSpeed;
renderer.material.SetTextureOffset ("_MainTex", Vector2(-offset,0));
}