using System;
using System.Collections.Generic;
using System.Linq;
namespace Practica_2_Juego_pastores__prueba_2
{
    class Program
    {

        //Práctica 2 _ Juego de los pastores
        //Práctica hecha en un equipo de tres por Andrés Ragot, José Salvatierra, Alicia Touris.

        static int Menu() //Función para mostrar en pantalla el menu
        {
            int opcion;
            bool numOpcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Elige una opción");
                Console.WriteLine(" 1. Mostrar reglas"); //Opción para ver las reglas del juego
                Console.WriteLine(" 2. Personalizar"); //Opción para personalizar el tablero y el nombre de los jugadores
                Console.WriteLine(" 3. Jugar (Con dos jugadores humanos)"); //Opción para jugar la partida
                Console.WriteLine(" 4. High scores"); //Opción que muestra los nombres de los jugadores y cuántas partidas han ganado y perdido
                Console.WriteLine(" 5. Jugar (Contra el ordenador)"); //Opción para Jugar contra una AI
                Console.WriteLine(" 6. Salir"); //Opción para salir del programa
                numOpcion = int.TryParse(Console.ReadLine(), out opcion);
                if (numOpcion == false || opcion < 1 || opcion > 6) //Comando para que el jugador no se pase del limite de opciones en el menu
                {
                    Console.WriteLine("Opción incorrecta, inserte un valor entre 1 y 3. \n Pulse una tecla para continuar");
                    Console.ReadKey();
                }
            } 
            while (!numOpcion || opcion < 1 || opcion > 6);
            return opcion;
        }
        
        static void Escribir_Reglas() //Función para mostrar las reglas
        {
            Console.WriteLine("Las reglas son: ");
            Console.WriteLine("--> En cada partida hay dos jugadores(ya sean los dos humanos, o uno humano contra el ordenador)." +
                "Y un tablero de a x b dimensiones");
            Console.WriteLine("--> Cada jugador deberá ir retirando fichas del tablero de 1 a el maximo de fichas que haya en dicha fila.");
            Console.WriteLine("--> Cada jugador deberá retirar al menos una ficha en cada turno.");
            Console.WriteLine("--> En un turno un jugador no puede retirar fichas en varias filas diferentes.");
            Console.WriteLine("--> Ganará el jugador que obligue al oponente a retirar la última piedra." +
                "De forma que cuando solo quede una ficha, el jugador que tenga que retirarla ha perdido.");
            Console.ReadKey();
        } 
        
        static int Menu_Personalizacion() //Función para mostrar el menu de personalización
        {
            Console.WriteLine("¿Qué vas a querer personalizar?");
            Console.WriteLine("1. Tablero (El tablero por defecto es de 4x6)");
            Console.WriteLine("2. Nombres de los jugadores(Por defecto don jugador 1 y jugador 2)");
            bool eleccion;
            int eleccion1;
            eleccion = int.TryParse(Console.ReadLine(), out eleccion1);
            if (eleccion == false || eleccion1 < 1 || eleccion1 > 2) //Comando para que el jugador no se pase del limite de opciones en el menu
            {
                Console.WriteLine("Opción incorrecta, inserte un valor entre 1 y 2. \n Pulse una tecla para continuar");
                Console.ReadKey();
            }
            return eleccion1;
        }
        
        static void Personalizar_Tablero(int [] tablero,ref int fila, ref int columna)//Función para personalizar el tablero
        {
            bool EleccionFila;
            bool EleccionColumna;
            Console.WriteLine("Vamos a elegir las dimensiones del tablero: ");
            Console.WriteLine("¿Cuántas filas quieres que haya?(Máximo 50): "); //Pedimos el número de filas que va a temner el tablero personalizado
            EleccionFila = int.TryParse(Console.ReadLine(), out fila);
            if (EleccionFila == false || fila < 1 || fila > 50)//Hacemos un control de fallos para que no se puedan escribir
                                                               //valores que no sean numéricos o esten fuera de un intervalo de como máximo 100 filas x 100 columnas ya que sería de por si una gran cantidad de fichas.
            {
                Console.WriteLine("Opción incorrecta, inserte un número entre 1 y 50. \n Pulse una tecla para continuar");
                Console.ReadKey();
            }
            
            Console.WriteLine("¿Cuántas columnas quieres que haya?(Máximo 50): ");//Pedimos el número de columnas que va a temner el tablero personalizado
            EleccionColumna = int.TryParse(Console.ReadLine(), out columna);
            
            if (EleccionColumna == false || columna < 1 || columna > 50)
            {
                Console.WriteLine("Opción incorrecta, inserte un número entre 1 y 50. \n Pulse una tecla para continuar");
                Console.ReadKey();
            }
            
            List<int> temp = new List<int>();
            for (int i = 0; i < fila; i++)
            {
                temp.Add(columna);
            }
            
            tablero = new int[temp.Count()];
            tablero = temp.ToArray();
        }
        
