using NUnit.Framework;
using System;
using Shouldly;

namespace ClassLibrary.Test
{
    [TestFixture]
    public class TestEntities
    {
        //-----------------------------Test entity Author---------------------------------------
        [Test]
        public void AddArgumentEmptyAuthor()
        {
            Should.Throw<ArgumentNullException>(() => new Author("", "Tolstoj"));
            Should.Throw<ArgumentNullException>(() => new Author("Lev", ""));
        }

        [Test]
        public void AddArgunemtNullAuthor()
        {
            Should.Throw<ArgumentNullException>(() => new Author("Lev", null));
            Should.Throw<ArgumentNullException>(() => new Author(null, "Tolstoj"));
        }

        [Test]
        public void NewAuthor()
        {
            Author author = new Author("Lev", "Tolstoj");
            Assert.AreEqual(true, author.Name == "Lev" && author.LastName == "Tolstoj");
        }

        [Test]
        public void CheckDifferentID()
        {
            Author author = new Author("Lev", "Tolstoj");
            Author author1 = new Author("Alexsandr", "Tolstoj");
            Author author2 = new Author("Alexsandr", "Puskin");

            Assert.IsTrue(author.ID != author1.ID);
            Assert.IsTrue(author.ID != author2.ID);
            Assert.IsTrue(author1.ID != author2.ID);
        }
        //-----------------------------Test entity Book---------------------------------------
        [Test]
        public void AddArgumentEmptyBook()
        {
            Author author = new Author("Lev", "Tolstoj");
            
            Should.Throw<ArgumentNullException>(() => new Book("226611156", "", author));
            Should.Throw<ArgumentNullException>(() => new Book("", "War and Peace", author));
        }

        [Test]
        public void AddArgunemtNullBook()
        {
            Author author = new Author("Lev", "Tolstoj");

            Should.Throw<ArgumentNullException>(() => new Book("226611156", null, author));
            Should.Throw<ArgumentNullException>(() => new Book(null, "War and Peace", author));
            Should.Throw<ArgumentNullException>(() => new Book("226611156", "War and Peace", null));
        }

        [Test]
        public void NewBook()
        {
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);

            Assert.AreEqual("226611156", book.ISBN);
            Assert.AreEqual("War and Peace", book.NameBook);
            Assert.AreEqual(author, book.AuthorBook);
        }

        [Test]
        public void DifferentIDBooks()
        {
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            Book book1 = new Book("226611156", "War and Peace", author);
            book.ID.ShouldNotBe(book1.ID);
        }

        //-----------------------------Test entity Reader---------------------------------------
        [Test]
        public void AddArgumentEmptyReader()
        {
            Should.Throw<ArgumentNullException>(() => new Reader("", "Tolstoj"));
            Should.Throw<ArgumentNullException>(() => new Reader("Lev", ""));
        }

        [Test]
        public void AddArgunemtNullReader()
        {
            Should.Throw<ArgumentNullException>(() => new Reader("Lev", null));
            Should.Throw<ArgumentNullException>(() => new Reader(null, "Tolstoj"));
        }

        [Test]
        public void AddReader()
        {
            Reader reader = new Reader("Lev", "Tolstoj");

            reader.Name.ShouldBe("Lev");
            reader.LastName.ShouldBe("Tolstoj");
        }

        [Test]
        public void DifferentIDReaders()
        {
            Reader reader = new Reader("Lev", "Tolstoj");
            Reader reader1 = new Reader("Alexsandr", "Tolstoj");

            reader.ID.ShouldNotBe(reader1.ID);
        }

        [Test]
        public void AddBookInCard()
        {
            Reader reader = new Reader("Lev", "Tolstoj");
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            DepartmentReaders DR = new DepartmentReaders();
            DR.AddReader(reader);
            DR.BorrowBook(reader, book);

            reader.BorrowedBooks.Count.ShouldBe(1);
            reader.BorrowedBooks[0].book.ShouldBe(book);
        }

        [Test]
        public void AddEqualBooksInCard()
        {
            Reader reader = new Reader("Lev", "Tolstoj");
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            DepartmentReaders DR = new DepartmentReaders();
            DR.AddReader(reader);
            DR.BorrowBook(reader, book);
            //reader.AddBookInCard(book);

            Should.Throw<ArgumentException>(() => DR.BorrowBook(reader, book));
        }

        [Test]
        public void DeleteBookInCard()
        {
            Reader reader = new Reader("Lev", "Tolstoj");
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            DepartmentReaders DR = new DepartmentReaders();
            DR.AddReader(reader);
            DR.BorrowBook(reader, book);
            DR.ReturnBook(reader.ID, book.ID);

            reader.BorrowedBooks.Count.ShouldBe(0);
        }

        [Test]
        public void DeleteNotExistBookInCard()
        {
            Reader reader = new Reader("Lev", "Tolstoj");
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            DepartmentReaders DR = new DepartmentReaders();
            DR.AddReader(reader);
            DR.BorrowBook(reader, book);

            Should.Throw<ArgumentException>(() => DR.ReturnBook(reader.ID, book.ID+1)).Message.ShouldBe("you didn't take this book");
        }
    }
}