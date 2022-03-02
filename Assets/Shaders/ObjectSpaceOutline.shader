Shader "ToonExtras/ObjectSpaceOutline"
{
    Properties
    { 
        _Color("Main Color", color)  = (1.0,1.0,1.0,1.0)
        _Thickness("Thickness", float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            Cull Front
            Blend SrcAlpha OneMinusSrcAlpha
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
            };            
            
            //Uniforms
            half4 _Color;
            half _Thickness;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                const float3 displacedPosition = IN.positionOS.xyz + IN.normalOS * _Thickness;
                OUT.positionHCS = TransformObjectToHClip(displacedPosition);
                return OUT;
            }

            half4 frag() : SV_Target
            {
                return _Color;
            }
            ENDHLSL
        }
    }
}