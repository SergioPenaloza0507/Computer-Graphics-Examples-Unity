#define PI 3.141592654f

float DistributionGGX(float3 normal, float3 halfwayVector, float roughness);

float3 FresnelSchlick(float3 F0, float3 halfwayVector, float3 viewDirection);

float GeometrySchlickGGX(float3 normal, float3 viewDirection, float roughness);

float GeometrySmith(float geometryView, float geometryLight);

float CookTorranceBRDF(float D, float3 F, float G, float3 viewDirection, float3 lightDirection, float3 normal);

void PBRShading_float
(
    //Light properties
    float3 lightColor,
    float3 lightDirection,

    //Surface Properties
    float3 albedo,
    float3 normal,
    float roughness,
    float metalness,
    float ambientOcclusion,

    //Camera Properties
    float3 viewDirection
)
{
    
}

float DistributionGGX(float3 normal, float3 halfwayVector, float roughness)
{
    const float squaredRoughness = roughness * roughness;
    const float ndoth = dot(normal, halfwayVector);
    float denom = PI * pow((ndoth * ndoth) * (squaredRoughness - 1) + 1, 2);

    return squaredRoughness/denom;
}

float3 FresnelSchlick(float3 F0, float3 halfwayVector, float3 viewDirection)
{
    return F0 + (1- F0)* pow(1 - dot(halfwayVector, viewDirection), 5);
}

float GeometrySchlickGGX(float3 normal, float3 viewDirection, float roughness)
{
    float K = (roughness + 1);
    K *= K;
    K /= 8;

    float ndotv = dot(normal, viewDirection);
    float denom = ndotv * (1-K) + K;

    return ndotv/denom;
}

float GeometrySmith(float geometryView, float geometryLight)
{
    return geometryView * geometryLight;
}

float CookTorranceBRDF(float D, float3 F, float G, float3 viewDirection, float3 lightDirection, float3 normal)
{
    float num = D*F*G;
    float denom = 4 * dot(viewDirection, normal) * dot(lightDirection, normal);
}


