Shader "Example/URPUnlitShaderBasic"
{
    Properties
    {
        _Color("Main Color", color) = (0.0,0.0,1.0,1.0)
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

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 _Color;
            
            half4 frag() : SV_Target
            {
                return _Color;
            }
            ENDHLSL
            
            //float - decimal - 32 bits
            //half - decimal - 16 bits
            //fixed - decimal - 4 bits
            //float2 , 3 , 4 float2x2, float3x3 (decimal, decimal) - 32 bits
            //half2, 3, 4, half 2x2, half3x3
            //fixed ...
            
            //sampler2D 
            //sampler3D 
            //samplerCUBE 
        }
    }
}