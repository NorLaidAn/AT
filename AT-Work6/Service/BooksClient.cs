using AT_Work6.Model;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AT_Work6.Service
{
    internal class BooksClient
    {
        private readonly HttpClient client;

        public BooksClient(HttpClient client)
        {
            this.client = client;
        }

        // CREATE
        public async Task<HttpResponseMessage> CreateBook(Book book)
        {
            var json = JsonSerializer.Serialize(book);

            Log.Information($"Creating book: {json}");

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("/books", content);

            var body = await response.Content.ReadAsStringAsync();

            Log.Information($"Response: {response.StatusCode} | Body: {body}");

            return response;
        }

        // GET
        public async Task<HttpResponseMessage> GetAllBooks()
        {
            Log.Information($"Getting all books");

            var response = await client.GetAsync("/books");

            var body = await response.Content.ReadAsStringAsync();

            Log.Information($"Response: {response.StatusCode} | Body: {body}");

            return response;
        }

        public async Task<HttpResponseMessage> GetBookByGuidId(Guid? id)
        {
            Log.Information($"Getting book with {id}");
            var response = await client.GetAsync($"/books/{id}");

            var body = await response.Content.ReadAsStringAsync();

            Log.Information($"Response: {response.StatusCode} | Body: {body}");

            return response;
        }

        public async Task<HttpResponseMessage> GetBookByStringId(string id)
        {
            Log.Information($"Getting book with {id}");
            var response = await client.GetAsync($"/books/{id}");

            var body = await response.Content.ReadAsStringAsync();

            Log.Information($"Response: {response.StatusCode} | Body: {body}");

            return response;
        }

        // UPDATE
        public async Task<HttpResponseMessage> UpdateGuidBook(Guid? id, Book book)
        {
            Log.Information($"Updating book with {id}");

            var json = JsonSerializer.Serialize(book);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            return await client.PutAsync($"/books/{id}", content);
        }

        public async Task<HttpResponseMessage> UpdateStringBook(String? id, Book book)
        {
            var json = JsonSerializer.Serialize(book);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            return await client.PutAsync($"/books/{id}", content);
        }

        // DELETE
        public async Task<HttpResponseMessage> DeleteGuidBook(Guid? id)
        {
            Log.Information("Deleting book: {Id}", id);

            var response = await client.DeleteAsync($"/books/{id}");

            var body = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(body))
            {
                Log.Information($"Response body: {body}");
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteStringBook(String? id)
        {
            Log.Information("Deleting book: {Id}", id);

            var response = await client.DeleteAsync($"/books/{id}");

            var body = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(body))
            {
                Log.Information($"Response body: {body}");
            }

            return response;
        }
    }
}
