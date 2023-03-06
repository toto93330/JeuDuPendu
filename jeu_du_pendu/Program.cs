using Microsoft.VisualBasic;
using System;
using System.Diagnostics.Metrics;
using System.Linq;
using AsciiArt;

namespace jeu_du_pendu
{
    internal class Program
    {

        static void AfficherMot(string mot, List<char> lettres)
        {
            

            for (int i = 0; i < mot.Length; i++)
            {
                if (lettres.Contains(mot[i]))
                {
                    Console.Write(mot[i] + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
                
            }


        }

        static char DemanderUneLettre()
        {

            while (true)
            {
                Console.Write("Rentrez une lettre :");

                string resultat = Console.ReadLine().ToUpper();
        
                if(resultat.Length == 1)
                {
                    char conversion = resultat[0];
                    return conversion;
                }

                Console.WriteLine("ERREUR : Vous devez rentrer une lettre");
            }
        }

        static void DevinerMot(string mot)
        {
            
            var lettresDeviner = new List<char>();
            int life = 7;
            var lettresPasDansLeMot = new List<char>();
            int pendu = 0;

            while (life > 0)
            {
                Console.WriteLine(Ascii.PENDU[pendu]);
                Console.WriteLine("");
                Console.WriteLine("");
                AfficherMot(mot, lettresDeviner);
                Console.WriteLine("");
                Console.WriteLine("");
                var lettre = DemanderUneLettre();
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("");
                for (int i = 0; i < mot.Length; i++)
                {
                    if (mot.Contains(lettre))
                    {

                            int memeLettre = 0;

                            for (int j = 0; j < mot.Length; j++)
                            {
                                if (mot[j] == lettre)
                                {
                                    memeLettre++;
                                }
                            }

                            if (lettresDeviner.Contains(lettre))
                            {
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("La lettre " + lettre + " à déjà ete trouvé !!!");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                break;
                            }
                            else
                            {
                                while (memeLettre > 0)
                                {
                                    lettresDeviner.Add(lettre);
                                    memeLettre--;
                                }
                            }

                            Console.WriteLine(lettresDeviner.Count);
                            Console.WriteLine(mot.Length);
                            Console.WriteLine("La lettre " + lettre + " est dans le mot");

                               if (lettresDeviner.Count == mot.Length)
                                {
                                    Console.Clear();
                                    Console.WriteLine("");
                                    Console.WriteLine("");
                                    Console.WriteLine("Bravo vous avez trouver le mot !!!");
                                    Console.WriteLine("");
                                    Console.WriteLine("");
                                    /* ON GAGNE NOUVELLE PARTIE */
                                    NouvellePartie(mot, true);
                                }

                            Console.WriteLine("");
                            Console.WriteLine("");
                            break;  
                    }
                    else
                    {
                        if (!lettresPasDansLeMot.Contains(lettre))
                        {
                            lettresPasDansLeMot.Add(lettre);
                            life--;
                            pendu++;
                        }
                        
                        Console.WriteLine("La lettre " + lettre + " n'est pas dans le mot les lettres utilisé : " + String.Join(", ", lettresPasDansLeMot));
                        Console.WriteLine("");
                        Console.WriteLine("");
                        

                        if (life == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("VOUS AVEZ PERDU LE MOT ETAIT : " + mot);
                            Console.WriteLine("");
                            Console.WriteLine("");

                            /* PERDU ON RECOMENCE */
                            NouvellePartie(mot);
                        }

                        Console.Write("Il vous reste : " + life + " vies");
                        Console.WriteLine(""); 
                       
                        break;
                    }
                }

            }

            Console.Clear();
        
        }

        static void NouvellePartie(string mot, bool restart = false)
        {
            if(restart)
            {
                /*Nouvelle partie*/
                Console.WriteLine("Voulez vous commencer une nouvelle partie ? o/n");
            }
            else
            {
                /*Nouvelle recommencer*/
                Console.WriteLine("Voulez vous commencer une nouvelle partie ? o/n");
            }
            
            var recommencer = Console.ReadLine();
            Console.Clear();
            if (recommencer == "o" || recommencer == "n")
            {

                if (recommencer == "o")
                {
                    var mots = ChargerLesMots("mots.txt");

                    if (restart)
                    {
                        /*Nouvelle partie*/
                        int nombreAleatoire = (new Random()).Next(0, mots.Length);
                        DevinerMot(mot);
                    }
                    else
                    {
                        /*recommencer*/
                        int nombreAleatoire = (new Random()).Next(0, mots.Length);
                        DevinerMot(mot);
                    }
                }
                else
                {
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("ERREUR : Vous devez ecrire o pour 'oui' ou n 'non'");
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
        static string[] ChargerLesMots(string nomDuFichier)
        {
            try
            {
                return File.ReadAllLines(nomDuFichier);
            }catch(Exception e)
            {
                Console.WriteLine("Erreur de lecture du fichier : " + nomDuFichier);
            }
            return null;
        }

        static void Main(string[] args)
        {
            var mots = ChargerLesMots("mots.txt");


            if( (mots == null) || (mots.Length == 0))
            {
                Console.WriteLine("la liste de mot est vide");
            }
            else
            {

            int nombreAleatoire = (new Random()).Next(0, mots.Length);
            string mot = mots[nombreAleatoire].Trim().ToUpper();
            DevinerMot(mot);
            }
           

        }
    }
}