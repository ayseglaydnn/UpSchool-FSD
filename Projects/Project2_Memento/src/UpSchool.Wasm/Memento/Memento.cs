using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UpSchool.Domain.Dtos;
using UpSchool.Wasm.Common.Utilities;
using UpSchool.Wasm.Memento;

namespace UpSchool.Wasm.Memento
{
    public class Memento
    {
        private int position = 0;
        private List<PasswordMemento> memento;
        private PasswordMemento currentMemento;


        public Memento()
        {
            memento = new List<PasswordMemento>();
            currentMemento = new PasswordMemento();

        }

        public int Position()
        {
            return position;
        }

        public int MementoLength()
        {
            return memento.Count();
        }

        public void save(string password)
        {
            PasswordMemento passwordMemento = new PasswordMemento()
            {
                Password = password,
                PasswordCssColour = CssUtilities.GetCssColourClassForPasswords(password.Length)
            };

            memento.Add(passwordMemento);
            position = memento.Count - 1;
        }

        public PasswordMemento undo()
        {
            if (position > 0)
            {
                currentMemento = memento[position - 1];
                position--;
                return currentMemento;
            }
            else
            {
                currentMemento = memento[position];
                return currentMemento;
            }
        }

        public PasswordMemento redo()
        {
            if (position + 1 < memento.Count)
            {
                currentMemento = memento[position + 1];
                position++;
                return currentMemento;
            }
            else
            {
                currentMemento = memento[position];
                return currentMemento;
            }

        }

    }
}