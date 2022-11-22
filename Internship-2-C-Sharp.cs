using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Linq;

namespace Svjetsko_prvenstvo
{
    class Program
    {

        static void Main(string[] args)
        {



            var points = new Dictionary<string, int>()
    {
    { "Hrvatska", 0},
    { "Belgija", 0},
    { "Maroko", 0},
    { "Kanada", 0}
    };

            var players = new Dictionary<string, (string position, double rating)>()
    {
    { "Luka Modrić", ("MF", 88) },
    { "Marcelo Brozović", ("MF", 86) },
    { "Mateo Kovačić", ("MF", 84) },
    { "Ivan Perišić", ("MF", 84) },
    { "Andrej Kramarić", ("FW", 82) },
    { "Ivan Rakitić", ("MF", 82) },
    { "Joško Gvardiol", ("DF", 81) },
    { "Mario Pašalić", ("MF", 81) },
    { "Lovro Majer", ("MF", 80) },
    { "Dominik Livaković", ("GK", 80) },
    { "Ante Rebić", ("FW", 80) },
    { "Josip Brekalo", ("MF", 79) },
    { "Borna Sosa", ("DF", 78) },
    { "Duje Ćaleta-Car", ("DF", 78) },
    { "Nikola Vlašić", ("MF", 78) },
    { "Dejan Lovren", ("DF", 78) },
    { "Mislav Oršić", ("FW", 77) },
    { "Marko Livaja", ("FW", 77) },
    { "Domagoj Vida", ("DF", 76) },
    { "Ante Budimir", ("FW", 78) }
    };


            var results = new Dictionary<string, (int first, int second)>()
     {
     { "Hrvatska - Maroko",( 0 , 0)},
     { "Hrvatska - Belgija",( 0 , 0)},
     { "Hrvatska - Kanada", ( 0 , 0)},
     { "Belgija - Maroko",( 0 , 0)},
     { "Belgija - Kanada",( 0 , 0)},
     { "Kanada - Maroko", ( 0 , 0) }
     };


            var cDF = 0;
            var cGK = 0;
            var cMF = 0;
            var cFW = 0;

            var shooters = new Dictionary<string, int>();

            var goaldiffH = 0;
            var goaldiffB = 0;
            var goaldiffK = 0;
            var goaldiffM = 0;

            var newname = "0";
            var newposition = "0";
            var newrating = 0;

            var game = new Dictionary<string, int>();
            foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
            {
                if (Equals(item.Value.position, "DF") == true)
                {
                    game.Add(item.Key, 0);
                    cDF++;
                }
                if (cDF == 4)
                    break;
            }

            foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
            {
                if (Equals(item.Value.position, "GK") == true)
                {
                    game.Add(item.Key, 0);
                    cGK++;
                }
                if (cGK == 1)
                    break;
            }

            foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
            {
                if (Equals(item.Value.position, "MF") == true)
                {
                    game.Add(item.Key, 0);
                    cMF++;
                }
                if (cMF == 3)
                    break;
            }

            foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
            {
                if (Equals(item.Value.position, "FW") == true)
                {
                    game.Add(item.Key, 0);
                    cFW++;
                }
                if (cFW == 3)
                    break;
            }


            Console.WriteLine("SVJETSKO PRVENSTVO");

            do
            {
                int flag = 2;

                Console.WriteLine("\nSTART");
                Console.WriteLine("\t1 - Odradi trening");
                Console.WriteLine("\t2 - Odigraj utakmicu");
                Console.WriteLine("\t3 - Statistika");
                Console.WriteLine("\t4 - Kontrola igraca");
                Console.WriteLine("\t0 - Izlaz iz aplikacije");

                Console.Write("\nUnesi broj akcije: ");
                var act = 0 ;
                var z = 0;
                do
                {
                    bool parse = int.TryParse(Console.ReadLine(), out act);
                    if (parse == true)
                        z = 1;
                }
                while (z == 0);
                
                if (act < 1 || act > 4)
                {
                    Console.WriteLine("\nIzbor akcije nije ispravan - unesi broj akcije: ");
                    continue;
                }

                //TRENING
                if (act == 1)
                {
                    Console.WriteLine("\nTRENING");
                    foreach (var item in players)
                    {
                        Random rand = new Random();
                        var per = 0.01 * rand.Next(-5, 6);
                        players[item.Key] = (item.Value.position, (item.Value.rating + item.Value.rating * per));
                        Console.WriteLine($"{item.Key}, {item.Value.position}, Stari rating: {item.Value.rating}, Novi rating: {(item.Value.rating + item.Value.rating * per)}");
                    }
                    foreach (var item in players)
                    {
                        if (item.Value.rating >= 100)
                            players[item.Key] = (item.Value.position, 100);
                    }

                    do
                    {
                        Console.WriteLine("\nOdaberi 0 ako želiš izaći iz aplikacije. Odaberi 1 ako želiš nastaviti.");
                        int youchose = 0;
                        do
                        {
                            bool parse = int.TryParse(Console.ReadLine(), out youchose);
                            if (parse == true)
                                z = 1;
                        }
                        while (z == 0);
                        if (youchose == 1)
                        {
                            flag = 1;
                            break;
                        }
                        else if (youchose == 0)
                        {
                            flag = 0;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nIzbor akcije nije ispravan - unesi broj akcije: ");
                            continue;
                        }
                    } while (true);
                    if (flag == 0)
                    {
                        return;
                    }
                    else
                        continue;
                }

                //UTAKMICA
                else if (act == 2)
                {
                    Console.WriteLine("\nUTAKMICA");
                    if (game.Count() < 11)
                    {
                        Console.WriteLine("\nTvoj tim nije potpun.");
                        do
                        {
                            Console.WriteLine("\nOdaberi 0 ako želiš izaći iz aplikacije. Odaberi 1 ako želiš nastaviti.");
                            int youchose = 0;
                            do
                            {
                                bool parse = int.TryParse(Console.ReadLine(), out youchose);
                                if (parse == true)
                                    z = 1;
                            }
                            while (z == 0);
                            if (youchose == 1)
                            {
                                flag = 1;
                                break;
                            }
                            else if (youchose == 0)
                            {
                                flag = 0;
                                break;
                            }
                            else
                            {
                                Console.Write("\nIzbor akcije nije ispravan - unesi broj akcije: ");
                                continue;
                            }
                        } while (true);
                        if (flag == 0)
                        {
                            return;
                        }
                        else
                            continue;
                    }

                    else
                    {
                        Console.WriteLine("\nTvoj tim: ");
                        foreach (var item in game)
                            Console.WriteLine(item.Key);

                        foreach (var item in results)
                        {
                            var rand1 = new Random();
                            var rand2 = new Random();
                            results[item.Key] = (rand1.Next(0, 11), rand2.Next(0, 11));

                            if (rand1.Next(0, 11) < rand2.Next(0, 11) && Equals(item.Key, "Hrvatska - Belgija") == true || Equals(item.Key, "Hrvatska - Maroko") == true || Equals(item.Key, "Hrvatska - Kanada") == true)
                            {
                                foreach (var p in players)
                                    players[p.Key] = (p.Value.position, (p.Value.rating - 2));
                            }
                            else if (rand1.Next(0, 11) > rand2.Next(0, 11) && Equals(item.Key, "Hrvatska - Belgija") == true || Equals(item.Key, "Hrvatska - Maroko") == true || Equals(item.Key, "Hrvatska - Kanada") == true)
                            {
                                foreach (var p in players)
                                    players[p.Key] = (p.Value.position, (p.Value.rating + 2));
                            }
                            if (rand1.Next(0, 11) < rand2.Next(0, 11))


                                switch (item.Key)
                                {
                                    case "Hrvatska - Maroko":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Maroko") == true)
                                                points[t.Key] = t.Value + 3;

                                        goaldiffH -= rand2.Next(0, 11);
                                        goaldiffH += rand1.Next(0, 11);
                                        goaldiffM -= rand1.Next(0, 11);
                                        goaldiffM += rand2.Next(0, 11);
                                        break;

                                    case "Hrvatska - Kanada":
                                        foreach (var t in points)

                                            if (Equals(t.Key, "Kanada") == true)
                                                points[t.Key] = t.Value + 3;

                                        goaldiffH -= rand2.Next(0, 11);
                                        goaldiffH += rand1.Next(0, 11);
                                        goaldiffK -= rand1.Next(0, 11);
                                        goaldiffK += rand2.Next(0, 11);
                                        break;

                                    case "Hrvatska - Belgija":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Belgija") == true)
                                                points[t.Key] = t.Value + 3;

                                        goaldiffH -= rand2.Next(0, 11);
                                        goaldiffH += rand1.Next(0, 11);
                                        goaldiffB -= rand1.Next(0, 11);
                                        goaldiffB += rand2.Next(0, 11);
                                        break;

                                    case "Belgija - Maroko":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Belgija") == true)
                                                points[t.Key] = t.Value + 3;

                                        goaldiffB -= rand2.Next(0, 11);
                                        goaldiffB += rand1.Next(0, 11);
                                        goaldiffM -= rand1.Next(0, 11);
                                        goaldiffM += rand2.Next(0, 11);
                                        break;

                                    case "Belgija - Kanada":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Kanada") == true)
                                                points[t.Key] = t.Value + 3;

                                        goaldiffB += rand2.Next(0, 11);
                                        goaldiffB -= rand1.Next(0, 11);
                                        goaldiffK += rand1.Next(0, 11);
                                        goaldiffK -= rand2.Next(0, 11);
                                        break;

                                    case "Kanada - Maroko":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Maroko") == true)
                                                points[t.Key] = t.Value + 3;

                                        goaldiffK += rand2.Next(0, 11);
                                        goaldiffK -= rand1.Next(0, 11);
                                        goaldiffM += rand1.Next(0, 11);
                                        goaldiffM -= rand2.Next(0, 11);

                                        break;
                                }


                            else if (rand1.Next(0, 11) > rand2.Next(0, 11))
                                switch (item.Key)
                                {
                                    case "Hrvatska - Maroko":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Hrvatska") == true)
                                                points[t.Key] = t.Value + 3;
                                        break;

                                    case "Hrvatska - Kanada":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Hrvatska") == true)
                                                points[t.Key] = t.Value + 3;
                                        break;

                                    case "Hrvatska - Belgija":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Hrvatska") == true)
                                                points[t.Key] = t.Value + 3;
                                        break;

                                    case "Belgija - Maroko":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Belgija") == true)
                                                points[t.Key] = t.Value + 3;
                                        break;

                                    case "Belgija - Kanada":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Belgija") == true)
                                                points[t.Key] = t.Value + 3;
                                        break;

                                    case "Kanada - Maroko":
                                        foreach (var t in points)
                                            if (Equals(t.Key, "Kanada") == true)
                                                points[t.Key] = t.Value + 3;
                                        break;

                                }
                            else
                                foreach (var t in points)
                                    points[t.Key] = t.Value + 1;
                        }

                        foreach (var goal in shooters)
                            shooters[goal.Key] = (0);
                        foreach (var r in results)
                            if (Equals(r.Key, "Hrvatska - Belgija") == true || Equals(r.Key, "Hrvatska - Maroko") == true || Equals(r.Key, "Hrvatska - Kanada") == true)
                            {
                                if (r.Value.first != 0)
                                {
                                    var goal = r.Value.first;
                                    do
                                    {
                                        goal--;
                                        foreach (var g in game)
                                        {
                                            var rand3 = new Random();
                                            var shooter = rand3.Next(0, game.Count());

                                            var count = -1;
                                            foreach (var i in game)
                                            {
                                                count++;
                                                if (count == shooter)
                                                {
                                                    if (!shooters.ContainsKey(i.Key))
                                                        shooters[i.Key] = 1;
                                                    else
                                                        shooters[i.Key] = (i.Value + 1);

                                                    foreach (var p in players)
                                                        if (Equals(players[p.Key], game[i.Key]) == true)
                                                            players[p.Key] = (p.Value.position, (p.Value.rating + 5));
                                                }
                                            }
                                        }
                                    } while (goal > 0);
                                }
                            }
                        Console.WriteLine("\nRezultati trenutnog kola:");
                        foreach (var team in results)
                            Console.WriteLine($"{team.Key} : {team.Value.first} - {team.Value.second}");
                    }

                    do
                    {
                        Console.WriteLine("\nOdaberi 0 ako želiš izaći iz aplikacije. Odaberi 1 ako želiš nastaviti.");
                        int youchose = 0;
                        do
                        {
                            bool parse = int.TryParse(Console.ReadLine(), out youchose);
                            if (parse == true)
                                z = 1;
                        }
                        while (z == 0);
                        if (youchose == 1)
                        {
                            flag = 1;
                            break;
                        }
                        else if (youchose == 0)
                        {
                            flag = 0;
                            break;
                        }
                        else
                        {
                            Console.Write("\nIzbor akcije nije ispravan - unesi broj akcije: ");
                            continue;
                        }
                    } while (true);
                    if (flag == 0)
                    {
                        return;
                    }
                    else
                        continue;
                }

                //STATISTIKA
                else if (act == 3)
                {
                    Console.WriteLine("\nSTATISTIKA");

                    Console.WriteLine("\t1 - Ispis onako kako su spremljeni");
                    Console.WriteLine("\t2 - Ispis po rating uzlazno");
                    Console.WriteLine("\t3 - Ispis po rating silazno");
                    Console.WriteLine("\t4 - Ispis igrača po imenu i prezimenu (ispis pozicije i ratinga)");
                    Console.WriteLine("\t5 - Ispis najboljeg igrača po ratingu");
                    Console.WriteLine("\t6 - Ispis najboljeg igrača po poziciji ");
                    Console.WriteLine("\t7 - Ispis trenutnih prvih 11 igrača");
                    Console.WriteLine("\t8 - Ispis strijelaca i koliko golova imaju");
                    Console.WriteLine("\t9 - Ispis svih rezultata ekipe");
                    Console.WriteLine("\t10 - Ispis rezultat svih ekipa");
                    Console.WriteLine("\t11 - Ispis tablice grupe");

                    Console.WriteLine("\nUnesi broj akcije: ");
                    var chosen = 0;
                    do
                    {
                        bool parse = int.TryParse(Console.ReadLine(), out chosen);
                        if (parse == true)
                            z = 1;
                    }
                    while (z == 0);

                    switch (chosen)
                    {
                        case 1:
                            foreach (var item in players)
                                Console.WriteLine($"Ime: {item.Key}, Pozicija: {item.Value.position}, Rating: {item.Value.rating}");
                            break;

                        case 2:
                            foreach (var item in players.OrderBy(Key => Key.Value.rating))
                                Console.WriteLine($"Ime: {item.Key}, Pozicija: {item.Value.position}, Rating: {item.Value.rating}");
                            break;

                        case 3:
                            foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
                                Console.WriteLine($"Ime: {item.Key}, Pozicija: {item.Value.position}, Rating: {item.Value.rating}");
                            break;

                        case 4:
                            foreach (var item in players.OrderBy(Key => Key.Key))
                                Console.WriteLine($"Ime: {item.Key}, Pozicija: {item.Value.position}, Rating: {item.Value.rating}");
                            break;

                        case 5:
                            {
                                Console.WriteLine("\nNajbolji igrači po ratingu.");
                                int c = 0;
                                var max = players.ElementAt(0);

                                foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
                                    if (Equals(item, max) == true)
                                        c++;
                                int ccw = 0;
                                foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
                                {
                                    Console.WriteLine($"{item.Key}");
                                    ccw++;
                                    if (ccw == c)
                                        break;
                                }
                                break;
                            }

                        case 6:
                            {
                               
                                Console.WriteLine("\nIgrači po poziciji.");
                                foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
                                {
                                    if (Equals(item.Value.position, "DF") == true)
                                    {
                                        Console.WriteLine($"Obrambeni: {item.Key}");
                                    }
                                }
                                foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
                                {
                                    
                                    if (Equals(item.Value.position, "GK") == true)
                                    {
                                        Console.WriteLine($"Golman: {item.Key}");
                                        
                                    }
                                }
                                foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
                                {
                                    if (Equals(item.Value.position, "MF") == true)
                                    {
                                        Console.WriteLine($"Vezni: {item.Key}");
                                            
                                    }
                                }
                                foreach (var item in players.OrderByDescending(Key => Key.Value.rating))
                                {
                                    if (Equals(item.Value.position, "FW") == true)
                                    {
                                        Console.WriteLine($"Napadač: {item.Key}");
                                        
                                    }
                                }
                                break;
                            }

                        case 7:
                            Console.WriteLine("\nTvoj tim: ");
                            foreach (var item in game)
                                Console.WriteLine($"{item.Key}");
                            break;

                        case 8:
                            Console.WriteLine("\nStrijelci: ");
                            foreach (var i in shooters)
                                Console.WriteLine($"{i.Key}, {i.Value}");
                            break;

                        case 9:
                        case 10:
                            {
                                Console.WriteLine("Ispis rezultata: ");
                                Console.WriteLine("\t1 - Belgija");
                                Console.WriteLine("\t2 - Hrvatska");
                                Console.WriteLine("\t3 - Maroko");
                                Console.WriteLine("\t4 - Kanada");
                                Console.WriteLine("\t0 - Izlaz iz aplikacije");

                                var op3 = 1;
                                do
                                {
                                    bool parse = int.TryParse(Console.ReadLine(), out op3);
                                    if (parse == true)
                                        z = 1;
                                }
                                while (z == 0);
                                switch (op3)
                                {
                                    case 1:
                                        foreach (var item in results)
                                            if (Equals(item.Key, "Hrvatska - Belgija") == true)
                                                Console.WriteLine($"Hrvatska - Belgija : {item.Value}");
                                            else if (Equals(item.Key, "Belgija - Kanada") == true)
                                                Console.WriteLine($"Belgija - Kanada : {item.Value}");
                                            else if (Equals(item.Key, "Belgija - Maroko") == true)
                                                Console.WriteLine($"Belgija - Maroko : {item.Value}");
                                        break;

                                    case 2:
                                        foreach (var item in results)
                                            if (Equals(item.Key, "Hrvatska - Maroko") == true)
                                                Console.WriteLine($"Hrvatska - Maroko : {item.Value}");
                                            else if (Equals(item.Key, "Hrvatska - Belgija") == true)
                                                Console.WriteLine($"Hrvatska - Belgija : {item.Value}");
                                            else if (Equals(item.Key, "Hrvatska - Kanada") == true)
                                                Console.WriteLine($"Hrvatska - Kanada : {item.Value}");
                                        break;

                                    case 3:
                                        foreach (var item in results)
                                            if (Equals(item.Key, "Belgija - Maroko") == true)
                                                Console.WriteLine($"Belgija - Maroko : {item.Value}");
                                            else if (Equals(item.Key, "Kanada - Maroko") == true)
                                                Console.WriteLine($"Kanada - Maroko : {item.Value}");
                                            else if (Equals(item.Key, "Hrvatska - Maroko") == true)
                                                Console.WriteLine($"Hrvatska - Maroko : {item.Value}");
                                        break;

                                    case 4:
                                        foreach (var item in results)
                                            if (Equals(item.Key, "Kanada - Maroko") == true)
                                                Console.WriteLine($"Kanada - Maroko : {item.Value}");
                                            else if (Equals(item.Key, "Belgija - Kanada") == true)
                                                Console.WriteLine($"Belgija - Kanada : {item.Value}");
                                            else if (Equals(item.Key, "Hrvatska - Kanada") == true)
                                                Console.WriteLine($"Hrvatska - Kanada : {item.Value}");
                                        break;
                                    case 0:
                                        {
                                            Console.WriteLine("IZLAZ IZ APLIKACIJE");
                                            return;
                                        }
                                    default:
                                        Console.WriteLine("Izbor akcije nije ispravan ");
                                        break;

                                }
                                break;
                            }

                        case 11:
                            {
                                Console.WriteLine("\nTablica.");
                                int place = 0;
                                foreach (var it in points.OrderByDescending(Key => Key.Value))
                                {
                                    place++;
                                    Console.Write(place);
                                    Console.Write(".");

                                    if (Equals(it.Key, "Hrvatska") == true)
                                        Console.WriteLine($"{it.Key} --> bodova: {it.Value} --> gol razlika: {goaldiffH}");
                                    if (Equals(it.Key, "Belgija") == true)
                                        Console.WriteLine($"{it.Key} --> bodova: {it.Value} --> gol razlika: {goaldiffB}");
                                    if (Equals(it.Key, "Maroko") == true)
                                        Console.WriteLine($"{it.Key} --> bodova: {it.Value} --> gol razlika: {goaldiffM}");
                                    if (Equals(it.Key, "Kanada") == true)
                                        Console.WriteLine($"{it.Key} --> bodova: {it.Value} --> gol razlika: {goaldiffK}");
                                }
                                break;
                            }

                        default:
                            Console.WriteLine("Izbor akcije nije ispravan ");
                            break;
                    }

                    do
                    {
                        Console.WriteLine("\nOdaberi 0 ako želiš izaći iz aplikacije. Odaberi 1 ako želiš nastaviti.");
                        var youchose = 0;
                        do
                        {
                            bool parse = int.TryParse(Console.ReadLine(), out youchose);
                            if (parse == true)
                                z = 1;
                        }
                        while (z == 0);
                        if (youchose == 1)
                        {
                            flag = 1;
                            break;
                        }
                        else if (youchose == 0)
                        {
                            flag = 0;
                            break;
                        }
                        else
                        {
                            Console.Write("\nIzbor akcije nije ispravan - unesi broj akcije: ");
                            continue;
                        }
                    } while (true);
                    if (flag == 0)
                    {
                        return;
                    }
                    else
                        continue;
                }

                //IGRAČI
                else if (act == 4)
                {
                    Console.WriteLine("\nUREĐIVANJE IGRAČA");
                    Console.WriteLine("\t1 - Unos novog igrača");
                    Console.WriteLine("\t2 - Brisanje igrača");
                    Console.WriteLine("\t3 - Uređivanje igrača");
                    Console.WriteLine("\t0 - Izlaz iz aplikacije");
                    Console.WriteLine("\nUnesi broj akcije: ");
                    var chosen = 1;
                    do
                    {
                        bool parse = int.TryParse(Console.ReadLine(), out chosen);
                        if (parse == true)
                            z = 1;
                    }
                    while (z == 0);

                    switch (chosen)
                    {
                        //UNOS NOVOG IGRAČA
                        case 1:
                            {
                                Console.WriteLine("\nUnos novog igrača.");
                                if (players.Count() > 26)
                                    Console.WriteLine("Tvoj tim je pun.");
                                Console.WriteLine("Željeni broj novih igrača: ");
                                int cnp = 0;
                                do
                                {
                                    bool parse = int.TryParse(Console.ReadLine(), out cnp);
                                    if (parse == true)
                                        z = 1;
                                }
                                while (z == 0);
                                do
                                {
                                    if (players.Count() + cnp <= 26)
                                    {
                                        do
                                        {
                                            var f1 = 1;
                                            do
                                            {
                                                Console.WriteLine("\nUnesi ime i prezime igrača:");
                                                newname = Console.ReadLine();
                                                if (!players.ContainsKey(newname))
                                                    f1 = 0;
                                                else
                                                    Console.WriteLine("Već je u timu. Unesi opet.");
                                            } while (f1 != 0);

                                            var f2 = 1;
                                            do
                                            {
                                                Console.WriteLine("\nDostupne pozicije: GK, DF, MF, FW");
                                                Console.WriteLine("\nUnesi poziciju igrača:");
                                                newposition = Console.ReadLine();
                                                newposition = newposition.ToUpper();
                                                if (!(Equals(newposition, "GK") != true && Equals(newposition, "DF") != true && Equals(newposition, "MF") != true && Equals(newposition, "FW") != true))
                                                    f2 = 0;
                                                else
                                                    Console.WriteLine("Ta pozicija nije ponuđena.");
                                            } while (f2 != 0);

                                            var f3 = 1;
                                            do
                                            {
                                                Console.WriteLine("\nUnesi rating do 100: ");
                                                newrating = 0;
                                                do
                                                {
                                                    bool parse = int.TryParse(Console.ReadLine(), out newrating);
                                                    if (parse == true)
                                                        z = 1;
                                                }
                                                while (z == 0);
                                                if (newrating <= 100 && newrating >= 0)
                                                    f3 = 0;
                                                else
                                                    Console.WriteLine("Rating mora biti u intervalu [0,100].");
                                            } while (f3 != 0);

                                            players.Add(newname, (newposition, newrating));
                                            Console.WriteLine("Novi igrač je dodan.");
                                            cnp--;
                                        } while (cnp != 0);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nUnesi manji broj novih igrača. Tvoj tim je uskoro pun.");
                                        continue;
                                    }
                                    break;
                                } while (true);
                                break;
                            }

                        //BRISANJE IGRAČA
                        case 2:
                            {
                                Console.WriteLine("\nBrisanje igrača unosom imena i prezimena");

                                var f1 = 1;
                                int ce;
                                do
                                {
                                    Console.WriteLine("Broj igrača koje želimo izbrisati: ");
                                    ce = 1;
                                    do
                                    {
                                        bool parse = int.TryParse(Console.ReadLine(), out ce);
                                        if (parse == true)
                                            z = 1;
                                    }
                                    while (z == 0);
                                    if (ce > 0 && ce <= players.Count)
                                        f1 = 0;
                                    else
                                        Console.WriteLine("Taj broj igrača nije dopušten.");
                                } while (f1 != 0);

                                do
                                {
                                    ce--;
                                    Console.WriteLine("\nUnesi ime i prezime igrača:");
                                    var name = Console.ReadLine();
                                    if (players.ContainsKey(name) == false)
                                    {
                                        Console.WriteLine("Taj igrač ne priprada timu. ");

                                        ce++;
                                        continue;
                                    }
                                    players.Remove(name);
                                } while (ce != 0);

                                Console.WriteLine("Igrači su izbrisani");
                                break;
                            }

                        //UREĐIVANJE
                        case 3:
                            {
                                Console.WriteLine("\nUređivanje.");
                                Console.WriteLine("\t1 - Uredi ime i prezime igrača");
                                Console.WriteLine("\t2 - Uredi poziciju igrača (GK, DF, MF ili FW)");
                                Console.WriteLine("\t3 - Uredi rating igrača (od 1 do 100)");
                                Console.WriteLine("Unesi broj akcije: ");
                                var option = 0;
                                do
                                {
                                    bool parse = int.TryParse(Console.ReadLine(), out option);
                                    if (parse == true)
                                        z = 1;
                                }
                                while (z == 0);
                                if (option < 1 || option > 3)
                                {
                                    Console.Write("\nIzbor akcije nije ispravan - unesi broj akcije: ");
                                    continue;
                                }

                                switch (option)
                                {
                                    case 1:
                                        do
                                        {
                                            Console.WriteLine("\nUnesi ime i prezime igrača za uređivanje: ");
                                            var editname = Console.ReadLine();

                                            if (!(players.ContainsKey(editname) == true))
                                            {
                                                Console.WriteLine("Uneseno ime nije u timu.");
                                                continue;
                                            }

                                            Console.WriteLine(" Unesi novo ime:");
                                            var editedname = Console.ReadLine();
                                            players[editedname] = players[editname];
                                            players.Remove(editname);
                                            break;
                                        } while (true);
                                        Console.WriteLine("Igrač je uređen.");
                                        break;

                                    case 2:
                                        {
                                            do
                                            {
                                                Console.WriteLine("\nUnesi ime i prezime igrača za uređivanje: ");
                                                var editname = Console.ReadLine();
                                                if (!(players.ContainsKey(editname) == true))
                                                {
                                                    Console.WriteLine("Uneseno ime nije u timu.");
                                                    continue;
                                                }

                                                Console.WriteLine("Unesi novu poziciju: GK, DF, MF ili FW");
                                                var editedposition = Console.ReadLine();
                                                editedposition = editedposition.ToUpper();
                                                if (!(Equals(editedposition, "GK") == true || Equals(editedposition, "DF") == true || Equals(editedposition, "MF") == true || Equals(editedposition, "FW") == true))
                                                {
                                                    Console.WriteLine("Unesena pozicija nije ponuđena.");
                                                    continue;
                                                }
                                                double samerating = 0.0;
                                                foreach (var item in players)
                                                    if (Equals(item.Key, editname) == true)
                                                        samerating = item.Value.rating;
                                                players[editname] = (editedposition, samerating);
                                                break;
                                            } while (true);
                                            Console.WriteLine("Igrač je uređen.");
                                            break;
                                        }
                                    case 3:
                                        {
                                            var f1 = 0;
                                            do
                                            {
                                                var editname = "";
                                                if (f1 == 0)
                                                {
                                                    Console.WriteLine("\nUnesi ime i prezime igrača za uređivanje: ");
                                                    editname = Console.ReadLine();
                                                    
                                                    if (!(players.ContainsKey(editname) == true))
                                                    {
                                                        Console.WriteLine("Uneseno ime nije u timu ili je krivo uneseno.");
                                                        continue;
                                                    }
                                                }
                                                Console.WriteLine("Unesi novi rating:");
                                                var editedrating = 0;
                                                do
                                                {
                                                    bool parse = int.TryParse(Console.ReadLine(), out editedrating);
                                                    if (parse == true)
                                                        z = 1;
                                                }
                                                while (z == 0);
                                                if (editedrating > 100 || editedrating < 0)
                                                {
                                                    Console.WriteLine("Rating mora biti u intervalu [0,100].");
                                                    f1 = 1;
                                                    continue;
                                                }


                                                players[editname] = (players[editname].position, editedrating);

                                                Console.WriteLine("Igrač je uređen.");
                                                break;

                                            } while (true);
                                            break;
                                        }

                                }

                                break;
                            }


                        case 0:
                            {
                                Console.WriteLine("IZLAZ IZ APLIKACIJE");
                                return;
                            }

                    }
                }

                //IZLAZ IZ APLIKACIJE
                else if (act == 0)
                {
                    Console.WriteLine("IZLAZ IZ APLIKACIJE");
                    Console.ReadKey();
                    return;
                }

                do
                {
                    Console.WriteLine("\nOdaberi 0 ako želiš izaći iz aplikacije. Odaberi 1 ako želiš nastaviti.");
                    int youchose = 0;
                    do
                    {
                        bool parse = int.TryParse(Console.ReadLine(), out youchose);
                        if (parse == true)
                            z = 1;
                    }
                    while (z == 0);
                    if (youchose == 1)
                    {
                        flag = 1;
                        break;
                    }
                    else if (youchose == 0)
                    {
                        flag = 0;
                        break;
                    }
                    else
                    {
                        Console.Write("\nUnesi broj akcije opet: ");
                        continue;
                    }
                } while (true);
                if (flag == 0)
                {
                    return;
                }
                else
                    continue;

            } while (true);

        }
    }
}

