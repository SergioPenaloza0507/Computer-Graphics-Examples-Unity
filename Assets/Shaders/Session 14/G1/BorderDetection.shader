Shader "Example/BorderDetectionG1"
{
    Properties
    { 
        _MainColor("Main Color", Color) = (1,1,1,1)
        _LineColor("Outline Color", Color) = (0,1,0,1)
        _LineThreshold("Outline Threshold", float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include  "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float3 normalWS     : NORMAL;
                float3 viewDirWS    : TEXCOORD0;
            };   

            half4 _MainColor;
            half4 _LineColor;
            half _LineThreshold;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.viewDirWS = normalize(GetCameraPositionWS() - TransformObjectToWorld(IN.positionOS));
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half factor = dot(normalize(IN.normalWS), normalize(IN.viewDirWS));
                half outlineFactor = step(_LineThreshold, factor);
                return lerp(_LineColor, _MainColor, outlineFactor);
            }
            ENDHLSL
        }
    }
}