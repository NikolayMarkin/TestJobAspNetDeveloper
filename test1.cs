using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Вы собираетесь совершить долгое путешествие через множество населенных пунктов. 
 * Чтобы не запутаться, вы сделали карточки вашего путешествия. Каждая карточка содержит в себе пункт отправления и 
 * пункт назначения. 
 * Гарантируется, что если упорядочить эти карточки так, чтобы для каждой карточки в упорядоченном списке 
 * пункт назначения на ней совпадал с пунктом отправления в следующей карточке в списке, получится один список карточек 
 * без циклов и пропусков. 
 * Например, у нас есть карточки 
 * Мельбурн > Кельн 
 * Москва > Париж 
 * Кельн > Москва 
 * Если упорядочить их в соответствии с требованиями выше, то получится следующий список: 
 * Мельбурн > Кельн, Кельн > Москва, Москва > Париж 
 * Требуется: 
 * Написать функцию, которая принимает набор неупорядоченных карточек и возвращает набор упорядоченных карточек в 
 * соответствии с требованиями выше, то есть в возвращаемом из функции списке карточек для каждой карточки пункт 
 * назначения на ней должен совпадать с пунктом отправления на следующей карточке. 
 * Дать оценку сложности получившегося алгоритма сортировки 
 * Написать тесты 
 * Оценивается правильность работы, производительность и читабельность кода  
 */
/*
 * Реализованный алгоритм имеет сложность O(n^2).
 */

namespace TestJob1
{
    class Card
    {
        public Card(string depart, string dest)
        {
            Departure = depart;
            Destination = dest;    
        }

        public string Departure { get; set; }
        public string Destination { get; set; }

        public override string ToString()
        {
            return Departure + "->" + Destination;
        }
    }

    class Program
    {
        static List<Card> sort(List<Card> list)
        {
            List<Card> sorted_list = new List<Card>();

            // выбрать первый эл-т в исходном списке
            Card curCard = list[0];
            sorted_list.Add(curCard);
            list.Remove(curCard);
            // найти все элементы стоящие справа 
            while(list.Count() != 0)
            {
                Card nextItem = getNextCard(list, curCard, (cur, item) => { return cur.Destination == item.Departure; });
                if (nextItem != null)
                {
                    sorted_list.Add(nextItem);
                    curCard = nextItem;
                }
                else
                {
                    break;
                }
            }
            // найти все элементы стоящие слева
            curCard = sorted_list[0];
            while (list.Count() != 0)
            {
                Card nextItem = getNextCard(list, curCard, (cur, item) => { return cur.Departure == item.Destination; });
                if (nextItem != null)
                {
                    sorted_list.Insert(0, nextItem);
                    curCard = nextItem;
                }
                else
                {
                    break;
                }
            }

            return sorted_list;
        }
        /*
         * Осуществляет поиск следующего элемента в списке по заданному условию. 
         * При нахождении удаляет его из исходного списка
         */
        static Card getNextCard(List<Card> list, Card curCard, Func<Card, Card, bool> is_next)
        {
            foreach(Card item in list)
            {
                if (is_next(curCard, item))
                {
                    list.Remove(item);
                    return item;
                }
            }
            return null;
        }

        static void Main(string[] args)
        {
            // тесты
            // необходимо протестировать:
            // 1. случай нулевой длины списка
            // 2. частично упорядоченный список
            // 3. полностью не упорядоченный список

            List<Card> list1 = new List<Card>();
            list1.Add(new Card("Мельбурн", "Кельн"));
            list1.Add(new Card("Москва", "Париж"));
            list1.Add(new Card("Кельн", "Москва"));
            list1.Add(new Card("Стамбул", "Мельбурн"));

            List<Card> sorted_list = sort(list1);

            Console.WriteLine("Отсортированный список:");
            foreach (Card item in sorted_list)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
