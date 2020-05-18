using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SVU.Logging.IServices;
using SVU.Logging.Models.SaveMyData;
using SVU.Shared.Static;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SVU.Logging.Services
{
    /// <summary>
    /// logs any data into Savemydata.sariahouloubi.com
    ///     for docs on the APIs <see cref="https://savemydata.sariahouloubi.com/home/apiguid"/>
    /// </summary>
    public class SaveMyDataLoggingService : ILoggingService
    {
        #region Properties
        /// <summary>
        /// The authentication token to work with the requests
        /// </summary>
        public string AuthToken { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public SaveMyDataConfiguration SaveMyDataConfiguration { get; private set; }
        /// <summary>
        /// The base url to send the data into
        /// </summary>
        public static Uri BASE_URL = new Uri("http://savemydata.sariahouloubi.com/");
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public SaveMyDataLoggingService(IConfiguration configuration)
        {
            SaveMyDataConfiguration = new SaveMyDataConfiguration();
            //Bind the section values
            configuration.GetSection("SaveMyData").Bind(SaveMyDataConfiguration);
        }
        #endregion

        public async Task LogRequest(HttpRequest httpRequest)
        {

            #region Read the request body if any
            string requestBody = "No content";
            //If there was any content provided with the request
            if (httpRequest.ContentLength > 0)
            {
                var injectedRequestStream = new MemoryStream();
                using (StreamReader reader = new StreamReader(httpRequest.Body))
                {
                    requestBody = await reader.ReadToEndAsync();
                    //Rewrite the body back to the request
                    var bytesToWrite = Encoding.UTF8.GetBytes(requestBody);
                    injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                    injectedRequestStream.Seek(0, SeekOrigin.Begin);
                    httpRequest.Body = injectedRequestStream;
                }
            }
            #endregion
            await CreateRecord(SaveMyDataConfiguration.DbName, SaveMyDataConfiguration.RequestsTableName, new
            {
                httpRequest.Method,
                Host = httpRequest.Host.HasValue ? httpRequest.Host.Host : "No host found",
                httpRequest.IsHttps,
                QueryString = httpRequest.QueryString.HasValue ? httpRequest.QueryString.Value : "No Query string values",
                httpRequest.Path,
                httpRequest.PathBase,
                ContentType = httpRequest.ContentType ?? "No ContentType",
                Content = requestBody,
                IP4 = httpRequest.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                DataTime = DateTime.UtcNow.ToString()
            });
        }

        public async Task LogException(Exception ex)
        {
            await CreateRecord(SaveMyDataConfiguration.DbName, SaveMyDataConfiguration.ExceptionTableName, new
            {
                ex.Message,
                BaseMessage = ex.GetBaseException().Message,
                Source = ex.Source ?? "Unkown Source",
                StackTrace = ex.StackTrace ?? "Unkown stacktrace",
                DataTime = DateTime.UtcNow.ToString()
            });
        }

        #region Helpers
        /// <summary>
        /// Tries to get an authenication token based on the set username and password
        /// </summary>
        /// <returns></returns>
        public async Task GetAuthToken()
        {
            try
            {
                //Create the http client
                using (var httpClient = new HttpClient())
                {
                    //Set the base address
                    httpClient.BaseAddress = BASE_URL;
                    //Create the request
                    using (var request = new HttpRequestMessage())
                    {
                        //Set the request method
                        request.Method = HttpMethod.Post;
                        //Set the request body
                        request.Content = new StringContent(JsonConvert.SerializeObject(new
                        {
                            email = SaveMyDataConfiguration.Email,
                            password = SaveMyDataConfiguration.Password
                        }), Encoding.UTF8, "application/json");

                        //Set the request uri
                        request.RequestUri = new Uri("/api/authapi/login", UriKind.Relative);
                        //Send the request
                        var response = await httpClient.SendAsync(request);
                        //Check if the response is sucessfull
                        if (response.IsSuccessStatusCode)
                        {
                            //try to read the data
                            var data = await response.Content.ReadAsAsync<LoginUserResponseModel>();

                            if (data != null)
                            {
                                AuthToken = $"Bearer {data.Token}";
                                IsAuthenticated = true;
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Console.WriteLine(ex.GetBaseException().Message);
            }
            IsAuthenticated = false;
        }

        /// <summary>
        /// Sends a post request to create a new record
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task CreateRecord(string dbName, string table, object data)
        {
            if (IsAuthenticated)
            {
                try
                {
                    //Create the http client
                    using (var httpClient = new HttpClient())
                    {
                        //Set the base address
                        httpClient.BaseAddress = BASE_URL;
                        //Create the request
                        using (var request = new HttpRequestMessage())
                        {
                            //Add the auth header
                            request.Headers.Add(RequestHeaderNames.Authorization, AuthToken);
                            //Set the request method
                            request.Method = HttpMethod.Post;
                            //Set the request body
                            request.Content = new StringContent(JsonConvert.SerializeObject(new
                            {
                                Database = dbName,
                                Table = table,
                                Data = data
                            }), Encoding.UTF8, "application/json");

                            //Set the request uri
                            request.RequestUri = new Uri("/api/recordapi/create", UriKind.Relative);
                            //Send the request
                            var response = await httpClient.SendAsync(request);
                            //Check if the response is sucessfull
                            if (response.IsSuccessStatusCode)
                            {
                                return;
                            }
                        }
                    }
                }
                catch (Exception innerEx)
                {
                    Console.WriteLine(innerEx.GetBaseException().Message);
                }
            }
            else
            {
                await GetAuthToken();
                //Try one more time
                if (IsAuthenticated)
                {
                    //Retry to log the record
                    await CreateRecord(dbName, table, data);
                }
            }
        }
        #endregion
    }
}