        static void Personalizar_Jugadores(ref string jugador1, ref string jugador2)//Función para personalizar el nombre de los jugadores 
        {
            Console.WriteLine("Vamos a cambiar el nombre de los jugadores");
            Console.WriteLine("Elija el nuevo nombre del jugador 1");
            jugador1 = Console.ReadLine();
            Console.WriteLine("Elija el nuevo nombre del jugador 2");
            jugador2 = Console.ReadLine();
        }
        
        static void Dibujar_Tablero(int[] array) //Función para dibujar el tablero con los valores establecidos para filas y columnas. Por defecto será 4x6
        {
            Console.WriteLine(); 
            for (int i = 0; i < array.Length; i++) //Bucle para mostrar el tablero en pantalla
            {
                Console.Write((i+1) + "\t");
                for (int j = 0; j < array[i]; j++)
                {
                    Console.Write("O ");
                }
                Console.WriteLine();
            }
        }
        
        static int[] TableroDefault(int[] array, int fila,int columnas) //Función para que el tablero se reinicie una vez acabado el programa
        {
            array = new int[fila];
            for(int i = 0; i < fila; i++)
            {
                array[i] = columnas;
            }
            return array;
        }
        
        static void PreguntarJugada(int[] tablero) //Función para que el jugador haga una jugada
        {
            int numfila;
            bool FilaElegida;
            int numColumnas;
            bool ColumnasElegida;
            do
            {
                do // Se pide la fila en la que se van a eliminar fichas
                {
                    Console.Write("Elige fila (1 a {0}) ", tablero.Length);
                    FilaElegida = int.TryParse(Console.ReadLine(), out numfila);

                    if (FilaElegida == false || numfila < 1 || numfila > tablero.Length)
                    {
                        Console.WriteLine("Opción incorrecta, inserte un número entre 1 y {0}. \n Pulse una tecla para continuar", tablero.Length);
                        Console.ReadKey();
                    }
                }
                while (tablero[numfila-1]==0);
                
                do
                {
                    Console.Write("¿Cuántas fichas quieres eliminar? (1 a {0}) ", tablero[numfila - 1]);
                    ColumnasElegida = int.TryParse(Console.ReadLine(), out numColumnas);
                    if (ColumnasElegida == false || numColumnas < 1 || numColumnas > tablero[numfila - 1])
                    {
                        Console.WriteLine("Opción incorrecta, inserte un número entre 1 y {0}. \n Pulse una tecla para continuar", tablero[numfila - 1]);
                        Console.ReadKey();
                    }
                }
                while ((numColumnas < 1) || (numColumnas > tablero[numfila - 1]));
            } 
            while (tablero[numfila - 1] == 0);
            
            tablero[numfila - 1] -= numColumnas;
        }
        
        static int FilasLlenasQuedan(int[] tablero) // Función que comprueba que una fila está vacia
        {
            int j = 0;
            
            for (int i = 0; i<tablero.Length; i++)
            {
                if (tablero[i] != 0)
                {
                    j++;
                }
            }
            
            return j;
        }
        
