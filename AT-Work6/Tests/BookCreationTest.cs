using Allure.Net.Commons;
using Allure.NUnit;
using AT_Work6.Model;
using AT_Work6.Reporting;
using AT_Work6.Service;
using AT_Work6.TestData;
using AT_Work6.TestHelpers;
using NUnit.Framework;
using Serilog;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AT_Work6.Tests
{
    [TestFixture]
    [AllureNUnit]
    internal class BookCreationTest
    {
        BooksClient client;
        Book? book;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MyEnvironment.CreateReportEnviroment();
        }

        [SetUp]
        public async Task SetupAsync()
        {
            MyLogger.Configure();
            var token = await ConfigService.GetToken();
            var httpClient = HttpClientFactory.Create(ConfigService.BaseUrl,token);
            client = new BooksClient(httpClient);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.CloseAndFlush();
        }

        #region Create book

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksCreation))]
        [Category("Creation")]
        public async Task CreateBook_ShouldReturn201Async(Book book)
        {
            var response = await CreateTestHelper.CreateBookResponse(client, book);

            var desBody = await GetTestHelper.Read<Book>(response);

            CreateTestHelper.CleanerAsync(client, desBody);
            Log.Information("Sucsessfull cleaning: StatusCode == 204");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksCreation))]
        [Category("Creation")]
        public async Task CreateBook_ShouldMatchResponseAsync(Book book)
        {
            var response = await CreateTestHelper.CreateBookResponse(client, book);

            var desBody = await GetTestHelper.Read<Book>(response);

            CreateTestHelper.MatchResponseValidation(book, desBody);
            Log.Information("Assertion passed: ResponseBody == Book");

            CreateTestHelper.CleanerAsync(client, desBody);
            Log.Information("Sucsessfull cleaning: StatusCode == 204");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksCreation))]
        [Category("Creation")]
        public async Task CreateBook_ShouldRejectDuplicateAsync(Book book)
        {
            var response = await CreateTestHelper.CreateBookResponse(client, book);
            var response2 = await client.CreateBook(book);

            response2.StatusCode.ShouldBe(HttpStatusCode.Conflict);
            Log.Information("Assertion passed: StatusCode == 409");

            var desBody = await GetTestHelper.Read<Book>(response);

            CreateTestHelper.CleanerAsync(client, desBody);
            Log.Information("Sucsessfull cleaning: StatusCode == 204");
        }

        #endregion

        #region Get all books

        [Test]
        [Category("Getting")]
        public async Task GetAllBooks_ShouldReturnListAsync()
        {
            var response = await client.GetAllBooks();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            Log.Information("Assertion passed: StatusCode == 200");

            var books = await GetTestHelper.Read<List<Book>>(response);

            books.ShouldNotBeNull();
            Log.Information("Assertion passed: Books aren't empty");
        }

        [Test]
        [Category("Getting")]
        public async Task GetAllBooks_ShouldContainFieldsAsync()
        {
            var response = await client.GetAllBooks();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            Log.Information("Assertion passed: StatusCode == 200");

            var books = await GetTestHelper.Read<List<Book>>(response);

            GetTestHelper.BookListFieldsValidation(books);
            Log.Information("Assertion passed: fields aren't empty");
        }

        #endregion

        #region Get book by id

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksCreation))]
        [Category("Getting by id")]
        public async Task GetBookById_ShouldReturnBookAsync(Book book)
        {
            var createResponse = await CreateTestHelper.CreateBookResponse(client, book);

            var desBody = await GetTestHelper.Read<Book>(createResponse);

            var response = await client.GetBookByGuidId(desBody.id);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            Log.Information("Assertion passed: StatusCode == 200");

            CreateTestHelper.CleanerAsync(client, desBody);
            Log.Information("Sucsessfull cleaning: StatusCode == 204");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksGettingCorrect))]
        [Category("Getting by id")]
        public async Task GetBookById_ShouldReturn404Async(Guid id)
        {
            var response = await client.GetBookByGuidId(id);
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            Log.Information("Assertion passed: StatusCode == 404");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksGettingIncorrect))]
        [Category("Getting by id")]
        public async Task GetBookById_ShouldReturn400Async(string id)
        {
            var response = await client.GetBookByStringId(id);
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            Log.Information("Assertion passed: StatusCode == 400");
        }

        #endregion

        #region Update book

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksUpdate))]
        [Category("Update")]
        public async Task UpdateBook_ShouldUpdateSuccessfullyAsync(Book[] books)
        {
            var createResponse = await CreateTestHelper.CreateBookResponse(client, books[0]);

            var desBody = await GetTestHelper.Read<Book>(createResponse);

            var response = await client.UpdateGuidBook(desBody.id, books[1]);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
            Log.Information("Assertion passed: StatusCode == 204");

            CreateTestHelper.CleanerAsync(client, desBody);
            Log.Information("Sucsessfull cleaning: StatusCode == 204");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksUpdate))]
        [Category("Update")]
        public async Task UpdateBook_ShouldMatchUpdatedResponseAsync(Book[] books)
        {
            var createResponse = await CreateTestHelper.CreateBookResponse(client, books[0]);

            var desBody = await GetTestHelper.Read<Book>(createResponse);

            var response = await client.UpdateGuidBook(desBody.id, books[1]);

            var searchResponse = await client.GetBookByGuidId(desBody.id);
            var bookToCheck = await GetTestHelper.Read<Book>(searchResponse);

            GetTestHelper.BookFieldsValidation(bookToCheck, books[1]);
            Log.Information("Assertion passed: books are equal");

            CreateTestHelper.CleanerAsync(client, desBody);
            Log.Information("Sucsessfull cleaning: StatusCode == 204");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksUpdateCorrect))]
        [Category("Update")]
        public async Task UpdateBook_ShouldReturn404Async(Book book, Guid id)
        {
            var response = await client.UpdateGuidBook(id, book);
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            Log.Information("Assertion passed: StatusCode == 404");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksUpdateIncorrect))]
        [Category("Update")]
        public async Task UpdateBook_ShouldReturn400Async(Book book, string id)
        {
            var response = await client.UpdateStringBook(id, book);
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            Log.Information("Assertion passed: StatusCode == 400");
        }

        #endregion

        #region Delete book

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksCreation))]
        [Category("Deletion")]
        public async Task DeleteBook_ShouldRemoveBook(Book book)
        {
            var createResponse = await CreateTestHelper.CreateBookResponse(client, book);

            var desBody = await GetTestHelper.Read<Book>(createResponse);

            var response = await client.DeleteGuidBook(desBody.id);

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
            Log.Information("Assertion passed: StatusCode == 204");

            var getResponse = await client.GetBookByGuidId(desBody.id);
            getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            Log.Information("Assertion passed: StatusCode == 404");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksDelitionCorrect))]
        [Category("Deletion")]
        public async Task DeleteBook_ShouldReturn404Async(Guid id)
        {
            var response = await client.DeleteGuidBook(id);

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            Log.Information("Assertion passed: StatusCode == 404");
        }

        [TestCaseSource(typeof(BookData), nameof(BookData.BooksDelitionIncorrect))]
        [Category("Deletion")]
        public async Task DeleteBook_ShouldReturn400Async(String id)
        {
            var response = await client.DeleteStringBook(id);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            Log.Information("Assertion passed: StatusCode == 400");
        }

        #endregion
    }
}
