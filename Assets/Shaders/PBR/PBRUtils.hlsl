//#define INTELLISENSE_ACTIVE
#define PI 3.141592654f

#if defined(INTELLISENSE_ACTIVE)
//Include core library and lighting for intellisense purposes
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#endif

#define LIGHT_PARAMETERS float3 lightColor, float3 lightDirectionWS, float distanceAttenuation, float shadowAttenuation, float3 ambientLighting
#define GEOMETRY_PARAMETERS float3 positionWS, float3 normalWS, float3 viewDirectionWS
#define SURFACE_PARAMETERS float3 albedo, float3 roughness, float3 metallic, float ambientOcclusion

/*
 *Fresnel term calculation using Schlick's approximation
 *https://en.wikipedia.org/wiki/Schlick%27s_approximation
 *F0 : surface reflection at zero incidence
*/
float3 FresnelSchlick(float cosTheta, float3 F0);

/*Trowbridge-Reitz GGX Distribution fucntion
 *(Trowbridge and Reitz, 1975) https://www.scitepress.org/Papers/2021/102527/102527.pdf
*/
float DistributionGGX(float3 normal, float3 halfwayLightVector, float roughness);

float GeometrySchlickGGX(float normalDotViewDirection, float roughness);

float GeometrySmith(float3 normal, float3 viewDirection, float3 lightDirection, float roughness);

/*
 * Physically based rendering lighting function in linear space, remember to gamma correct this function's result
 */
void PBRLighting_float(LIGHT_PARAMETERS, GEOMETRY_PARAMETERS, SURFACE_PARAMETERS, out float3 shadedFragment)
{
    const float3 normal = normalize(normalWS);
    const float3 viewDirection = normalize(viewDirectionWS);

    const float3 lightDirection = normalize(lightDirectionWS);
    const float3 halfWayLightVector = normalize(viewDirection + lightDirection);

    const float3 radiance = lightColor * distanceAttenuation * shadowAttenuation;

    float3 F0 = 0.4f;
    F0 = lerp(F0, albedo, metallic);
    const float3 fresnel = FresnelSchlick(max(dot(halfWayLightVector, viewDirection), 0.0), F0);


    //Cook-Torrance BRDF
    const float normalDistributionFunction = DistributionGGX(normal, halfWayLightVector, roughness);
    const float geometryDistributionFunction = GeometrySmith(normal, viewDirection, lightDirection, roughness);

    const float3 numerator = normalDistributionFunction * geometryDistributionFunction * fresnel;
    const float3 denominator = 4.0f * max(dot(normal, viewDirection), 0.0f) * max(dot(normal,lightDirection), 0.0f) + 0.0001f;
    const float3 specular = numerator / denominator;

    const float3 reflectedEnergy = fresnel;
    float3 diffusedEnergy = 1.0f - reflectedEnergy;

    diffusedEnergy *= 1.0 - metallic;

    const float normalDotLightDirection = max(dot(normal, lightDirection), 0.0f);
    const float3 reflectanceResult = (diffusedEnergy * albedo / PI + specular) * radiance * normalDotLightDirection;

    const float3 ambient = ambientLighting * albedo * ambientOcclusion;
    shadedFragment = ambient + reflectanceResult;
}

float3 FresnelSchlick(float cosTheta, float3 F0)
{
    return F0 + (1.0f - F0) * pow(clamp(1.0f - cosTheta, 0.0f, 1.0f), 5.0);
}

float DistributionGGX(float3 normal, float3 halfwayLightVector, float roughness)
{
    const float roughnessSquared = roughness * roughness;
    const float roughnessSquared2 = roughnessSquared * roughnessSquared;
    const float normalDotHalfway = max(dot(normal, halfwayLightVector), 0.0f);
    const float normalDotHalfwaySquared = normalDotHalfway * normalDotHalfway;

    const float numerator = roughnessSquared2;
    float denominator = normalDotHalfwaySquared * (roughnessSquared2 - 1.0f) + 1.0f;

    denominator = PI * denominator * denominator;

    return numerator/denominator;
}

float GeometrySchlickGGX(float normalDotViewDirection, float roughness)
{
    const float roughnessTerm = roughness + 1.0f;
    const float remappedRoughnessTerm = (roughnessTerm * roughnessTerm) / 8.0f;

    const float numerator = normalDotViewDirection;
    const float denominator = normalDotViewDirection * (1 - remappedRoughnessTerm) + remappedRoughnessTerm;
    return numerator / denominator;
}

float GeometrySmith(float3 normal, float3 viewDirection, float3 lightDirection, float roughness)
{
    float normalDotViewDirection = max(dot(normal, viewDirection), 0.0f);
    float normalDotLightDirection = max(dot(normal, lightDirection), 0.0f);
    float geometryObstructionDistribution = GeometrySchlickGGX(normalDotViewDirection, roughness);
    float geometryShadowingDistribution = GeometrySchlickGGX(normalDotLightDirection, roughness);

    return geometryObstructionDistribution * geometryShadowingDistribution;
}
    
