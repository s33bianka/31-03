using System.Reflection.Metadata;
using LinqConsoleLab.PL.Data;
using LinqConsoleLab.PL.Models;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        var query =
            from s in DaneUczelni.Studenci
            where s.Miasto.Equals("Warsaw")
            select $"{s.NumerIndeksu}, {s.Imie}, {s.Nazwisko}, {s.Miasto}";
        return query;
        
        /*
         * var method = DaneUczelni.Studenci.Where(s => s.Miasto.Equals("Warsaw").Select(s => $"{...}, {...}, ...");
         * return method;
         */
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        var method = DaneUczelni.Studenci.Select(s => $"{s.Email}");
        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        var query =
            from s in DaneUczelni.Studenci
            orderby s.Nazwisko, s.Imie
            select $"{s.NumerIndeksu}, {s.Imie}, {s.Nazwisko}";
        return query;
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        var method = DaneUczelni.Przedmioty.Where(p => p.Kategoria.Equals("Analytics"))
            .Select(p => $"{p.Nazwa}, {p.DataStartu}").FirstOrDefault();
        return new List<string>{method};
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        var method = DaneUczelni.Zapisy.Where(z => z.CzyAktywny.Equals(false)).Select(x => 1);
        bool exists = (method.Any() ? true : false);
        return new List<string>{$"{exists}"};
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        var method = DaneUczelni.Prowadzacy.Where(p => p.Katedra.Length == 0).Select(x => 1);
        bool exists = (method.Any() ? false : true);
        return new List<string>{$"{exists}"};
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        var method = DaneUczelni.Zapisy.Where(z => z.CzyAktywny.Equals(true)).Count();

        return new List<string> { $"{method}" };
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
        var method = DaneUczelni.Studenci.OrderBy(s => s.Miasto).Select(s => s.Miasto).Distinct();
        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        var method = DaneUczelni.Zapisy.OrderByDescending(z => z.DataZapisu)
            .Take(3).Select(z => $"{z.DataZapisu}, {z.StudentId}, {z.PrzedmiotId}");
        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        var method = DaneUczelni.Przedmioty.
            OrderBy(p => p.Nazwa).Skip(2).Take(2)
            .Select(p => $"{p.Nazwa}, {p.Kategoria}");

        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        var method = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId,
                (s, z) => $"{s.Imie}, {s.Nazwisko}, {z.DataZapisu}");
        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {
        var method = DaneUczelni.Zapisy
            .Join(DaneUczelni.Studenci, z => z.StudentId, s => s.Id,
                (z, s) => new { z, s })
            .Join(DaneUczelni.Przedmioty, z => z.z.PrzedmiotId, p => p.Id,
                (z, p) => $"{z.s.Imie}, {z.s.Nazwisko}, {p.Nazwa}");

        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        var method = DaneUczelni.Zapisy.Join(DaneUczelni.Przedmioty, z => z.PrzedmiotId,
            p => p.Id, (z, p) => p.Nazwa)
            .GroupBy(nazwa => nazwa)
            .Select(g => $"{g.Key}, {g.Count()}");

        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        var method = DaneUczelni.Zapisy
            .Where(z => z.OcenaKoncowa.HasValue)
            .Join(DaneUczelni.Przedmioty,
                z => z.PrzedmiotId, p => p.Id,
                (z, p) => new { p.Nazwa, Oc = z.OcenaKoncowa.Value })
            .GroupBy(g => g.Nazwa)
            .Select(g
                => $"{g.Key}, {g.Average(o => o.Oc)}");

        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        var method = DaneUczelni.Prowadzacy
            .GroupJoin(DaneUczelni.Przedmioty, prow => prow.Id,
                przedm => przedm.ProwadzacyId,
                (prow, przedm)
                    => $"{prow.Imie}, {prow.Nazwisko}, {przedm.Count()}");
        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        var method = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId,
                (s, z) => new { s.Imie, s.Nazwisko, z.OcenaKoncowa })
            .Where(g => g.OcenaKoncowa.HasValue)
            .GroupBy(g => new { g.Imie, g.Nazwisko })
            .Select(g
                => $"{g.Key.Imie}, {g.Key.Nazwisko}, {g.Max(m => m.OcenaKoncowa!.Value)}");
        
        return method;
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        var method = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId,
                (s, z) => new {s.Imie, s.Nazwisko, z.CzyAktywny})
            .Where(g => g.CzyAktywny)
            .GroupBy(g => new {g.Imie, g.Nazwisko})
            .Where(g => g.Count() > 1)
            .Select(g
                => $"{g.Key.Imie}, {g.Key.Nazwisko},  {g.Count()}");
        
        return method;
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        var method = DaneUczelni.Przedmioty
            .Where(p => p.DataStartu.Month == 4 && p.DataStartu.Year == 2026)
            .Join(DaneUczelni.Zapisy, p => p.Id, z => z.PrzedmiotId,
                (p, z) => new { p.Nazwa, z.OcenaKoncowa })
            .GroupBy(g => g.Nazwa)
            .Where(g
                => g.All(gr => !gr.OcenaKoncowa.HasValue))
            .Select(g => $"{g.Key}");
        
        return method;
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        var method = DaneUczelni.Prowadzacy
            .Join(DaneUczelni.Przedmioty, pr => pr.Id, p => p.ProwadzacyId,
                (pr, p) => new { pr.Imie, pr.Nazwisko, p.Id })
            .Join(DaneUczelni.Zapisy, p => p.Id, z => z.PrzedmiotId,
                (p, z) => new { p.Imie, p.Nazwisko, z.OcenaKoncowa })
            .Where(x => x.OcenaKoncowa.HasValue)
            .GroupBy(x => new { x.Imie, x.Nazwisko })
            .Select(g
                => $"{g.Key.Imie}, {g.Key.Nazwisko}, {g.Average(x => x.OcenaKoncowa!.Value):F2}");

        return method;
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        var method = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId,
                (s, z) => new { s.Miasto, z.CzyAktywny })
            .Where(x => x.CzyAktywny)
            .GroupBy(x => x.Miasto)
            .Select(g
                => new { Miasto = g.Key, Liczba = g.Count() })
            .OrderByDescending(x => x.Liczba)
            .Select(x
                => $"{x.Miasto}, {x.Liczba}");

        return method;
    }
    
    // zmiana pod commita bo nie dziala
}
