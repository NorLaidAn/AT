using AT_Work6.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_Work6.TestData
{
    internal class BookData
    {
        public static IEnumerable<Book> BooksCreation()
        {
            yield return new Book
            {
                title = "Clean Code",
                author = "Robert C. Martin",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
            };

            yield return new Book
            {
                title = "DDD",
                author = "Eric Evans",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
            };
        }

        public static IEnumerable<Book[]> BooksUpdate()
        {
            yield return new Book[]
            {
                new Book
                {
                title = "Clean Code",
                author = "Robert C. Martin",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
                },

                new Book
                {
                title = "Not Clean Code",
                author = "Not Robert C. Martin",
                publishedDate = DateTime.UtcNow
                }
            };

            yield return new Book[]
            {
                new Book
                {
                title = "DDD",
                author = "Eric Evans",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
                },

                new Book
                {
                title = "Not DDD",
                author = "Not Eric Evans",
                publishedDate = DateTime.UtcNow
                }
            };
        }

        public static IEnumerable<object[]> BooksUpdateCorrect()
        {
            yield return new object[]
            {
                new Book
                {
                title = "Clean Code",
                author = "Robert C. Martin",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
                },
                new Guid()
            };
            yield return new object[]
            {
                new Book
                {
                title = "DDD",
                author = "Eric Evans",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
                },
                new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd")
            };
        }

        public static IEnumerable<object[]> BooksUpdateIncorrect()
        {
            yield return new object[]
            {
                new Book
                {
                title = "Clean Code",
                author = "Robert C. Martin",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
                },
                "111112321"
            };

            yield return new object[]
            {
                new Book
                {
                title = "DDD",
                author = "Eric Evans",
                isbn = Guid.NewGuid().ToString(),
                publishedDate = DateTime.UtcNow
                },
                ""
            };
        }

        public static IEnumerable<Guid> BooksGettingCorrect()
        {
            yield return new Guid("c1a8e7d4-9f35-42ab-b6d1-3e7f2a9c5b84");

            yield return new Guid("7f4d9c3a-2b71-4e8f-9c12-5d8a6b1e4f90");
        }

        public static IEnumerable<String> BooksGettingIncorrect()
        {
            yield return "1111";

            yield return "";
        }

        public static IEnumerable<Guid> BooksDelitionCorrect()
        {
            yield return new Guid("7f4d9c3a-2b71-4e8f-9c12-5d8a6b1e4f90");

            yield return new Guid("c1a8e7d4-9f35-42ab-b6d1-3e7f2a9c5b84");
        }

        public static IEnumerable<String> BooksDelitionIncorrect()
        {
            yield return "111111";

            yield return "";
        }
    }
}
