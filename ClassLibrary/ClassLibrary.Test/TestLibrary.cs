using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace ClassLibrary.Test
{
    [TestFixture]
    class TestLibrary
    {

        [Test]
        public void BorrowBooks()
        {
            Library lib = new Library();
            //Add Book
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            lib.AddBookInLibrary(book);
            //Add reader
            Reader reader = new Reader("Ivan", "Ivanov");
            lib.AddReader(reader);
            //Borrow Books
            lib.ReaderBoroweBook(reader.ID, book.NameBook);

            lib.GetReader(reader.ID).BorrowedBooks.Count.ShouldBe(1);
            lib.GetReader(reader.ID).BorrowedBooks[0].book.ShouldBe(book);
        }

        [Test]
        public void NotReader()
        {
            Library lib = new Library();
            //Add Book
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            lib.AddBookInLibrary(book);

            Reader reader = new Reader("Ivan", "Ivanov");
            Should.Throw<ArgumentException>(() => lib.ReaderBoroweBook(reader.ID, book.NameBook)).Message.ShouldBe($"Not Exist Reader with (ID={reader.ID}) in DepartmentReaders");
            //for return book
            Should.Throw<ArgumentException>(() => lib.ReaderReturnBook(reader.ID, book.ID)).Message.ShouldBe($"Not Exist Reader with (ID={reader.ID}) in DepartmentReaders");
        }

        [Test]
        public void NotSetNamebook()
        {
            Library lib = new Library();
            //Add Book
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);
            //Add reader
            Reader reader = new Reader("Ivan", "Ivanov");
            lib.AddReader(reader);

            Should.Throw<ArgumentNullException>(() => lib.ReaderBoroweBook(reader.ID, "")).ParamName.ShouldBe("NameBook");
        }

        [Test]
        public void NotThisBooK()
        {
            Library lib = new Library();
            //Add Book
            Author author = new Author("Lev", "Tolstoj");
            Book book = new Book("226611156", "War and Peace", author);

            //Add reader
            Reader reader = new Reader("Ivan", "Ivanov");
            lib.AddReader(reader);

            Should.Throw<ArgumentException>(() => lib.ReaderBoroweBook(reader.ID, book.NameBook)).Message.ShouldBe($"This book ({book.NameBook}) not exist in RepositoryBooks");
        }

        [Test]
        public void ALLBooKWithISBNBorrowed()
        {
            Library lib = new Library();
            //Add Book
            Author author = new Author("Lev", "Tolstoj");
            Book book1 = new Book("226611156", "War and Peace", author);
            lib.AddBookInLibrary(book1);
            Book book2 = new Book("226611156", "War and Peace", author);
            lib.AddBookInLibrary(book2);
            //Add reader
            Reader reader1 = new Reader("Ivan", "Ivanov");
            lib.AddReader(reader1);
            Reader reader2 = new Reader("Alex", "Ivanov");
            lib.AddReader(reader2);
            Reader reader3 = new Reader("olja", "Ivanova");
            lib.AddReader(reader3);
            //Borrow Books
            lib.ReaderBoroweBook(reader1.ID, book1.NameBook);
            lib.ReaderBoroweBook(reader2.ID, book2.NameBook);

            Should.Throw<ArgumentException>(() => lib.ReaderBoroweBook(reader3.ID, book1.NameBook)).Message.ShouldContain("days when the nearest book is free");
        }

        [Test]
        public void BookBorrowedTwince()
        {
            Library lib = new Library();
            //Add Book
            Author author = new Author("Lev", "Tolstoj");
            Book book1 = new Book("226611156", "War and Peace", author);
            lib.AddBookInLibrary(book1);
            Book book2 = new Book("226611156", "War and Peace", author);
            lib.AddBookInLibrary(book2);
            //Add reader
            Reader reader1 = new Reader("Ivan", "Ivanov");
            lib.AddReader(reader1);

            //Borrow Books
            lib.ReaderBoroweBook(reader1.ID, book1.NameBook);

            Should.Throw<ArgumentException>(() => lib.ReaderBoroweBook(reader1.ID, book2.NameBook)).Message.ShouldBe("you have already taken simular book");
        }
    }
}
