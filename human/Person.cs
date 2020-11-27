using System.Collections;

namespace LINQ.human
{
    public class Person : IEnumerable
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public PersonGender gender { get; set; }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
