using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKbibliotheque
{
    static internal class NK
    {

        public static char DemanderUneLettreDans(string message, List<char> chars)
        {
            char letter = DemanderUneLettre(message);

            if (!chars.Contains(letter))
            {
                return DemanderUneLettreDans("ERRUER, Vous devez choisir une lettre parmis celles proposées : ", chars);
            }
            return letter;
        }
        public static char DemanderUneLettre(string message)
        {
            string chiffres = "0,1,2,3,4,5,6,7,8,9";
            string alphabetMin = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string alphabetMax = alphabetMin.ToString().ToUpper();
            string alphabet = alphabetMin + alphabetMax;

            Console.Write(message);
            string lettre = Console.ReadLine();

            //  Cas où l'utilisateur entre plusieurs lettre (un mot)
            if (lettre.Length > 1)
                return DemanderUneLettre("\nERREUR : Veuillez entrer une seule lettre : ");

            if (lettre.Length == 0)
                return DemanderUneLettre("\nERREUR : Vous devez entrer obligatoirement une lettre : ");

            if (chiffres.Contains(lettre))
                return DemanderUneLettre("\nERREUR : Vous ne pouvez entrer un chiffre, veuillez entrer une lettre : ");

            if (!alphabet.Contains(lettre))
                return DemanderUneLettre("\nERREUR : Caractere inconnu, Veuillez entrer une lettre de l'alphabet : ");

            lettre = lettre.ToUpper();

            return lettre[0];
            
        }
        public static int DemanderNombrePositifNonNull(string message)
        {
            int nombre = DemanderNombre(message);
            if (nombre > 0)
                return nombre;
            return DemanderNombrePositifNonNull("\nLe nombre doit etre superieur a zéro (0) : ");
        }
        public static int DemanderNombreEntre(string message, int min, int max)
        {
            int nombre = DemanderNombre(message);
            if (nombre < min || nombre > max)
                return DemanderNombreEntre($"\nLe nombre doit être compris entre {min} et {max} : ", min, max);
            return nombre;
        }
        public static int DemanderNombre(string message)
        {
            Console.Write(message);
            try
            {
                int number = int.Parse(Console.ReadLine());
                return number;
            }
            catch
            {
                return DemanderNombre("\nErreur, vous devez entrer un entier : ");
            }
        }
    }
}
