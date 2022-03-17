Shader "Example/MaskedColoratuionCode.shader"
{
    Properties
    {
        _Tex1 ("Texture 1", 2D) = "white" {}
        _Mask ("Mask", 2D) = "black" {}
        _Tint ("Tint", color) = (1,1,1,1)
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

            sampler2D _Tex1;
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
                half4 A = dot(tex2D(_Tex1, IN.uv), half4(0.299, 0.587, 0.114, 0.0)) * _Tint;
                half4 B = tex2D(_Tex1, IN.uv);
                half T = tex2D(_Mask, IN.uv);
                return lerp(B,A,T);
            }
            ENDHLSL
        }
    }
}