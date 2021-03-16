Shader "Unlit/ShaderSpiltScreen"
{
    Properties
    {
                 _MainTex("Rendered image of main camera", 2D) = "white" {}
         _SecondCameraTexture("The rendering of the second camera", 2D) = "white"{}
         [MaterialToggle]_SpiltScreenMode("Screen split screen mode switch",float) = 1
    }
        SubShader
         {
             Tags { "RenderType" = "Opaque" }
             LOD 100

             Pass
             {
                 CGPROGRAM
                 #pragma vertex vert
                 #pragma fragment frag


                 #include "UnityCG.cginc"

                 struct appdata
                 {
                     float4 vertex : POSITION;
                     float2 uv : TEXCOORD0;
                 };

                 struct v2f
                 {
                     float2 uv : TEXCOORD0;

                     float4 vertex : SV_POSITION;
                 };

                 sampler2D _MainTex;
                 float4 _MainTex_ST;
                 sampler2D _SecondCameraTexture;
                 float4 _SecondCameraTexture_ST;
                 bool _SpiltScreenMode;

                 v2f vert(appdata v)
                 {
                     v2f o;
                     o.vertex = UnityObjectToClipPos(v.vertex);
                     o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                     return o;
                 }

                 fixed4 frag(v2f i) : SV_Target
                 {
                     fixed4 col;

                 // Oblique rendering of different cameras
                 // There are two oblique lines: y-x=0, y+x-1=0
                 // Bring the x y value of uv into the diagonal line above, the value is greater than 0, above the line, the value is less than 0, and below the line
                if (_SpiltScreenMode) {
                    col = tex2D(_MainTex, i.uv) * ceil(i.uv.x + i.uv.y - 1) + tex2D(_SecondCameraTexture,i.uv) * ceil(1 - i.uv.x - i.uv.y);
                }
                else {
                    col = tex2D(_MainTex, i.uv) * ceil(i.uv.y - i.uv.x) + tex2D(_SecondCameraTexture,i.uv) * ceil(i.uv.x - i.uv.y);
                }



                return col;
            }
            ENDCG
        }
         }
}