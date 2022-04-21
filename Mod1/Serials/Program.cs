using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serials;

//Serialize();
//Deserialize();
SerializeVeel();

void SerializeVeel()
{
   var list = new Bogus.Faker<Person>()
    .RuleFor(p=>p.Id, fk=>fk.IndexGlobal)
    .RuleFor(p=>p.FirstName, fk=>fk.Person.FirstName)
    .RuleFor(p=>p.LastName, fk=>fk.Person.LastName)
    .RuleFor(p=>p.Age, fk=>fk.Random.Number(0, 123))
    .Generate(1000)
    .ToList();
    

    var fs = File.Create(@"D:\Netessentials_4_2022\Tmp\people.json");
    JsonSerializer sers = new JsonSerializer();
    sers.ContractResolver = new DefaultContractResolver {
        NamingStrategy = new CamelCaseNamingStrategy()
    };

    StreamWriter writer = new StreamWriter(fs);
    sers.Serialize(writer, list);

    writer.Flush();
    writer.Close();

    // foreach(var p in list){
    //     System.Console.WriteLine(p);
    // }
    
}

void Deserialize()
{
    FileStream fs = File.OpenRead(@"D:\Netessentials_4_2022\Tmp\person.xml");
    XmlReader reader = XmlReader.Create(fs);
   
    XmlSerializer sers = new XmlSerializer(typeof(Person));
    Person? p = sers.Deserialize(reader) as Person;

    System.Console.WriteLine(p);
}

void Serialize()
{
    Person p1 = new Person{ Id = 1, FirstName="Kees", LastName="Pieters", Age=42};
    FileStream fs = File.Create(@"D:\Netessentials_4_2022\Tmp\person.xml");
    XmlWriter writer = XmlWriter.Create(fs);
   
    XmlSerializer sers = new XmlSerializer(typeof(Person));
    sers.Serialize(writer, p1);

    writer.Flush();
    writer.Close();
    //System.Console.WriteLine(p1);
}