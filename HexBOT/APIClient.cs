using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using static HexBOT.Wrappers.CustomObjects;

namespace HexBOT
{
    internal class APIClient
    {
        private HttpClient Client;

        public static APIClient Login(string Token, WebProxy proxy = null)
        {
            APIClient ApiClient = new()
            {
                Client = new HttpClient(new HttpClientHandler { UseCookies = false, Proxy = proxy, AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }, true)
            };

            ApiClient.Client.DefaultRequestHeaders.Add("accept", "*/*");
            ApiClient.Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            ApiClient.Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            ApiClient.Client.DefaultRequestHeaders.Add("Accept-Language", "de-DE,de;q=0.9,en-US;q=0.8,en;q=0.7");
            ApiClient.Client.DefaultRequestHeaders.Add("apollographql-client-name", "garlic-bread");
            ApiClient.Client.DefaultRequestHeaders.Add("apollographql-client-version", "0.0.1");
            ApiClient.Client.DefaultRequestHeaders.Add("authorization", $"Bearer {Token}");
            ApiClient.Client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            ApiClient.Client.DefaultRequestHeaders.Add("DNT", "1");
            ApiClient.Client.DefaultRequestHeaders.Add("Host", "gql-realtime-2.reddit.com");
            ApiClient.Client.DefaultRequestHeaders.Add("Origin", "https://garlic-bread.reddit.com");
            ApiClient.Client.DefaultRequestHeaders.Add("Referer", "https://garlic-bread.reddit.com/");
            ApiClient.Client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"112\", \"Microsoft Edge\";v=\"94\", \";Not A Brand\";v=\"99\"");
            ApiClient.Client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            ApiClient.Client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            ApiClient.Client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            ApiClient.Client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            ApiClient.Client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
            ApiClient.Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36 OPR/100.0.0.0");

            return ApiClient;
        }

        public async Task<bool> PlacePixel(Vector2 Position, int Color)
        {
            PixelRequest request = new()
            {
                operationName = "setPixel",
                query = "mutation setPixel($input: ActInput!) {\n  act(input: $input) {\n    data {\n      ... on BasicMessage {\n        id\n        data {\n          ... on GetUserCooldownResponseMessageData {\n            nextAvailablePixelTimestamp\n            __typename\n          }\n          ... on SetPixelResponseMessageData {\n            timestamp\n            __typename\n          }\n          __typename\n        }\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n",
                variables = new Variables()
                {
                    input = new Input()
                    {
                        actionName = "r/replace:set_pixel",
                        PixelMessageData = new PixelMessageData()
                        {
                            canvasIndex = 4,
                            colorIndex = Color,
                            coordinate = new Coordinate()
                            {
                                x = (int)Position.X,
                                y = (int)Position.Y
                            }
                        }
                    }
                }
            };

            string Body = JsonConvert.SerializeObject(request);

            HttpRequestMessage Payload = new(HttpMethod.Post, $"https://gql-realtime-2.reddit.com/query")
            {
                Content = new StringContent(Body, Encoding.UTF8, "application/json")
            };
            Payload.Content.Headers.ContentType.CharSet = "";

            HttpResponseMessage Response = await Client.SendAsync(Payload);

            return Response.IsSuccessStatusCode;
        }
    }
}
