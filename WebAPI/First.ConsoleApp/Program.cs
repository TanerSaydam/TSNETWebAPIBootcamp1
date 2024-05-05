using System.Net.Http.Json;

HttpClient httpClient = new();

HttpResponseMessage message = await httpClient.GetAsync("https://localhost:7107/api/Todos");

var result = await message.Content.ReadFromJsonAsync<List<string>>();

Console.ReadLine();