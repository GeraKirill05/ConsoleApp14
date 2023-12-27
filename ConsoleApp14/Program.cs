using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp14;

// Интерфейс для представления товаров на складе
interface Product
{
    string Наименование { get; set; }
    int Количество { get; set; }
    decimal Цена { get; set; }

    decimal РассчитатьОбщуюСтоимость();
}

// Класс, представляющий лекарственные препараты
public class Preparat : Product
{
    public int Id { get; set; }
    public string Наименование { get; set; }
    public int Количество { get; set; }
    public decimal Цена { get; set; }
    public DateTime СрокГодности { get; set; }

    public decimal РассчитатьОбщуюСтоимость()
    {
        return Количество * Цена;
    }
}

// Класс, представляющий медицинское оборудование
public class MedInstrument : Product
{
    public int Id { get; set; }
    public string Наименование { get; set; }
    public int Количество { get; set; }
    public decimal Цена { get; set; }
    public decimal СрокГарантии { get; set; }

    public decimal РассчитатьОбщуюСтоимость()
    {
        return Количество * Цена;
    }
}
public class ListProviders 
{
    public int Id { get; set; } 
    public string Наименование { get; set; }
    public string Адресс { get; set; }
    public string Номер { get; set; }

    public string ВывестиПоставщиков()
    {
        return Наименование ;
    }
}

public class Program
    
{
    static List<Product> товары = new List<Product>();
    static List<ListProviders> поставщики;
    static void Main(string[] args)

    {
        Console.ForegroundColor = ConsoleColor.Blue;
        ProductDbContext dbContext = new ProductDbContext();
        dbContext.Lekarstvos.Add(new Preparat { Наименование = "Апирин", Количество = 100, Цена = 2.5m, СрокГодности = new DateTime(2022, 12, 31) });
        dbContext.SaveChanges();
        ProductDbContext dbContext1 = new ProductDbContext();
        dbContext1.MedInstruments.Add(new MedInstrument { Наименование = "Электрокардиограф", Количество = 5, Цена = 1500, СрокГарантии = 2 });
        dbContext1.SaveChanges();
        ProductDbContext dbContext2 = new ProductDbContext();
        dbContext.Providers.Add(new ListProviders { Наименование = "Василиййй", Адресс = " смирнова 1", Номер = "+3752920202020", });
        dbContext.SaveChanges();
        List<Product> товары = new List<Product>();
        поставщики = new List<ListProviders>();

        // Добавление примеров товаров на склад

        товары.Add(new Preparat { Наименование = "Аспирин", Количество = 100, Цена = 2.5m, СрокГодности = new DateTime(2022, 12, 31) });
        товары.Add(new MedInstrument { Наименование = "Электрокардиограф", Количество = 5, Цена = 1500, СрокГарантии = 2 });
        поставщики.Add(new ListProviders { Наименование = "Василий", Адресс = " смирнова 1", Номер = "+3752920202020" });

        bool exitRequested = false;
        while (!exitRequested)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Поиск товаров");
            Console.WriteLine("2. Вывод общей стоимости всех товаров");
            Console.WriteLine("3. Вывод списка поставщиков");
            Console.WriteLine("4. Добавить новое лекарство");
            Console.WriteLine("5. Добавить нового поставщика");
            Console.WriteLine("6. Вывод списка лекарств");
            Console.WriteLine("0. Выход");

            Console.Write("Выберите пункт меню: ");
            string выбранныйПункт = Console.ReadLine();

            switch (выбранныйПункт)
            {

                case "1":
                    Console.Write("Введите ключевое слово: ");
                    string keyword = Console.ReadLine();

                    bool found = false;
                    foreach (Product товар in товары)
                    {
                        if (товар.Наименование.Contains(keyword))
                        {
                            Console.WriteLine($"{товар.Наименование} - {товар.Количество} шт. - {товар.Цена} руб.");
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Товары не найдены.");
                    }
                    break;

                case "2":
                    decimal totalCost = 0;
                    foreach (Product товар in товары)
                    {
                        totalCost += товар.Количество * товар.Цена;
                    }

                    Console.WriteLine($"Общая стоимость всех товаров: {totalCost} руб.");
                    break;

                case "3":
                    DisplayListProviders();
                    break;
                case "4":
                    AddPreparat();
                    break;
                case "5":
                    AddProvider();
                    break;
                case "6":
                    DisplayListPreporate();
                    break;
                case "0":
                    exitRequested = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void DisplayListProviders()
    {
        Console.WriteLine("Список поставщиков:");
        
        foreach (var post in поставщики)
        {
            Console.WriteLine($"Наименование: {post.Наименование}");
            Console.WriteLine($"Адрес: {post.Адресс}");
            Console.WriteLine($"Телефон: {post.Номер}");
        }
    }

    static void AddPreparat()
    {
        Preparat новоеЛекарство = new Preparat();

        Console.Write("Введите наименование лекарства: ");
        новоеЛекарство.Наименование = Console.ReadLine();

        Console.Write("Введите количество лекарства: ");
        новоеЛекарство.Количество = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите цену лекарства: ");
        новоеЛекарство.Цена = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Введите срок годности лекарства: ");
        новоеЛекарство.СрокГодности = Convert.ToDateTime(Console.ReadLine());

        товары.Add(новоеЛекарство);

        Console.WriteLine("Лекарство успешно добавлено!");
    }
    static void DisplayListPreporate()
    {
        Console.WriteLine("Список лекарств:");

        foreach (var post in товары)
        {
            Console.WriteLine($"Наименование: {post.Наименование}");
            Console.WriteLine($"Адрес: {post.Количество}");
         //   Console.WriteLine($"Телефон: {post.}");
        }
    }

    static void AddProvider()
    {
        ListProviders новыйПоставщик = new ListProviders();

        Console.Write("Введите наименование поставщика: ");
        новыйПоставщик.Наименование = Console.ReadLine();

        Console.Write("Введите адрес поставщика: ");
        новыйПоставщик.Адресс = Console.ReadLine();

        Console.Write("Введите телефон поставщика: ");
        новыйПоставщик.Номер = Console.ReadLine();

        поставщики.Add(новыйПоставщик);

        Console.WriteLine("Поставщик успешно добавлен!");
    }
}
    
