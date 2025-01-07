using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gravity.Helpers 
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }  // Numer aktualnej strony
        public int TotalPages { get; private set; } // Łączna liczba stron

        // Konstruktor inicjalizujący listę i obliczający liczbę stron
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // Zaokrąglenie w górę

            AddRange(items); // Dodanie elementów do listy
        }

        // Czy jest poprzednia strona?
        public bool HasPreviousPage => PageIndex > 1;

        // Czy jest następna strona?
        public bool HasNextPage => PageIndex < TotalPages;

        // Metoda tworząca stronicowaną listę z zapytania
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync(); // Liczba wszystkich rekordów
            var items = await source.Skip((pageIndex - 1) * pageSize) // Pominięcie elementów
                .Take(pageSize) // Pobranie odpowiedniej liczby elementów
                .ToListAsync(); // Konwersja do listy
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}