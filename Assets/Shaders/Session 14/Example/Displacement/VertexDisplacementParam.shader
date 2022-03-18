Shader "URPCustoms/VertexDisplacementParam"
{
    Properties
    {
        _Color ("Main Color", color) = (1,0.5,0,1)
        _Texture("Main Texture", 2D) = "white" {}
        _DisplacementAmount ("Displacement Amount", range(0,1)) = 0.0 
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
                float3 normal       : NORMAL;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            half4 _Color;
            half _DisplacementAmount;
            sampler2D _Texture;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz + IN.normal * _DisplacementAmount);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return tex2D(_Texture, IN.uv) * _Color;
            }
            ENDHLSL
        }
    }
}