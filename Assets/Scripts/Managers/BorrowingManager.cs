using System.Linq;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class BorrowingManager : MonoBehaviour //kitap ödünç
{
    private LibraryManager libraryManager;

    public BorrowingManager(LibraryManager libraryManager) // LM'nin verilerine eriş /Dependency
    {
        this.libraryManager = libraryManager;
    }
    public bool BorrowBook(int isbn) // kopya sayısı kontrolü ekle /kopya > ödünç alma/
    {
        if (isbn <= 0)
            throw new ArgumentNullException(nameof(isbn), "Geçerli bir ISBN giriniz.");
        var book = libraryManager.ListBooks().FirstOrDefault(b => b.ISBN == isbn);

        if (book == null)
            throw new InvalidOperationException($"ISBN'de kitap bulunamadi: {isbn}");
        if (book.CopyCount > book.BorrowedCount)
        {
            book.BorrowedCount++;
            return true;
        }
        return false;
    }

    public bool ReturnBook(int isbn) // ödünç kitabı verme kontrolü ekle /ödünç sayısı >0/
    {
        if (isbn <= 0)
            throw new ArgumentNullException(nameof(isbn), "Geçerli bir ISBN giriniz.");
        var book = libraryManager.ListBooks().FirstOrDefault(b => b.ISBN == isbn);

        if (book == null)
            throw new InvalidOperationException($"ISBN'de kitap bulunamadı: {isbn}");

        if (book.BorrowedCount > 0)
        {
            book.BorrowedCount--;
            return true;
        }
        return false;
    }
}