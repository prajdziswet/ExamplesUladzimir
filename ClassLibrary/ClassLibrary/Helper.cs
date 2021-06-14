using System;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;

public static class Helper
{
    public static bool IsNullOrWhiteSpace(this string str)
    {
        return String.IsNullOrWhiteSpace(str);
    }

    public static bool IsExist(this string str)
    {
        return !String.IsNullOrWhiteSpace(str);
    }

    public static List<Book> FreeBook(this List<Book> allbook, List<BorrowedBook> borrowbook)
    {
        return allbook.Where(book => !borrowbook.Any(x=>x.book.ID==book.ID)).ToList();
    }

}