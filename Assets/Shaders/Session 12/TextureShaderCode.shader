Shader "URPCustoms/TextureShaderCode"
{
    Properties
    {
        _Texture("Main Texrture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Opaq ue" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 texcoord     : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 texcoord     : TEXCOORD0;
            };

            sampler2D _Texture;
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return tex2D(_Texture, IN.texcoord);
            }
            ENDHLSL
        }
    }
}