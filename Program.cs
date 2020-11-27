using System;
using System.Collections.Generic;
using LINQ.human;
using LINQ.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            LinqOperation linq = new LinqOperation();
            List<Person> persons = new List<Person>
            {
                new Person() { name = "Nikita", surname = "Hozya", age = 19, gender = PersonGender.male },
                new Person() { name = "Kira", surname = "Hozya", age = 39, gender = PersonGender.female },
                new Person() { name = "Alexander", surname = "Bogdanov", age = 30, gender = PersonGender.male },
                new Person() { name = "Alena", surname = "Titova", age = 50, gender = PersonGender.female},
                new Person() { name = "Matusevich", surname = "Yan", age = 50, gender = PersonGender.male },
                new Person() { name = "Paul", surname = "Zhiganov", age = 58, gender = PersonGender.male },
                new Person() { name = "Pauline", surname = "Suzina", age = 58, gender = PersonGender.female },
                new Person() { name = "Ivan", surname = "Zhiganov", age = 17, gender = PersonGender.male },
                new Person() { name = "Emil", surname = "Zabolotny", age = 36, gender = PersonGender.male }
            };

            List<Person> sortedPerson;

            Console.WriteLine("Input data:\n");
            PrintList(persons);
            Console.ReadKey();

            Console.WriteLine("\n1) Sorted in descending/ascending age order:\n");
            try
            {
                sortedPerson = linq.SortAge(persons, Aggregation.max);
                PrintList(sortedPerson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
            Console.WriteLine("\n2) Sorting by name and gender:\n");
            sortedPerson = linq.SortingNameAndGende(persons, PersonGender.female);
            PrintList(sortedPerson);

            Console.ReadKey();
            try
            {
                Console.WriteLine("\n3) Average age of people with a name shorter than 5 characters: " + linq.AverageAgeWithNameFiveCharacters(persons));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
            Console.WriteLine("\n4) Group namesakes:\n");
            linq.Homonyms(persons);

            Console.ReadKey();
            Console.WriteLine("\n5) Number of minors: " + linq.NumberMinors(persons));

            Console.ReadKey();
            Console.WriteLine("\n6) All letters that do not occur in the record of people's names: \n");
            Dictionary<char, bool> letters = linq.LettersDoNotOccurRecordingNames(persons);
            bool first = true;
            foreach (KeyValuePair<char, bool> letter in letters)
            {
                if (letter.Value)
                {
                    if (first)
                    {
                        Console.Write($"{letter.Key}");
                        first = false;
                    }
                    else
                    {
                        Console.Write($", {letter.Key}");
                    }
                }
            }

            Console.ReadKey();
            Console.WriteLine("\n\n7) Collection contents in Json format:" + linq.FromListToJson(persons));

            Console.ReadKey();
            Console.WriteLine("\n8) Age found in people of both gender:\n");
            List<int> ages = linq.AgeFoundPersonBothGender(persons);
            first = true;
            foreach (int age in ages)
            {
                if (first)
                {
                    Console.Write($"{age}");
                    first = false;
                }
                else
                {
                    Console.Write($", {age}");
                }
            }

            Console.ReadKey();
            Console.WriteLine("\n9) Person with min/max age:\n");
            sortedPerson = linq.AgeMinOrMax(persons, Aggregation.max);
            PrintList(sortedPerson);

            Console.ReadKey();
            Console.WriteLine("\n10) Couples:\n");
            linq.Couples(persons);

            Console.ReadKey();
        }

        static void PrintList(List<Person> persons)
        {
            foreach (Person person in persons)
            {
                Console.WriteLine($"Name: {person.name} surname: {person.surname} age: {person.age} gender: {person.gender}");
            }
        }
    }
}