        static void JugadaIA(int[] tablero) //Función  que reliza la jugada de la computadora
        {
            int numfila;
            int numColumnas;
            Random rnd = new Random();
            if (FilasLlenasQuedan(tablero) >= 1)
            {
                do
                {
                    Console.WriteLine("Elige fila (1 a {0}) ", tablero.Length);
                    do // Se pide la fila en la que se van a eliminar fichas
                    {
                        numfila = rnd.Next(1, tablero.Length);
                    } while ((tablero[numfila - 1] == 0));
                    Console.WriteLine(numfila);

                    Console.WriteLine("Elige hasta donde vas a eliminar (1 a {0}) ", tablero[numfila - 1]);
                    do
                    {
                        numColumnas = rnd.Next(1, tablero[numfila - 1]);
                    } while ((numColumnas < 1) || (numColumnas > tablero[numfila - 1]));
                    Console.WriteLine(numColumnas);
                } while (tablero[numfila - 1] == 0);
                tablero[numfila - 1] -= numColumnas;
            }
            else if (FilasLlenasQuedan(tablero) == 1)
            {
                Console.WriteLine("Elige fila (1 a {0}) ", tablero.Length);
                do // Se pide la fila en la que se van a eliminar fichas
                {
                    numfila = rnd.Next(1, tablero.Length);
                } while ((tablero[numfila - 1] == 0));
                Console.WriteLine(numfila);

                Console.WriteLine("Elige hasta donde vas a eliminar (1 a {0}) ", tablero[numfila - 1]);
                do
                {
                    numColumnas = rnd.Next(tablero[numfila - 1] - 1, tablero[numfila - 1]);
                } while ((numColumnas < 1) || (numColumnas > tablero[numfila - 1]));
                Console.WriteLine(numColumnas);
                tablero[numfila - 1] -= numColumnas;
            }

            }
        static bool Comprobar_Perdedor(int[] array)//Función que comprueba quien ha ganado
        {
            bool SoloQuedaUnaFicha = false;
            int contador = 0;
            //Comprobamos que el número total de fichas sea 1   
            for (int fila = 0; fila < array.Length; fila++)
            {
                if (array[fila] == 0)
                {
                    contador++;
                }
            }
            if (contador == array.Length)
            {
                SoloQuedaUnaFicha = true;
            }
            return SoloQuedaUnaFicha;
        }
        static string HighScore(List<string> nombres, List<int> victorias, List<int> derrotas)//Función que pone en pantalla una tabla con las victorias y derrotas de los jugadores (la IA no aparece)
        {
            string message = "";
            int i = 0;
            foreach(var value in nombres)
            {
                message += "" + value + " tiene " + victorias[i] + " victoria(s), y " + derrotas[i] + " derrota(s)\n";
                i++;
            }
            return message;
        }

