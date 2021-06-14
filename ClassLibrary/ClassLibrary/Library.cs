using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    public class Library
    {
        private RepositoryBooks repositoryBooks = new RepositoryBooks();

        public void AddBookInLibrary(Book book) => repositoryBooks.AddBook(book);

        public List<Book> FindBooks(String NameBook = "", String NameAutor = "", String LastNameAutor = "") =>
            repositoryBooks.FindBooks(NameBook, NameAutor, LastNameAutor);

        public Book GetBook(int ID) => repositoryBooks.GetBook(ID);

        private DepartmentReaders departmentReaders = new DepartmentReaders();

        public void AddReader(Reader reader) => departmentReaders.AddReader(reader);

        public Reader GetReader(int IDReader) => departmentReaders.GetReader(IDReader);


        public void ReaderBoroweBook(int IDReader, String NameBook)
        {
            if (departmentReaders.GetReader(IDReader)==null)
            {
                throw new ArgumentException($"Not Exist Reader with (ID={IDReader}) in DepartmentReaders");
            }

            if (NameBook.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(NameBook));
            }

            var listAllBookWithName = repositoryBooks.FindBooks(NameBook);
          
            if (listAllBookWithName.Count == 0)
            {
                    throw new ArgumentException($"This book ({NameBook}) not exist in RepositoryBooks");
            }

           String ISBN = listAllBookWithName[0].ISBN;

           List<BorrowedBook> borrowedBookWithISBN = departmentReaders.BorrowedBooksWithISBN(ISBN);

            //Free Book
            List<Book> freebooks = listAllBookWithName.FreeBook(borrowedBookWithISBN);

            if (freebooks.Count==0)
            {
                DateTime? days=departmentReaders.GetDayWhenFreeBook(borrowedBookWithISBN);
                if (days==null)
                {
                    throw new ArgumentException($"ALL books borrowed, and readers are not return book");
                }
                throw new ArgumentException($"ALL books borrowed. {days.Value.ToString("dd-MM")} days when the nearest book is free ");
            }

            Book freeBook = freebooks.First();

            departmentReaders.BorrowBook(departmentReaders.GetReader(IDReader),
                                                freeBook);
        }

        public void ReaderReturnBook(int IDReader, int ID_Book) =>
            departmentReaders.ReturnBook(IDReader, ID_Book);
    }
}
