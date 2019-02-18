using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Demo
{
    class Program
    {
        private static Person _fromFilePerson = null;

        static void Main(string[] args)
        {
            var person = CreatePersonMessage();

            var byteMessage = person.ToByteArray();
            _fromFilePerson = Person.Parser.ParseFrom(byteMessage);

            SerializeToFile(person);

            DeserializeFromFile();
            
            
        }

        private static Person CreatePersonMessage()
        {
            return new Person()
            {
                Id = 1,
                Name = "AdriseYounis",
                Email = "adriseyounis@gmail.com",
                Phones =
                {
                    new Person.Types.PhoneNumber
                    {
                        Number = "555-4321", Type = Person.Types.PhoneType.Home
                    }
                }
            };
        }

        private static void DeserializeFromFile()
        {
            using (var input = File.OpenRead("Person"))
            {
                _fromFilePerson = Person.Parser.ParseFrom(input);
            }
        }

        private static void SerializeToFile(Person person)
        {
            using (var output = File.Create("Person"))
            {
                person.WriteTo(output);
            }
        }
    }
}
