Shader "Example/MasksG2Code"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Mask("Mask", 2D) = "black" {}
        _Tint ("Tint", Color) = (1,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;         
            };

            sampler2D _MainTex;
            sampler2D _Mask;
            half4 _Tint;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                //A
                half luminanceA = dot(tex2D(_MainTex, IN.uv).rgb, half3(0.299, 0.587, 0.114));
                half4 a = _Tint * luminanceA;

                //B
                half4 b = tex2D(_MainTex, IN.uv);

                half t = tex2D(_Mask, IN.uv).r;

                return lerp(b,a,t);
                
            }
            ENDHLSL
        }
    }
}