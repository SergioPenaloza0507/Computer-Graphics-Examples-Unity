Shader "URPCustoms/RimBorderDetector"
{
    Properties
    {
        _Color ("Main Color", color) = (1,0.5,0,1)
        _BorderColor ("Border Color", color) = (0,0,0,1)
        _BorderSize("Border Size", Range(0,1)) = 0.0
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
                float3 normalOS : NORMAL;
            };

            

            half4 _Color;
            half4 _BorderColor;
            half _BorderSize;

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float3 normalWS : NORMAL;
                float3 viewDirWS : TEXCOORD2;
            };
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.viewDirWS = GetCameraPositionWS() - TransformObjectToWorld(IN.positionOS.xyz);
                return OUT;
            }

            half3 frag(Varyings IN) : SV_Target
            {
                const float3 normal = normalize(IN.normalWS);
                const float3 viewDir = normalize(IN.viewDirWS);
                //Not actually an accurate fresnel reflection model
                const float fresnel = saturate(dot(normal, viewDir));
                const float border = step(_BorderSize, fresnel);
                return lerp(_BorderColor,_Color, border);
            }
            ENDHLSL
        }
    }
}