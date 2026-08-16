using System.Text;

string name = "Omar";
int age = 20;

var fileData = File.ReadAllText("./File.txt");

StringBuilder s = new StringBuilder(fileData);

/* string.Replace("{{Name}}", name);

string.Replace("{{age}}", $"{age}"); */

s.Replace("{{Name}}", name);
s.Replace("{{age}}", $"{age}");

System.Console.WriteLine("The Result : " + s);