        static void Main(string[] args)
        {
            // Tablero de juego
            int[] tableroMaximo = { 6, 6, 6, 6 };
            int columnas = 6, filas = 4;

            List<string> nombresJugadores = new List<string>();
            List<int> victoriasTotales = new List<int>();
            List<int> derrotasTotales = new List<int>();

            int victoriasJugador1 = 0;
            int derrotasJugador1 = 0;
            int victoriasJugador2 = 0;
            int derrotasJugador2 = 0;
            victoriasTotales.Add(victoriasJugador1);
            victoriasTotales.Add(victoriasJugador2);
            derrotasTotales.Add(derrotasJugador1);
            derrotasTotales.Add(derrotasJugador2);


            string jugador1 = "Jugador 1";
            string jugador2 = "Jugador 2";
            nombresJugadores.Add(jugador1);
            nombresJugadores.Add(jugador2);
            //Variables que marcan la partida(si se juega o no)
            bool seguirJugando = true;
            bool terminado;

            // Dibujar el tablero inicial
            while (seguirJugando)
            {
                int opcion = Menu();  //Pone en pantalla el menu principal
                switch (opcion)
                {
                    case 1://Pone las reglas en pantalla
                        Escribir_Reglas();
                        break;
                    case 2://Pone el memu para persolalizar el nombre y el tablero
                        int eleccion = Menu_Personalizacion();
                        switch (eleccion)
                        {
                            case 1://Opción para perzonalizar el tablero 
                                Personalizar_Tablero(tableroMaximo, ref filas, ref columnas);
                                break;
                            case 2://Opción para perzonalizar el nombre de los jugadores
                                Personalizar_Jugadores(ref jugador1, ref jugador2);
                                nombresJugadores.Add(jugador1);
                                nombresJugadores.Add(jugador2);
                                victoriasTotales.Add(0);
                                victoriasTotales.Add(0);
                                derrotasTotales.Add(0);
                                derrotasTotales.Add(0);
                                break;
                        }
                        break;
                    case 3: //Opción para empezar a jugar con 2 jugadores humanos (por defecto está en el tablero de 4X6)

                        Dibujar_Tablero(tableroMaximo);
                        do
                        {
                            //Turno jugador 1
                            Console.WriteLine("Turno: {0} ", jugador1);
                            PreguntarJugada(tableroMaximo);
                            // Dibujar la casilla del jugador 1
                            Dibujar_Tablero(tableroMaximo);
                            // Comprobar si ha terminado la partida
                            terminado = Comprobar_Perdedor(tableroMaximo);
                            if (terminado)
                            {
                                Console.WriteLine("El {0} es el ganador", jugador2);
                                foreach (string value in nombresJugadores)
                                {
                                    if (value.Equals(jugador2))
                                    {
                                        int position = nombresJugadores.IndexOf(jugador2);
                                        victoriasTotales[position] += 1;
                                    }
                                }
                                foreach (string value in nombresJugadores)
                                {
                                    if (value.Equals(jugador1))
                                    {
                                        int position = nombresJugadores.IndexOf(jugador1);
                                        derrotasTotales[position] += 1;
                                    }
                                }

                            }
                            else
                            {
                                // Pedir al jugador 2
                                Console.WriteLine("Turno: {0}", jugador2);
                                PreguntarJugada(tableroMaximo);
                                // Dibujar la casilla del jugador 2
                                Dibujar_Tablero(tableroMaximo);
                                // Comprobar si ha terminado la partida
                                terminado = Comprobar_Perdedor(tableroMaximo);
                                if (terminado)
                                {
                                    Console.WriteLine("El {0} es el ganador", jugador1);
                                    foreach (string value in nombresJugadores)
                                    {
                                        if (value.Equals(jugador1))
                                        {
                                            int position = nombresJugadores.IndexOf(jugador1);
                                            victoriasTotales[position] += 1;
                                        }
                                    }
                                    foreach (string value in nombresJugadores)
                                    {
                                        if (value.Equals(jugador2))
                                        {
                                            int position = nombresJugadores.IndexOf(jugador2);
                                            derrotasTotales[position] += 1;
                                        }
                                    }

                                    
                                }
                            }
                        } 
                        while (!terminado);
                        
                        Console.WriteLine("FIN DEL JUEGO");
                        int[] temp = TableroDefault(tableroMaximo, filas, columnas);
                        tableroMaximo = new int[filas];
                        Array.Copy(temp, tableroMaximo, temp.Length);
                        Console.ReadKey();
                        
                        break;
                        
                    case 4: //Opción para poner en pantalla el highscore
                        Console.WriteLine(HighScore(nombresJugadores, victoriasTotales, derrotasTotales));
                        Console.ReadKey();

                        break;

                    case 5://Opción para empezar a jugar contra la IA (por defecto está en el tablero de 4X6)
                        Dibujar_Tablero(tableroMaximo);
                        do
                        {
                            //Turno jugador 1
                            Console.WriteLine("Turno: {0} ", jugador1);
                            PreguntarJugada(tableroMaximo);
                            // Dibujar la casilla del jugador 1
                            Dibujar_Tablero(tableroMaximo);
                            // Comprobar si ha terminado la partida
                            terminado = Comprobar_Perdedor(tableroMaximo);
                            
                            if (terminado)
                            {
                                Console.WriteLine("El Ordenador es el ganador");

                            }
                            else
                            {
                                // Pedir al jugador 2
                                Console.WriteLine("Turno Ordenador");
                                JugadaIA(tableroMaximo);
                                // Dibujar la casilla del jugador 2
                                Dibujar_Tablero(tableroMaximo);
                                // Comprobar si ha terminado la partida
                                terminado = Comprobar_Perdedor(tableroMaximo);
                                if (terminado)
                                {
                                    Console.WriteLine("El {0} es el ganador", jugador1);
                                }
                            }
                        } 
                        while (!terminado);
                        
                        Console.WriteLine("FIN DEL JUEGO");
                        int[] temp1 = TableroDefault(tableroMaximo, filas, columnas);
                        tableroMaximo = new int[filas];
                        Array.Copy(temp1, tableroMaximo, temp1.Length);
                        Console.ReadKey();
                        
                        break;

                    case 6: //Opción para finalizar el programa
                        seguirJugando = false;
                        break;
                }
            }
        }   
    }
}
