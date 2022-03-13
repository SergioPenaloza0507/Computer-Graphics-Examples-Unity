Shader "URPCustoms/Session12Code"
{
    Properties
    {
        _TextureA ("Texture A", 2D) = "white" {}
        _TextureB ("Texture B", 2D) = "black" {}
        _T ("Interpolation", Range(0,1)) = 0.0
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
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _TextureA;
            sampler2D _TextureB;
            half _T;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                const half4 aCol = tex2D(_TextureA, IN.uv);
                const half4 bCol = tex2D(_TextureB, IN.uv);
                return lerp(aCol, bCol, _SinTime.w * 0.5 + 0.5);
            }
            ENDHLSL
        }
    }
}