using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCreator.Console
{
    public class CharacterManager
    {
        public List<Character> CharacterLists { get; set; }
        private List<Character> SelectedCharacters { get; set; }

        private Random _random;
        public CharacterManager()
        {
            CharacterLists = new List<Character>();
            _random = new Random();
            SelectedCharacters = new List<Character>();
        }

        public List<Character> AddList(string includeStatus, string includeType)
        {

            if (includeStatus == "y" || includeStatus == "Y")
            {
                foreach (var x in includeType)
                {
                    Character character = new Character()
                    {
                        character = x
                    };
                    CharacterLists.Add(character);
                }
            }
            return CharacterLists;
        }

        public int CharactersCount => CharacterLists.Count();

        public char GetRandomCharacter()
        {
            var randomIndex = _random.Next(0, CharactersCount); // get random value between 0 and length of character list 
            var RandomCharacter = CharacterLists[randomIndex];
            return RandomCharacter.character;
        }

        public string GetRandomPassword(int PasswordLength)
        {
            var password = new char[PasswordLength];
            for (int i = 0; i < PasswordLength; i++)
            {
                password[i] = Convert.ToChar(GetRandomCharacter());
            }
            string GeneratedPassword = string.Join("", password);

            return GeneratedPassword;
        }
    }
}
