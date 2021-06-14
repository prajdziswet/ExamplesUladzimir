using System;

namespace ClassLibrary
{
    public class BorrowedBook
    {
        public Book book
        {
            get;
            private set;
        }

        public DateTime dateTime
        {
            get;
            private set;
        } = DateTime.Now;

        public DateTime dateTimeReturn
    {
        get => dateTime.AddDays(30);
    }
    public BorrowedBook(Book book)
        {
            this.book = book;
        }

    }
}
