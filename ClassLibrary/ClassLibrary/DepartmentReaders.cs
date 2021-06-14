using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    public class DepartmentReaders
    {
        private List<Reader> Readers
        {
            get;
            set;
        }  = new List<Reader>();


        public void AddReader(Reader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Reader reference is null");
            }
            Reader foundReader =
                Readers.FirstOrDefault(temp=> temp.Name==reader.Name&&temp.LastName==reader.LastName);

            if (foundReader == null)
            {
                Readers.Add(reader);
            }
            else
            {
                throw new ArgumentException("This reader exists in List");
            }
        }

        public Reader GetReader(int IDReader)
        {
            return Readers.FirstOrDefault(x => x.ID == IDReader);
        }

        public bool CheckBorrowedBook(int IDBook)
        {
            BorrowedBook borrowedBook = Readers.SelectMany(x => x.BorrowedBooks)?.FirstOrDefault(x=>x?.book.ID==IDBook);
            return borrowedBook != null;
        }

        public int CountBorrowedBooksWithISBN(String ISBN)
        {
            return BorrowedBooksWithISBN(ISBN).Count;
        }

        public List<BorrowedBook> BorrowedBooksWithISBN(String ISBN)
        {
            return Readers.SelectMany(x => x.BorrowedBooks.Where(y => y.book.ISBN == ISBN)).ToList();
        }

        public DateTime? GetDayWhenFreeBook(List<BorrowedBook> borrowedBooksISBN)
        {
            if (borrowedBooksISBN==null||borrowedBooksISBN.Count==0)
            {
                throw new ArgumentNullException(nameof(borrowedBooksISBN), "In DepartmentReaders.GetTimeWhenFreeBook");
            }
            DateTime timeNow = DateTime.Now;

            return borrowedBooksISBN.Where(book => book.dateTimeReturn >= timeNow).Select(book => book.dateTimeReturn).Min();
        }

        public bool CheckReader(Reader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("Null Argument \"reader\" in AddBook");
            }
            return GetReader(reader.ID)!=null;
        }

        public void BorrowBook(Reader reader,Book book)
        {
            if (reader == null||book==null)
            {
                throw new ArgumentNullException($"Null Argument {((reader==null)?"reader":"book")} in AddBook");
            }

            if (!CheckReader(reader))
            {
                throw new ArgumentException("This reader not Exist in DataBase");
            }

            if (CheckBorrowedBook(book.ID))
            {
                throw new ArgumentException($"This book with ISBN={book.ISBN} borrowed");
            }

            reader.AddBookInCard(book);
        }

        public void ReturnBook(int IDReader, int ID_Book)
        {
            Reader reader = GetReader(IDReader);
            if (reader == null)
            {
                throw new ArgumentException($"Not Exist Reader with (ID={IDReader}) in DepartmentReaders");
            }
                reader.DeleteBookInCard(ID_Book);        
        }

    }
}