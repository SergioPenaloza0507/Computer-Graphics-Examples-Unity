void GetMainLightData_half(half3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAttenuation, out half ShadowAttenuation)
{
    #if defined(SHADERGRAPH_PREVIEW)
    Direction = half3(0.5, 0.5, 0);
    Color = 1;
    DistanceAttenuation = 1;
    ShadowAttenuation = 1;
    #else
    #if defined(SHADOWS_SCREEN)
    half4 clipPos = TransformWorldToHClip(WorldPos);
    half4 shadowCoord = ComputeScreenPos(clipPos);
    #else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
    #endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAttenuation = mainLight.distanceAttenuation;
    ShadowAttenuation = mainLight.shadowAttenuation;
    #endif
}