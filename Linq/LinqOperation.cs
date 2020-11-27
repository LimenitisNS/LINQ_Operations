using System;
using System.Collections.Generic;
using System.Linq;
using LINQ.human;

namespace LINQ.Linq
{
    public class LinqOperation
    {
        public List<Person> SortAge(List<Person> persons, Aggregation aggregation)
        {
            switch(aggregation)
            {
                case Aggregation.max:
                    var sortedPerson = from person in persons
                                       orderby person.age descending
                                       select person;
                    return FillList(sortedPerson);

                case Aggregation.min:
                    sortedPerson = from person in persons
                                       orderby person.age ascending
                                       select person;
                    return FillList(sortedPerson);

                default:
                    throw new Exception("There is no such sorting method");
            }
        }

        public List<Person> SortingNameAndGende(List<Person> persons, PersonGender gender)
        {
            switch(gender)
            {
                case PersonGender.male:
                    var sortedPerson = from person in persons
                                       where person.gender == PersonGender.male
                                       orderby person.name
                                       select person;
                    return FillList(sortedPerson);

                case PersonGender.female:
                    sortedPerson = from person in persons
                                       where person.gender == PersonGender.female
                                       orderby person.name
                                       select person;
                    return FillList(sortedPerson);

                default:
                    return new List<Person> { };
            }
        }

        public double AverageAgeWithNameFiveCharacters(List<Person> persons)
        {
            try
            {
                return persons.Where(person => person.name.Length < 5).Average(person => person.age);
            }
            catch(InvalidOperationException)
            {
                throw new InvalidOperationException("The list is empty");
            }
        }

        public void Homonyms(List<Person> persons)
        {
            var homonyms = from person in persons
                           group person by person.surname;

            foreach (IGrouping<string, Person> homonym in homonyms)
            {
                if (homonym.Count() == 1)
                {
                    continue;
                }
                else
                {

                    foreach (var person in homonym)
                        Console.WriteLine($"{homonym.Key} {person.name}");

                    Console.WriteLine();
                }
            }
        }

        public int NumberMinors(List<Person> persons)
        {
            return persons.Where(person => person.age < 18).Count();
        }

        public List<Person> AgeMinOrMax(List<Person> persons, Aggregation aggregation)
        {
            switch (aggregation)
            {
                case Aggregation.max:
                    var maxAge = persons.GroupBy(person => person.age).Max(person => person.Key);
                    List<Person> maxAgePerson = persons.Where(age => age.age == maxAge).ToList();
                    return maxAgePerson;

                case Aggregation.min:
                    var minAge = persons.GroupBy(person => person.age).Min(person => person.Key);
                    List<Person> minAgePerson = persons.Where(age => age.age == minAge).ToList();
                    return minAgePerson;

                default:
                    return new List<Person> { };
            }
            
        }

        public List<int> AgeFoundPersonBothGender(List<Person> persons)
        {
            List<int> ages = new List<int> { };

            var ageMalePersons = persons
                .Where(male => male.gender == PersonGender.male)
                .Select(male => male);

            foreach(Person ageMale in ageMalePersons)
            {
                var ageFemaleAge = persons
                    .Where(female => female.gender == PersonGender.female && female.age == ageMale.age)
                    .Select(female => female.age);

                if(ageFemaleAge.Count() > 0)
                {
                    ages.Add(ageMale.age);
                }
            }

            return ages;
        }

        public string FromListToJson(List<Person> persons)
        {
            var lintPerson = from person in persons
                             select person;

            string jsonString = "{\n\t\"Persons\":[\n";

            foreach (Person person in lintPerson)
            {
                jsonString +=
                    "\t\t{\n" +
                    $"\t\t\t\"name\": \"{person.name}\"\n" +
                    $"\t\t\t\"surname\": \"{person.surname}\"\n" +
                    $"\t\t\t\"age\": {person.age}\n" +
                    $"\t\t\t\"gender\": {person.gender}\n";

                if (person == lintPerson.Last())
                {
                    jsonString += "\t\t}\n";
                }
                else
                {
                    jsonString += "\t\t},\n";
                }
            }

            jsonString = jsonString + "\t]\n}";

            return jsonString;
        }

        public Dictionary<char, bool> LettersDoNotOccurRecordingNames(List<Person> persons)
        {
            Dictionary<char, bool> letters = new Dictionary<char, bool>
            {
                { 'A', true }, { 'B', true }, { 'C', true }, { 'D', true }, { 'E', true }, { 'F', true }, { 'G', true }, { 'H', true }, { 'I', true },
                { 'J', true }, { 'K', true }, { 'L', true }, { 'M', true }, { 'N', true }, { 'O', true }, { 'P', true }, { 'Q', true }, { 'R', true },
                { 'S', true }, { 'T', true }, { 'U', true }, { 'V', true }, { 'W', true }, { 'X', true }, { 'Y', true }, { 'Z', true }
            };

            var linqPerson = from person in persons
                             select person;

            foreach(Person person in persons)
            {
                foreach(char letter in person.name.ToUpper())
                {
                    if(letters[letter])
                    {
                        letters[letter] = false;
                    }
                }
            }

            return letters;
        }

        public void Couples(List<Person> persons)
        {
            var males = from person in persons
                       where person.gender == PersonGender.male
                       select person;

            var females = from person in persons
                        where person.gender == PersonGender.female
                        select person;

            foreach (Person male in males)
            {
                foreach(Person female in females)
                {
                    Console.Write($"{male.name} {male.surname} : {female.name} {female.surname} | ");
                }

                Console.WriteLine();
            }
        }

        private List<Person> FillList(IOrderedEnumerable<Person> select)
        {
            List<Person> newList = new List<Person> { };

            foreach(Person person in select)
            {
                newList.Add(person);
            }

            return newList;
        }
    }
}
