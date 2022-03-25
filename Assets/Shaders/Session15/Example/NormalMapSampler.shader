Shader "Example/NormalMapSampler.shader"
{
    Properties
    {
        _NormalMap("Normal Map", 2D) = "bump" {}   
        _NormalScale("Normal Map Scale",  float) = 0.0 
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #define LIGHT_DIR float3(1, 1, 1)
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float3 tangentOS    : TANGENT;
                float2 texcoord     : TEXCOORD;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float3 normalOS     : NORMAL;
                float3 tangentOS    : TANGENT;
                float3 binormalOS   : BINORMAL;
                float2 texcoord     : TEXCOORD0;
            };            

            sampler2D _NormalMap;
            half _NormalScale;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalOS = IN.normalOS;
                OUT.tangentOS = IN.tangentOS;
                OUT.binormalOS = normalize(cross(OUT.tangentOS, OUT.normalOS));
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                //Unity compressed normal map
                const float3 tangentSpaceNormalMap = UnpackNormalScale(tex2D(_NormalMap, IN.texcoord), _NormalScale);
                const float3x3 TBNMatrix = transpose(float3x3(IN.tangentOS, IN.binormalOS, IN.normalOS));
                const float3 objectSpaceNormals = mul(TBNMatrix, tangentSpaceNormalMap);
                const float3 worldSpaceNormals = normalize(TransformObjectToWorldNormal(objectSpaceNormals));
                return saturate(dot(worldSpaceNormals, LIGHT_DIR)) + 0.5;
            }
            ENDHLSL
        }
    }
}