using Microsoft.Graph;
using PasswordCreator.Console;
using System;

var enteranceControl = new EnteranceControl();
var characterManager = new CharacterManager();

Console.WriteLine("\n****** Welcome to the Aysegul's Password Generator! ******\n");
Console.WriteLine("Do you want to include Numbers in your password? Enter y for yes and anything else for no.\n");
var includeNumberStatus = Console.ReadLine();
Console.WriteLine("Do you want to include Lowercase Characters in your password? Enter y for yes and anything else for no.\n");
var includeLowerCharStatus = Console.ReadLine();
Console.WriteLine("Do you want to include Uppercase Characters in your password? Enter y for yes and anything else for no.\n");
var includeUpperCharStatus = Console.ReadLine();
Console.WriteLine("Do you want to include Special Characters in your password? Enter y for yes and anything else for no.\n");
var includeSpecialCharStatus = Console.ReadLine();
Console.WriteLine("How many characters do you want your password to have?\n");
//int PasswordLength = Convert.ToInt32(Console.ReadLine());
var passwordLength = Console.ReadLine();
bool isDigit = enteranceControl.IsDigit(passwordLength);

while (isDigit == false)
{
    Console.WriteLine("You entered non-numeric value. Enter a numeric value!!");
    passwordLength = Console.ReadLine();
    isDigit = enteranceControl.IsDigit(passwordLength);
}
int IntPasswordLength = Convert.ToInt32(passwordLength);

String Numbers = "0123456789";
String LowerChars = "abcdefghijklmnopqrstuvwxyz";
String UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
String SpecialChar = "!\"#$%&'()*+,‑./:<>=?@";


var CharacterList = characterManager.AddList(includeNumberStatus, Numbers);
CharacterList = characterManager.AddList(includeLowerCharStatus, LowerChars);
CharacterList = characterManager.AddList(includeUpperCharStatus, UpperChars);
CharacterList = characterManager.AddList(includeSpecialCharStatus, SpecialChar);

if(includeNumberStatus == "y" || includeLowerCharStatus == "y" || includeUpperCharStatus == "y" || includeSpecialCharStatus =="y")
    Console.WriteLine(characterManager.GetRandomPassword(IntPasswordLength));
else
    Console.WriteLine("There is not any characters available");

