Shader "Example/BorderDetectionG2"
{
    Properties
    {
        _FillColor("Fill Color", Color) = (1,1,1,1)
        _OutlineColor("Outline Color", Color) = (1,1,0,1)
        _OutlineThreshold("Outline Threshold", float) = 0.5
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
                float3 normalOS     : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float3 normalWS : NORMAL;
                float3 viewDirWS : TEXCOORD0;
            };

            half4 _FillColor;
            half4 _OutlineColor;
            half _OutlineThreshold;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.viewDirWS = normalize(GetCameraPositionWS() - TransformObjectToWorld(IN.positionOS.xyz));
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half rim = dot(normalize(IN.normalWS), IN.viewDirWS);
                half outlineFactor = step(_OutlineThreshold, rim);
                return lerp(_OutlineColor, _FillColor, outlineFactor);
            }
            ENDHLSL
        }
    }
}