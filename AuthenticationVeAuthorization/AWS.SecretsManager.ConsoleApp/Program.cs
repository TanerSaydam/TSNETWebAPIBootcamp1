using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;


var secret = await GetSecret();

Console.ReadLine();

static async Task<string> GetSecret()
{
    string secretName = "JWT2";
    string region = "eu-west-3";

    IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

    GetSecretValueRequest request = new GetSecretValueRequest
    {
        SecretId = secretName,
        //VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
    };

    GetSecretValueResponse response;

    try
    {
        response = await client.GetSecretValueAsync(request);
    }
    catch (Exception e)
    {
        // For a list of the exceptions thrown, see
        // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
        throw e;
    }

    string secret = response.SecretString;

    var dictionary = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(secret);

    // Dictionary içinden JWT objesini al
    var jwtDict = dictionary["JWT"];
    var jwt = new JWT(jwtDict["Issuer"], jwtDict["Audience"], jwtDict["SecretKey"]);
    return secret;

    // Your code goes here
}

public record JWT(
    string Issuer,
    string Audience,
    string SecretKey);