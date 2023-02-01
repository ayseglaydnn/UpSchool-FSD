using Microsoft.Graph;
using PasswordCreator.Console;
using System;

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
int PasswordLength = Convert.ToInt32(Console.ReadLine());

String Numbers = "0123456789";
String LowerChars = "abcdefghijklmnopqrstuvwxyz";
String UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
String SpecialChar = "!\"#$%&'()*+,‑./:<>=?@";

var characterManager = new CharacterManager();

var CharacterList = characterManager.AddList(includeNumberStatus, Numbers);
CharacterList = characterManager.AddList(includeLowerCharStatus, LowerChars);
CharacterList = characterManager.AddList(includeUpperCharStatus, UpperChars);
CharacterList = characterManager.AddList(includeSpecialCharStatus, SpecialChar);

Console.WriteLine(characterManager.GetRandomPassword(PasswordLength));


