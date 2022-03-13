Shader "URPCustoms/ColorShaderCode"
{
    Properties
    {
        _ColorA ("ColorA", color) = (1,0.0,0,1)
        _ColorB ("ColorA", color) = (0,1,0,1)
        _Interp ("Interpolation", float) = 0.5
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
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
            };

            half4 _ColorA;
            half4 _ColorB;
            half _Interp;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag() : SV_Target
            {
                return lerp(_ColorA, _ColorB, _Interp);
            }
            ENDHLSL
        }
    }
}